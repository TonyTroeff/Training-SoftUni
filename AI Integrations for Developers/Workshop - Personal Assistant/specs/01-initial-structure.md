# Task

Build the foundation of a desktop chat application in Python + tkinter.
The UI and the plumbing are yours; the AI agent behind it is mine — you stub it.

# Tech constraints

- Package management: `uv` with `pyproject.toml` (PEP 621 metadata, locked via `uv.lock`). No `requirements.txt`, no `setup.py`.
- Configuration: `pydantic-settings` (`BaseSettings`) loading from `.env`.
- Tooling configured in `pyproject.toml`: `ruff` (lint + format), `mypy` in strict mode. The code must pass both with zero findings.
- Full type annotations. No `Any` in public signatures.
- Standard library `tkinter`/`ttk` only for the UI — no CustomTkinter, no PyQt.

# Configuration

A single `Config` model, instantiated once at startup and injected downward.
Never read `os.environ` outside it.
Include at least: model name, API base URL, API key (`SecretStr`).
Ship a committed `.env.example`; keep `.env` gitignored.

# AIClient contract

This is the seam I will implement later, so the contract matters more than the implementation.

- One public method. It takes the conversation history along with the current user message and returns an **async iterator of typed events**.
- Events are a discriminated union — at minimum: text delta and tool call. Model them as pydantic models or frozen dataclasses with a literal `type` discriminator.
- `AIClient.__init__` receives the config (or a narrowed slice of it) — no global lookups.
- Define the `AICLient` class without actually implementing it — this is my job.

# UI requirements

- Streaming a long response must never block or stutter the UI.
- Message list at the top, growing top → bottom, in a scrollable region that reflows on window resize and supports selectable/copyable text.
- Visual distinction between user and assistant turns (alignment, background, or both). Assistant text appears incrementally as deltas arrive.
- Auto-scroll to the newest message — but only when the user is already at the bottom; do not yank the viewport if they've scrolled up to read.
- Multi-line input at the bottom: `Enter` sends, `Shift+Enter` inserts a newline, the box grows to a few lines then scrolls.
- Send is disabled and a stop/cancel affordance is available while streaming.
- Errors surface in the UI as a distinct message state, not a traceback in the console or a modal popup.
- Keep spacing, font sizing, and colors deliberate — it should look considered, not like a default ttk demo.
