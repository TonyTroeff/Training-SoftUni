"""The interactive CLI: a prompt that takes free text or slash commands."""

from __future__ import annotations

import json
import traceback
from typing import Any

from langchain_core.messages import AIMessage, RemoveMessage
from langgraph.checkpoint.sqlite import SqliteSaver
from langgraph.graph.message import REMOVE_ALL_MESSAGES
from langgraph.types import Command
from rich.console import Console
from rich.panel import Panel
from rich.text import Text

from time_planner import memory
from time_planner.clock import next_day, now_in, previous_day, week_bounds
from time_planner.config import CHECKPOINT_DB, ensure_data_dirs, thread_id_for
from time_planner.context import PlannerContext
from time_planner.graph import build_graph
from time_planner.models import get_model
from time_planner.observability import enable_tracing
from time_planner.rendering import render_day, render_week
from time_planner.routing import DELEGATES
from time_planner.settings import validate_settings

#: Ceiling on model/tool steps in one turn, so a confused agent cannot spin.
RECURSION_LIMIT = 25

APPROVALS = {"y", "yes", "ok", "okay", "approve", "approved", "do it", "go ahead"}

#: Single-day commands, and the day each one asks about relative to today.
DAY_COMMANDS = {"/yesterday": previous_day, "/today": lambda day: day, "/tomorrow": next_day}

#: Commands answered from memory, without running the graph.
COMMANDS = (*DAY_COMMANDS, "/this-week")

HELP = """\
[bold]Commands[/bold]
  [cyan]/today[/cyan]        the agenda for today
  [cyan]/yesterday[/cyan]    the agenda for yesterday
  [cyan]/tomorrow[/cyan]     the agenda for tomorrow
  [cyan]/this-week[/cyan]    the whole current week, as a table (from Monday)
  [cyan]/help[/cyan]         this message
  [cyan]/exit[/cyan]         quit (Ctrl-D also works)

Every entry is printed with its priority: [bold red]P1[/bold red] high, P2 normal, P3 low.

Anything else is a prompt, e.g. [dim]"move my standup to 10:00"[/dim]."""


def run() -> None:
    ensure_data_dirs()
    console = Console()
    tracing_project = enable_tracing()

    # Built once and passed into every invocation; LangGraph hands it to every
    # node, every agent and every tool.
    context = PlannerContext()

    with SqliteSaver.from_conn_string(str(CHECKPOINT_DB)) as checkpointer:
        graph, model_error = _build(checkpointer)
        config = {
            "configurable": {"thread_id": thread_id_for(context.user_id)},
            "recursion_limit": RECURSION_LIMIT,
        }

        _greet(console, context, tracing_project, model_error)
        if graph is not None:
            _show_interrupts(console, graph, config, title="Unfinished request")

        while True:
            try:
                raw = console.input("\n[bold cyan]>[/bold cyan] ").strip()
            except (EOFError, KeyboardInterrupt):
                console.print("\n[dim]Bye.[/dim]")
                return
            if not raw:
                continue
            if raw in ("/exit", "/quit"):
                console.print("[dim]Bye.[/dim]")
                return
            if raw == "/help":
                console.print(HELP)
                continue
            if raw in COMMANDS:
                # A command never runs the graph: there is nothing to reason
                # about, so we read memory and draw the answer ourselves.
                _run_command(console, context, raw)
                continue
            if graph is None:
                console.print(f"[red]No model available:[/red] {model_error}")
                continue
            _prompt_turn(console, graph, config, context, raw)


def _build(checkpointer) -> tuple[Any | None, str | None]:
    """Compile the graph, or explain why we could not.

    The workers are `create_agent` subgraphs, so building the graph builds a
    chat model. Without an API key that fails - and the slash commands should
    still work, so the failure is reported rather than raised.
    """
    try:
        get_model()  # fail fast and once, rather than mid-turn in every worker
        return build_graph(checkpointer=checkpointer), None
    except Exception as error:
        return None, f"{type(error).__name__}: {error}"


