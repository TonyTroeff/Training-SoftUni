"""The application window: assembles the shell and routes events into it."""

from __future__ import annotations

import ctypes
import json
import sys
import tkinter as tk
from tkinter import ttk
from typing import TYPE_CHECKING, assert_never

from assistant.ai.events import StreamError, TextDelta, ToolCall, ToolResult
from assistant.runtime.bridge import AsyncTkBridge
from assistant.session import ChatSession, StreamHandlers
from assistant.ui.composer import Composer
from assistant.ui.theme import apply_ttk_styles, build_theme
from assistant.ui.transcript import TranscriptView

if TYPE_CHECKING:
    from collections.abc import Mapping
    from types import TracebackType

    from pydantic import JsonValue

    from assistant.ai.client import AIClient
    from assistant.ai.events import AIEvent
    from assistant.config import Config
    from assistant.session import FinishReason

__all__ = ["AssistantApp", "enable_dpi_awareness"]

_MIN_WIDTH = 560
_MIN_HEIGHT = 440
_MAX_ARGUMENT_CHARS = 120

_STATUS: dict[str, str] = {
    "idle": "Ready",
    "streaming": "Streaming…",
    "completed": "Ready",
    "cancelled": "Stopped",
    "failed": "Last turn failed",
}


def enable_dpi_awareness() -> None:
    """Ask Windows for per-monitor DPI awareness before the root window exists.

    Without this the window is bitmap-scaled on high-DPI displays and every
    font looks soft. A no-op elsewhere.
    """
    if sys.platform != "win32":
        return
    try:
        ctypes.windll.shcore.SetProcessDpiAwareness(1)
    except (AttributeError, OSError):
        return


