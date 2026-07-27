"""Chooses which :class:`~assistant.ai.client.AIClient` the shell runs against."""

from __future__ import annotations

from typing import TYPE_CHECKING

from assistant.ai.client import AIClient

if TYPE_CHECKING:
    from assistant.config import Config

__all__ = ["build_client"]


def build_client(config: Config) -> AIClient:
    """Return the real client when credentials exist, the scripted one otherwise.

    Args:
        config: The application configuration.

    Returns:
        A client bound to the narrowed AI configuration slice.
    """
    ai_config = config.ai_client_config()
    return AIClient(ai_config)
