"""Process-level configuration: filesystem locations and the chat model."""

from __future__ import annotations

import os
from pathlib import Path

from dotenv import load_dotenv

load_dotenv()

#: Root of all on-disk persistence (long-term store + checkpoints).
DATA_DIR = Path(os.getenv("TIME_PLANNER_DATA_DIR", ".time_planner")).resolve()

#: Long-term memory (preferences, settings, events by day) - see `memory.py`.
STORE_DIR = DATA_DIR / "store"

#: Short-term memory (thread checkpoints) so an interrupted run can be resumed
#: after the CLI is restarted.
CHECKPOINT_DB = DATA_DIR / "checkpoints.sqlite"

#: Model used by the "Main" node and by the three worker agents.
MODEL_NAME = os.getenv("TIME_PLANNER_MODEL", "gpt-4.1-mini")

#: LangSmith project traces land in, when LANGSMITH_API_KEY is set.
LANGSMITH_PROJECT = "personalized-time-planner"


def thread_id_for(user_id: str) -> str:
    """A stable thread per user, so interrupted work survives a restart."""
    return f"cli-{user_id}"


def ensure_data_dirs() -> None:
    STORE_DIR.mkdir(parents=True, exist_ok=True)
    CHECKPOINT_DB.parent.mkdir(parents=True, exist_ok=True)
