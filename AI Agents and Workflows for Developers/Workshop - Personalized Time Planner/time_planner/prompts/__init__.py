"""System and user prompt templates."""

from time_planner.prompts.calendar import CALENDAR_SYSTEM_PROMPT
from time_planner.prompts.main import MAIN_SYSTEM_PROMPT
from time_planner.prompts.preferences import PREFERENCES_SYSTEM_PROMPT
from time_planner.prompts.settings import SETTINGS_SYSTEM_PROMPT
from time_planner.prompts.user import USER_PROMPT_TEMPLATE, build_user_prompt

__all__ = [
    "CALENDAR_SYSTEM_PROMPT",
    "MAIN_SYSTEM_PROMPT",
    "PREFERENCES_SYSTEM_PROMPT",
    "SETTINGS_SYSTEM_PROMPT",
    "USER_PROMPT_TEMPLATE",
    "build_user_prompt",
]
