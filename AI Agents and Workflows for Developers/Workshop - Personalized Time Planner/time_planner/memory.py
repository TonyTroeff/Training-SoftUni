"""Long-term memory, stored on the local file system.

Backed by langchain's `LocalFileStore` - a byte store whose keys are paths
relative to a root directory, so the layout on disk is readable:

    <root>/troeff/preferences.json
    <root>/troeff/settings.json
    <root>/troeff/events/2026-09-08.json

Every key starts with the `user_id` that `PlannerContext` carries, so memory is
naturally scoped per user.
"""

from __future__ import annotations

import json
from datetime import date
from functools import lru_cache
from typing import Any

from langchain_classic.storage import LocalFileStore

from time_planner.clock import days_between
from time_planner.config import STORE_DIR


@lru_cache(maxsize=1)
def get_store() -> LocalFileStore:
    STORE_DIR.mkdir(parents=True, exist_ok=True)
    return LocalFileStore(STORE_DIR)


# -- keys ----------------------------------------------------------------


def preferences_key(user_id: str) -> str:
    return f"{user_id}/preferences.json"


def settings_key(user_id: str) -> str:
    return f"{user_id}/settings.json"


def events_key(user_id: str, day: str) -> str:
    """`day` is an ISO date, e.g. "2026-09-08"."""
    return f"{user_id}/events/{day}.json"


# -- raw access ----------------------------------------------------------


def read_json(key: str, default: Any) -> Any:
    (raw,) = get_store().mget([key])
    if raw is None:
        return default
    try:
        return json.loads(raw.decode("utf-8"))
    except (UnicodeDecodeError, json.JSONDecodeError):
        return default


def write_json(key: str, value: Any) -> None:
    payload = json.dumps(value, indent=2, ensure_ascii=False).encode("utf-8")
    get_store().mset([(key, payload)])


# -- preferences (free text, one entry per line) --------------------------


def load_preferences(user_id: str) -> list[str]:
    return read_json(preferences_key(user_id), [])


def save_preferences(user_id: str, preferences: list[str]) -> None:
    write_json(preferences_key(user_id), preferences)


# -- settings (closed schema, see settings.py) ----------------------------


def load_raw_settings(user_id: str) -> dict[str, Any]:
    return read_json(settings_key(user_id), {})


def save_raw_settings(user_id: str, settings: dict[str, Any]) -> None:
    write_json(settings_key(user_id), settings)


# -- events (one file per day) --------------------------------------------


def load_events(user_id: str, day: str) -> list[dict[str, Any]]:
    return read_json(events_key(user_id, day), [])


def save_events(user_id: str, day: str, events: list[dict[str, Any]]) -> None:
    write_json(events_key(user_id, day), events)


def load_events_range(user_id: str, start: date, end: date) -> list[dict[str, Any]]:
    """Every entry between two inclusive dates, in chronological order."""
    collected: list[dict[str, Any]] = []
    for day in days_between(start, end):
        collected.extend(load_events(user_id, day.isoformat()))
    return sorted(collected, key=lambda e: (e.get("date", ""), e.get("start") or "99:99"))
