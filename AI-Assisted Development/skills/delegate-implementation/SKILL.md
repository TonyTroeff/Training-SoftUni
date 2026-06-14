---
name: delegate-implementation
description: Implements features, bug fixes, refactoring, and code changes by breaking work into focused sub-problems. Use whenever writing, modifying, or creating code. Use when the user asks to implement, build, create, add, fix, update, refactor, or change anything in the codebase.
---

# Delegate Implementation

Decompose tasks into non-overlapping sub-problems and delegate each to the appropriate expert running in isolation.

## Core Principle

**Every implementation task must be decomposed and delegated.** Do not implement directly. Instead:

1. Break down into non-overlapping sub-problems (maximum 5)
2. Identify the expert for each sub-problem
3. Execute subagents (with concurrency limits)

## Step 1: Decompose the Task

Break the task into **non-overlapping sub-problems** (maximum 5):

- **Non-overlapping**: Sub-problems should not conflict with each other
- **Self-contained**: Each sub-problem can be completed and verified by the expert

### Decomposition Guidelines

1. **Separate by stack**: Backend work vs Frontend work
2. **Separate by feature boundary**: Distinct capabilities become separate sub-problems
3. **Keep it coarse**: Aim for 1-5 sub-problems, not too granular steps

## Step 2: Identify Expert per Sub-problem

For each sub-problem, determine the expert:

| Expert              | When to Use                                                                                                                         |
| ------------------- | ----------------------------------------------------------------------------------------------------------------------------------- |
| **Backend Expert**  | Server-side logic, API endpoints, business rules, data persistence, validation, background processing, authentication/authorization |
| **Frontend Expert** | User interface, user interactions, visual components, client-side state, presentation logic, form handling, navigation              |

## Step 3: Execute Subagents

### Execution

Spawn multiple subagents to work autonomously over the given sub-problems, ensuring their work stays non-conflicting.

**Simultaneous execution is allowed only when BOTH are true:**

1. **Non-overlapping implementation work**: No shared files, components, or shared state that could conflict.
2. **Different expert types**: The subagents are different experts.

**Exception (Allowed): Backend + Frontend may work simultaneously against the same API** as long as the API protocol/contract is explicitly agreed upfront (routes, request/response payloads, error shapes, and auth assumptions) and treated as the source of truth.

**Hard rule: never run two subagents of the same expert type simultaneously.**

- ✅ May run simultaneously: **Backend Expert** + **Frontend Expert** (including when coordinating on the same API via an agreed contract)
- ❌ Must not run simultaneously: **Backend Expert** + **Backend Expert**
- ❌ Must not run simultaneously: **Frontend Expert** + **Frontend Expert**

If there are dependencies between sub-problems (e.g., UI depends on an API contract), execute the prerequisite sub-problem first.

### Subagent Prompt Structure

Each subagent prompt must include:

1. **Sub-problem description**: Clear description of what to implement
2. **Acceptance criteria**: What defines "done" for this sub-problem
3. **Prohibitions**: The subagent should avoid further delegation - it must implement the assigned sub-problem autonomously. Prohibit the subsequent use of this "delegate-implementation" skill.

## Example

Task: "Generate a searchable PDF in the background and allow users to download it from the UI."

### Decomposition

| #   | Sub-problem                                                                                                           | Expert   |
| --- | --------------------------------------------------------------------------------------------------------------------- | -------- |
| 1   | Define the API contract (endpoints, DTOs, errors) for downloading a searchable PDF                                    | Backend  |
| 2   | Add a file-conversion implementation to generate searchable PDFs, plus the required background operation and executor | Backend  |
| 3   | Introduce a new user action: "download_searchable_pdf"                                                                | Backend  |
| 4   | Implement the "download searchable PDF" endpoint (wired to the background operation and conversion output)            | Backend  |
| 5   | Add the "Download Searchable PDF" UI (button/action + download flow)                                                  | Frontend |

### Execution

1. Run Backend Expert subagent for sub-problem 1 and treat the output as the source-of-truth API contract.
2. Then start simultaneous execution of:
    - Sub-problem 5 (Frontend Expert), and
    - Sub-problems 2 → 3 → 4 (Backend Expert).
3. Backend sub-problems (2 → 3 → 4) execute sequentially due to the concurrency limitation (no Backend + Backend parallelism).

## Guardrails

- **Always decompose first**: Never implement directly without decomposition
- **Maximum 5 sub-problems**: Keep decomposition coarse, not granular
- **Concurrency limitations**: Never run same expert types simultaneously; Backend + Frontend may run simultaneously when implementation work is non-overlapping (API contract coordination is allowed if agreed upfront)
- **Experts validate internally**: Each expert runs their own guardrails
