"""The one place that answers "what time is it?"."""

from __future__ import annotations

from datetime import date, datetime, timedelta


def now_in(timezone: str) -> datetime:
    """Current local time in `timezone`, falling back to system local time."""
    try:
        from zoneinfo import ZoneInfo

        return datetime.now(ZoneInfo(timezone))
    except Exception:  # unknown zone, or no tzdata on this platform
        return datetime.now().astimezone()


def previous_day(day: date) -> date:
    return day - timedelta(days=1)


def next_day(day: date) -> date:
    return day + timedelta(days=1)


def week_bounds(day: date) -> tuple[date, date]:
    """Monday..Sunday of the week containing `day`."""
    start = day - timedelta(days=day.weekday())
    return start, start + timedelta(days=6)


def days_between(start: date, end: date) -> list[date]:
    return [start + timedelta(days=offset) for offset in range((end - start).days + 1)]
