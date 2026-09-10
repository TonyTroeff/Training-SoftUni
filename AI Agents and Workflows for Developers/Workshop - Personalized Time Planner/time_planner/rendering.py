"""Terminal rendering of calendar entries."""

from __future__ import annotations

from datetime import date
from typing import Any

from rich.console import Console
from rich.panel import Panel
from rich.table import Table
from rich.text import Text

from time_planner.clock import days_between

STATUS_STYLES = {"planned": "", "done": "dim strike", "cancelled": "dim red strike"}

#: How the commands spell a priority out: P1 shouts, P3 stays out of the way.
PRIORITY_LABELS = {"high": "P1", "normal": "P2", "low": "P3"}
PRIORITY_STYLES = {"P1": "bold red", "P2": "dim", "P3": "dim"}


def priority_label(event: dict[str, Any]) -> str:
    return PRIORITY_LABELS.get(event.get("priority", "normal"), "P2")


def priority_style(event: dict[str, Any]) -> str:
    """A struck-out entry keeps its status style, so P1 does not revive it."""
    status = STATUS_STYLES.get(event.get("status", "planned"), "")
    return status or PRIORITY_STYLES[priority_label(event)]


def entry_style(event: dict[str, Any], settings: dict[str, Any]) -> str:
    """Colour an entry by its first tag that has a colour in the settings."""
    status = STATUS_STYLES.get(event.get("status", "planned"), "")
    if status:
        return status
    tag_colors: dict[str, str] = settings.get("tag_colors") or {}
    for tag in event.get("tags") or []:
        if tag in tag_colors:
            return tag_colors[tag]
    return "bold" if event.get("priority") == "high" else ""


def _to_12h(value: str) -> str:
    hour, minute = (int(part) for part in value.split(":"))
    suffix = "AM" if hour < 12 else "PM"
    return f"{(hour % 12) or 12}:{minute:02d} {suffix}"


def _clock(value: str, time_format: str) -> str:
    return _to_12h(value) if time_format == "12h" else value


def _time_range(event: dict[str, Any], time_format: str) -> str:
    """The intuitive bit: when it starts, when it ends, or that it is all day."""
    start, end = event.get("start"), event.get("end")
    if not start:
        return "all day"
    if not end:
        return _clock(start, time_format)
    return f"{_clock(start, time_format)}-{_clock(end, time_format)}"


def _sort_key(event: dict[str, Any]) -> tuple[str, str]:
    # All-day entries first, then chronological - how a calendar reads.
    return (event.get("date", ""), event.get("start") or "")


def _tag_legend(events: list[dict[str, Any]], settings: dict[str, Any]) -> Text | None:
    """A one-line key for the colours actually used in what we just drew."""
    tag_colors: dict[str, str] = settings.get("tag_colors") or {}
    used = [tag for tag in tag_colors if any(tag in (e.get("tags") or []) for e in events)]
    if not used:
        return None
    legend = Text()
    for index, tag in enumerate(used):
        if index:
            legend.append("  ")
        legend.append(f"* {tag}", style=tag_colors[tag])
    return legend


def render_day(
    console: Console, events: list[dict[str, Any]], day: date, settings: dict[str, Any]
) -> None:
    """The `/today` view: a chronological agenda for a single day."""
    time_format = settings.get("time_format", "24h")
    of_day = sorted((e for e in events if e.get("date") == day.isoformat()), key=_sort_key)

    body = Text()
    if not of_day:
        body.append("Nothing scheduled.", style="dim")
    for index, event in enumerate(of_day):
        if index:
            body.append("\n")
        body.append(f"{_time_range(event, time_format):>13}  ", style="dim")
        body.append(f"{priority_label(event)} ", style=priority_style(event))
        body.append(event.get("title", "(untitled)"), style=entry_style(event, settings))
        if event.get("location"):
            body.append(f"  @{event['location']}", style="dim")
        if event.get("tags"):
            body.append(f"  #{' #'.join(event['tags'])}", style="dim")

    console.print(Panel(body, title=day.strftime("%A, %d %B %Y"), border_style="cyan"))
    legend = _tag_legend(of_day, settings)
    if legend:
        console.print(legend)


def _day_cell(
    events: list[dict[str, Any]], settings: dict[str, Any], time_format: str
) -> Text:
    """One column's worth of entries: time above, title below."""
    if not events:
        return Text("-", style="dim")
    cell = Text()
    for index, event in enumerate(sorted(events, key=_sort_key)):
        if index:
            cell.append("\n\n")
        cell.append(_time_range(event, time_format), style="dim")
        cell.append("\n")
        cell.append(f"{priority_label(event)} ", style=priority_style(event))
        cell.append(event.get("title", "(untitled)"), style=entry_style(event, settings))
    return cell


def render_week(
    console: Console,
    events: list[dict[str, Any]],
    start: date,
    end: date,
    settings: dict[str, Any],
    today: date | None = None,
) -> None:
    """The `/this-week` view: one column per day, entries stacked underneath."""
    time_format = settings.get("time_format", "24h")

    by_day: dict[str, list[dict[str, Any]]] = {}
    for event in events:
        by_day.setdefault(event.get("date", ""), []).append(event)

    table = Table(
        title=f"{start.strftime('%d %b')} - {end.strftime('%d %b %Y')}",
        border_style="cyan",
        expand=True,
        show_lines=False,
        pad_edge=False,
    )
    days = days_between(start, end)
    for day in days:
        is_today = day == today
        table.add_column(
            f"{day.strftime('%a')}\n{day.strftime('%d %b')}",
            header_style="bold reverse cyan" if is_today else "bold cyan",
            justify="left",
            overflow="fold",
            ratio=1,
        )

    table.add_row(
        *(_day_cell(by_day.get(day.isoformat(), []), settings, time_format) for day in days)
    )

    console.print(table)
    legend = _tag_legend(events, settings)
    if legend:
        console.print(legend)
