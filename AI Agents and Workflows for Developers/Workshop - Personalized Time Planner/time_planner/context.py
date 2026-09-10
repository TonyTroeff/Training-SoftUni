"""Runtime context that is threaded through every component of the graph.

LangGraph passes this object to every node (and every subgraph node) via
`Runtime[PlannerContext]`, so any component can reach the current `user_id`
without smuggling it through the graph state.
"""

from __future__ import annotations

from dataclasses import dataclass

# Hardcoded for test/development purposes. In a real deployment this would come
# from the authenticated session and be passed to `graph.invoke(..., context=...)`.
DEFAULT_USER_ID = "troeff"

# Used for "what day is it" reasoning and for rendering. Kept in the context
# (not in the settings store) because it describes the running process, while
# the settings describe the user.
DEFAULT_TIMEZONE = "Europe/Sofia"


@dataclass
class PlannerContext:
    """Static, per-invocation context shared by all nodes and subgraphs."""

    user_id: str = DEFAULT_USER_ID
    timezone: str = DEFAULT_TIMEZONE
