"""Conversation state, and the glue that drives an :class:`AIClient` from the UI.

Every mutation of session state happens on the Tk thread: the worker coroutine
only forwards events through :meth:`AsyncTkBridge.post`, and the handlers below
run where the widgets live. That keeps the threading story to one sentence.
"""

from __future__ import annotations

import asyncio
from dataclasses import dataclass
from functools import partial
from typing import TYPE_CHECKING, Literal, TypeAlias

from assistant.ai.events import TextDelta
from assistant.ai.messages import Message

if TYPE_CHECKING:
    from collections.abc import Callable

    from assistant.ai.client import AIClient
    from assistant.ai.events import AIEvent
    from assistant.runtime.bridge import AsyncTkBridge, TaskHandle

__all__ = ["ChatSession", "FinishReason", "StreamHandlers"]

FinishReason: TypeAlias = Literal["completed", "cancelled", "failed"]


@dataclass(frozen=True)
class StreamHandlers:
    """UI-thread callbacks for the lifetime of one streamed turn."""

    on_event: Callable[[AIEvent], None]
    on_error: Callable[[str], None]
    on_finished: Callable[[FinishReason], None]


class ChatSession:
    """Holds the conversation and runs one streamed turn at a time."""

    def __init__(
        self,
        *,
        client: AIClient,
        bridge: AsyncTkBridge
    ) -> None:
        """Wire the session to its client and its thread bridge."""
        self._client = client
        self._bridge = bridge
        self._messages: list[Message] = []
        self._task: TaskHandle | None = None
        self._assistant_parts: list[str] = []

    @property
    def streaming(self) -> bool:
        """Whether a turn is currently in flight."""
        return self._task is not None and self._task.running

    @property
    def messages(self) -> tuple[Message, ...]:
        """The settled conversation, oldest first."""
        return tuple(self._messages)

    def send(self, user_message: str, handlers: StreamHandlers) -> None:
        """Record ``user_message`` and start streaming the reply.

        Args:
            user_message: The user's turn. Assumed non-empty and stripped.
            handlers: Callbacks invoked on the Tk thread as the turn unfolds.
        """
        history = self._trimmed_history()
        self._messages.append(Message(role="user", content=user_message))
        self._assistant_parts = []
        self._task = self._bridge.submit(partial(self._run, history, user_message, handlers))

    def cancel(self) -> None:
        """Cancel the turn in flight, if any."""
        if self._task is not None:
            self._task.cancel()

    def reset(self) -> None:
        """Cancel any turn in flight and forget the conversation."""
        self.cancel()
        self._messages.clear()
        self._assistant_parts = []

    def _trimmed_history(self) -> tuple[Message, ...]:
        """The most recent turns, capped at the configured limit."""
        return tuple(self._messages[:])

    async def _run(
        self,
        history: tuple[Message, ...],
        user_message: str,
        handlers: StreamHandlers,
    ) -> None:
        """Consume the client's stream on the worker loop."""
        try:
            async for event in self._client.stream(history, user_message):
                self._bridge.post(partial(self._deliver, event, handlers))
        except asyncio.CancelledError:
            self._bridge.post(partial(self._finish, "cancelled", handlers))
            raise
        except Exception as exc:  # broad on purpose: any client failure becomes a UI state
            self._bridge.post(partial(handlers.on_error, _describe(exc)))
            self._bridge.post(partial(self._finish, "failed", handlers))
        else:
            self._bridge.post(partial(self._finish, "completed", handlers))

    def _deliver(self, event: AIEvent, handlers: StreamHandlers) -> None:
        """Accumulate assistant text and hand the event to the view. Tk thread."""
        if isinstance(event, TextDelta):
            self._assistant_parts.append(event.text)
        handlers.on_event(event)

    def _finish(self, reason: FinishReason, handlers: StreamHandlers) -> None:
        """Settle the assistant turn and release the session. Tk thread."""
        content = "".join(self._assistant_parts).strip()
        if content:
            self._messages.append(Message(role="assistant", content=content))
        self._assistant_parts = []
        self._task = None
        handlers.on_finished(reason)


def _describe(exc: BaseException) -> str:
    """Render an exception as a single line fit for the transcript."""
    detail = str(exc).strip()
    return f"{type(exc).__name__}: {detail}" if detail else type(exc).__name__
