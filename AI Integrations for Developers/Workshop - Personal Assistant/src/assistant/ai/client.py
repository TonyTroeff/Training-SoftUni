"""The seam between the UI shell and whatever actually talks to a model.

This module is intentionally a contract and nothing else. The shell depends on
the signature of :meth:`AIClient.stream` and on the event union in
:mod:`assistant.ai.events`; it depends on no transport, SDK or agent loop.
"""

from __future__ import annotations

import json
from dataclasses import dataclass
from datetime import UTC, datetime
from pathlib import Path
from typing import TYPE_CHECKING, Any

from openai import OpenAI
from openai.types.responses import (
    ResponseFunctionToolCall,
    ResponseTextDeltaEvent,
)
from pydantic import BaseModel, Field, ValidationError

from assistant.ai.events import TextDelta, ToolCall, ToolResult

if TYPE_CHECKING:
    from collections.abc import AsyncIterator, Callable, Sequence

    from openai.types.responses import Response

    from assistant.ai.events import AIEvent
    from assistant.ai.messages import Message
    from assistant.config import AIClientConfig

__all__ = ["AIClient"]

_SCHEMA_FILENAME = "SCHEMA.md"
_ENTRY_SUFFIX = ".md"


class ToolOutcome(BaseModel):
    """What a tool handler returns: a payload plus whether it represents failure.

    ``content`` is the JSON string fed back to the model as the tool's output;
    ``is_error`` lets the caller flag the turn without re-parsing that string.
    """

    is_error: bool
    content: str


class GetHierarchiesArgs(BaseModel):
    pass


class CreateHierarchyArgs(BaseModel):
    name: str
    description: str
    additional_attributes: list[str] = Field(
        description='Attribute definitions as "name: description" strings, '
        'e.g. "role: how the person relates to the user".',
    )


class GetEntriesArgs(BaseModel):
    hierarchy: str


class GetEntryDetailsArgs(BaseModel):
    hierarchy: str
    name: str


class CreateEntryArgs(BaseModel):
    hierarchy: str
    name: str
    attributes: list[str] = Field(
        description='Frontmatter fields as "key: value" strings, '
        'e.g. "title: Jane Smith". One field per string.',
    )
    body: str | None


def _ok(payload: object) -> ToolOutcome:
    """Wrap a successful result payload as a non-error :class:`ToolOutcome`."""
    return ToolOutcome(is_error=False, content=json.dumps(payload))


def _err(message: str) -> ToolOutcome:
    """Wrap a failure ``message`` as an error :class:`ToolOutcome`."""
    return ToolOutcome(is_error=True, content=json.dumps({"error": message}))


def get_hierarchies(raw_args: str, config: AIClientConfig) -> ToolOutcome:
    GetHierarchiesArgs.model_validate(json.loads(raw_args))

    hierarchies = [
        {"name": directory.name, "description": _read_schema(directory)}
        for directory in _sorted_children(config.data_dir)
        if directory.is_dir()
    ]
    return _ok(hierarchies)


def create_hierarchy(raw_args: str, config: AIClientConfig) -> ToolOutcome:
    args = CreateHierarchyArgs.model_validate(json.loads(raw_args))

    directory = _hierarchy_dir(config, args.name)
    if directory is None:
        return _err(f"{args.name!r} is not a valid hierarchy name.")
    if directory.exists():
        return _err(f"A hierarchy named {directory.name!r} already exists.")

    directory.mkdir(parents=True)
    schema = _render_schema(args.description, args.additional_attributes)
    (directory / _SCHEMA_FILENAME).write_text(schema, encoding="utf-8")
    return _ok({"status": "created"})


def get_entries(raw_args: str, config: AIClientConfig) -> ToolOutcome:
    args = GetEntriesArgs.model_validate(json.loads(raw_args))

    directory = _hierarchy_dir(config, args.hierarchy)
    if directory is None or not directory.is_dir():
        return _err(f"There is no hierarchy named {args.hierarchy!r}.")

    entries = [
        {"name": path.name}
        for path in _sorted_children(directory)
        if path.is_file() and path.suffix == _ENTRY_SUFFIX and path.name != _SCHEMA_FILENAME
    ]
    return _ok(entries)


def get_entry_details(raw_args: str, config: AIClientConfig) -> ToolOutcome:
    args = GetEntryDetailsArgs.model_validate(json.loads(raw_args))

    directory = _hierarchy_dir(config, args.hierarchy)
    if directory is None or not directory.is_dir():
        return _err(f"There is no hierarchy named {args.hierarchy!r}.")

    filename = _safe_segment(args.name)
    if filename is None:
        return _err(f"{args.name!r} is not a valid entry name.")
    if not filename.endswith(_ENTRY_SUFFIX):
        filename += _ENTRY_SUFFIX

    path = directory / filename
    if filename == _SCHEMA_FILENAME or not path.is_file():
        return _err(f"There is no entry named {filename!r} in {directory.name!r}.")

    return _ok({"content": path.read_text(encoding="utf-8")})


