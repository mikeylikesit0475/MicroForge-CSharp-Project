# MicroForge — Definition of Done (Global)

> **Project:** MicroForge — AI-Powered Manufacturing Intelligence Platform
> **Last Updated:** 2026-07-14
> **Applies To:** Every user story in the Product Backlog

---

## Purpose

The Definition of Done (DoD) is a shared checklist that defines when a user story is **truly complete** — not just "code written" but fully integrated, tested, documented, and ready for demonstration. For a solo developer, this checklist replaces the team-based review process with disciplined self-verification and AI-assisted review.

> [!IMPORTANT]
> A story is **not done** until every applicable checkbox below is checked. If a checkbox does not apply to a specific story, mark it `N/A` with a brief justification in the story's tracking notes.

---

## Definition of Done Checklist

### 1. Code Quality

- [ ] **Code compiles** — `dotnet build` succeeds with zero errors across the entire solution.
- [ ] **No new warnings** — No new compiler warnings introduced (or existing warnings explicitly suppressed with justification).
- [ ] **Code follows project conventions** — Naming conventions (PascalCase for public members, camelCase for locals), file organization, and namespace structure are consistent.
- [ ] **No hardcoded secrets** — All sensitive values (connection strings, JWT keys, API keys) are loaded from configuration (`appsettings.json`, environment variables, or user secrets).
- [ ] **Dead code removed** — No commented-out code blocks, unused imports, or orphaned methods remain.
- [ ] **AI-assisted review completed** — Code has been reviewed with the local AI assistant (Nemotron3-Super) for correctness, security, and best practices. Any AI-suggested improvements are either applied or documented as intentional deviations.

### 2. Testing

- [ ] **Unit tests written** — All new domain logic has corresponding unit tests (xUnit + FluentAssertions).
- [ ] **Unit tests pass** — `dotnet test --filter Category=Unit` passes with zero failures.
- [ ] **Integration tests pass** — `dotnet test --filter Category=Integration` passes with zero failures (for stories that modify API behavior).
- [ ] **Edge cases covered** — At least one test covers a boundary condition or error case for each public method.
- [ ] **No test regressions** — All previously passing tests still pass after the story's changes.
- [ ] **Manual smoke test completed** — The feature has been manually exercised in the running application (API via Swagger or Blazor UI) and behaves as expected.

### 3. Acceptance Criteria

- [ ] **All acceptance criteria met** — Every Given/When/Then criterion listed in the story has been verified and passes.
- [ ] **Demonstrated** — The feature can be demonstrated end-to-end (e.g., via Swagger for API stories, via Blazor for UI stories).

### 4. Security

- [ ] **Authentication enforced** — Protected endpoints require valid JWT; unauthenticated requests return 401.
- [ ] **Authorization enforced** — Role-based access is respected; unauthorized requests return 403.
- [ ] **Data boundaries enforced** — Users only see data for their assigned production lines.
- [ ] **Input validation** — All user inputs are validated; invalid inputs return structured 400 responses.
- [ ] **No sensitive data in logs** — Passwords, tokens, and PII are not logged (Serilog destructuring excludes sensitive properties).
- [ ] **Security scan passed** — If the story involves new first-party code, a Snyk code scan has been run with no high/critical severity issues unresolved.

### 5. Documentation

- [ ] **API documentation updated** — New or modified endpoints are reflected in Swagger/OpenAPI (XML comments on controllers and DTOs).
- [ ] **Code comments (where needed)** — Complex algorithms, non-obvious design decisions, and workarounds have inline comments explaining "why" (not "what").
- [ ] **README updated (if applicable)** — Setup instructions, environment variables, or architecture diagrams are updated if the story changes them.
- [ ] **Technical debt logged** — Any shortcuts, known limitations, or deferred improvements are recorded in the Technical Debt Register.

### 6. Observability

- [ ] **Structured logging** — Key operations (API calls, tool invocations, pipeline events, errors) produce structured Serilog log entries with correlation IDs.
- [ ] **Health checks updated** — If a new dependency is introduced (e.g., vector store), a corresponding health check is added.
- [ ] **Error handling** — Exceptions are caught, logged, and surfaced as appropriate HTTP status codes (not swallowed silently).

### 7. Integration & Deployment

- [ ] **Docker build succeeds** — `docker build` completes without errors with the story's changes included.
- [ ] **docker-compose up works** — `docker-compose up --build` starts the full stack and the new feature is functional in the containerized environment.
- [ ] **Database migrations applied** — Any new EF Core migrations apply cleanly to a fresh database and to an existing database with data.
- [ ] **Configuration documented** — Any new `appsettings.json` keys are documented with default values and descriptions.

### 8. Performance

- [ ] **No obvious performance regressions** — The feature does not introduce noticeable latency or memory issues during manual testing.
- [ ] **Database queries optimized** — EF Core queries use appropriate includes, projections, and avoid N+1 patterns (verified via Serilog SQL logging in Development mode).
- [ ] **Caching considered** — Hot data paths use the cache service where appropriate.

---

## Quick Reference: Story Completion Workflow

```
1. Implement the story
       ↓
2. Write/update unit tests → Run all tests
       ↓
3. Verify all acceptance criteria (manual + automated)
       ↓
4. Run AI-assisted code review (Nemotron3-Super)
       ↓
5. Run Snyk security scan (if applicable)
       ↓
6. Update Swagger docs / README / comments
       ↓
7. Build and test in Docker
       ↓
8. Log any tech debt discovered
       ↓
9. Check every DoD checkbox
       ↓
✅ Story is DONE
```

---

## Non-Functional Requirements (Always Apply)

These cross-cutting concerns are part of every story's DoD:

| NFR | Requirement |
|---|---|
| **Response Time** | API endpoints respond in ≤ 500ms (p95) for data queries; ≤ 5000ms for agent chat. |
| **Availability** | Application survives a database restart without crashing (health checks degrade gracefully). |
| **Scalability** | No global static mutable state; services are DI-registered with appropriate lifetimes. |
| **Accessibility** | Blazor UI meets basic accessibility: semantic HTML, keyboard navigable, color contrast ≥ 4.5:1. |
| **Browser Support** | Blazor UI works in Chrome (latest), Edge (latest), Firefox (latest). |

---

> [!TIP]
> **For a solo developer:** Don't skip the DoD checklist because "you know the code." The checklist protects against the blind spots that come from being the sole author, reviewer, and tester. Treat your AI coding assistant as your reviewer — ask it to critique your implementation against the acceptance criteria.
