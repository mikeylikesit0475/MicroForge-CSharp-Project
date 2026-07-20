# MicroForge — Scrum Ceremonies

> **Project:** MicroForge — AI-Powered Manufacturing Intelligence Platform
> **Team:** Solo developer (Mike) + AI Pair (Nemotron3-Super, local)
> **Sprint Duration:** 2 weeks
> **Last Updated:** 2026-07-14

---

## Overview

Traditional Scrum ceremonies are designed for teams of 3–9 people. For a solo developer with an AI coding assistant, the ceremonies are adapted to serve their core purposes — **inspection, adaptation, and transparency** — without the overhead of team coordination. The AI assistant acts as a sounding board, reviewer, and accountability partner.

> [!NOTE]
> The "team" in this context is you + your AI assistant. The ceremonies below are self-facilitated. Their value comes from the discipline of structured reflection, not from interpersonal dynamics.

---

## Ceremony Schedule

```
Week 1 of Sprint
├── Monday    → Sprint Planning (start of sprint)
├── Tue–Fri   → Daily Standups (async journal)
│
Week 2 of Sprint
├── Mon–Thu   → Daily Standups (async journal)
├── Friday    → Sprint Review + Sprint Retrospective
```

---

## 1. Sprint Planning

> **When:** First Monday of each sprint
> **Duration:** 60–90 minutes
> **Format:** Solo session with AI assistant

### Purpose

Select stories from the Product Backlog for the sprint, break them into tasks, identify risks, and establish the Sprint Goal.

### Agenda

| Step | Duration | Activity |
|---|---|---|
| 1 | 10 min | **Review velocity** — Check actual vs. planned velocity from the previous sprint. Adjust this sprint's capacity if needed. |
| 2 | 10 min | **Review the Sprint Goal** — Read and internalize the pre-defined sprint goal from `sprint-plan.md`. Adjust if circumstances have changed. |
| 3 | 20 min | **Story selection & commitment** — Review committed stories for the sprint. Confirm they are still relevant and properly prioritized. Add/remove stories if velocity data suggests adjustment. |
| 4 | 20 min | **Task decomposition** — Break each committed story into implementation tasks (e.g., "Create migration", "Implement service", "Write tests", "Update Swagger"). Record tasks in a local tracking tool or markdown file. |
| 5 | 10 min | **Dependency & risk check** — Identify cross-story dependencies and blockers. Flag stories that depend on unfinished prior work. |
| 6 | 10 min | **AI assistant briefing** — Brief the AI assistant on the sprint's scope. Share relevant context: "This sprint we're building the AI agent framework. Key classes will be IAgentTool, ToolRegistry, AgentRuntime." This primes the assistant for the sprint's work. |

### Artifacts Produced

- [ ] Sprint Backlog (list of stories + tasks for this sprint)
- [ ] Updated Sprint Goal (if adjusted)
- [ ] Risk log entries (if new risks identified)
- [ ] AI assistant context brief (shared with Nemotron3-Super)

### Template: Sprint Planning Notes

```markdown
# Sprint [N] Planning Notes — [Date]

## Sprint Goal
> [1-2 sentence goal]

## Committed Stories
| Story ID | Title | Points | Tasks |
|---|---|---|---|
| MF-XXX | ... | N | Task 1, Task 2, ... |

## Velocity Adjustment
- Previous sprint planned: X pts
- Previous sprint actual: Y pts
- This sprint capacity: Z pts

## Risks & Dependencies
- [Risk/dependency description]

## AI Context Brief
- [Key classes, patterns, or decisions to prime the AI on]
```

---

## 2. Daily Standup (Async Journal)

> **When:** Every working day (Mon–Fri)
> **Duration:** 5–10 minutes
> **Format:** Written journal entry (markdown file or dedicated tool)

### Purpose

Maintain daily awareness of progress, blockers, and plan adjustments. For a solo developer, this replaces the verbal standup with a written log that serves as both accountability and a project diary.

### Three Questions

Write brief answers to:

1. **What did I complete yesterday?**
   - List completed tasks or stories (with story IDs).