def create_entry(raw_args: str, config: AIClientConfig) -> ToolOutcome:
    args = CreateEntryArgs.model_validate(json.loads(raw_args))

    directory = _hierarchy_dir(config, args.hierarchy)
    if directory is None or not directory.is_dir():
        return _err(f"There is no hierarchy named {args.hierarchy!r}.")

    filename = _safe_segment(args.name)
    if filename is None:
        return _err(f"{args.name!r} is not a valid entry name.")
    if not filename.endswith(_ENTRY_SUFFIX):
        filename += _ENTRY_SUFFIX
    if filename == _SCHEMA_FILENAME:
        return _err(f"{_SCHEMA_FILENAME!r} is reserved for the schema.")

    path = directory / filename
    if path.exists():
        return _err(f"An entry named {filename!r} already exists.")

    # ``recorded_at`` is ours to stamp, not the model's — drop any it supplied
    # and record the real capture time.
    attributes = _strip_attribute(args.attributes, "recorded_at")
    attributes.append(f"recorded_at: {datetime.now(UTC).isoformat()}")

    path.write_text(_render_entry(attributes, args.body), encoding="utf-8")

    return _ok({"status": "created"})


def _sorted_children(directory: Path) -> list[Path]:
    """Return the directory's children in name order, or nothing if it is absent."""
    if not directory.is_dir():
        return []
    return sorted(directory.iterdir(), key=lambda path: path.name)


def _read_schema(hierarchy_dir: Path) -> str:
    """Return the text of the hierarchy's ``SCHEMA.md``, or ``""`` when it has none."""
    schema = hierarchy_dir / _SCHEMA_FILENAME
    if not schema.is_file():
        return ""
    return schema.read_text(encoding="utf-8").strip()


def _render_schema(description: str, attributes: list[str]) -> str:
    """Render a hierarchy's ``SCHEMA.md``: its description and attribute notes."""
    lines = [description.strip(), ""]
    notes = [attr.strip() for attr in attributes if attr.strip()]
    if notes:
        lines.append("## Attributes")
        lines.append("")
        lines += [f"- {note}" for note in notes]
        lines.append("")
    return "\n".join(lines).rstrip() + "\n"


def _strip_attribute(attributes: list[str], key: str) -> list[str]:
    """Return ``attributes`` without any ``"key: ..."`` whose key matches ``key``.

    Case-insensitive, so a model that writes ``Recorded_At`` is still overridden.
    """
    target = key.strip().lower()
    return [a for a in attributes if a.partition(":")[0].strip().lower() != target]


def _render_entry(attributes: list[str], body: str | None) -> str:
    lines = ["---"]
    for attribute in attributes:
        key, _, value = attribute.partition(":")
        key = key.strip()
        if key:
            lines.append(f"{key}: {json.dumps(value.strip())}")
    lines.append("---")

    if body:
        lines.append("")
        lines.append(body)

    return "\n".join(lines)


def _hierarchy_dir(config: AIClientConfig, hierarchy: str) -> Path | None:
    """Resolve a model-supplied hierarchy name to a directory inside ``data_dir``.

    Returns ``None`` for anything that is not a plain single path segment, so a
    name like ``../..`` cannot walk out of the data directory.
    """
    name = _safe_segment(hierarchy)
    if name is None:
        return None
    return config.data_dir / name


def _safe_segment(value: str) -> str | None:
    """Return ``value`` stripped if it is a single, non-traversing path segment.

    Returns ``None`` for anything that would escape its parent directory — empty,
    ``.``/``..``, or a value containing a path separator.
    """
    name = value.strip()
    if not name or name in {".", ".."} or name != Path(name).name:
        return None
    return name


def _extract_tool_calls(response: Response) -> list[ResponseFunctionToolCall]:
    """Return the function-call items the model emitted in ``response``."""
    return [item for item in response.output if isinstance(item, ResponseFunctionToolCall)]


@dataclass(frozen=True)
class _Tool:
    """A registered tool: its model-facing description, args model, and handler."""

    description: str
    args_type: type[BaseModel]
    handler: Callable[[str, AIClientConfig], ToolOutcome]


TOOLS_REGISTER: dict[str, _Tool] = {
    "get_hierarchies": _Tool("Retrieve all hierarchies.", GetHierarchiesArgs, get_hierarchies),
    "create_hierarchy": _Tool("Create a new hierarchy.", CreateHierarchyArgs, create_hierarchy),
    "get_entries": _Tool(
        "Retrieve all entries for a given hierarchy.", GetEntriesArgs, get_entries
    ),
    "get_entry_details": _Tool(
        "Retrieve the full Markdown content of a single entry.",
        GetEntryDetailsArgs,
        get_entry_details,
    ),
    "create_entry": _Tool(
        "Create a new entry within a given hierarchy.", CreateEntryArgs, create_entry
    ),
}


