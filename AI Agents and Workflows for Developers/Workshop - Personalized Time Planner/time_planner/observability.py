"""LangSmith tracing, switched on by the presence of an API key.

Importing `time_planner.config` has already loaded `.env`, so putting
`LANGSMITH_API_KEY` there is enough to get traces; leaving it out keeps the
whole run local. Nothing else in the app knows about tracing - the LangChain
and LangGraph SDKs pick it up from the environment.
"""

from __future__ import annotations

import os

from time_planner.config import LANGSMITH_PROJECT


def enable_tracing() -> str | None:
    """Turn LangSmith tracing on when it is configured.

    Returns the project traces will land in, or `None` when tracing is off.
    """
    if not os.getenv("LANGSMITH_API_KEY"):
        # An API key is the switch: without one, make sure a stale environment
        # variable cannot leave tracing half-enabled and failing on every call.
        os.environ["LANGSMITH_TRACING"] = "false"
        os.environ.pop("LANGCHAIN_TRACING_V2", None)
        return None

    project = os.getenv("LANGSMITH_PROJECT") or LANGSMITH_PROJECT
    os.environ["LANGSMITH_PROJECT"] = project
    os.environ["LANGSMITH_TRACING"] = "true"
    return project
