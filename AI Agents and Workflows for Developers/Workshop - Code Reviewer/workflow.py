import operator
import os
from pathlib import Path
import shutil
import stat
from typing import Annotated, List, TypedDict

from langchain.agents import create_agent
from langchain.messages import HumanMessage, SystemMessage
from langchain.tools import tool
from langchain_openai import ChatOpenAI
from langgraph.graph import END, START, StateGraph
from langgraph.graph.state import CompiledStateGraph, Send
from langgraph.runtime import Runtime
from pydantic import BaseModel, Field

from config import AppSettings
from git_helper import ChangedFile, Commit, clone_github_repository, get_change, get_changed_files, get_commits
from github_helper import PullRequest, get_pull_request, post_pull_request_comment



class WorkflowState(TypedDict):
    repository: str
    pull_request_id: int

    working_directory: Path
    pull_request: PullRequest
    commits: List[Commit]
    changed_files: List[ChangedFile]

    delegation_matrix: List[List[str]]
    analysis_results: Annotated[List[str], operator.add]


class WorkflowContext(TypedDict):
    github_access_token: str


class DelegationMatrix(BaseModel):
    groups: List[List[str]] = Field(
        description="A matrix where each inner list contains the file paths assigned to one review group."
    )


class AnalyzeCodeTask(TypedDict):
    group: List[str]
    working_directory: Path
    pull_request: PullRequest
    commits: List[Commit]


def create_workflow(settings: AppSettings) -> CompiledStateGraph[WorkflowState, WorkflowContext]:
    split_model = ChatOpenAI(model=settings.openai_model, api_key=settings.openai_api_key, base_url=settings.openai_base_url).with_structured_output(DelegationMatrix)
    analyze_model = ChatOpenAI(model=settings.openai_model, api_key=settings.openai_api_key, base_url=settings.openai_base_url, reasoning_effort="high")
    aggregate_model = ChatOpenAI(model=settings.openai_model, api_key=settings.openai_api_key, base_url=settings.openai_base_url, reasoning_effort="low")

    graph_builder = StateGraph(WorkflowState, WorkflowContext)
    graph_builder.add_node("load_pull_request", _load_pull_request)
    graph_builder.add_node("clone", _clone_repository)
    graph_builder.add_node("get_commits", _get_commits)
    graph_builder.add_node("get_changed_files", _get_changed_files)
    graph_builder.add_node("split_work", _create_split_work_node(split_model))
    graph_builder.add_node("analyze", _create_analyze_node(analyze_model))
    graph_builder.add_node("write_comments", _create_write_comments_node(aggregate_model))

    graph_builder.add_edge(START, "load_pull_request")
    graph_builder.add_edge("load_pull_request", "clone")
    graph_builder.add_edge("clone", "get_commits")
    graph_builder.add_edge("get_commits", "get_changed_files")
    graph_builder.add_edge("get_changed_files", "split_work")
    graph_builder.add_conditional_edges("split_work", _delegate_work, ["analyze", END])
    graph_builder.add_edge("analyze", "write_comments")
    graph_builder.add_edge("write_comments", END)

    return graph_builder.compile()


def _load_pull_request(state: WorkflowState, runtime: Runtime[WorkflowContext]):
    print(f"Loading pull request #{state['pull_request_id']} from repository {state['repository']}...")
    pull_request = get_pull_request(state["repository"], state["pull_request_id"], runtime.context["github_access_token"])

    return { "pull_request": pull_request }


def _clone_repository(state: WorkflowState, runtime: Runtime[WorkflowContext]):
    destination = Path(f"./work/{state['pull_request_id']}")
    if destination.exists():
        print("Cleaning up existing repository clone...")
        shutil.rmtree(destination, onerror=_handle_remove_readonly)

    print(f"Cloning repository {state['repository']} for pull request #{state['pull_request_id']}...")
    working_directory = clone_github_repository(state["repository"], destination, runtime.context["github_access_token"])
    print("Repository cloned successfully.")

    return { "working_directory": working_directory }

def _get_commits(state: WorkflowState, runtime: Runtime[WorkflowContext]):
    print(f"Getting commits...")
    commits = get_commits(state["working_directory"], state["pull_request"].head_branch, state["pull_request"].base_branch, github_access_token=runtime.context["github_access_token"])

    return { "commits": commits }


def _get_changed_files(state: WorkflowState, runtime: Runtime[WorkflowContext]):
    print(f"Getting changed files...")
    changed_files = get_changed_files(state["working_directory"], state["pull_request"].head_branch, state["pull_request"].base_branch, github_access_token=runtime.context["github_access_token"])

    return { "changed_files": changed_files }


def _create_split_work_node(model: ChatOpenAI):
    def _split_work(state: WorkflowState):
        changed_files = state["changed_files"]
        if not changed_files:
            return { "delegation_matrix": [] }

        system_prompt = (
            "You are an expert code reviewer. Split changed files into meaningful review groups for parallel analysis. "
            "Every changed file must appear in exactly one group. Prefer multiple groups when the files naturally separate. "
        )
        user_prompt = (
            "Return a delegation matrix for these changed files. Each row is one review group and must contain only file paths from the provided list.\n\n"
            f"Pull request context:\n{_format_pull_request_for_prompt(state['pull_request'])}\n\n"
            f"Commits in this pull request:\n{_format_commits_for_prompt(state['commits'])}\n\n"
            f"Changed files:\n{_format_changed_files_for_prompt(changed_files)}\n\n"
            "Output requirements:\n"
            "- Use all files exactly once.\n"
            "- Prefer several cohesive groups to enable parallel review.\n"
            "- Use the pull request description and commit history to infer intent and group related files together.\n"
            "- Group by review cohesion such as feature area, layer, tests, configuration, or docs when that makes sense."
        )

        response = model.invoke(
            input=[
                SystemMessage(system_prompt),
                HumanMessage(user_prompt),
            ]
        )

        return { "delegation_matrix": response.groups }
    
    return _split_work


