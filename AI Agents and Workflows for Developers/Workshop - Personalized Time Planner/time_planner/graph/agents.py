"""The three worker agents.

Each is built with `create_agent`, so each is a compiled subgraph with its own
model, tools and system prompt. They are mounted into the graph by
`workers.py`, which is what puts their steps under the parent's checkpointer.

The calendar agent additionally runs `HumanInTheLoopMiddleware`: deleting an
entry is the one irreversible thing this app can do, so that call is put to the
user for approval instead of being trusted to the model's own judgement. The
middleware needs a checkpointer, which the agent inherits from the parent graph.
"""

from __future__ import annotations

from functools import lru_cache

from langchain.agents import create_agent
from langchain.agents.middleware import HumanInTheLoopMiddleware

from time_planner.context import PlannerContext
from time_planner.graph.tools import CALENDAR_TOOLS, PREFERENCES_TOOLS, SETTINGS_TOOLS
from time_planner.models import get_model
from time_planner.prompts import (
    CALENDAR_SYSTEM_PROMPT,
    PREFERENCES_SYSTEM_PROMPT,
    SETTINGS_SYSTEM_PROMPT,
)
from time_planner.routing import MANAGE_CALENDAR, MANAGE_PREFERENCES, MANAGE_SETTINGS


def _agent(name: str, system_prompt: str, tools, middleware=()):
    return create_agent(
        model=get_model(),
        tools=tools,
        system_prompt=system_prompt,
        context_schema=PlannerContext,
        middleware=middleware,
        name=name,
    )


@lru_cache(maxsize=1)
def preferences_agent():
    return _agent(MANAGE_PREFERENCES, PREFERENCES_SYSTEM_PROMPT, PREFERENCES_TOOLS)


@lru_cache(maxsize=1)
def settings_agent():
    return _agent(MANAGE_SETTINGS, SETTINGS_SYSTEM_PROMPT, SETTINGS_TOOLS)


@lru_cache(maxsize=1)
def calendar_agent():
    approval = HumanInTheLoopMiddleware(
        # `update_event` can be added here the same way if moving entries should
        # be confirmed too.
        interrupt_on={"delete_event": {"allowed_decisions": ["approve", "reject"]}},
        description_prefix="The calendar worker wants to delete an entry",
    )
    return _agent(MANAGE_CALENDAR, CALENDAR_SYSTEM_PROMPT, CALENDAR_TOOLS, [approval])
