"""The conversation record handed to :class:`~assistant.ai.client.AIClient`."""

from __future__ import annotations

from typing import Literal, TypeAlias

from pydantic import BaseModel, ConfigDict

__all__ = ["Message", "Role"]

Role: TypeAlias = Literal["user", "assistant"]
"""Turn author. The system prompt is configuration, not a turn, so it is absent here."""


class Message(BaseModel):
    """One completed turn of the conversation.

    Only settled turns become messages; a response still streaming lives in the
    view until it finishes.
    """

    model_config = ConfigDict(frozen=True)

    role: Role
    content: str