2. **What will I work on today?**
   - List planned tasks for the day.

3. **Are there any blockers or concerns?**
   - Technical blockers, unclear requirements, knowledge gaps, motivation issues.
   - If blocked, note the mitigation plan (research, ask AI, simplify scope).

### AI Assistant Integration

At the start of each working session, share a brief summary with the AI assistant:

> *"Today I'm working on MF-012 (Agent Runtime & Tool Registry). I'm implementing the IToolRegistry interface and auto-discovery via reflection. The key challenge is schema validation for tool inputs."*

This gives the AI assistant context and enables more targeted assistance throughout the day.

### Template: Daily Standup

```markdown
## [Date] — Daily Standup

### ✅ Completed Yesterday
- [Task/story completed]

### 🎯 Today's Plan
- [Task planned]

### 🚧 Blockers / Concerns
- [Blocker or "None"]

### 💡 Notes
- [Insights, decisions, or learnings]
```

---

## 3. Sprint Review

> **When:** Last Friday of each sprint (first half of the session)
> **Duration:** 30–45 minutes
> **Format:** Solo demonstration + documentation

### Purpose

Inspect the increment — demonstrate what was built, verify against the Sprint Goal, and document the state of the product.

### Agenda

| Step | Duration | Activity |
|---|---|---|
| 1 | 5 min | **Sprint Goal check** — Reread the Sprint Goal. Was it achieved? Partially? State the assessment clearly. |
| 2 | 15 min | **Demo walkthrough** — Run the application and walk through each completed story. For API stories, demonstrate via Swagger. For UI stories, demonstrate in the Blazor app. For infrastructure stories, show the running Docker containers, logs, or test results. |
| 3 | 5 min | **Acceptance criteria verification** — For each completed story, confirm all acceptance criteria are met. Mark stories as Done or Not Done. |
| 4 | 5 min | **Velocity recording** — Record the number of story points completed. Update the burndown chart in `sprint-plan.md`. |
| 5 | 5 min | **Backlog grooming** — Review upcoming sprint's stories. Are they still relevant? Do they need refinement based on what was learned this sprint? |
| 6 | 5 min | **Screen recordings / screenshots** — Capture key demo moments for the project portfolio (optional but recommended for a skills-demonstration project). |

### Artifacts Produced

- [ ] Sprint Review summary (what was delivered, what was not)
- [ ] Updated velocity tracker
- [ ] Updated burndown chart
- [ ] Screenshots/recordings of completed features
- [ ] Backlog adjustments (if any)

### Template: Sprint Review Notes

```markdown
# Sprint [N] Review — [Date]

## Sprint Goal
> [Goal statement]

## Goal Achievement: [Achieved / Partially Achieved / Not Achieved]

## Completed Stories
| Story ID | Title | Points | Status |
|---|---|---|---|
| MF-XXX | ... | N | ✅ Done |

## Not Completed
| Story ID | Title | Points | Reason |
|---|---|---|---|
| MF-XXX | ... | N | [Reason for incompletion] |

## Velocity
- Planned: X pts
- Completed: Y pts
- Carry-over: Z pts

## Demo Highlights
- [Feature 1]: [Brief description + screenshot link]

## Backlog Adjustments
- [Any changes to upcoming sprints]
```

---

## 4. Sprint Retrospective

> **When:** Last Friday of each sprint (second half of the session, immediately after the Review)
> **Duration:** 20–30 minutes
> **Format:** Written reflection + process improvement

### Purpose

Inspect the process — reflect on what went well, what didn't, and what to improve in the next sprint. For a solo developer, this is the most important ceremony because there is no team to naturally surface process issues.

### Format: 4Ls Retrospective

| Category | Prompt |
|---|---|
| **Liked** 😊 | What went well? What practices, tools, or decisions worked? |
| **Learned** 📚 | What new knowledge or skills were gained? What surprised you? |
| **Lacked** 😤 | What was missing? What tools, knowledge, or time was insufficient? |
| **Longed For** 🌟 | What do you wish you had? What process change would help next sprint? |

