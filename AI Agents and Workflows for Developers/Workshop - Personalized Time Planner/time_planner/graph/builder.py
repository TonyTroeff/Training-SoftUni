"""Assembly of the top-level planner graph.

    START -> load_preferences ┐
    START -> load_settings    ├-> main -> manage_preferences -> END
                              ┘         -> manage_settings    -> END
                                        -> manage_calendar    -> END

Slash commands never reach this graph - the CLI answers them straight from
long-term memory. Everything here is for free-text requests.
"""

from __future__ import annotations

from langgraph.graph import END, START, StateGraph

from time_planner.context import PlannerContext
from time_planner.graph.agents import calendar_agent, preferences_agent, settings_agent
from time_planner.graph.nodes import load_preferences, load_settings, main
from time_planner.graph.workers import build_worker
from time_planner.routing import (
    DELEGATES,
    LOAD_PREFERENCES,
    LOAD_SETTINGS,
    MAIN,
    MANAGE_CALENDAR,
    MANAGE_PREFERENCES,
    MANAGE_SETTINGS,
)
from time_planner.state import PlannerState


def build_graph(*, checkpointer=None):
    """Compile the planner graph.

    Args:
        checkpointer: short-term memory - one thread per user, so a worker that
            stopped to ask a question resumes where it left off, even after the
            CLI has been restarted.
    """
    builder = StateGraph(PlannerState, context_schema=PlannerContext)

    builder.add_node(LOAD_PREFERENCES, load_preferences)
    builder.add_node(LOAD_SETTINGS, load_settings)
    builder.add_node(MAIN, main)
    # Each worker is a `create_agent` subgraph, mounted on its own channel.
    builder.add_node(MANAGE_PREFERENCES, build_worker(MANAGE_PREFERENCES, preferences_agent()))
    builder.add_node(MANAGE_SETTINGS, build_worker(MANAGE_SETTINGS, settings_agent()))
    builder.add_node(MANAGE_CALENDAR, build_worker(MANAGE_CALENDAR, calendar_agent()))

    # Both loaders run in parallel; main waits for both.
    builder.add_edge(START, LOAD_PREFERENCES)
    builder.add_edge(START, LOAD_SETTINGS)
    builder.add_edge(LOAD_PREFERENCES, MAIN)
    builder.add_edge(LOAD_SETTINGS, MAIN)

    # `main` routes with a Command, so it needs no static edges of its own.
    for delegate in DELEGATES:
        builder.add_edge(delegate, END)

    return builder.compile(checkpointer=checkpointer)
