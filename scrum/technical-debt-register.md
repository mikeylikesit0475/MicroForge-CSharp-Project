# MicroForge — Technical Debt Register

> **Project:** MicroForge — AI-Powered Manufacturing Intelligence Platform
> **Last Updated:** 2026-07-14
> **Owner:** Mike (Solo Developer)

---

## Purpose

This register tracks **technical debt** — intentional shortcuts, known limitations, deferred improvements, and quality compromises discovered during development. Every item represents a gap between the current implementation and the ideal implementation.

> [!IMPORTANT]
> **Log debt immediately when it's discovered.** Don't rely on memory. If you take a shortcut, write it down here before moving on. Future-you will thank present-you.

---

## Severity Definitions

| Severity | Definition | Action Timeline |
|---|---|---|
| 🔴 **Critical** | Debt that will cause bugs, security vulnerabilities, data loss, or blocks other features if not addressed. | Must resolve within the current sprint. |
| 🟠 **High** | Debt that significantly degrades performance, maintainability, or developer experience. Will compound quickly. | Schedule for next sprint. |
| 🟡 **Medium** | Debt that is noticeable but manageable. Increases cognitive load or makes future changes harder. | Schedule within 2–3 sprints. |
| 🟢 **Low** | Cosmetic debt, minor inconsistencies, or "nice-to-have" improvements. Does not impact functionality. | Address when convenient or during slack time. |

---

## Category Definitions

| Category | Description | Examples |
|---|---|---|
| **Architecture** | Structural decisions that need revisiting | Missing abstraction layer, tight coupling, wrong DI lifetime |
| **Code Quality** | Code smells, duplication, or convention violations | Duplicate logic, magic numbers, inconsistent naming |
| **Testing** | Missing or inadequate test coverage | Untested edge cases, no integration tests for a feature |
| **Security** | Security shortcuts or known vulnerabilities | Hardcoded config value, missing input validation |
| **Performance** | Known performance issues or unoptimized paths | N+1 queries, missing indexes, unbounded queries |
| **Documentation** | Missing or outdated documentation | Undocumented API changes, stale README |
| **Infrastructure** | DevOps, Docker, CI/CD, or deployment issues | Missing health check, unoptimized Docker image |
| **UX/UI** | Frontend shortcuts or accessibility gaps | Missing loading states, poor error messages |

---

## Active Technical Debt

