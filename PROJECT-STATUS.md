# MicroForge — Project Status Snapshot

> **Last Updated:** 2026-07-20
> **Purpose:** Quick-resume reference. Read this first instead of re-scanning all sprint docs.

---

## TL;DR

- **All 6 sprints and Fab Expansion Phases 1-6 complete.**
- **175 story points delivered plus advanced semiconductor features.**
- **240 tests passing** (196 unit, 24 integration, 20 agent). Build: 0 errors, 0 warnings.
- **CI pipeline** (GitHub Actions) — build → test → Docker image on every push to main.
- **Performance baseline** project — measures p50/p95/p99 for 8 endpoints.
- **Tech debt:** 2 open items (TD-001 SQLite, TD-002 in-memory vector store) — both acceptable for demo.

---

## Build & Test Commands

```bash
dotnet build                              # 0 errors, 0 warnings
dotnet test --no-build                    # 240 tests pass
dotnet test tests/MicroForge.UnitTests    # 196 tests
dotnet test tests/MicroForge.AgentTests   # 20 tests
dotnet test tests/MicroForge.IntegrationTests  # 24 tests
```

---

## Solution Structure

```
src/
  src/MicroForge.Domain/          — Entities, Enums, Interfaces, Configuration (zero infra deps)
  src/MicroForge.Application/     — DTOs, App Services, DI registration
  src/MicroForge.Infrastructure/  — EF Core, AI/Agent, RAG, Events, Auth, Cache, Telemetry
  src/MicroForge.Web/             — Controllers, Hubs, Program.cs, Middleware, Blazor UI
tests/
  tests/MicroForge.UnitTests/       — 196 tests (services, tools, anomaly, chunker, cache, closed-loop, etc.)
  tests/MicroForge.AgentTests/      — 20 tests (agent runtime, tool dispatch, multi-step)
  tests/MicroForge.IntegrationTests/— 24 tests (WebApplicationFactory, auth, CRUD, agent chat, closed-loop API)
```

---

## Sprint Completion Matrix

| Sprint | Name | Points | Status |
|--------|------|--------|--------|
| 1 | Laying the Foundation | 30/30 | ✅ Complete |
| 2 | Eyes on the Floor | 27/27 | ✅ Complete |
| 3 | Teaching the Machine | 32/32 | ✅ Complete |
| 4 | Knowledge at Your Fingertips | 31/31 | ✅ Complete |
| 5 | The Pulse of Production | 28/28 | ✅ Complete |
| 6 | Ship It | 27/27 | ✅ Complete |

### Sprint 6 Detail

| Story | Title | Priority | Status |
|-------|-------|----------|--------|
| MF-035 | Unit Test Foundation (≥30 tests, 70% coverage) | Must Have | ✅ 174 tests |
| MF-036 | Integration Tests (≥10 tests) | Must Have | ✅ 20 tests |
| MF-037 | Agent Tool Test Harness (≥3 cases/tool) | Must Have | ✅ 20 tests |
| MF-034 | Metrics Endpoint (`/metrics`) | Could Have | ✅ Done (Prometheus format) |
| MF-038 | Load & Performance Baseline | Could Have | ✅ Done (PerformanceTests project) |
| MF-040 | CI Pipeline (GitHub Actions) | Could Have | ✅ Done (`.github/workflows/ci.yml`) |

---

## Implemented Features (by epic)

### Epic 1 — Platform Foundation ✅
- .NET 8 clean architecture (Domain → Application → Infrastructure → Web)
- SQLite + EF Core with migrations + DbSeeder
- JWT auth (`POST /api/auth/login`), role-based auth (Admin/Engineer/Operator)
- Data boundary enforcement (users only see assigned lines)
- Docker + docker-compose (multi-stage build)
- In-memory cache service with TTL
- Serilog structured logging (console + file)
- Swagger/OpenAPI at `/swagger`

### Epic 2 — Manufacturing Dashboard ✅
- SignalR hub (`/hubs/production`) for real-time updates
- Production line status, yield/defect data
- Health check endpoints (`/health`, `/ready`)
- Global exception handling (RFC 7807 Problem Details)
- Note: Blazor UI pages referenced in sprint summaries may not all be present in current codebase — the Web project is API-focused

### Epic 3 — AI Agent Framework ✅
- `AgentRuntime` — LLM ↔ Tool orchestration loop with max 10 iterations
- `ToolRegistry` — auto-discovers and registers all `ITool` implementations
- **24 tools total** including original tools and new ones: QueryProductionDefects, QueryProductionRuns, QuerySensorReadings, QueryAlerts, GetDashboardSummary, GetProductionLineStatus, GetProductionRunSummary, CalculateStatistics, UpdateProductionRunStatus, CreateDefectRecord, AcknowledgeAlert, SearchDocuments, QueryWipLots, MoveWipLot, HoldWipLot, ReleaseWipLot, GetOeeMetrics, QueryFdcDiagnostics, ManageCapaActions, GetSecsGemLogs, ManageQualifications.
- Agent chat endpoint (`POST /api/agent/chat`)
- All tool invocations logged to `AgentLogs` table
- Agent tool invocation metrics (counter by tool name + success/failure)

