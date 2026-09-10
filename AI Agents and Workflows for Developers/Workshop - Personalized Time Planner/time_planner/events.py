"""The shape of a calendar entry."""

from __future__ import annotations

from typing import Literal

from pydantic import BaseModel, Field

EventStatus = Literal["planned", "done", "cancelled"]
Priority = Literal["low", "normal", "high"]


class Event(BaseModel):
    """One entry in the user's day. Entries without a start time are all-day."""

    model_config = {"extra": "forbid"}

    id: str = Field(description="Stable identifier, unique within its day.")
    title: str
    date: str = Field(pattern=r"^\d{4}-\d{2}-\d{2}$")
    start: str | None = Field(default=None, pattern=r"^\d{2}:\d{2}$")
    end: str | None = Field(default=None, pattern=r"^\d{2}:\d{2}$")
    status: EventStatus = "planned"
    priority: Priority = "normal"
    location: str | None = None
    notes: str | None = None
    tags: list[str] = Field(default_factory=list)


#: Injected verbatim into the calendar agent's system prompt.
EVENT_FORMAT_SPEC = """\
A calendar entry is a JSON object with exactly these fields:

- id: string, unique within its day (e.g. "standup-0900").
- title: string, short and human-readable.
- date: "YYYY-MM-DD".
- start: "HH:MM" or null (null means it is not tied to a time of day).
- end: "HH:MM" or null.
- status: "planned" | "done" | "cancelled".
- priority: "low" | "normal" | "high".
- location: string or null.
- notes: string or null.
- tags: list of strings. Tags drive the colors the CLI uses, so reuse the tags
  the user already has (see `tag_colors` in their settings) instead of
  inventing a new spelling of an existing one."""
