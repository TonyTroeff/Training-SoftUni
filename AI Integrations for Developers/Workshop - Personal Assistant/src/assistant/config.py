"""Application configuration.

:func:`load_config` is the only place in the package that touches the
environment. Everything downstream receives a :class:`Config` (or the narrower
:class:`AIClientConfig`) by injection.
"""

from __future__ import annotations

from pathlib import Path

from pydantic import BaseModel, ConfigDict, Field, SecretStr, ValidationError, field_validator
from pydantic_settings import BaseSettings, SettingsConfigDict

__all__ = ["AIClientConfig", "Config", "ConfigError", "load_config"]

_DEFAULT_ENV_FILE = ".env"
_DEFAULT_DATA_DIR = Path("data")


class ConfigError(RuntimeError):
    """Raised when the environment cannot be turned into a usable config."""


class AIClientConfig(BaseModel):
    """The slice of configuration the AI seam is allowed to see.

    Deliberately narrower than :class:`Config`: an :class:`~assistant.ai.client.AIClient`
    has no business knowing the window geometry.
    """

    model_config = ConfigDict(frozen=True, protected_namespaces=())

    model_name: str
    api_base_url: str
    api_key: SecretStr
    system_prompt: str | None

    data_dir: Path


class Config(BaseSettings):
    """Everything the application needs to start, read from ``.env`` and the environment.

    Settings are prefixed with ``ASSISTANT_`` — see ``.env.example``.
    """

    model_config = SettingsConfigDict(
        env_file=_DEFAULT_ENV_FILE,
        env_file_encoding="utf-8",
        env_prefix="ASSISTANT_",
        extra="ignore",
        frozen=True,
        protected_namespaces=(),
    )

    model_name: str = Field(default="claude-opus-4-8")
    api_base_url: str = Field(default="https://api.anthropic.com/v1")
    api_key: SecretStr = Field(default=SecretStr(""))

    system_prompt_path: Path | None = Field(default=None)

    data_dir: Path = Field(default=_DEFAULT_DATA_DIR)

    window_title: str = Field(default="Personal Assistant")
    window_width: int = Field(default=920, ge=480)
    window_height: int = Field(default=760, ge=420)

    @field_validator("data_dir")
    @classmethod
    def _absolute_data_dir(cls, value: Path) -> Path:
        """Expand ``~`` and anchor a relative path to the current working directory."""
        return value.expanduser().resolve()

    def ensure_data_dir(self) -> Path:
        """Create the data directory if it is missing.

        Returns:
            The absolute path to the data directory.

        Raises:
            ConfigError: If the path exists as a non-directory, or cannot be created.
        """
        path = self.data_dir
        try:
            path.mkdir(parents=True, exist_ok=True)
        except OSError as exc:
            message = f"ASSISTANT_DATA_DIR is not usable: {path} ({exc})"
            raise ConfigError(message) from exc
        return path

    def ai_client_config(self) -> AIClientConfig:
        """Project this config down to the slice the AI client receives."""
        return AIClientConfig(
            model_name=self.model_name,
            api_base_url=self.api_base_url,
            api_key=self.api_key,
            system_prompt=self._read_system_prompt(),
            data_dir=self.data_dir
        )

    def _read_system_prompt(self) -> str | None:
        path = self.system_prompt_path
        if path is None:
            return None
        if not path.is_file():
            message = f"ASSISTANT_SYSTEM_PROMPT_PATH points at a missing file: {path}"
            raise ConfigError(message)
        return path.read_text(encoding="utf-8")


def load_config() -> Config:
    """Build the single :class:`Config` instance for this process.

    The data directory is created up front so storage failures surface at
    startup rather than on the first write.

    Returns:
        The validated configuration.

    Raises:
        ConfigError: If the environment fails validation or the data directory
            cannot be created.
    """
    try:
        config = Config()
    except ValidationError as exc:
        raise ConfigError(str(exc)) from exc
    config.ensure_data_dir()
    return config
