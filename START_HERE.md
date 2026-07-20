# START HERE — Greenfield Build (READ THIS FIRST, BEFORE ANY OTHER FILE)

You are building the MicroForge system **FROM SCRATCH**. There is no existing code, no prior work, no history, and no predecessor to continue. This is a greenfield implementation to be completed in this session.

If anything else in this repository seems to imply that work has already happened, **that implication is wrong** — see the traps below.

---

## IGNORE THESE FRAMING TRAPS IN THE OTHER DOCS

The spec documents were written as if describing an ongoing project. They are not. Treat all "sprint," "history," and "prior assistant" language as scope/process description, **not** as a record of progress:

- **`scrum/sprint-plan.md`** says *"6 sprints over 12 weeks"* with per-sprint velocity. That is the intended **SCOPE** broken into phases — **NOT** a schedule of work already done. Build **all** of it, now, in this session.
- **`scrum/technical-debt-register.md`** contains an entry *"Discovered: Sprint 1 — 2026-07-14, Status: Open."* That is a **TEMPLATE PLACEHOLDER**, not real history. **No sprint has occurred.** Treat the register as empty.
- **`README.md`** mentions *"a local AI coding assistant (Nemotron3-Super)."* That is flavor text describing how the challenge was authored. It is **NOT** a predecessor you are replacing or continuing. You are the only developer; there is no prior assistant's work to build on.
- **`scrum/product-backlog.md`** and **`scrum/ceremonies.md`** describe the backlog and the process. Use them to understand **WHAT** to build and **HOW** to work — not to infer what is already finished.

**Do not "start from Sprint 1 and stop." Do not ask to continue in a later session. Do not treat any backlog item as already done.** Deliver the complete, working, buildable solution in this session.

---

## THE GOLDEN RULE — what makes a passing submission

After every meaningful chunk of work, run these and do not move on until all are green:

```bash
dotnet build MicroForge.sln          # MUST be 0 errors
dotnet test MicroForge.sln           # MUST be all green
dotnet run --project <WebProject> --urls http://localhost:50xx
# then, in another shell, prove the app actually serves:
curl -i -X POST http://localhost:50xx/api/auth/login \
  -H "Content-Type: application/json" -d '{<valid credentials from your seed data>}'
```

Never declare a piece "done" until it builds, its tests pass, and (for the API) it boots and a request returns a real response — not 500, not a crash. If the build has errors, fix them before writing more code. If the app crashes on startup, fix it before adding features. **"It should work" is not done. "I ran it and got 200" is done.** Do not describe work as complete that you have not verified by running it.

---

## SPEC READING ORDER (the actual source of truth)

1. `README.md` — what the product is (ignore the "Nemotron3-Super" flavor text per above).
2. `architecture/system-design.md` — the explicit design: Clean Architecture layers, the `ITool`/`IAgentTool` interface, the 9 named tools, the multi-step agent example, the guardrails table (per-tool 30s / session 2min / max 10 iterations), the full ER diagram with every column/PK/FK/index, the API design tables, JWT structure, the RBAC matrix. Implement exactly this.
3. `scrum/definition-of-done.md` — the Definition of Done. **Every checkbox is mandatory.** It requires `dotnet build` with zero errors, migrations applied, authentication enforced, no hardcoded secrets, correct DI lifetimes.
4. `grading/rubric.md` + `grading/evaluation-checklist.md` — the 100-point / 10-category rubric you will be graded against. Note the weight on the custom AI agent and the RAG pipeline.

---

## HOW YOU WILL BE GRADED

By a reviewer who **builds, tests, and runs** the app — not by reading the code. A beautiful codebase that does not build or boot scores the same as an empty folder. Your score is determined entirely by what executes.

Do **not** edit anything under `grading/`.

When you believe you are finished, do not write a narrative summary. Paste the **actual command output** of `dotnet build` (error/warning count line), `dotnet test` (passed/failed line), and a live `curl` to login showing the HTTP status + token. If any of those is not green, you are not finished.