"""Tools for the three worker agents.

The signatures, docstrings and storage layout are final - the bodies are not.
Every tool takes a `ToolRuntime[PlannerContext]`, which is how `user_id` reaches
it: LangGraph injects the context the CLI passed to `invoke`, so a tool can
never touch another user's data by accident. `ToolRuntime` is the tool-side
counterpart of the `Runtime` that graph nodes get, and it carries the tool call
id and the agent state as well as the context.

A tool that raises `ValueError` is not a crash: the agent is handed the message
as that tool call's result and gets to correct itself, which is why every
failure below says what was wrong instead of only that something was.
"""

from __future__ import annotations

import re
from datetime import date as date_cls
from typing import Any

from langchain.tools import ToolRuntime, tool
from pydantic import ValidationError

from time_planner import memory
from time_planner.clock import now_in
from time_planner.context import PlannerContext
from time_planner.events import Event
from time_planner.settings import Settings, validate_settings


# -- shared helpers ------------------------------------------------------


def _settings_of(runtime: ToolRuntime[PlannerContext]) -> dict[str, Any]:
    return validate_settings(memory.load_raw_settings(runtime.context.user_id))


def _parse_date(value: Any, field: str) -> date_cls:
    """Dates arrive from a model, so they are checked rather than trusted."""
    try:
        return date_cls.fromisoformat(str(value))
    except (TypeError, ValueError):
        raise ValueError(
            f"{field} must be an ISO date like 2026-09-08, got {value!r}."
        ) from None


def _minutes(value: str) -> int:
    hour, minute = value.split(":")
    return int(hour) * 60 + int(minute)


def _hhmm(total: int) -> str:
    return f"{total // 60:02d}:{total % 60:02d}"


def _slugify(text: str, limit: int = 40) -> str:
    slug = re.sub(r"[^a-z0-9]+", "-", text.lower()).strip("-")
    # Strip again after truncating, so a cut mid-word leaves no trailing dash.
    return slug[:limit].strip("-") or "entry"


def _unique_id(base: str, taken: set[str]) -> str:
    if base not in taken:
        return base
    for suffix in range(2, 100):
        candidate = f"{base}-{suffix}"
        if candidate not in taken:
            return candidate
    raise ValueError(f"Could not find a free id near {base!r}.")


def _validated(event: dict[str, Any]) -> dict[str, Any]:
    try:
        return Event(**event).model_dump()
    except ValidationError as error:
        raise ValueError(f"That is not a valid calendar entry: {error}") from None


# -- preferences ---------------------------------------------------------


@tool
def read_preferences(runtime: ToolRuntime[PlannerContext]) -> list[str]:
    """Read the user's preferences: a list of free-text sentences."""
    return memory.load_preferences(runtime.context.user_id)


@tool
def save_preferences(
    preferences: list[str], runtime: ToolRuntime[PlannerContext]
) -> str:
    """Replace the whole preference list.

    Args:
        preferences: The complete new list. Read the current one first and edit
            it minimally - anything you leave out is lost.
    """
    if not isinstance(preferences, list) or any(
        not isinstance(entry, str) for entry in preferences
    ):
        raise ValueError("preferences must be a list of plain strings.")

    cleaned = [entry.strip() for entry in preferences if entry.strip()]
    memory.save_preferences(runtime.context.user_id, cleaned)
    return f"Saved {len(cleaned)} preference(s)."


PREFERENCES_TOOLS = [read_preferences, save_preferences]


# -- settings ------------------------------------------------------------


@tool
def read_settings(runtime: ToolRuntime[PlannerContext]) -> dict[str, Any]:
    """Read the user's settings document, with defaults filled in."""
    return _settings_of(runtime)


@tool
def update_settings(
    changes: dict[str, Any], runtime: ToolRuntime[PlannerContext]
) -> dict[str, Any]:
    """Merge changes into the stored settings and return the new document.

    Args:
        changes: Only the settings keys you are changing. Untouched keys keep
            their current values.
    """
    if not isinstance(changes, dict):
        raise ValueError("changes must be a JSON object of settings keys.")

    current = _settings_of(runtime)
    merged = dict(current)
    for key, value in changes.items():
        # `notifications` and `tag_colors` are objects, so merging them means
        # "add or change these entries", not "replace the whole object".
        if isinstance(value, dict) and isinstance(current.get(key), dict):
            merged[key] = {**current[key], **value}
        else:
            merged[key] = value

    try:
        document = Settings(**merged).model_dump()
    except ValidationError as error:
        raise ValueError(
            f"Those settings are not valid, so nothing was saved: {error}"
        ) from None
    if _minutes(document["work_day_end"]) <= _minutes(document["work_day_start"]):
        raise ValueError(
            "work_day_end must be later than work_day_start; nothing was saved."
        )

    memory.save_raw_settings(runtime.context.user_id, document)
    return document


SETTINGS_TOOLS = [read_settings, update_settings]


# -- calendar ------------------------------------------------------------


@tool
def list_events(
    start_date: str, end_date: str, runtime: ToolRuntime[PlannerContext]
) -> list[dict[str, Any]]:
    """List every calendar entry in a date range, in chronological order.

    Args:
        start_date: Inclusive ISO date, YYYY-MM-DD.
        end_date: Inclusive ISO date, YYYY-MM-DD.
    """
    start = _parse_date(start_date, "start_date")
    end = _parse_date(end_date, "end_date")
    if end < start:
        raise ValueError(f"end_date ({end}) is before start_date ({start}).")
    return memory.load_events_range(runtime.context.user_id, start, end)


