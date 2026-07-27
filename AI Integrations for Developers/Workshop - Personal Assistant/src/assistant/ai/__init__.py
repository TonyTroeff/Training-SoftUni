"""The model-facing seam: typed events, the conversation record, and the client."""

from __future__ import annotations

from assistant.ai.client import AIClient
from assistant.ai.events import AIEvent, StreamError, TextDelta, ToolCall, ToolResult
from assistant.ai.messages import Message, Role

__all__ = [
    "AIClient",
    "AIEvent",
    "Message",
    "Role",
    "StreamError",
    "TextDelta",
    "ToolCall",
    "ToolResult",
]
