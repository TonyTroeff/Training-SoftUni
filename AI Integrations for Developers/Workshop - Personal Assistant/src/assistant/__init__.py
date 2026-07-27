"""Desktop chat shell for a personal AI assistant.

The package is split along one seam: everything under :mod:`assistant.ui` and
:mod:`assistant.runtime` is the shell, and :class:`assistant.ai.client.AIClient`
is the model backend it talks to. The shell only ever sees typed events.
"""

from __future__ import annotations

__all__ = ["__version__"]

__version__ = "0.1.0"
