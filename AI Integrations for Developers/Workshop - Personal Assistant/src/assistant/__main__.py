"""Process entry point: build the config once, then hand it downward."""

from __future__ import annotations

import sys

from assistant.ai.factory import build_client
from assistant.config import ConfigError, load_config
from assistant.ui.app import AssistantApp, enable_dpi_awareness

__all__ = ["main"]

_CONFIG_EXIT_CODE = 2


def main() -> int:
    """Start the application.

    Returns:
        ``0`` on a clean exit, ``2`` when the configuration is unusable.
    """
    enable_dpi_awareness()
    try:
        config = load_config()
    except ConfigError as exc:
        sys.stderr.write(f"Configuration error:\n{exc}\n")
        return _CONFIG_EXIT_CODE

    app = AssistantApp(config=config, client=build_client(config))
    app.mainloop()
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
