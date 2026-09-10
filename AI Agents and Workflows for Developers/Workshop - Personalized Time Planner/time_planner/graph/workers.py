"""Adapters that mount the three `create_agent` workers into the graph.

Each worker talks on its own channel of the parent state (`manage_calendar`,
`manage_settings`, `manage_preferences`), while `create_agent` always works on
a channel called `messages`. This module is the two-node adapter between the
two, wrapped in a subgraph so the agent can be added with `add_node`.

That detail matters: a subgraph added as a node inherits the parent's
checkpointer and gets its own checkpoint namespace, which is what lets an
agent stop mid-run to ask a question and carry on from that exact step when it
is resumed - even in a later process. Invoking the agent by hand from inside a
plain node does not give you that; the node restarts and the work is replayed.
"""

from __future__ import annotations

from typing import Annotated

from langchain_core.messages import AnyMessage
from langgraph.graph import END, START, StateGraph
from langgraph.graph.message import add_messages

from time_planner.context import PlannerContext
from time_planner.state import PlannerState


class WorkerState(PlannerState):
    """Parent state plus the private `messages` channel the agent works on."""

    messages: Annotated[list[AnyMessage], add_messages]


def build_worker(channel: str, agent):
    """Wrap `agent` so it reads and writes `channel` instead of `messages`."""

    def seed(state: WorkerState) -> dict:
        # The channel already holds the instruction the Main node wrote.
        return {"messages": state[channel]}

    def collect(state: WorkerState) -> dict:
        # Messages carry ids, so `add_messages` merges rather than duplicates.
        return {channel: state["messages"]}

    return (
        StateGraph(WorkerState, context_schema=PlannerContext)
        .add_node("seed", seed)
        .add_node("agent", agent)
        .add_node("collect", collect)
        .add_edge(START, "seed")
        .add_edge("seed", "agent")
        .add_edge("agent", "collect")
        .add_edge("collect", END)
        .compile(name=channel)
    )