### AI Assistant Retrospective

Include a specific reflection on the AI assistant collaboration:

- How effectively did the AI assistant contribute this sprint?
- Were there areas where the AI was particularly helpful or unhelpful?
- What context or instructions could improve AI assistance next sprint?
- Did the AI introduce any errors or antipatterns that needed correction?

### Action Items

Every retrospective must produce **1–3 concrete action items** for the next sprint. These are process improvements, not feature work.

**Good action items:**
- "Start each session by re-reading the current story's acceptance criteria before coding."
- "Run `dotnet test` after every commit, not just at the end of the day."
- "Ask the AI to generate test cases before writing implementation code."

**Bad action items:**
- "Write better code." (Too vague)
- "Finish faster." (Not actionable)

### Template: Sprint Retrospective

```markdown
# Sprint [N] Retrospective — [Date]

## 😊 Liked
- [What went well]

## 📚 Learned
- [What was learned]

## 😤 Lacked
- [What was missing]

## 🌟 Longed For
- [What would help]

## 🤖 AI Assistant Reflection
- Helpfulness: [1-5] / 5
- Best contribution: [Description]
- Biggest gap: [Description]
- Improvement for next sprint: [Action]

## Action Items for Next Sprint
1. [ ] [Concrete, actionable improvement]
2. [ ] [Concrete, actionable improvement]
3. [ ] [Concrete, actionable improvement]

## Previous Action Items Review
| Action Item | Status |
|---|---|
| [From last retro] | ✅ Done / ❌ Not Done / 🔄 In Progress |
```

---

## 5. Backlog Refinement (Ad Hoc)

> **When:** Mid-sprint (Wednesday of Week 1), as needed
> **Duration:** 15–20 minutes
> **Format:** Solo review of upcoming stories

### Purpose

Ensure that stories planned for the next sprint are well-understood, properly sized, and have clear acceptance criteria before Sprint Planning.

### Activities

1. **Review next sprint's stories** — Read each story and its acceptance criteria. Are they clear enough to start coding?
2. **Decompose large stories** — If any story feels too large (> 8 points), consider splitting it.
3. **Update estimates** — Adjust story point estimates based on new knowledge gained during the current sprint.
4. **Identify new stories** — If current sprint work reveals missing stories, add them to the backlog.
5. **Re-prioritize** — If project priorities have shifted, adjust story priorities.

---

## Ceremony Calendar (Visual)

```
┌─────────────────────────────────────────────────────────┐
│                    SPRINT N (2 weeks)                    │
├──────────┬──────────┬──────────┬──────────┬──────────────┤
│  Monday  │ Tuesday  │Wednesday │ Thursday │   Friday     │
├──────────┼──────────┼──────────┼──────────┼──────────────┤
│ PLANNING │ Standup  │ Standup  │ Standup  │  Standup     │
│ Standup  │          │          │          │              │
│          │          │          │          │              │
├──────────┼──────────┼──────────┼──────────┼──────────────┤
│ Standup  │ Standup  │ Standup  │ Standup  │  REVIEW      │
│          │          │ Refine-  │          │  RETRO       │
│          │          │ ment     │          │              │
│          │          │ (opt.)   │          │              │
└──────────┴──────────┴──────────┴──────────┴──────────────┘
```

---

## Time Investment Summary

| Ceremony | Frequency | Duration | Sprint Total |
|---|---|---|---|
| Sprint Planning | 1× per sprint | 60–90 min | 60–90 min |
| Daily Standup | 10× per sprint | 5–10 min | 50–100 min |
| Sprint Review | 1× per sprint | 30–45 min | 30–45 min |
| Sprint Retrospective | 1× per sprint | 20–30 min | 20–30 min |
| Backlog Refinement | 1× per sprint (optional) | 15–20 min | 15–20 min |
| **Total** | | | **~3–5 hours / sprint** |

> [!TIP]
> The ceremony overhead is ~3–5 hours out of ~80 working hours per sprint (4–6%). This is minimal and the structured reflection will save far more time than it costs by catching issues early and keeping work focused.
