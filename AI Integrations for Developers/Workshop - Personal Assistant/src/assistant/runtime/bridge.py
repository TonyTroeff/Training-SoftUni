"""Marshalling between Tk's main loop and an asyncio loop on a worker thread.

Tk is single-threaded and its widgets may only be touched from the thread that
created them. The bridge therefore keeps two strict lanes:

* coroutines run on a private event loop owned by a daemon thread;
* anything that touches a widget goes through :meth:`AsyncTkBridge.post`, which
  queues a callback for the Tk thread to drain on its next tick.

Nothing here blocks the UI: the drain is bounded per tick, and the worker thread
never waits on Tk.
"""

from __future__ import annotations

import asyncio
import queue
import threading
from typing import TYPE_CHECKING

if TYPE_CHECKING:
    import tkinter as tk
    from collections.abc import Awaitable, Callable
    from concurrent.futures import Future

__all__ = ["AsyncTkBridge", "TaskHandle"]

_MAX_CALLBACKS_PER_TICK = 256
"""Cap on work per drain, so a fast stream can never starve redraws."""

_SHUTDOWN_TIMEOUT_S = 2.0


class TaskHandle:
    """A coroutine running on the worker loop, cancellable from the UI thread."""

    __slots__ = ("_future",)

    def __init__(self, future: Future[None]) -> None:
        """Wrap the future returned by :func:`asyncio.run_coroutine_threadsafe`."""
        self._future = future

    @property
    def running(self) -> bool:
        """Whether the task has neither finished nor been cancelled."""
        return not self._future.done()

    def cancel(self) -> None:
        """Request cancellation.

        The coroutine sees :exc:`asyncio.CancelledError` at its next suspension
        point; this call itself does not wait for that to happen.
        """
        self._future.cancel()


class AsyncTkBridge:
    """Owns a background event loop and pumps callbacks back onto the Tk thread."""

    def __init__(self, root: tk.Misc, *, poll_interval_ms: int = 16) -> None:
        """Prepare the loop and the callback queue.

        Args:
            root: Any widget on the Tk main thread; used only for scheduling.
            poll_interval_ms: Drain cadence. 16 ms is roughly one frame.
        """
        self._root = root
        self._poll_interval_ms = poll_interval_ms
        self._loop = asyncio.new_event_loop()
        self._thread = threading.Thread(
            target=self._run_loop,
            name="assistant-asyncio",
            daemon=True,
        )
        self._pending: queue.SimpleQueue[Callable[[], None]] = queue.SimpleQueue()
        self._active = False

    def start(self) -> None:
        """Start the worker thread and schedule the first drain. Idempotent."""
        if self._active:
            return
        self._active = True
        self._thread.start()
        self._root.after(self._poll_interval_ms, self._drain)

    def submit(self, factory: Callable[[], Awaitable[None]]) -> TaskHandle:
        """Run ``factory()`` on the worker loop.

        Args:
            factory: Called on the worker thread to produce the awaitable, so
                coroutine objects are never created on the UI thread.

        Returns:
            A handle for cancelling the task.
        """

        async def _run() -> None:
            await factory()

        return TaskHandle(asyncio.run_coroutine_threadsafe(_run(), self._loop))

    def post(self, callback: Callable[[], None]) -> None:
        """Queue ``callback`` to run on the Tk thread. Safe from any thread."""
        self._pending.put(callback)

    def close(self) -> None:
        """Cancel outstanding tasks and shut the worker loop down. Idempotent."""
        if not self._active:
            return
        self._active = False
        self._loop.call_soon_threadsafe(self._cancel_all)
        self._loop.call_soon_threadsafe(self._loop.stop)
        self._thread.join(timeout=_SHUTDOWN_TIMEOUT_S)
        if not self._thread.is_alive():
            self._loop.close()

    def _run_loop(self) -> None:
        """Worker thread entry point."""
        asyncio.set_event_loop(self._loop)
        self._loop.run_forever()

    def _cancel_all(self) -> None:
        """Cancel every task on the worker loop. Runs on the worker thread."""
        for task in asyncio.all_tasks(self._loop):
            task.cancel()

    def _drain(self) -> None:
        """Run queued callbacks on the Tk thread, then reschedule."""
        for _ in range(_MAX_CALLBACKS_PER_TICK):
            try:
                callback = self._pending.get_nowait()
            except queue.Empty:
                break
            callback()
        if self._active:
            self._root.after(self._poll_interval_ms, self._drain)
