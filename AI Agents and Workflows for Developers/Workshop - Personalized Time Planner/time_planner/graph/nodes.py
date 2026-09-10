"""Nodes of the top-level graph."""

from __future__ import annotations

from typing import Literal

from langchain_core.messages import HumanMessage, SystemMessage
from langgraph.runtime import Runtime
from langgraph.types import Command

from time_planner import memory
from time_planner.clock import now_in
from time_planner.context import PlannerContext
from time_planner.models import get_model
from time_planner.prompts import MAIN_SYSTEM_PROMPT, build_user_prompt
from time_planner.routing import MANAGE_CALENDAR, RoutingDecision
from time_planner.settings import validate_settings
from time_planner.state import PlannerState


# -- loaders -------------------------------------------------------------


def load_preferences(state: PlannerState, runtime: Runtime[PlannerContext]) -> dict:
    """Read the user's free-text preferences from long-term memory."""
    return {"preferences": memory.load_preferences(runtime.context.user_id)}


def load_settings(state: PlannerState, runtime: Runtime[PlannerContext]) -> dict:
    """Read the user's structured settings, filling in the defaults."""
    stored = memory.load_raw_settings(runtime.context.user_id)
    return {"settings": validate_settings(stored)}


# -- main ----------------------------------------------------------------

Worker = Literal["manage_preferences", "manage_settings", "manage_calendar"]


def main(state: PlannerState, runtime: Runtime[PlannerContext]) -> Command[Worker]:
    """Analyze the request and hand it to the right worker(s).

    Delegating means two things at once, which is why this returns a `Command`:
    the instruction is written into that worker's channel, and the graph is told
    to go there.
    """
    user_input = (state.get("user_input") or "").strip()
    decision = _route(state, runtime, user_input)

    update: dict[str, list] = {}
    goto: list[str] = []
    for delegation in decision.delegations:
        if delegation.delegate in goto:
            # One conversation per worker per turn: a second instruction for the
            # same worker is folded into the first rather than overwriting it.
            continue
        update[delegation.delegate] = [
            HumanMessage(_wrap(state, runtime, delegation.instructions))
        ]
        goto.append(delegation.delegate)
    return Command(update=update, goto=goto)


def _route(
    state: PlannerState, runtime: Runtime[PlannerContext], user_input: str
) -> RoutingDecision:
    """Ask the model which worker(s) should handle this request.

    `RoutingDecision` is a closed schema, so the model can only name a worker
    that exists - but it can still fail or return nothing usable, and a routing
    failure must not cost the user their turn. The calendar worker is the
    fallback: it is the one that can read the calendar and answer for itself.
    """
    prompt = [
        SystemMessage(MAIN_SYSTEM_PROMPT),
        HumanMessage(_wrap(state, runtime, user_input)),
    ]
    try:
        decision = get_model().with_structured_output(RoutingDecision).invoke(prompt)
    except Exception as error:  # a bad response, a rate limit, a dropped call
        return _fallback(user_input, f"Routing failed ({type(error).__name__}).")
    if not decision or not decision.delegations:
        return _fallback(user_input, "The router named no worker.")
    return decision


def _fallback(user_input: str, reasoning: str) -> RoutingDecision:
    return RoutingDecision(
        reasoning=f"{reasoning} Falling back to the calendar worker.",
        delegations=[{"delegate": MANAGE_CALENDAR, "instructions": user_input}],
    )


def _wrap(state: PlannerState, runtime: Runtime[PlannerContext], request: str) -> str:
    """Put a request into the shared template, so it carries today's date."""
    return build_user_prompt(
        request=request,
        preferences=state.get("preferences") or [],
        settings=state.get("settings") or {},
        now=now_in(runtime.context.timezone),
        timezone=runtime.context.timezone,
    )
