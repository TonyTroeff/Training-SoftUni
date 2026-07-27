"""Events streamed out of :class:`~assistant.ai.client.AIClient`.

A frozen, discriminated union: every member carries a literal ``type`` tag, so
consumers can exhaustively match and the wire format can be validated with
``TypeAdapter(AIEvent)``.
"""

from __future__ import annotations

from typing import Annotated, Literal, TypeAlias

from pydantic import BaseModel, Field

__all__ = ["AIEvent", "StreamError", "TextDelta", "ToolCall", "ToolResult"]

class TextDelta(BaseModel):
    """An incremental chunk of assistant prose.

    Chunks are appended verbatim; the client decides the granularity.
    """

    type: Literal["text_delta"] = "text_delta"
    text: str


class ToolCall(BaseModel):
    """The model asked for a tool to run.

    Emitted once the call's arguments are complete, not per argument fragment.
    """

    type: Literal["tool_call"] = "tool_call"
    name: str
    arguments: str


class ToolResult(BaseModel):
    """The outcome of a previously announced :class:`ToolCall`."""

    type: Literal["tool_result"] = "tool_result"
    name: str
    content: str
    is_error: bool = False


class StreamError(BaseModel):
    """A failure the client wants rendered as a message rather than raised.

    Raising out of the stream is also supported and lands in the same UI state;
    use this event when the stream can report the problem and keep going.
    """

    type: Literal["error"] = "error"
    message: str
    retryable: bool = False


AIEvent: TypeAlias = Annotated[
    TextDelta | ToolCall | ToolResult | StreamError,
    Field(discriminator="type"),
]
"""Everything a stream can emit."""
