"""User *settings* - the structured half of long-term memory.

Settings have a predefined format (validated here, and described verbatim to
the models in the system prompts). Preferences, by contrast, are free text.
"""

from __future__ import annotations

from typing import Any, Literal

from pydantic import BaseModel, Field

WeekDay = Literal["mon", "tue", "wed", "thu", "fri", "sat", "sun"]

#: Colors the CLI can actually render (rich's standard palette).
TAG_COLORS = (
    "red", "green", "yellow", "blue", "magenta", "cyan", "white",
    "bright_red", "bright_green", "bright_yellow", "bright_blue",
    "bright_magenta", "bright_cyan", "grey50",
)
TagColor = Literal[
    "red", "green", "yellow", "blue", "magenta", "cyan", "white",
    "bright_red", "bright_green", "bright_yellow", "bright_blue",
    "bright_magenta", "bright_cyan", "grey50",
]


class Notifications(BaseModel):
    enabled: bool = True
    minutes_before: int = Field(default=15, ge=0, le=1440)


class Settings(BaseModel):
    """The one and only shape a settings document may have."""

    model_config = {"extra": "forbid"}

    timezone: str = "Europe/Sofia"
    language: Literal["en", "bg"] = "en"
    time_format: Literal["24h", "12h"] = "24h"
    first_day_of_week: Literal["monday", "sunday"] = "monday"
    work_days: list[WeekDay] = Field(default_factory=lambda: ["mon", "tue", "wed", "thu", "fri"])
    work_day_start: str = Field(default="09:00", pattern=r"^\d{2}:\d{2}$")
    work_day_end: str = Field(default="18:00", pattern=r"^\d{2}:\d{2}$")
    default_event_duration_minutes: int = Field(default=30, ge=5, le=480)
    break_between_events_minutes: int = Field(default=10, ge=0, le=120)
    daily_focus_hours: int = Field(default=4, ge=0, le=16)
    notifications: Notifications = Field(default_factory=Notifications)
    #: Tag -> color, used to paint entries in the CLI.
    tag_colors: dict[str, TagColor] = Field(
        default_factory=lambda: {"work": "blue", "personal": "green", "health": "magenta"}
    )


DEFAULT_SETTINGS: dict[str, Any] = Settings().model_dump()


def validate_settings(stored: dict[str, Any]) -> dict[str, Any]:
    """Merge a stored document onto the defaults, falling back if it is broken."""
    try:
        return Settings(**{**DEFAULT_SETTINGS, **stored}).model_dump()
    except Exception:
        # A document written by an older version must not take the CLI down.
        return dict(DEFAULT_SETTINGS)


#: Injected verbatim into the system prompts so the models know the exact,
#: closed format they are allowed to write back to the settings store.
SETTINGS_FORMAT_SPEC = f"""\
Settings are a single JSON object with EXACTLY these keys - no others may be
added, and none may be removed:

- timezone: IANA timezone name, e.g. "Europe/Sofia".
- language: one of "en" | "bg".
- time_format: one of "24h" | "12h".
- first_day_of_week: one of "monday" | "sunday".
- work_days: list of "mon" | "tue" | "wed" | "thu" | "fri" | "sat" | "sun".
- work_day_start: "HH:MM" (24-hour).
- work_day_end: "HH:MM" (24-hour), must be later than work_day_start.
- default_event_duration_minutes: integer, 5..480.
- break_between_events_minutes: integer, 0..120.
- daily_focus_hours: integer, 0..16.
- notifications: object with:
    - enabled: boolean
    - minutes_before: integer, 0..1440
- tag_colors: object mapping an entry tag to the color the CLI paints it in,
  e.g. {{"work": "blue", "gym": "bright_green"}}. Colors must come from this
  list: {", ".join(TAG_COLORS)}.

Anything the user asks for that does NOT fit one of these keys is a
*preference*, not a setting, and must be stored as free text instead."""
