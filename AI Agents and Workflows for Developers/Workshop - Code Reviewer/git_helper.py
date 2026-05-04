from base64 import b64encode
from dataclasses import dataclass
import os

from pathlib import Path
from subprocess import run


STATUS_NAMES = {
    "A": "added",
    "D": "deleted",
    "M": "modified",
}


@dataclass(frozen=True)
class ChangedFile:
    status: str
    path: str


@dataclass(frozen=True, slots=True)
class Commit:
    sha: str
    author_name: str
    author_email: str
    authored_at: str
    subject: str


def clone_github_repository(repository: str, destination: Path, github_access_token: str) -> Path:
    repository_url = f"https://github.com/{repository}.git"
    destination = destination.expanduser().resolve()
    destination.parent.mkdir(parents=True, exist_ok=True)

    # IDEA: Use --depth=1 for shallow clone. However, it may require additional work to manage the branches.
    _run_git_command(
        ["clone", repository_url, str(destination)],
        github_access_token=github_access_token,
    )
    return destination


def get_changed_files(repository_path: Path, head_branch_name: str, base_branch_name: str, remote: str = "origin", github_access_token: str | None = None) -> list[ChangedFile]:
    diff_filter = "".join(STATUS_NAMES.keys())
    diff_output = _run_git_command(
        ["diff", f"{remote}/{base_branch_name}...{remote}/{head_branch_name}", "--minimal", "--name-status", f"--diff-filter={diff_filter}"],
        cwd=repository_path,
        github_access_token=github_access_token,
    )

    changed_files: list[ChangedFile] = []
    for line in diff_output.splitlines():
        if not line:
            continue

        parts = line.split(maxsplit=1)
        if len(parts) != 2:
            continue

        status = STATUS_NAMES.get(parts[0])
        if not status:
            continue

        changed_files.append(ChangedFile(status=status, path=parts[1]))

    return changed_files


def get_commits(repository_path: Path, head_branch_name: str, base_branch_name: str, remote: str = "origin", github_access_token: str | None = None) -> list[Commit]:
    pretty_format = "%H%x1f%an%x1f%ae%x1f%aI%x1f%s"
    log_output = _run_git_command(
        ["log", "--reverse", f"--pretty=format:{pretty_format}", f"{remote}/{base_branch_name}..{remote}/{head_branch_name}"],
        cwd=repository_path,
        github_access_token=github_access_token
    )

    commits: list[Commit] = []
    for line in log_output.splitlines():
        if not line.strip():
            continue

        sha, author_name, author_email, authored_at, subject = line.split("\x1f", maxsplit=4)
        commits.append(
            Commit(sha=sha, author_name=author_name, author_email=author_email, authored_at=authored_at, subject=subject)
        )

    return commits


def get_change(repository_path: Path, head_branch_name: str, base_branch_name: str, file_path: str, remote: str = "origin", github_access_token: str | None = None) -> str:
    change_output = _run_git_command(
        ["diff", f"{remote}/{base_branch_name}...{remote}/{head_branch_name}", "--minimal", "--unified=0", "--", file_path],
        cwd=repository_path,
        github_access_token=github_access_token,
    )
    return change_output


def _run_git_command(
    command: list[str],
    cwd: Path | None = None,
    github_access_token: str | None = None
) -> str:
    git_command = ["git"]
    if github_access_token:
        encoded_credentials = b64encode(f"x-access-token:{github_access_token}".encode("utf-8")).decode("ascii")
        git_command.extend(["-c", f"http.extraheader=AUTHORIZATION: basic {encoded_credentials}"])

    process_env = os.environ.copy()
    process_env["GIT_TERMINAL_PROMPT"] = "0"

    process = run(
        [*git_command, *command],
        cwd=str(cwd) if cwd is not None else None,
        capture_output=True,
        text=True,
        check=False,
        env=process_env,
    )

    if process.returncode != 0:
        command_text = ' '.join(['git', '[...]', *command])
        raise RuntimeError(f"Git command `{command_text}` failed with exit code {process.returncode}:\n{process.stderr.strip()}")

    return process.stdout