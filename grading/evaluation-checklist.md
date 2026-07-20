# MicroForge — Evaluation Checklist

> **Total Points:** 100  
> **Usage:** Check each item during review. Sum the points for checked items to calculate the final score.  
> **Version:** 1.0  
> **Last Updated:** 2026-07-14

---

## Instructions

1. Review each checklist item against the submitted codebase
2. Check items that are **fully implemented and working**
3. Half-credit (noted where applicable) may be given for partial implementation
4. Sum all checked items for the total score
5. Apply the grade scale at the bottom

---

## 1. Architecture & System Design (15 Points)

### Solution Structure (5 pts)
- [ ] **(2 pts)** Solution has separate projects: Domain, Application, Infrastructure, Web
- [ ] **(1 pt)** Domain layer has zero dependencies on infrastructure packages (EF Core, HTTP)
- [ ] **(1 pt)** All cross-layer dependencies flow inward (Web → App → Domain)
- [ ] **(1 pt)** `IServiceCollection` extension methods for clean DI registration per layer

### Design Patterns & Principles (5 pts)
- [ ] **(1 pt)** Repository or data access abstraction pattern implemented
- [ ] **(1 pt)** MediatR used for CQRS-style command/query separation
- [ ] **(1 pt)** Strategy pattern or similar used for interchangeable components (e.g., embedding providers)
- [ ] **(1 pt)** `IOptions<T>` pattern used for typed configuration
- [ ] **(1 pt)** No `new` keyword for service instantiation — all resolved via DI

### SOLID Adherence (5 pts)
- [ ] **(1 pt)** **Single Responsibility:** Each class has one clear purpose
- [ ] **(1 pt)** **Open/Closed:** New tools/features added via new classes, not modifying existing ones
- [ ] **(1 pt)** **Liskov Substitution:** Derived types are substitutable for their base interfaces
- [ ] **(1 pt)** **Interface Segregation:** No "fat" interfaces — interfaces are focused
- [ ] **(1 pt)** **Dependency Inversion:** All service dependencies are interfaces, not concrete classes

**Subtotal: ___ / 15**

---

## 2. Full-Stack Implementation (15 Points)

### API Quality (6 pts)
- [ ] **(1 pt)** RESTful resource naming (`/api/production-lines`, not `/api/getLines`)
- [ ] **(1 pt)** Correct HTTP verbs: GET (read), POST (create), PUT (update), DELETE (delete)
- [ ] **(1 pt)** Proper status codes: `201 Created` with Location header, `204 No Content`
- [ ] **(1 pt)** `404 Not Found` for missing resources, `409 Conflict` for duplicates
- [ ] **(1 pt)** RFC 7807 Problem Details for error responses
- [ ] **(1 pt)** Pagination support (`?page=1&pageSize=20`) on list endpoints

### Blazor UI (5 pts)
- [ ] **(1 pt)** Dashboard page with real-time sensor data visualization
- [ ] **(1 pt)** AI agent chat interface with message history
- [ ] **(1 pt)** Document upload and semantic search interface
- [ ] **(1 pt)** Production run management with CRUD operations
- [ ] **(1 pt)** Reusable Blazor components (not monolithic pages)

### Real-Time & Integration (4 pts)
- [ ] **(2 pts)** SignalR hub pushes live sensor data to dashboard
- [ ] **(1 pt)** Alert notifications pushed to connected clients in real-time
- [ ] **(1 pt)** Swagger/OpenAPI documentation accessible at `/swagger`

**Subtotal: ___ / 15**

---

## 3. Custom AI Agent System (15 Points)

### Tool Registry (5 pts)
- [ ] **(2 pts)** `ITool` interface with `Name`, `Description`, `Schema`, `InvokeAsync`
- [ ] **(1 pt)** `IToolRegistry` manages tool registration and discovery
- [ ] **(1 pt)** JSON Schema definitions exist for each tool's parameters
- [ ] **(1 pt)** Input validation against JSON schemas before tool execution

