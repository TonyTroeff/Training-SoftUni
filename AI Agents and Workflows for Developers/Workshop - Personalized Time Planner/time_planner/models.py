"""The chat model shared by the Main node and the three worker agents."""

from __future__ import annotations

from functools import lru_cache

from langchain.chat_models import init_chat_model

from time_planner.config import MODEL_NAME


@lru_cache(maxsize=1)
def get_model():
    """Build the chat model.

    Raises whatever the provider raises when no API key is configured; the CLI
    catches that and keeps running, since the slash commands never need a model.

    Cached: the Main node and all three agents share one client. `lru_cache`
    does not cache exceptions, so a missing key still fails on every call.
    """
    return init_chat_model(MODEL_NAME)