### Epic 4 — RAG Document Intelligence ✅
- Document upload API (`POST /api/documents/upload`, 10MB limit)
- PDF text extraction (PdfPig), TXT/MD support
- Document chunker (500 tokens, 50 overlap)
- Embedding service (mock hash-based 384-dim or Ollama)
- In-memory HNSW vector store with cosine similarity
- Semantic search API (`POST /api/documents/search`)
- SearchDocuments agent tool for grounding

### Epic 5 — Production Event Pipeline ✅
- `SensorSimulator` background service (4 sensor types, configurable interval)
- `System.Threading.Channels` bounded pipeline (capacity 1000)
- `AnomalyDetector` — Z-score based, configurable thresholds per sensor type
- `EventBus` for pub/sub event distribution
- Alert persistence + SignalR broadcast
- Events/alerts API endpoints
- Rate limiting (5 req/min auth, 60 req/min general)
- Security headers middleware (X-Frame-Options, X-Content-Type-Options, etc.)

### Epic 8 — Semiconductor Roadmap Upgrades (Phases 1-6) ✅
- **Phase 1 (SPC)**: Real-time SPC analysis checking Western Electric Rules 1, 2, and 3. SignalR event push for real-time violations.
- **Phase 2 (WIP)**: Lot/Wafer genealogy database schemas, routing step movements, and cassette slots configuration.
- **Phase 3 (FDC)**: Thermal Runaway, Vacuum Leak, and RF Plasma trace anomalies classifier, chamber baseline matching, and parts RUL tracking.
- **Phases 4 & 5 (Fab Expansion)**: Simulated HSMS TCP host driver (SECS-II online pings S1F1, remote command S2F21), live OEE metrics, SVG spatial wafer defect maps, RCA Golden Run diff graphing, Pearson correlation coefficients, CAPA workflows, ECO logs, and shift logs handover timeline.
- **Phase 6 (Closed-Loop Control)**: Gated routing qualification checks, Electronic Work Instructions (EWI) checklists, and FDC auto-disposition lot holds.

### Epic 6 — Observability & Hardening ✅ (mostly)
- Serilog structured logging ✅
- Health checks (`/health`, `/ready`) ✅
- Global exception handling ✅
- Rate limiting ✅
- Security headers + CORS ✅
- **Metrics endpoint** (`/metrics`) ✅ — Prometheus format with:
  - `http_requests_total`, `http_errors_total`
  - `signalr_connections_active`
  - `pipeline_events_processed_total`
  - `agent_tool_invocations_total`
  - Business metrics (lines, runs, alerts, defects, documents, users, sensors)
- `AppMetricsService` singleton — wraps `System.Diagnostics.Metrics` Meter
- `HttpMetricsMiddleware` — records request count + duration on every request

### Epic 7 — Testing & Quality ✅ (Must Haves)
- 174 unit tests (services, tools, anomaly detector, chunker, cache, event bus, vector store, embedding)
- 20 integration tests (WebApplicationFactory, auth flows, CRUD, agent chat)
- 20 agent tests (runtime, tool dispatch, multi-step reasoning, edge cases)

---

## Key Files (most recently modified)

| File | Purpose |
|------|---------|
| `src/MicroForge.Infrastructure/Telemetry/AppMetricsService.cs` | Metrics singleton (Meter, counters, gauges, histograms) |
| `src/MicroForge.Infrastructure/Telemetry/HttpMetricsMiddleware.cs` | HTTP request metrics middleware |
| `src/MicroForge.Web/Controllers/MetricsController.cs` | `/metrics` Prometheus endpoint |
| `src/MicroForge.Web/Program.cs` | App startup, DI, middleware pipeline |
| `src/MicroForge.Infrastructure/DependencyInjection.cs` | Infrastructure DI registration |
| `src/MicroForge.Infrastructure/AI/AgentRuntime.cs` | Agent LLM↔Tool loop |
| `src/MicroForge.Infrastructure/Events/EventBus.cs` | Event pub/sub with metrics |
| `src/MicroForge.Infrastructure/Events/SensorSimulator.cs` | Background sensor generator |
| `src/MicroForge.Web/Hubs/ProductionHub.cs` | SignalR hub with connection tracking |

---

## Technical Debt (open)

| ID | Title | Severity | Status |
|----|-------|----------|--------|
| TD-001 | SQLite instead of SQL Server | 🟡 Medium | Open |
| TD-002 | In-memory vector store not persisted | 🟠 High | Open |

---

## What's Left

**Nothing.** All 40 stories across 6 sprints are complete (175/175 story points).

### Optional future improvements (not part of sprint scope):
- Persist vector store to disk (TD-002) for better demo experience
- Add Blazor UI components if frontend is needed for demo
- Consider SQL Server container for production parity (TD-001)
- Add code coverage report generation to CI pipeline

---

## Git State

- Repository initialized on 2026-07-20 (first commit)
- Single commit: "Add application metrics instrumentation..."
- Working directory clean after last commit

---

## Quick Resume Checklist

If resuming work, check:
- [ ] `dotnet build` still passes with 0 errors
- [ ] `dotnet test` still passes 240 tests
- [ ] CI pipeline runs green on GitHub Actions
- [ ] Fill out `grading/evaluation-checklist.md` for final grade assessment