def _greet(
    console: Console,
    context: PlannerContext,
    tracing_project: str | None,
    model_error: str | None,
) -> None:
    today = now_in(context.timezone).date()
    console.print(
        Panel(
            f"[bold]Personalized Time Planner[/bold]\n"
            f"[dim]{today.strftime('%A, %d %B %Y')} - user {context.user_id}[/dim]"
            f"\n\n{HELP}",
            border_style="cyan",
        )
    )
    if tracing_project:
        console.print(f"[dim]LangSmith tracing on, project '{tracing_project}'.[/dim]")
    if model_error:
        console.print(
            f"[yellow]Prompts are unavailable ({model_error}).[/yellow] "
            "[dim]Commands still work.[/dim]"
        )


# -- commands ------------------------------------------------------------


def _run_command(console: Console, context: PlannerContext, command: str) -> None:
    """Answer the day and week commands straight from long-term memory."""
    settings = validate_settings(memory.load_raw_settings(context.user_id))
    today = now_in(context.timezone).date()

    if command == "/this-week":
        start, end = week_bounds(today)
        events = memory.load_events_range(context.user_id, start, end)
        render_week(console, events, start, end, settings, today=today)
        return

    day = DAY_COMMANDS[command](today)
    events = memory.load_events_range(context.user_id, day, day)
    render_day(console, events, day, settings)


# -- prompts -------------------------------------------------------------


def _prompt_turn(
    console: Console, graph, config: dict, context: PlannerContext, raw: str
) -> None:
    pending = graph.get_state(config).interrupts
    if pending:
        # A worker is parked on a question; this input is the answer it waited
        # for, and it continues from that exact step.
        payload: Any = _resume(pending, raw)
    else:
        payload = {
            "user_input": raw,
            # Each turn starts the worker conversations from scratch.
            **{channel: [RemoveMessage(id=REMOVE_ALL_MESSAGES)] for channel in DELEGATES},
        }

    try:
        with console.status("[dim]thinking...[/dim]"):
            result = graph.invoke(payload, config, context=context)
    except Exception:
        console.print(
            Panel(traceback.format_exc().strip(), title="Error", border_style="red")
        )
        return

    _show_answers(console, result)
    _show_interrupts(console, graph, config, title="Your answer?")


def _resume(pending, raw: str) -> Command:
    """Turn what the user typed into the resume value the worker expects.

    A tool-approval interrupt from `HumanInTheLoopMiddleware` wants one decision
    per proposed call; a plain `interrupt()` just wants the text.
    """
    requests = _action_requests(pending[0].value)
    if requests is None:
        return Command(resume=raw)
    if raw.strip().lower() in APPROVALS:
        decision: dict[str, Any] = {"type": "approve"}
    else:
        decision = {"type": "reject", "feedback": raw}
    return Command(resume={"decisions": [decision] * len(requests)})


def _action_requests(value: Any) -> list[dict[str, Any]] | None:
    """The proposed tool calls in a HITL interrupt, or None if it is not one."""
    if isinstance(value, dict) and isinstance(value.get("action_requests"), list):
        return value["action_requests"]
    return None


def _show_answers(console: Console, state: dict) -> None:
    """Print what each worker said last."""
    spoke = False
    for channel in DELEGATES:
        answer = _last_answer(state.get(channel) or [])
        if not answer:
            continue
        spoke = True
        console.print(Text(f"{channel}: ", style="dim").append(answer, style=""))
    if not spoke:
        console.print("[dim]No answer.[/dim]")


def _last_answer(messages: list) -> str:
    for message in reversed(messages):
        if isinstance(message, AIMessage) and message.text.strip():
            return message.text.strip()
    return ""


def _show_interrupts(console: Console, graph, config: dict, *, title: str) -> None:
    for pending in graph.get_state(config).interrupts:
        console.print(
            Panel(_describe(pending.value), title=title, border_style="yellow")
        )


def _describe(value: Any) -> Text:
    """Render a pending question - approval requests get their own layout."""
    requests = _action_requests(value)
    if requests is None:
        return Text(str(value))

    body = Text()
    for index, request in enumerate(requests):
        if index:
            body.append("\n")
        description = (request.get("description") or "").strip()
        if description:
            # The middleware's description already spells the call out in full.
            body.append(description, style="bold")
        else:
            body.append(f"Approve this call?\n  {request.get('name', '?')}(", style="bold")
            body.append(json.dumps(request.get("args", {}), ensure_ascii=False), style="dim")
            body.append(")", style="bold")
    body.append("\n\nReply ", style="dim")
    body.append("yes", style="bold green")
    body.append(" to approve, or say why not to reject.", style="dim")
    return body