@tool
def create_event(
    event: dict[str, Any], runtime: ToolRuntime[PlannerContext]
) -> dict[str, Any]:
    """Create a calendar entry.

    Args:
        event: The new entry, matching the documented entry format.
    """
    if not isinstance(event, dict):
        raise ValueError("event must be a JSON object in the entry format.")

    draft = dict(event)
    day = _parse_date(draft.get("date"), "event.date").isoformat()
    draft["date"] = day

    # A timed entry with no end gets the user's default duration - otherwise
    # that setting would never be applied to anything.
    if draft.get("start") and not draft.get("end"):
        default = _settings_of(runtime)["default_event_duration_minutes"]
        draft["end"] = _hhmm(min(_minutes(draft["start"]) + default, 23 * 60 + 59))

    user_id = runtime.context.user_id
    stored = memory.load_events(user_id, day)
    # An id the model did not supply is derived from the title and the time, so
    # the file on disk stays readable: "business-meeting-1400".
    stem = _slugify(str(draft.get("title") or "entry"), limit=32)
    clock = (draft.get("start") or "").replace(":", "")
    base = str(draft.get("id") or "").strip() or (f"{stem}-{clock}" if clock else stem)
    draft["id"] = _unique_id(base, {entry.get("id") for entry in stored})

    created = _validated(draft)
    memory.save_events(user_id, day, [*stored, created])
    return created


@tool
def update_event(
    date: str,
    event_id: str,
    changes: dict[str, Any],
    runtime: ToolRuntime[PlannerContext],
) -> dict[str, Any]:
    """Change an existing calendar entry.

    Args:
        date: The day the entry is currently stored under, YYYY-MM-DD.
        event_id: The entry's id within that day.
        changes: The fields to change. Pass a new `date` to move the entry to
            another day.
    """
    if not isinstance(changes, dict):
        raise ValueError("changes must be a JSON object of entry fields.")

    user_id = runtime.context.user_id
    day = _parse_date(date, "date").isoformat()
    stored = memory.load_events(user_id, day)
    index = next(
        (i for i, entry in enumerate(stored) if entry.get("id") == event_id), None
    )
    if index is None:
        known = ", ".join(str(entry.get("id")) for entry in stored) or "(none)"
        raise ValueError(f"No entry {event_id!r} on {day}. That day holds: {known}.")

    updated = _validated({**stored[index], **changes})
    target = updated["date"]
    if target == day:
        stored[index] = updated
        memory.save_events(user_id, day, stored)
        return updated

    # Moved to another day: it leaves one file and joins another, and its id
    # only has to be unique within the day it lands in.
    remaining = [entry for i, entry in enumerate(stored) if i != index]
    destination = memory.load_events(user_id, target)
    updated["id"] = _unique_id(updated["id"], {e.get("id") for e in destination})
    memory.save_events(user_id, day, remaining)
    memory.save_events(user_id, target, [*destination, updated])
    return updated


@tool
def delete_event(
    date: str, event_id: str, runtime: ToolRuntime[PlannerContext]
) -> str:
    """Permanently delete a calendar entry.

    The user is asked to approve this call before it runs - see the
    human-in-the-loop middleware on the calendar agent.

    Args:
        date: The day the entry is stored under, YYYY-MM-DD.
        event_id: The entry's id within that day.
    """
    user_id = runtime.context.user_id
    day = _parse_date(date, "date").isoformat()
    stored = memory.load_events(user_id, day)
    remaining = [entry for entry in stored if entry.get("id") != event_id]
    if len(remaining) == len(stored):
        known = ", ".join(str(entry.get("id")) for entry in stored) or "(none)"
        raise ValueError(f"No entry {event_id!r} on {day}. That day holds: {known}.")

    memory.save_events(user_id, day, remaining)
    return f"Deleted {event_id!r} from {day}."


@tool
def find_free_slots(
    date: str, duration_minutes: int, runtime: ToolRuntime[PlannerContext]
) -> list[dict[str, str]]:
    """Find free slots on a day, respecting the user's working hours.

    Args:
        date: The day to search, YYYY-MM-DD.
        duration_minutes: The smallest slot worth returning.
    """
    day = _parse_date(date, "date")
    try:
        wanted = int(duration_minutes)
    except (TypeError, ValueError):
        raise ValueError(
            f"duration_minutes must be a number, got {duration_minutes!r}."
        ) from None
    if wanted <= 0:
        raise ValueError("duration_minutes must be greater than zero.")

    settings = _settings_of(runtime)
    window_start = _minutes(settings["work_day_start"])
    window_end = _minutes(settings["work_day_end"])
    gap = settings["break_between_events_minutes"]
    default_duration = settings["default_event_duration_minutes"]

    # Nothing can be booked in the past, so today's window starts now.
    now = now_in(runtime.context.timezone)
    if day == now.date():
        window_start = max(window_start, now.hour * 60 + now.minute)

    busy: list[tuple[int, int]] = []
    for entry in memory.load_events(runtime.context.user_id, day.isoformat()):
        # A cancelled entry holds no time, and one with no start is an all-day
        # note rather than something occupying a slot.
        if entry.get("status") == "cancelled" or not entry.get("start"):
            continue
        start = _minutes(entry["start"])
        end = _minutes(entry["end"]) if entry.get("end") else start + default_duration
        # The break the user wants between entries is padding around each one.
        busy.append((start - gap, max(end, start) + gap))

    free: list[dict[str, str]] = []
    cursor = window_start
    for start, end in sorted(busy):
        if start - cursor >= wanted:
            free.append({"start": _hhmm(cursor), "end": _hhmm(start)})
        cursor = max(cursor, end)
    if window_end - cursor >= wanted:
        free.append({"start": _hhmm(cursor), "end": _hhmm(window_end)})
    return free


CALENDAR_TOOLS = [list_events, create_event, update_event, delete_event, find_free_slots]
