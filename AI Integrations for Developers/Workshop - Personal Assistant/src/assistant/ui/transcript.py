"""The scrollable, selectable message transcript.

Implemented as a single ``tk.Text`` with tagged ranges rather than one widget
per message. That buys three of the requirements outright: word wrap reflows on
resize, selection spans turns, and appending a delta is one insert instead of a
relayout.
"""

from __future__ import annotations

import tkinter as tk
from contextlib import contextmanager
from tkinter import ttk
from typing import TYPE_CHECKING

if TYPE_CHECKING:
    from collections.abc import Iterator

    from assistant.ui.theme import Theme

__all__ = ["TranscriptView"]

_END = "end-1c"
_BOTTOM_EPSILON = 0.002
_PLACEHOLDER = "Ask anything. Enter sends, Shift+Enter starts a new line."

_LABELS: dict[str, str] = {"user": "You", "assistant": "Assistant", "error": "Error"}


class TranscriptView(ttk.Frame):
    """Renders the conversation and owns the scroll policy."""

    def __init__(self, master: tk.Misc, theme: Theme) -> None:
        """Build the text surface, its scrollbar and the jump-to-latest affordance."""
        super().__init__(master, style="Transcript.TFrame")
        self._theme = theme
        self._is_empty = True
        self._run_open = False
        self._turn_produced_output = False
        self._jump_visible = False

        palette = theme.palette
        self._text = tk.Text(
            self,
            wrap="word",
            state="disabled",
            relief="flat",
            borderwidth=0,
            highlightthickness=0,
            background=palette.transcript_bg,
            foreground=palette.text,
            font=theme.fonts.body,
            padx=theme.spacing.md,
            pady=theme.spacing.md,
            insertwidth=0,
            cursor="arrow",
            selectbackground=palette.selection_bg,
            selectforeground=palette.selection_text,
            inactiveselectbackground=palette.selection_bg,
        )
        self._scrollbar = ttk.Scrollbar(
            self,
            orient="vertical",
            style="Chat.Vertical.TScrollbar",
            command=self._text.yview,
        )
        self._text.configure(yscrollcommand=self._on_scroll)

        self.rowconfigure(0, weight=1)
        self.columnconfigure(0, weight=1)
        self._text.grid(row=0, column=0, sticky="nsew")
        self._scrollbar.grid(row=0, column=1, sticky="ns")

        self._jump_button = ttk.Button(
            self,
            text="↓  Jump to latest",
            style="Ghost.TButton",
            command=self.scroll_to_bottom,
            takefocus=False,
        )
        self._menu = self._build_menu()

        self._configure_tags()
        self._bind_events()
        self._show_placeholder()

    # -- public API ---------------------------------------------------------

    def clear(self) -> None:
        """Drop every turn and return to the empty state."""
        with self._editing():
            self._text.delete("1.0", "end")
        self._run_open = False
        self._turn_produced_output = False
        self._is_empty = True
        self._show_placeholder()

    def add_user_turn(self, text: str) -> None:
        """Append a completed user turn."""
        self._close_run()
        self._insert_turn("user", text)

    def begin_assistant_turn(self) -> None:
        """Open an assistant turn so deltas can be appended into it."""
        self._close_run()
        self._drop_placeholder()
        with self._editing():
            self._insert_label("assistant")
        self._turn_produced_output = False

    def append_assistant_text(self, chunk: str) -> None:
        """Append a streamed delta to the open assistant turn."""
        if not chunk:
            return
        with self._editing():
            self._text.insert("end", chunk, ("body_assistant",))
        self._run_open = True
        self._turn_produced_output = True

    def add_tool_call(self, name: str, arguments: str) -> None:
        """Show a tool invocation inside the open assistant turn."""
        self._insert_aside(f"⚙  {name}({arguments})", "tool")
        self._turn_produced_output = True

    def add_tool_result(self, name: str, content: str, *, is_error: bool) -> None:
        """Show a tool's outcome inside the open assistant turn."""
        marker = "✕" if is_error else "✓"
        self._insert_aside(f"{marker}  {name} → {content}", "tool_error" if is_error else "tool")
        self._turn_produced_output = True

    def add_error(self, message: str) -> None:
        """Render a failure as its own turn state — never a dialog, never a traceback."""
        self._close_run()
        self._drop_placeholder()
        self._insert_turn("error", message)
        self._turn_produced_output = True

    def end_assistant_turn(self, *, note: str | None = None) -> None:
        """Close the open assistant turn, optionally stamping it with ``note``."""
        if not self._turn_produced_output:
            self._insert_aside("(no response)", "notice")
        elif note is not None:
            self._insert_aside(note, "notice")
        self._close_run()
        self._turn_produced_output = False

    def scroll_to_bottom(self) -> None:
        """Jump the viewport to the newest text."""
        self._text.see(_END)
        self._text.yview_moveto(1.0)

    # -- rendering ----------------------------------------------------------

    def _insert_turn(self, kind: str, text: str) -> None:
        """Insert a complete label-plus-body block."""
        self._drop_placeholder()
        with self._editing():
            self._insert_label(kind)
            self._text.insert("end", text, (f"body_{kind}",))
        self._run_open = True
        self._close_run()

    def _insert_label(self, kind: str) -> None:
        """Insert the small role caption above a body. Caller holds ``_editing``."""
        self._text.insert("end", _LABELS[kind] + "\n", (f"label_{kind}",))

    def _insert_aside(self, text: str, tag: str) -> None:
        """Insert a secondary line (tool trace, stop notice) between body runs."""
        self._close_run()
        self._drop_placeholder()
        with self._editing():
            self._text.insert("end", text, (tag,))
        self._run_open = True
        self._close_run()

    def _close_run(self) -> None:
        """Terminate the current tagged run with an untagged separator line."""
        if not self._run_open:
            return
        with self._editing():
            self._text.insert("end", "\n", ("gap",))
        self._run_open = False

    def _show_placeholder(self) -> None:
        """Fill an empty transcript with a muted prompt."""
        if not self._is_empty:
            return
        with self._editing():
            self._text.insert("end", _PLACEHOLDER, ("placeholder",))

    def _drop_placeholder(self) -> None:
        """Remove the empty-state text before the first real turn."""
        if not self._is_empty:
            return
        self._is_empty = False
        with self._editing():
            self._text.delete("1.0", "end")

    @contextmanager
    def _editing(self) -> Iterator[None]:
        """Temporarily make the widget writable, preserving the scroll policy.

        Auto-scroll is decided *before* the edit: if the viewport was already at
        the bottom it follows the new text, otherwise it is left exactly where
        the reader put it.
        """
        follow = self._at_bottom()
        self._text.configure(state="normal")
        try:
            yield
        finally:
            self._text.configure(state="disabled")
            if follow:
                self._text.see(_END)

    def _at_bottom(self) -> bool:
        """Whether the last line is currently visible."""
        return self._text.yview()[1] >= 1.0 - _BOTTOM_EPSILON

    # -- wiring -------------------------------------------------------------

    def _configure_tags(self) -> None:
        """Define the visual language: one tag per role and per aside kind."""
        palette = self._theme.palette
        spacing = self._theme.spacing
        fonts = self._theme.fonts
        text = self._text

        label_common: dict[str, object] = {
            "font": fonts.label,
            "spacing1": spacing.md,
            "spacing3": spacing.xs,
        }
        body_common: dict[str, object] = {
            "font": fonts.body,
            "spacing1": spacing.sm,
            "spacing2": 5,
            "spacing3": spacing.sm,
        }
        aside_common: dict[str, object] = {
            "font": fonts.mono,
            "lmargin1": spacing.gutter,
            "lmargin2": spacing.gutter + spacing.md,
            "rmargin": spacing.opposite_inset,
            "spacing1": spacing.xs,
            "spacing3": spacing.xs,
        }
        left_column: dict[str, object] = {
            "lmargin1": spacing.gutter,
            "lmargin2": spacing.gutter,
            "rmargin": spacing.opposite_inset,
        }
        right_column: dict[str, object] = {
            "justify": "right",
            "lmargin1": spacing.opposite_inset,
            "lmargin2": spacing.opposite_inset,
            "rmargin": spacing.gutter,
        }

        text.tag_configure("gap", {"font": fonts.hint, "spacing1": 0, "spacing3": 0})
        text.tag_configure(
            "placeholder",
            {
                "justify": "center",
                "foreground": palette.text_faint,
                "font": fonts.body,
                "spacing1": spacing.xl,
                "lmargin1": spacing.xl,
                "lmargin2": spacing.xl,
                "rmargin": spacing.xl,
            },
        )
        text.tag_configure(
            "label_user",
            {
                **label_common,
                "justify": "right",
                "foreground": palette.text_muted,
                "rmargin": spacing.gutter,
            },
        )
        text.tag_configure(
            "body_user",
            {
                **body_common,
                **right_column,
                "background": palette.user_bg,
                "foreground": palette.user_text,
            },
        )
        text.tag_configure(
            "label_assistant",
            {
                **label_common,
                "foreground": palette.text_muted,
                "lmargin1": spacing.gutter,
                "lmargin2": spacing.gutter,
            },
        )
        text.tag_configure(
            "body_assistant",
            {
                **body_common,
                **left_column,
                "background": palette.assistant_bg,
                "foreground": palette.assistant_text,
            },
        )
        text.tag_configure(
            "label_error",
            {
                **label_common,
                "foreground": palette.error_accent,
                "lmargin1": spacing.gutter,
                "lmargin2": spacing.gutter,
            },
        )
        text.tag_configure(
            "body_error",
            {
                **body_common,
                **left_column,
                "background": palette.error_bg,
                "foreground": palette.error_text,
            },
        )
        text.tag_configure(
            "tool",
            {**aside_common, "background": palette.tool_bg, "foreground": palette.tool_text},
        )
        text.tag_configure(
            "tool_error",
            {**aside_common, "background": palette.error_bg, "foreground": palette.error_text},
        )
        text.tag_configure(
            "notice",
            {
                "foreground": palette.text_faint,
                "font": fonts.hint,
                "lmargin1": spacing.gutter,
                "lmargin2": spacing.gutter,
                "spacing1": spacing.xs,
            },
        )

    def _build_menu(self) -> tk.Menu:
        """Right-click menu, so copying does not depend on knowing the shortcut."""
        palette = self._theme.palette
        menu = tk.Menu(
            self,
            tearoff=0,
            background=palette.field_bg,
            foreground=palette.text,
            activebackground=palette.accent,
            activeforeground=palette.accent_text,
            borderwidth=0,
            font=self._theme.fonts.body,
        )
        menu.add_command(label="Copy", command=self._copy_selection)
        menu.add_command(label="Select all", command=self._select_all)
        return menu

    def _bind_events(self) -> None:
        """Keyboard and mouse affordances for a read-only text surface."""
        self._text.bind("<Control-c>", self._on_copy)
        self._text.bind("<Control-C>", self._on_copy)
        self._text.bind("<Control-a>", self._on_select_all)
        self._text.bind("<Control-A>", self._on_select_all)
        self._text.bind("<Button-3>", self._on_context_menu)
        self._text.bind("<Configure>", self._on_configure)

    def _on_scroll(self, first: float, last: float) -> None:
        """Keep the scrollbar and the jump button in sync with the viewport.

        Tk hands these through as strings; ``float`` accepts either shape.
        """
        self._scrollbar.set(first, last)
        self._set_jump_visible(visible=float(last) < 1.0 - _BOTTOM_EPSILON)

    def _set_jump_visible(self, *, visible: bool) -> None:
        """Show the jump button only while the reader is behind the newest text."""
        if visible == self._jump_visible:
            return
        self._jump_visible = visible
        if visible:
            self._jump_button.place(relx=1.0, rely=1.0, x=-self._jump_offset(), y=-16, anchor="se")
        else:
            self._jump_button.place_forget()

    def _jump_offset(self) -> int:
        """Horizontal inset that clears the scrollbar."""
        return max(self._scrollbar.winfo_width(), 12) + self._theme.spacing.md

    def _on_configure(self, _event: tk.Event[tk.Misc]) -> None:
        """Re-evaluate the jump button after a resize reflows the text."""
        self._set_jump_visible(visible=not self._at_bottom())

    def _on_context_menu(self, event: tk.Event[tk.Misc]) -> str:
        """Pop the copy menu at the pointer."""
        self._menu.tk_popup(event.x_root, event.y_root)
        return "break"

    def _on_copy(self, _event: tk.Event[tk.Misc]) -> str:
        """Copy the selection, if any."""
        self._copy_selection()
        return "break"

    def _on_select_all(self, _event: tk.Event[tk.Misc]) -> str:
        """Select the whole transcript."""
        self._select_all()
        return "break"

    def _copy_selection(self) -> None:
        """Put the selected transcript text on the clipboard."""
        if not self._text.tag_ranges("sel"):
            return
        self.clipboard_clear()
        self.clipboard_append(self._text.get("sel.first", "sel.last"))

    def _select_all(self) -> None:
        """Select every character in the transcript."""
        self._text.tag_add("sel", "1.0", _END)
        self._text.focus_set()