class AssistantApp(tk.Tk):
    """The main window.

    Owns the widget tree, the thread bridge and the chat session, and is the
    only place where AI events are translated into transcript operations.
    """

    def __init__(self, config: Config, client: AIClient) -> None:
        """Assemble the window around ``client``.

        Args:
            config: The single config instance, created at startup.
            client: The AI seam. Nothing here knows how it is implemented.
        """
        super().__init__()
        self._config = config

        self.title(config.window_title)
        self.geometry(f"{config.window_width}x{config.window_height}")
        self.minsize(_MIN_WIDTH, _MIN_HEIGHT)
        self.tk.call("tk", "scaling", self.winfo_fpixels("1i") / 72.0)

        self._theme = build_theme(self)
        apply_ttk_styles(self, self._theme)
        self.configure(background=self._theme.palette.app_bg)

        self._bridge = AsyncTkBridge(self)
        self._session = ChatSession(client=client, bridge=self._bridge)
        self._handlers = StreamHandlers(
            on_event=self._on_event,
            on_error=self._on_error,
            on_finished=self._on_finished,
        )

        self._transcript, self._composer, self._status = self._build_layout()
        self._bridge.start()
        self.protocol("WM_DELETE_WINDOW", self._on_close)
        self.bind("<Escape>", self._on_escape)
        self._composer.focus_input()

    # -- layout -------------------------------------------------------------

    def _build_layout(self) -> tuple[TranscriptView, Composer, ttk.Label]:
        """Build the header, transcript, composer and status bar."""
        spacing = self._theme.spacing

        self.rowconfigure(1, weight=1)
        self.columnconfigure(0, weight=1)

        header = ttk.Frame(self, style="Header.TFrame", padding=(spacing.lg, spacing.md))
        header.columnconfigure(0, weight=1)
        header.grid(row=0, column=0, sticky="ew")

        ttk.Label(header, text=self._config.window_title, style="Heading.TLabel").grid(
            row=0, column=0, sticky="w"
        )
        ttk.Label(header, text=self._config.model_name, style="Subtle.TLabel").grid(
            row=1, column=0, sticky="w", pady=(2, 0)
        )
        ttk.Button(
            header,
            text="New chat",
            style="Ghost.TButton",
            command=self._on_new_chat,
            takefocus=False,
        ).grid(row=0, column=1, rowspan=2, sticky="e")

        transcript = TranscriptView(self, self._theme)
        transcript.grid(row=1, column=0, sticky="nsew", padx=spacing.lg)

        ttk.Frame(self, style="Divider.TFrame", height=1).grid(
            row=2, column=0, sticky="ew", pady=(spacing.md, 0)
        )

        composer = Composer(
            self,
            self._theme,
            on_send=self._on_send,
            on_stop=self._session.cancel,
        )
        composer.grid(row=3, column=0, sticky="ew")

        status_bar = ttk.Frame(
            self,
            style="TFrame",
            padding=(spacing.lg, spacing.xs, spacing.lg, spacing.sm - 2),
        )
        status_bar.columnconfigure(0, weight=1)
        status_bar.grid(row=4, column=0, sticky="ew")
        status = ttk.Label(status_bar, text=_STATUS["idle"], style="Subtle.TLabel")
        status.grid(row=0, column=0, sticky="w")

        return transcript, composer, status

    # -- event routing ------------------------------------------------------

    def _on_send(self, message: str) -> None:
        """Start a turn: render it, lock the composer, hand off to the session."""
        self._transcript.add_user_turn(message)
        self._transcript.begin_assistant_turn()
        self._composer.set_streaming(streaming=True)
        self._set_status("streaming")
        self._session.send(message, self._handlers)

    def _on_event(self, event: AIEvent) -> None:
        """Translate one AI event into a transcript operation. Tk thread."""
        match event:
            case TextDelta():
                self._transcript.append_assistant_text(event.text)
            case ToolCall():
                self._transcript.add_tool_call(event.name, _format_arguments(event.arguments))
            case ToolResult():
                self._transcript.add_tool_result(event.name, event.content, is_error=event.is_error)
            case StreamError():
                self._transcript.add_error(event.message)
            case unreachable:
                assert_never(unreachable)

    def _on_error(self, message: str) -> None:
        """Surface a client failure in the transcript."""
        self._transcript.add_error(message)

    def _on_finished(self, reason: FinishReason) -> None:
        """Close the turn out and hand control back to the composer."""
        self._transcript.end_assistant_turn(note="Stopped" if reason == "cancelled" else None)
        self._composer.set_streaming(streaming=False)
        self._set_status(reason)

    def _on_new_chat(self) -> None:
        """Cancel anything in flight and start over."""
        self._session.reset()
        self._transcript.clear()
        self._composer.set_streaming(streaming=False)
        self._set_status("idle")

    def _on_escape(self, _event: tk.Event[tk.Misc]) -> None:
        """Escape cancels a stream, matching the Stop button."""
        if self._session.streaming:
            self._session.cancel()

    def _on_close(self) -> None:
        """Tear the worker loop down before the widgets disappear."""
        self._session.cancel()
        self._bridge.close()
        self.destroy()

    def _set_status(self, key: str) -> None:
        """Update the status line."""
        self._status.configure(text=_STATUS.get(key, _STATUS["idle"]))

    def report_callback_exception(
        self,
        exc: type[BaseException],
        val: BaseException,
        tb: TracebackType | None,
    ) -> None:
        """Route Tk callback failures into the transcript instead of the console.

        Args:
            exc: Exception class, unused.
            val: The exception itself, which is what the user sees.
            tb: Traceback, unused — a traceback is not a UI.
        """
        del exc, tb
        self._transcript.add_error(f"{type(val).__name__}: {val}")


def _format_arguments(arguments: str) -> str:
    """Render tool arguments as compact JSON, truncated to one line's worth."""
    return arguments
    #if not arguments:
    #    return ""
    #rendered = json.dumps(dict(arguments), ensure_ascii=False, separators=(", ", ": "))
    #if len(rendered) > _MAX_ARGUMENT_CHARS:
    #    return rendered[: _MAX_ARGUMENT_CHARS - 1] + "…"
    #return rendered
