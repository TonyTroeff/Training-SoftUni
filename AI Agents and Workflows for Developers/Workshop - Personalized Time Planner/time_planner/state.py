"""Shared state of the planner graph."""

from __future__ import annotations

from typing import Annotated, Any

from langchain_core.messages import AnyMessage
from langgraph.graph.message import add_messages
from typing_extensions import TypedDict


class PlannerState(TypedDict):
    """What flows between the nodes of the top-level graph.

    The three `manage_*` channels are the conversations with the three worker
    agents. The Main node opens each one by writing the instruction it wants
    that worker to carry out; the worker appends everything it says and does.
    They start empty on every turn - the CLI clears them with
    `RemoveMessage(REMOVE_ALL_MESSAGES)`.
    """

    #: Free-text preferences, one per entry, loaded from long-term memory.
    preferences: list[str]
    #: Structured settings, loaded from long-term memory (see `settings.py`).
    settings: dict[str, Any]
    #: The raw prompt the user typed.
    user_input: str

    manage_preferences: Annotated[list[AnyMessage], add_messages]
    manage_settings: Annotated[list[AnyMessage], add_messages]
    manage_calendar: Annotated[list[AnyMessage], add_messages]