def _create_analyze_node(model: ChatOpenAI):
    system_prompt = """You are an expert code reviewer assigned to a subset of a pull request.

You must only report issues that are directly tied to the assigned changed files.

Review rules:
- You are responsible for every assigned file, not just the most interesting ones.
- Treat the assigned file list as a checklist and inspect the diff for every assigned file before returning.
- Focus on correctness, security, behavior changes, missing validation, broken assumptions, regression risk, and missing tests.
- Ignore style nits and low-signal commentary.
- Only report issues you can justify from the code.
- When possible, anchor each finding to a changed file and line number.
- Your final response must always contain exactly two sections: Findings and Notes.
- In Findings, list only actionable issues tied to the assigned changed files.
- For each finding, use this structure when possible: title, file, line, comment.
- In Notes, write plain text only. Use it for brief caveats, missing-test concerns, or important follow-up questions.
- Do not repeat the group name, the assigned file list, or any other wrapper metadata in the response.
- If there are no meaningful issues, say so explicitly in Findings.
- If there are no notes, say "None" in Notes.
"""

    def _analyze(task: AnalyzeCodeTask, runtime: Runtime[WorkflowContext]):
        assigned_files = "\n".join(task["group"])

        user_prompt = (
            f"Pull request context:\n{_format_pull_request_for_prompt(task['pull_request'])}\n\n"
            f"Commits in this pull request:\n{_format_commits_for_prompt(task['commits'])}\n\n"
            f"Assigned changed files (you must consider all of them before returning):\n{assigned_files}\n\n"
            "Use the tools to inspect the diffs and nearby code. Check every assigned file at least once, even if some files do not produce findings."
            "Only report actionable findings tied to these files, and return only the Findings and Notes sections."
        )

        @tool
        def git_diff_file(file_path: str) -> str:
            """Return the unified diff for a single file vs the base ref with line coordinates.

            Args:
                file_path: path to the file relative to repo root.
            """
            return get_change(task["working_directory"], task["pull_request"].head_branch, task["pull_request"].base_branch, file_path, github_access_token=runtime.context["github_access_token"])

        worker_agent = create_agent(
            model=model,
            tools=[git_diff_file],
            system_prompt=system_prompt,
        )

        response = worker_agent.invoke(
            input={
                "messages": [HumanMessage(user_prompt)]
            },
            config={
                # NOTE: If the PR is very large and the review group consists of many files, this recursion limit may have to be modified.
                "recursion_limit": 100
            }
        )

        return { "analysis_results": [response["messages"][-1].content] }
    
    return _analyze


def _format_pull_request_for_prompt(pull_request: PullRequest) -> str:
    return (
        f"Title: {pull_request.title}\n"
        f"Description:\n{pull_request.description}"
    )


def _format_commits_for_prompt(commits: list[Commit]) -> str:
    return "\n".join(f"- {commit.sha[:7]} | {commit.subject}" for commit in commits)


def _format_changed_files_for_prompt(changed_files: list[ChangedFile]) -> str:
    return "\n".join(f"- [{changed_file.status}] {changed_file.path}" for changed_file in changed_files)


def _create_write_comments_node(model: ChatOpenAI):
    def _write_comments(state: WorkflowState, runtime: Runtime[WorkflowContext]):
        system_prompt = """You are an expert code reviewer performing a pull-request review.

You have access to tools to:
  - Post review comments to the GitHub pull request (general thread).

Review workflow:
  1. Aggregate the worker findings and notes across all groups.
  2. Identify real issues: bugs, security problems, correctness problems, unclear behavior,
    inconsistent logic, weak error handling, missing tests, and clear code-quality issues.
    Skip nits and stylistic preferences unless they materially affect maintainability or correctness.
  3. Post precise, actionable comments where the issue lives.

Guidelines:
  - Be concrete: cite exact lines and explain the problem and a suggested fix.
  - Prefer a few high-signal comments over many low-signal ones.
  - Do not add praise, encouragement, or generic positive commentary.
  - Do not invent issues. If something is unclear, say exactly what is unclear and why it matters.
  - Don't comment on unchanged code unless it's directly impacted by the change.
  - Never claim you have modified the code — you can't; you only read and comment.
"""

        @tool
        def post_pr_comment(content: str) -> str:
            """Post a general (non-file) comment on the pull request.

            Use this for overall review summaries or top-level remarks.
            """
            post_pull_request_comment(state["repository"], state["pull_request_id"], content, runtime.context["github_access_token"])

        aggregator_agent = create_agent(
            model=model,
            tools=[post_pr_comment],
            system_prompt=system_prompt,
        )

        user_prompt = (
            "Analysis results:\n\n"
            '\n\n'.join(state['analysis_results'])
        )

        aggregator_agent.invoke(
            input={
                "messages": [HumanMessage(user_prompt)]
            }
        )

    return _write_comments


def _delegate_work(state: WorkflowState):
    if not state["delegation_matrix"]:
        return END
    
    return [Send("analyze", { "group": group, "working_directory": state["working_directory"], "pull_request": state["pull_request"], "commits": state["commits"] }) for group in state["delegation_matrix"]]


def _handle_remove_readonly(retry, target: str, exc_info) -> None:
    if not issubclass(exc_info[0], PermissionError):
        raise exc_info[1]

    os.chmod(target, stat.S_IWRITE)
    retry(target)