def _strictify(schema: dict[str, Any]) -> dict[str, Any]:
    """Require ``additionalProperties: false`` on every object node, recursively.

    OpenAI strict function-calling rejects the call unless every object — the
    top-level parameters *and* each nested ``$defs`` definition — forbids extra
    properties. Mutates ``schema`` in place and returns it for convenience.
    """
    if schema.get("type") == "object":
        schema["additionalProperties"] = False
    nested = (*schema.get("$defs", {}).values(), *schema.get("properties", {}).values())
    for child in nested:
        if isinstance(child, dict):
            _strictify(child)
    items = schema.get("items")
    if isinstance(items, dict):
        _strictify(items)
    return schema


AI_TOOLS: list[Any] = [
    {
        "type": "function",
        "name": name,
        "description": tool.description,
        "parameters": _strictify(tool.args_type.model_json_schema()),
        "strict": True,
    }
    for name, tool in TOOLS_REGISTER.items()
]


class AIClient:
    """Turns a user message plus history into a stream of typed events.

    The implementation is left out on purpose — this is the hand-off point for
    the agent itself. To fill it in, replace the body of :meth:`stream` with an
    async generator::

        async def stream(
            self, history: Sequence[Message], user_message: str
        ) -> AsyncIterator[AIEvent]:
            yield TextDelta(text="...")

    Contract the shell relies on:

    * ``stream`` is called, not awaited, and the result is consumed with
      ``async for``.
    * ``history`` excludes ``user_message`` and is already trimmed to
      ``config.max_history_messages``.
    * Cancellation arrives as :exc:`asyncio.CancelledError` at the current
      ``await``; let it propagate after cleaning up.
    * Any other exception is caught by the shell and rendered as an error
      message, so raising is a legitimate way to report failure.
    * The stream runs on a worker event loop, never on the UI thread.
    """

    def __init__(self, config: AIClientConfig) -> None:
        """Store the configuration slice this client is allowed to use.

        Args:
            config: Model name, endpoint, credentials and limits. There are no
                global lookups; this is the client's whole world.
        """
        self._config = config
        self._client = OpenAI(
            api_key=config.api_key.get_secret_value(),
            base_url=config.api_base_url,
        )

    def _run_tool(self, name: str, raw_args: str) -> ToolOutcome:
        """Run tool ``name`` with the model's raw JSON ``raw_args``.

        Malformed arguments — unparseable JSON or a shape the handler's model
        rejects — are turned into an error :class:`ToolOutcome` rather than being
        raised, so the model sees the failure as a tool result and can retry
        instead of the whole turn collapsing.
        """
        tool = TOOLS_REGISTER.get(name)
        if tool is None:
            return _err(f"There is no tool named {name!r}.")
        try:
            return tool.handler(raw_args, self._config)
        except (json.JSONDecodeError, ValidationError) as exc:
            return _err(f"Invalid arguments for {name}: {exc}")

    async def stream(self, history: Sequence[Message], user_message: str) -> AsyncIterator[AIEvent]:
        """Stream the assistant's reply to ``user_message``.

        Args:
            history: Prior turns, oldest first, excluding ``user_message``.
            user_message: The turn being answered.

        Yields:
            Events from :data:`~assistant.ai.events.AIEvent`.
        """
        context: list[Any] = [{"role": "system", "content": self._config.system_prompt}]
        context += [{"role": item.role, "content": item.content} for item in history]
        context.append({"role": "user", "content": user_message})

        while True:
            # ``responses.stream`` is the SDK's streaming helper: a context
            # manager whose events arrive incrementally as the model produces
            # them. ``responses.create(stream=True)`` buffers instead, defeating
            # the whole point of a live transcript.
            with self._client.responses.stream(
                model=self._config.model_name,
                input=context,
                tools=AI_TOOLS,
            ) as events:
                for event in events:
                    if isinstance(event, ResponseTextDeltaEvent):
                        yield TextDelta(text=event.delta)
                final: Response = events.get_final_response()

            tool_calls = _extract_tool_calls(final)
            if not tool_calls:
                return

            # Feed the model's own turn (the function_call items) back before the
            # outputs, then run each tool and append its result.
            context += final.output
            for call in tool_calls:
                yield ToolCall(name=call.name, arguments=call.arguments)

                outcome = self._run_tool(call.name, call.arguments)
                yield ToolResult(name=call.name, content=outcome.content, is_error=outcome.is_error)
                context.append(
                    {
                        "type": "function_call_output",
                        "call_id": call.call_id,
                        "output": outcome.content,
                    }
                )
