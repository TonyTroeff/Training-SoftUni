"""Template for every user prompt handed to the models.

Every request is wrapped in this template so the model always sees, verbatim
and unambiguously, *which day it is right now* - LLMs have no clock, and a time
planner that guesses the date is worse than useless.
"""

from __future__ import annotations

import json
from datetime import datetime, timedelta
from typing import Any

from time_planner.clock import week_bounds

USER_PROMPT_TEMPLATE = """\
<current_moment>
Today is {weekday}, {today_iso}.
The current local time is {now_time} ({timezone}).
The current week starts on Monday {week_start} and ends on Sunday {week_end}.
Yesterday was {yesterday}. Tomorrow is {tomorrow}.
Whenever the user says "today", "tonight", "tomorrow", "this week", "next
week" or any other relative date, resolve it against the values above and use
absolute ISO dates (YYYY-MM-DD) from that point on. Never guess the date.
</current_moment>

<user_preferences>
{preferences}
</user_preferences>

<user_settings>
{settings}
</user_settings>

<request>
{request}
</request>"""


def build_user_prompt(
    *,
    request: str,
    preferences: list[str],
    settings: dict[str, Any],
    now: datetime,
    timezone: str,
) -> str:
    """Wrap `request` - a user prompt, or an instruction the Main node wrote."""
    today = now.date()
    week_start, week_end = week_bounds(today)
    return USER_PROMPT_TEMPLATE.format(
        weekday=today.strftime("%A"),
        today_iso=today.isoformat(),
        now_time=now.strftime("%H:%M"),
        timezone=timezone,
        week_start=week_start.isoformat(),
        week_end=week_end.isoformat(),
        yesterday=(today - timedelta(days=1)).isoformat(),
        tomorrow=(today + timedelta(days=1)).isoformat(),
        preferences="\n".join(f"- {p}" for p in preferences) or "(none recorded yet)",
        settings=json.dumps(settings, indent=2, ensure_ascii=False),
        request=request.strip(),
    )
