# Personal Assistant

A desktop chat window in Python + tkinter, built around one seam: the UI shell is
finished, the agent behind it is not.

## Running it

```bash
uv sync --all-groups
```

```bash
uv run personal-assistant
```

With no `ASSISTANT_API_KEY` set the app runs against `ScriptedAIClient`, a
stand-in that streams canned replies so the shell is usable before the agent
exists. It understands `/long`, `/tool`, `/error` and `/crash` — one per UI state
worth seeing.

## Configuration

Copy `.env.example` to `.env` and fill it in. `.env` is gitignored.

`assistant.config.load_config()` is the only code in the package that reads the
environment; everything else receives a `Config` by injection, and
`AIClient` receives only the narrower `AIClientConfig` slice.

`ASSISTANT_DATA_DIR` (default `./data`) is where every hierarchy and entry
lives. Relative paths resolve against the working directory, `~` is expanded,
and `load_config()` creates the directory up front so a bad path fails at
startup instead of on the first write.

## The seam

`src/assistant/ai/client.py` defines `AIClient` and deliberately does not
implement it. Replace the body of `stream` with an async generator:

```python
async def stream(
    self, history: Sequence[Message], user_message: str
) -> AsyncIterator[AIEvent]:
    yield TextDelta(text="...")
```

What the shell relies on:

- `stream` is called, not awaited, and consumed with `async for`.
- `history` excludes `user_message` and is already trimmed to
  `config.max_history_messages`.
- Cancellation arrives as `asyncio.CancelledError`; let it propagate.
- Any other exception is caught and rendered as an error turn, so raising is a
  legitimate way to report failure.
- The stream runs on a worker event loop, never on the UI thread.

Events are a frozen discriminated union in `ai/events.py` — `TextDelta`,
`ToolCall`, `ToolResult`, `StreamError` — each tagged with a literal `type`.

### Threading

Two lanes, no exceptions. Coroutines run on a private event loop owned by a
daemon thread. Anything that touches a widget is queued through
`AsyncTkBridge.post` and drained by Tk on its own thread, at most 256 callbacks
per 16 ms tick so a fast stream can never starve redraws.

## Checks

```bash
uv run ruff check . && uv run ruff format --check . && uv run mypy
```