### Agent Runtime (5 pts)
- [ ] **(2 pts)** `IAgentRuntime` orchestrates the LLM ↔ Tool invocation loop
- [ ] **(1 pt)** Multi-step reasoning: agent can chain 2+ tools sequentially
- [ ] **(1 pt)** Execution context provides user identity and session info to tools
- [ ] **(1 pt)** Tools enforce the same RBAC/data boundaries as API endpoints

### Tools & Guardrails (5 pts)
- [ ] **(2 pts)** At least 5 working tools across Data, RAG, Compute, Action categories
- [ ] **(1 pt)** Max iteration guard (≤ 10 tool calls per session)
- [ ] **(1 pt)** Timeout guards (per-tool ≤ 30s, session ≤ 2min)
- [ ] **(1 pt)** All agent interactions logged to `AgentLogs` table

**Subtotal: ___ / 15**

---

## 4. RAG Implementation (10 Points)

### Document Ingestion (3 pts)
- [ ] **(1 pt)** Documents can be uploaded via API endpoint
- [ ] **(1 pt)** `IDocumentChunker` splits documents with configurable chunk size and overlap
- [ ] **(1 pt)** Document metadata preserved through the pipeline

### Embedding & Storage (4 pts)
- [ ] **(2 pts)** `IEmbeddingService` generates vector embeddings for chunks
- [ ] **(1 pt)** `IVectorStore` stores embeddings with HNSW or ANN indexing
- [ ] **(1 pt)** Cosine similarity (or equivalent) used for search ranking

### Search & Grounding (3 pts)
- [ ] **(1 pt)** Semantic search returns top-K results with similarity scores
- [ ] **(1 pt)** LLM responses include source document citations
- [ ] **(1 pt)** Performance metrics logged (embedding time, search time)

**Subtotal: ___ / 10**

---

## 5. Data Persistence & Modeling (10 Points)

### Schema Design (4 pts)
- [ ] **(1 pt)** Properly normalized schema (at least 3NF)
- [ ] **(1 pt)** Separate `IEntityTypeConfiguration<T>` for each entity
- [ ] **(1 pt)** Indexes defined for foreign keys and common query patterns
- [ ] **(1 pt)** Audit fields (CreatedAt, UpdatedAt) on entities

### EF Core Usage (3 pts)
- [ ] **(1 pt)** Code-first migrations committed and functional
- [ ] **(1 pt)** Projections used for list queries (`.Select()` to DTOs)
- [ ] **(1 pt)** Pagination at database level (`.Skip().Take()`)

### Performance (3 pts)
- [ ] **(1 pt)** No N+1 query problems (`.Include()` or explicit joins used)
- [ ] **(1 pt)** `IMemoryCache` used for frequently read reference data
- [ ] **(1 pt)** Cache invalidation on write operations

**Subtotal: ___ / 10**

---

## 6. Security & Authentication (10 Points)

### JWT Implementation (4 pts)
- [ ] **(1 pt)** JWT generated with proper claims (`sub`, `roles`, `exp`, `iss`, `aud`)
- [ ] **(1 pt)** Token expiry ≤ 1 hour
- [ ] **(1 pt)** Refresh token mechanism with rotation
- [ ] **(1 pt)** Secrets loaded from environment variables (not hardcoded)

### Authorization (3 pts)
- [ ] **(1 pt)** `[Authorize]` on all non-public endpoints
- [ ] **(1 pt)** Role-based policies enforced (`Admin`, `Engineer`, `Operator`, `Viewer`)
- [ ] **(1 pt)** Custom `IAuthorizationHandler` for data boundary enforcement

### Input Validation & Hardening (3 pts)
- [ ] **(1 pt)** Input validation on all POST/PUT endpoints (FluentValidation or DataAnnotations)
- [ ] **(1 pt)** BCrypt password hashing (work factor ≥ 10)
- [ ] **(1 pt)** Rate limiting on authentication endpoints

**Subtotal: ___ / 10**

---

## 7. Testing & Quality (10 Points)

### Unit Tests (4 pts)
- [ ] **(1 pt)** Unit tests exist for service layer classes
- [ ] **(1 pt)** Unit tests exist for validators
- [ ] **(1 pt)** Unit tests exist for AI tools
- [ ] **(1 pt)** Unit tests exist for anomaly detector / chunker

