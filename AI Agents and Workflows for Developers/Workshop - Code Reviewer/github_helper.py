
from dataclasses import dataclass
import json
from typing import Any
from urllib.error import HTTPError, URLError
from urllib.request import Request, urlopen


@dataclass(frozen=True, slots=True)
class PullRequest:
    title: str
    description: str
    state: str
    is_draft: bool
    is_merged: bool
    author_login: str
    base_branch: str
    head_branch: str
    created_at: str
    updated_at: str


@dataclass(frozen=True, slots=True)
class PullRequestComment:
    id: int
    body: str
    path: str | None
    commit_id: str
    line: int | None
    side: str | None


def get_pull_request(repository: str, pull_request_number: int, github_access_token: str) -> PullRequest:
    payload = _github_api_request(
        "GET",
        f"/repos/{repository}/pulls/{pull_request_number}",
        github_access_token
    )
    return _parse_pull_request_info(payload)


def post_pull_request_comment(repository: str, pull_request_number: int, body: str, github_access_token: str) -> PullRequestComment:
    comment_body = body.strip()
    if not comment_body:
        raise ValueError("Pull request comment body cannot be empty.")

    payload = _github_api_request(
        "POST",
        f"/repos/{repository}/issues/{pull_request_number}/comments",
        github_access_token,
        payload={"body": comment_body},
    )
    return _parse_pull_request_comment(payload)


def _github_api_request(
    method: str,
    path: str,
    github_access_token: str,
    payload: dict[str, Any] | None = None,
) -> Any:
    if not github_access_token:
        raise RuntimeError("GitHub API operations require GITHUB_ACCESS_TOKEN.")

    request_body = None if payload is None else json.dumps(payload).encode("utf-8")
    request = Request(
        url=f"https://api.github.com{path}",
        data=request_body,
        method=method,
        headers={
            "Accept": "application/vnd.github+json",
            "Authorization": f"Bearer {github_access_token}",
            "Content-Type": "application/json",
            "User-Agent": "hAIlpr",
            "X-GitHub-Api-Version": "2022-11-28",
        },
    )

    try:
        with urlopen(request) as response:
            charset = response.headers.get_content_charset("utf-8")
            response_body = response.read().decode(charset)
            if not response_body:
                return None

            return json.loads(response_body)
    except HTTPError as exc:
        error_body = exc.read().decode("utf-8", errors="replace")
        message = error_body
        try:
            payload = json.loads(error_body)
            message = payload.get("message", error_body)
        except json.JSONDecodeError:
            pass

        raise RuntimeError(f"GitHub API request [{method} {path}] failed with status {exc.code}: {message}") from exc
    except URLError as exc:
        raise RuntimeError(f"GitHub API request [{method} {path}] failed: {exc.reason}") from exc


def _parse_pull_request_info(payload: dict[str, Any]) -> PullRequest:
    return PullRequest(
        title=payload.get("title", ""),
        description=payload.get("body", ""),
        state=payload.get("state", ""),
        is_draft=bool(payload.get("draft")),
        is_merged=bool(payload.get("merged")),
        author_login=payload.get("user", {}).get("login", ""),
        base_branch=payload.get("base", {}).get("ref", ""),
        head_branch=payload.get("head", {}).get("ref", ""),
        created_at=payload.get("created_at", ""),
        updated_at=payload.get("updated_at", ""),
    )


def _parse_pull_request_comment(payload: dict[str, Any]) -> PullRequestComment:
    return PullRequestComment(
        id=payload["id"],
        body=payload.get("body", ""),
        path=payload.get("path"),
        commit_id=payload.get("commit_id", ""),
        line=payload.get("line"),
        side=payload.get("side"),
    )