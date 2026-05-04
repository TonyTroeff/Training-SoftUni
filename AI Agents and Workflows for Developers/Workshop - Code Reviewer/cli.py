import os

from config import AppSettings


def apply_runtime_environment(settings: AppSettings) -> None:
    env_values = {
        "LANGSMITH_TRACING": settings.langsmith_tracing,
        "LANGSMITH_API_KEY": settings.langsmith_api_key,
        "LANGSMITH_PROJECT": settings.langsmith_project,
        "LANGSMITH_ENDPOINT": settings.langsmith_endpoint,
    }

    for key, value in env_values.items():
        if value is None:
            continue

        os.environ[key] = str(value)


def main():
    settings = AppSettings()
    apply_runtime_environment(settings)

    from workflow import create_workflow
    
    workflow = create_workflow(settings)
    final_state = workflow.invoke(
        input={
            "repository": "TryAtSoftware/CleanTests",
            "pull_request_id": 88
        },
        context={
            "github_access_token": settings.github_access_token,
        }
    )

    print("Workflow completed successfully.")
    print(final_state)


if __name__ == "__main__":
    main()
