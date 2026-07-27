"""Design tokens and the ttk styles derived from them.

Widgets never hard-code a colour, a font or a gap; they read them from a
:class:`Theme`. That is the whole reason this module exists — one place to
change how the app looks.
"""

from __future__ import annotations

from dataclasses import dataclass, field
from tkinter import font as tkfont
from tkinter import ttk
from typing import TYPE_CHECKING

if TYPE_CHECKING:
    import tkinter as tk
    from collections.abc import Sequence

__all__ = ["Fonts", "Palette", "Spacing", "Theme", "apply_ttk_styles", "build_theme"]

_SANS_CANDIDATES = ("Segoe UI Variable Text", "Segoe UI", "Inter", "Helvetica Neue", "DejaVu Sans")
_MONO_CANDIDATES = ("Cascadia Mono", "Consolas", "JetBrains Mono", "DejaVu Sans Mono", "Menlo")


@dataclass(frozen=True)
class Palette:
    """A restrained dark palette: two greys for structure, one blue for the user."""

    app_bg: str = "#0E1116"
    transcript_bg: str = "#12161C"
    composer_bg: str = "#161B23"
    field_bg: str = "#1B212B"
    border: str = "#252C38"

    text: str = "#E4E8EF"
    text_muted: str = "#8A93A3"
    text_faint: str = "#5C6575"

    accent: str = "#5B8CFF"
    accent_hover: str = "#7AA3FF"
    accent_text: str = "#0B0F16"

    user_bg: str = "#26406C"
    user_text: str = "#EDF2FF"
    assistant_bg: str = "#1A2029"
    assistant_text: str = "#DCE2EC"

    tool_bg: str = "#1C2431"
    tool_text: str = "#9CC5FF"

    error_bg: str = "#391E23"
    error_text: str = "#FFC9C9"
    error_accent: str = "#F17373"

    selection_bg: str = "#31538F"
    selection_text: str = "#FFFFFF"


@dataclass(frozen=True)
class Spacing:
    """Vertical and horizontal rhythm, in pixels."""

    xs: int = 4
    sm: int = 8
    md: int = 14
    lg: int = 20
    xl: int = 28

    gutter: int = 16
    """Inset of a turn from its own side of the transcript."""

    opposite_inset: int = 110
    """Inset from the far side, which is what gives turns their column width."""


@dataclass(frozen=True, eq=False)
class Fonts:
    """Resolved font objects. Created against a root, so never module-level."""

    body: tkfont.Font
    body_bold: tkfont.Font
    label: tkfont.Font
    heading: tkfont.Font
    hint: tkfont.Font
    mono: tkfont.Font


@dataclass(frozen=True, eq=False)
class Theme:
    """The tokens every widget in the shell reads from."""

    fonts: Fonts
    palette: Palette = field(default_factory=Palette)
    spacing: Spacing = field(default_factory=Spacing)


def build_theme(root: tk.Misc) -> Theme:
    """Resolve fonts against ``root`` and assemble the theme.

    Args:
        root: An existing widget; font families can only be queried once Tk is up.

    Returns:
        The theme for this process.
    """
    sans = _first_available(root, _SANS_CANDIDATES, "TkDefaultFont")
    mono = _first_available(root, _MONO_CANDIDATES, "TkFixedFont")
    fonts = Fonts(
        body=tkfont.Font(root=root, family=sans, size=11),
        body_bold=tkfont.Font(root=root, family=sans, size=11, weight="bold"),
        label=tkfont.Font(root=root, family=sans, size=9, weight="bold"),
        heading=tkfont.Font(root=root, family=sans, size=12, weight="bold"),
        hint=tkfont.Font(root=root, family=sans, size=9),
        mono=tkfont.Font(root=root, family=mono, size=10),
    )
    return Theme(fonts=fonts)


def apply_ttk_styles(root: tk.Misc, theme: Theme) -> None:
    """Configure every ttk style the shell uses.

    Args:
        root: The application window.
        theme: Tokens to derive the styles from.
    """
    palette = theme.palette
    spacing = theme.spacing
    fonts = theme.fonts

    style = ttk.Style(root)
    style.theme_use("clam")  # the only built-in theme that honours these colours

    style.configure("TFrame", background=palette.app_bg)
    style.configure("Header.TFrame", background=palette.app_bg)
    style.configure("Transcript.TFrame", background=palette.transcript_bg)
    style.configure("Composer.TFrame", background=palette.composer_bg)
    style.configure("Divider.TFrame", background=palette.border)

    style.configure("TLabel", background=palette.app_bg, foreground=palette.text, font=fonts.body)
    style.configure("Heading.TLabel", font=fonts.heading)
    style.configure("Subtle.TLabel", foreground=palette.text_muted, font=fonts.hint)
    style.configure(
        "Hint.TLabel",
        background=palette.composer_bg,
        foreground=palette.text_faint,
        font=fonts.hint,
    )

    style.configure(
        "Accent.TButton",
        background=palette.accent,
        foreground=palette.accent_text,
        font=fonts.body_bold,
        borderwidth=0,
        focusthickness=0,
        relief="flat",
        padding=(spacing.md, spacing.sm - 1),
    )
    style.map(
        "Accent.TButton",
        background=[
            ("disabled", palette.field_bg),
            ("pressed", palette.accent),
            ("active", palette.accent_hover),
        ],
        foreground=[("disabled", palette.text_faint)],
    )

    style.configure(
        "Ghost.TButton",
        background=palette.field_bg,
        foreground=palette.text,
        font=fonts.body,
        borderwidth=0,
        focusthickness=0,
        relief="flat",
        padding=(spacing.md - 2, spacing.sm - 1),
    )
    style.map(
        "Ghost.TButton",
        background=[
            ("disabled", palette.composer_bg),
            ("pressed", palette.border),
            ("active", palette.border),
        ],
        foreground=[("disabled", palette.text_faint)],
    )

    for name, trough in (
        ("Chat.Vertical.TScrollbar", palette.transcript_bg),
        ("Field.Vertical.TScrollbar", palette.field_bg),
    ):
        style.configure(
            name,
            background=palette.border,
            troughcolor=trough,
            bordercolor=trough,
            darkcolor=palette.border,
            lightcolor=palette.border,
            arrowcolor=palette.text_faint,
            borderwidth=0,
            arrowsize=12,
        )
        style.map(name, background=[("active", palette.text_faint)])


def _first_available(root: tk.Misc, candidates: Sequence[str], fallback: str) -> str:
    """Return the first installed family from ``candidates``, else ``fallback``."""
    installed = {name.casefold() for name in tkfont.families(root)}
    for name in candidates:
        if name.casefold() in installed:
            return name
    return fallback