> Add new entries below as they are discovered. Move resolved items to the [Resolved Technical Debt](#resolved-technical-debt) section.

### Template for New Entries

```markdown
### TD-XXX · [Short Title]

| Field | Value |
|---|---|
| **ID** | TD-XXX |
| **Severity** | 🔴 Critical / 🟠 High / 🟡 Medium / 🟢 Low |
| **Category** | Architecture / Code Quality / Testing / Security / Performance / Documentation / Infrastructure / UX/UI |
| **Discovered** | Sprint N — [Date] |
| **Related Story** | MF-XXX |
| **Description** | [What is the debt? What shortcut was taken?] |
| **Impact** | [What is the consequence of leaving this unresolved?] |
| **Ideal Solution** | [What would the proper implementation look like?] |
| **Effort Estimate** | [S / M / L — rough t-shirt size] |
| **Status** | 🔓 Open / 🔧 In Progress / ✅ Resolved |
| **Resolved** | [Sprint N — Date — Brief resolution notes] |
```

---

### TD-001 · [Example] SQLite Instead of SQL Server

| Field | Value |
|---|---|
| **ID** | TD-001 |
| **Severity** | 🟡 Medium |
| **Category** | Architecture |
| **Discovered** | Sprint 1 — 2026-07-14 |
| **Related Story** | MF-002 |
| **Description** | Using SQLite instead of SQL Server for development simplicity. SQLite lacks some features (e.g., full-text search, advanced JSON queries, concurrent write performance) that SQL Server provides. |
| **Impact** | Some queries may need rewriting if migrated to SQL Server. SQLite's single-writer model could cause contention under load. |
| **Ideal Solution** | Use SQL Server in a Docker container for development parity with production. Add a docker-compose service for SQL Server. |
| **Effort Estimate** | M |
| **Status** | 🔓 Open |
| **Resolved** | — |

---

### TD-002 · [Example] In-Memory Vector Store Not Persisted

| Field | Value |
|---|---|
| **ID** | TD-002 |
| **Severity** | 🟠 High |
| **Category** | Architecture |
| **Discovered** | Sprint 4 — [TBD] |
| **Related Story** | MF-021 |
| **Description** | The HNSW vector store is in-memory only. If the application restarts, all embeddings are lost and documents must be re-embedded. |
| **Impact** | Data loss on restart. Re-embedding is slow and computationally expensive. Poor demo experience. |
| **Ideal Solution** | Persist the HNSW index to disk (serialize/deserialize on startup/shutdown) or use a SQLite-backed vector store. |
| **Effort Estimate** | M |
| **Status** | 🔓 Open |
| **Resolved** | — |

---

<!-- 
### TD-003 · [Title]

| Field | Value |
|---|---|
| **ID** | TD-003 |
| **Severity** | |
| **Category** | |
| **Discovered** | Sprint N — [Date] |
| **Related Story** | MF-XXX |
| **Description** | |
| **Impact** | |
| **Ideal Solution** | |
| **Effort Estimate** | |
| **Status** | 🔓 Open |
| **Resolved** | — |
-->

---

## Resolved Technical Debt

> Move items here when they are resolved. Keep the full entry for historical reference.

| ID | Title | Severity | Resolved In | Resolution |
|---|---|---|---|---|
| — | — | — | — | — |

---

## Technical Debt Summary Dashboard

> Update this table at the end of each sprint during the Sprint Review.

| Sprint | Open Items | Critical | High | Medium | Low | Resolved This Sprint |
|---|---|---|---|---|---|---|
| Sprint 1 | 1 | 0 | 0 | 1 | 0 | 0 |
| Sprint 2 | 1 | 0 | 0 | 1 | 0 | 0 |
| Sprint 3 | 1 | 0 | 0 | 1 | 0 | 0 |
| Sprint 4 | 2 | 0 | 1 | 1 | 0 | 0 |
| Sprint 5 | 2 | 0 | 1 | 1 | 0 | 0 |
| Sprint 6 | 2 | 0 | 1 | 1 | 0 | 0 |

---

## Guidelines for Managing Technical Debt

### When to Log Debt

- **Always log** when you knowingly take a shortcut (time pressure, complexity deferral).
- **Always log** when the AI assistant flags a concern you choose not to address immediately.
- **Always log** when a code review (self or AI) identifies a "code smell" that isn't worth fixing right now.
- **Always log** when a test is skipped or a known edge case is not covered.
- **Always log** when a `// TODO` or `// HACK` comment is added to the code.

### When to Pay Down Debt

- **Critical:** Immediately. Stop feature work if needed.
- **High:** In the next sprint's planning, allocate 10–20% of capacity to debt items.
- **Medium/Low:** During sprint slack time (when you finish stories early), or batch into a dedicated "tech debt sprint" if the register grows too large.

### Debt Budget Rule

> [!WARNING]
> If the total number of 🔴 Critical + 🟠 High items exceeds **5**, declare a "tech debt emergency" and dedicate the next sprint to debt reduction before adding new features.

### Integration with Sprint Ceremonies

| Ceremony | Debt Activity |
|---|---|
| **Sprint Planning** | Review open debt items. Select high-priority items for the sprint if capacity allows. |
| **Daily Standup** | Note any new debt discovered during today's work. |
| **Sprint Review** | Update the Summary Dashboard. Report debt created vs. debt resolved. |
| **Sprint Retrospective** | Reflect on whether the sprint's velocity was impacted by existing debt. Discuss root causes of new debt. |

---

## Quick-Add Shortcut

For rapid logging during coding, use this minimal format and flesh it out later:

```markdown
### TD-XXX · [Title]
- **Severity:** 🟡 Medium
- **Sprint:** N
- **Story:** MF-XXX
- **What:** [One sentence]
- **Why it matters:** [One sentence]
```

---

> [!TIP]
> **A clean debt register is a sign of a healthy project.** The goal isn't zero debt — it's *conscious, managed* debt. Every shortcut logged is a shortcut you can plan to repay, rather than a surprise that bites you later.