### Integration Tests (3 pts)
- [ ] **(1 pt)** Integration tests use `WebApplicationFactory<Program>`
- [ ] **(1 pt)** API endpoint tests cover CRUD operations
- [ ] **(1 pt)** Auth tests verify JWT and role enforcement

### Agent Tests & Quality (3 pts)
- [ ] **(1 pt)** Agent test harness with mock LLM responses
- [ ] **(1 pt)** Edge case tests (null inputs, boundary values, unauthorized access)
- [ ] **(1 pt)** Code coverage ≥ 70% (report generated)

**Subtotal: ___ / 10**

---

## 8. Containerization & DevOps (5 Points)

### Dockerfile (3 pts)
- [ ] **(1 pt)** Multi-stage build (SDK → ASP.NET runtime)
- [ ] **(1 pt)** `.dockerignore` configured to exclude unnecessary files
- [ ] **(1 pt)** `HEALTHCHECK` instruction defined

### Docker Compose (2 pts)
- [ ] **(1 pt)** `docker-compose.yml` with named volumes, environment variables, restart policy
- [ ] **(1 pt)** `docker compose up` works from a fresh clone (no manual steps)

**Subtotal: ___ / 5**

---

## 9. Observability (5 Points)

### Logging (3 pts)
- [ ] **(1 pt)** Serilog configured with console and file sinks
- [ ] **(1 pt)** Structured log messages (templated, not string concatenation)
- [ ] **(1 pt)** Sensitive data excluded from logs (passwords, tokens, PII)

### Health & Metrics (2 pts)
- [ ] **(1 pt)** Health check endpoint at `/health` with DB connectivity check
- [ ] **(1 pt)** Readiness endpoint at `/ready` OR custom metrics at `/metrics`

**Subtotal: ___ / 5**

---

## 10. Code Quality & Standards (5 Points)

### Naming & Conventions (2 pts)
- [ ] **(1 pt)** Consistent C# naming: PascalCase types, `_camelCase` fields, camelCase locals
- [ ] **(1 pt)** `I` prefix on all interfaces; no naming inconsistencies

### Documentation & Git (3 pts)
- [ ] **(1 pt)** XML doc comments (`///`) on public interfaces and complex methods
- [ ] **(1 pt)** Git history shows incremental development (multiple meaningful commits)
- [ ] **(1 pt)** No compiler warnings, no commented-out code, no dead code

**Subtotal: ___ / 5**

---

## Final Score Calculation

| # | Category | Max | Score |
|---|---|---|---|
| 1 | Architecture & System Design | 15 | |
| 2 | Full-Stack Implementation | 15 | |
| 3 | Custom AI Agent System | 15 | |
| 4 | RAG Implementation | 10 | |
| 5 | Data Persistence & Modeling | 10 | |
| 6 | Security & Authentication | 10 | |
| 7 | Testing & Quality | 10 | |
| 8 | Containerization & DevOps | 5 | |
| 9 | Observability | 5 | |
| 10 | Code Quality & Standards | 5 | |
| | **TOTAL** | **100** | **___** |

---

## Grade Assignment

| Grade | Range | Meaning |
|---|---|---|
| **A+** | 95–100 | Exceeds Micron expectations |
| **A** | 90–94 | Meets Micron Senior level |
| **B+** | 85–89 | Meets Micron Mid level |
| **B** | 80–84 | Meets Micron Entry level |
| **C** | 70–79 | Needs improvement |
| **F** | <70 | Does not meet minimum bar |

### Final Grade: ___________

---

## Reviewer Notes

| Area | Strengths | Improvement Areas |
|---|---|---|
| Architecture | | |
| Full-Stack | | |
| AI Agent | | |
| RAG | | |
| Data | | |
| Security | | |
| Testing | | |
| DevOps | | |
| Observability | | |
| Code Quality | | |

### Overall Assessment:

_Write a brief paragraph summarizing the submission's strengths, weaknesses, and overall readiness for a Micron AI Full Stack Engineer role._
