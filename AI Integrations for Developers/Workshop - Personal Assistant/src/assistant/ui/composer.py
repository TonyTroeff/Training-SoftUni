"""The multi-line input area at the bottom of the window.

Enter sends, Shift+Enter inserts a newline, and the box grows with its content
up to :data:`_MAX_LINES` before it starts scrolling. While a turn is streaming,
Send is disabled and Stop takes over.
"""

from __future__ import annotations

import tkinter as tk
from tkinter import ttk
from typing import TYPE_CHECKING

if TYPE_CHECKING:
    from collections.abc import Callable

    from assistant.ui.theme import Theme

__all__ = ["Composer"]

_MIN_LINES = 1
_MAX_LINES = 6
_IDLE_HINT = "Enter to send · Shift+Enter for a new line"
_STREAMING_HINT = "Streaming… press Stop to cancel"


class Composer(ttk.Frame):
    """Input box, send/stop controls and the keyboard hint."""

    def __init__(
        self,
        master: tk.Misc,
        theme: Theme,
        *,
        on_send: Callable[[str], None],
        on_stop: Callable[[], None],
    ) -> None:
        """Build the input surface and its controls.

        Args:
            master: Parent widget.
            theme: Design tokens.
            on_send: Called with the stripped, non-empty message.
            on_stop: Called when the user cancels a stream.
        """
        super().__init__(master, style="Composer.TFrame", padding=(theme.spacing.md,) * 2)
        self._theme = theme
        self._on_send = on_send
        self._on_stop = on_stop
        self._streaming = False

        palette = theme.palette
        self._input = tk.Text(
            self,
            height=_MIN_LINES,
            wrap="word",
            relief="flat",
            borderwidth=0,
            highlightthickness=1,
            highlightbackground=palette.border,
            highlightcolor=palette.accent,
            background=palette.field_bg,
            foreground=palette.text,
            insertbackground=palette.accent,
            font=theme.fonts.body,
            padx=theme.spacing.md - 2,
            pady=theme.spacing.sm + 2,
            spacing3=3,
            undo=True,
            maxundo=200,
            selectbackground=palette.selection_bg,
            selectforeground=palette.selection_text,
        )
        self._input_scroll = ttk.Scrollbar(
            self,
            orient="vertical",
            style="Field.Vertical.TScrollbar",
            command=self._input.yview,
        )
        self._input.configure(yscrollcommand=self._input_scroll.set)

        self._hint = ttk.Label(self, text=_IDLE_HINT, style="Hint.TLabel")
        self._stop_button = ttk.Button(
            self,
            text="Stop",
            style="Ghost.TButton",
            command=self._on_stop,
            state="disabled",
            takefocus=False,
        )
        self._send_button = ttk.Button(
            self,
            text="Send",
            style="Accent.TButton",
            command=self._submit,
            state="disabled",
            takefocus=False,
        )

        self.columnconfigure(0, weight=1)
        self._input.grid(row=0, column=0, columnspan=3, sticky="ew")
        self._input_scroll.grid(row=0, column=3, sticky="ns")
        self._hint.grid(row=1, column=0, sticky="w", pady=(theme.spacing.sm, 0))
        self._stop_button.grid(row=1, column=1, sticky="e", pady=(theme.spacing.sm, 0))
        self._send_button.grid(
            row=1,
            column=2,
            sticky="e",
            padx=(theme.spacing.sm, 0),
            pady=(theme.spacing.sm, 0),
        )

        self._bind_events()
        self._sync()

    # -- public API ---------------------------------------------------------

    def focus_input(self) -> None:
        """Put the caret in the input box."""
        self._input.focus_set()

    def set_streaming(self, *, streaming: bool) -> None:
        """Switch between the idle and streaming affordances."""
        self._streaming = streaming
        self._hint.configure(text=_STREAMING_HINT if streaming else _IDLE_HINT)
        self._stop_button.configure(state="normal" if streaming else "disabled")
        self._sync()
        if not streaming:
            self.focus_input()

    # -- behaviour ----------------------------------------------------------

    def _bind_events(self) -> None:
        """Bind send/newline keys and everything that can change the content."""
        self._input.bind("<Return>", self._on_return)
        self._input.bind("<KP_Enter>", self._on_return)
        self._input.bind("<Shift-Return>", self._on_shift_return)
        for sequence in ("<KeyRelease>", "<<Paste>>", "<<Cut>>", "<<Undo>>", "<<Redo>>"):
            self._input.bind(sequence, self._on_content_changed, add="+")
        self._input.bind("<Configure>", self._on_content_changed, add="+")

    def _on_return(self, _event: tk.Event[tk.Misc]) -> str:
        """Enter sends and never inserts a newline."""
        self._submit()
        return "break"

    def _on_shift_return(self, _event: tk.Event[tk.Misc]) -> str:
        """Shift+Enter inserts a newline and regrows the box."""
        self._input.insert("insert", "\n")
        self._sync()
        return "break"

    def _on_content_changed(self, _event: tk.Event[tk.Misc]) -> None:
        """Resize the box and re-evaluate whether Send is available."""
        self._sync()

    def _submit(self) -> None:
        """Hand a non-empty message to the application and clear the box."""
        if self._streaming:
            return
        message = self._input.get("1.0", "end-1c").strip()
        if not message:
            return
        self._input.delete("1.0", "end")
        self._input.edit_reset()
        self._sync()
        self._on_send(message)

    def _sync(self) -> None:
        """Reconcile height, scrollbar visibility and the Send button state."""
        wanted = min(max(self._display_lines(), _MIN_LINES), _MAX_LINES)
        if wanted != self._current_height():
            self._input.configure(height=wanted)

        if self._display_lines() > _MAX_LINES:
            self._input_scroll.grid()
        else:
            self._input_scroll.grid_remove()

        has_text = bool(self._input.get("1.0", "end-1c").strip())
        self._send_button.configure(
            state="normal" if has_text and not self._streaming else "disabled"
        )

    def _display_lines(self) -> int:
        """Number of wrapped lines the content currently occupies.

        ``Text.count`` wraps a single measurement in a tuple on some Python
        versions and returns a bare int on others, so both shapes are accepted.
        """
        counted: object = self._input.count("1.0", "end-1c", "displaylines")
        if isinstance(counted, tuple) and counted:
            return int(counted[0])
        if isinstance(counted, int):
            return counted
        return _MIN_LINES

    def _current_height(self) -> int:
        """The height option as an int, whatever Tk hands back."""
        return int(str(self._input.cget("height")))
