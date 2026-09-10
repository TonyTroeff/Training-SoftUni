"""Node names and the structured decision the Main node produces."""

from __future__ import annotations

from typing import Literal

from pydantic import BaseModel, Field

# Node names - kept in one place so the prompts, the routing and the graph
# agree. The three worker names double as the state channels they talk on.
LOAD_PREFERENCES = "load_preferences"
LOAD_SETTINGS = "load_settings"
MAIN = "main"
MANAGE_PREFERENCES = "manage_preferences"
MANAGE_SETTINGS = "manage_settings"
MANAGE_CALENDAR = "manage_calendar"

#: The workers the Main node may delegate to.
DELEGATES = (MANAGE_PREFERENCES, MANAGE_SETTINGS, MANAGE_CALENDAR)

Delegate = Literal["manage_preferences", "manage_settings", "manage_calendar"]


class Delegation(BaseModel):
    """One unit of work handed to one worker."""

    delegate: Delegate
    instructions: str = Field(
        description="A self-contained task description for the worker, with every "
        "relative date already resolved to an absolute ISO date."
    )


class RoutingDecision(BaseModel):
    """What the Main node decided to do with the user's request."""

    reasoning: str = Field(description="One or two sentences on why this routing.")
    delegations: list[Delegation] = Field(
        min_length=1,
        description="At least one - every request is answered by a worker.",
    )
