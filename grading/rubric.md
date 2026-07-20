# MicroForge — Grading Rubric

> **Total Points:** 100  
> **Version:** 1.0  
> **Last Updated:** 2026-07-14  
> **Benchmark:** Micron AI Full Stack Engineer Role

---

## Grade Scale

| Grade | Points | Interpretation |
|---|---|---|
| **A+** | 95–100 | Exceeds Micron expectations — demonstrates senior+ engineering maturity |
| **A** | 90–94 | Meets Micron Senior level — production-quality work with strong design decisions |
| **B+** | 85–89 | Meets Micron Mid level — solid implementation with minor gaps |
| **B** | 80–84 | Meets Micron Entry level — functional implementation with room for growth |
| **C** | 70–79 | Needs improvement — core functionality works but lacks polish and depth |
| **F** | <70 | Does not meet minimum bar — significant gaps in implementation or understanding |

---

## Category 1: Architecture & System Design (15 Points)

### Criteria

Evaluates the structural quality of the codebase: how well concerns are separated, how maintainable and extensible the system is, and whether industry-standard design patterns are applied correctly.

### Point Breakdown

| Points | Level | Description |
|---|---|---|
| **14–15** | Exceptional | Clean Architecture fully implemented with Domain, Application, Infrastructure, and Web layers. Dependency injection used throughout. SOLID principles evident in every layer. Interfaces define all contracts. Design patterns (Repository, Unit of Work, Strategy, Mediator) applied correctly and purposefully. Architecture supports future extension without modification. |
| **11–13** | Proficient | Layered architecture with clear separation. DI used correctly. Most SOLID principles followed. Some design patterns applied. Minor coupling between layers in a few places. |
| **8–10** | Adequate | Basic project separation exists but boundaries are blurred. DI used but not consistently. Some business logic leaks into controllers or infrastructure. |
| **5–7** | Below Average | Minimal separation of concerns. Most logic in controllers or a single project. Few or no design patterns. Tight coupling throughout. |
| **0–4** | Insufficient | Monolithic structure with no architectural thought. Logic scattered randomly. No dependency injection. |

### What to Look For

- [ ] Solution has separate projects for Domain, Application, Infrastructure, and Web
- [ ] Domain layer has ZERO dependencies on infrastructure (EF Core, HTTP, etc.)
- [ ] All cross-layer dependencies flow inward (Web → Application → Domain)
- [ ] Interfaces defined in Domain/Application, implemented in Infrastructure
- [ ] `IServiceCollection` extensions used for clean DI registration
- [ ] Repository pattern or similar data access abstraction
- [ ] MediatR used for CQRS-style command/query separation
- [ ] Configuration is centralized and uses `IOptions<T>` pattern
- [ ] No `new` keyword for services — all resolved via DI

### Common Pitfalls (Point Deductions)

| Pitfall | Deduction |
|---|---|
| DbContext injected directly into Controllers | -3 |
| Domain entities have EF Core attributes (data annotations for DB) | -2 |
| Business logic in Controller action methods | -3 |
| No interface abstractions — concrete class dependencies everywhere | -3 |
| Circular project references | -4 |
| God class (single class > 500 lines doing everything) | -3 |

---

## Category 2: Full-Stack Implementation (15 Points)

### Criteria

Evaluates the quality and completeness of both the ASP.NET Core API and the Blazor Server UI, including proper HTTP semantics, real-time functionality, and user experience.

### Point Breakdown

| Points | Level | Description |
|---|---|---|
| **14–15** | Exceptional | API follows RESTful conventions perfectly — correct HTTP methods, status codes (201 Created, 204 No Content, 404, 409), content negotiation. Blazor UI is responsive, functional, and well-organized with reusable components. SignalR hubs push real-time sensor data and alerts. Error handling returns RFC 7807 Problem Details. Pagination, filtering, and sorting on list endpoints. |
| **11–13** | Proficient | API is RESTful with correct status codes for most scenarios. Blazor UI is functional with decent component structure. SignalR works for at least one real-time feature. Minor HTTP semantic issues. |
| **8–10** | Adequate | API works but returns 200 for everything. Blazor UI is basic but functional. SignalR may be incomplete. Limited error handling. |
| **5–7** | Below Average | API has significant REST violations. Blazor UI is minimal placeholder pages. No real-time features. |
| **0–4** | Insufficient | API barely functional. No Blazor UI or non-functional pages. |

### What to Look For

**API Quality:**
- [ ] Consistent resource naming (`/api/production-lines`, not `/api/getProductionLines`)
- [ ] Correct HTTP verbs (GET for reads, POST for creates, PUT for updates, DELETE for deletes)
- [ ] Proper status codes: `201 Created` with Location header, `204 No Content` for deletes
- [ ] `404 Not Found` for missing resources, `409 Conflict` for duplicates
- [ ] RFC 7807 Problem Details for error responses
- [ ] Pagination support (`?page=1&pageSize=20`) on list endpoints
- [ ] Request validation with meaningful error messages
- [ ] Swagger/OpenAPI documentation

**Blazor UI:**
- [ ] Dashboard page with real-time sensor data visualization
- [ ] Agent chat interface with message history
- [ ] Document upload and search interface
- [ ] Production run management (CRUD)
- [ ] Reusable Blazor components (not everything in one page)
- [ ] Loading states and error boundaries
- [ ] Responsive layout

**SignalR:**
- [ ] At least one hub for real-time sensor data
- [ ] Alert notifications pushed to connected clients
- [ ] Proper hub lifecycle management (OnConnectedAsync, OnDisconnectedAsync)

### Common Pitfalls (Point Deductions)

| Pitfall | Deduction |
|---|---|
| All endpoints return `200 OK` regardless of outcome | -3 |
| No input validation on POST/PUT endpoints | -2 |
| Blazor pages are empty scaffolds with no functionality | -4 |
| No Swagger/OpenAPI documentation | -1 |
| SignalR not implemented at all | -3 |
| API returns exceptions/stack traces to clients | -3 |

---

## Category 3: Custom AI Agent System (15 Points)

### Criteria

Evaluates the design and implementation of the MCP-style AI agent system — the most distinctive feature of MicroForge. Tests understanding of tool-use patterns, schema-driven design, agent orchestration, and responsible AI practices.

### Point Breakdown

| Points | Level | Description |
|---|---|---|
| **14–15** | Exceptional | MCP-style tool registry with JSON Schema definitions for each tool. Tools are auto-discovered or cleanly registered via DI. Agent runtime supports multi-step tool chains with conversation context. Input validation against JSON schemas. Comprehensive error handling and guardrails (max iterations, timeouts, data boundaries). Agent logs every interaction for audit. Tool results are grounded in real data. |
| **11–13** | Proficient | Tool registry implemented with schemas. Agent can invoke tools based on LLM requests. Basic multi-step support. Some guardrails in place. Logging present. |
| **8–10** | Adequate | Basic tool invocation works but no schema validation. Single-step only. Limited error handling. Few tools implemented. |
| **5–7** | Below Average | Agent system is a simple LLM wrapper with hardcoded responses. No real tool registry. No schema definitions. |
| **0–4** | Insufficient | No agent system implemented, or it's non-functional. |

### What to Look For

- [ ] `ITool` interface with `Name`, `Description`, `Schema`, `InvokeAsync`
- [ ] `IToolRegistry` that manages tool registration and discovery
- [ ] JSON Schema definitions for every tool's parameters
- [ ] Input validation against JSON schemas before tool invocation
- [ ] `IAgentRuntime` that orchestrates the LLM ↔ Tool loop
- [ ] Multi-step reasoning: agent can invoke 2+ tools sequentially to answer a query
- [ ] Execution context passes user identity and session info to tools
- [ ] Data boundary enforcement within tools (tools respect RBAC)
- [ ] Guardrails: max iterations (≤ 10), per-tool timeout (≤ 30s), total timeout (≤ 2min)
- [ ] Comprehensive audit logging to `AgentLogs` table
- [ ] At least 5 working tools covering Data, RAG, Compute, and Action categories
- [ ] Graceful error handling when LLM returns malformed tool calls

### Common Pitfalls (Point Deductions)

| Pitfall | Deduction |
|---|---|
| No JSON Schema definitions for tools | -4 |
| Tools have no error handling (exceptions bubble up) | -3 |
| No guardrails — agent could loop forever | -3 |
| Agent doesn't log interactions | -2 |
| Only 1-2 trivial tools implemented | -3 |
| Tools don't respect user RBAC/data boundaries | -3 |
| LLM API key hardcoded in source code | -2 |

---

## Category 4: RAG Implementation (10 Points)

### Criteria

Evaluates the Retrieval-Augmented Generation pipeline — document ingestion, chunking strategy, embedding generation, vector storage, semantic search, and grounded response generation.

### Point Breakdown

| Points | Level | Description |
|---|---|---|
| **10** | Exceptional | Complete RAG pipeline: upload → chunk → embed → store → search → ground. Chunking strategy is configurable with sensible defaults (500 tokens, 50 overlap). Embeddings generated via local model or API. Semantic search returns ranked results with similarity scores. LLM responses are explicitly grounded with source citations. Handles multiple document formats. |
| **8–9** | Proficient | RAG pipeline works end-to-end. Chunking and embedding implemented. Semantic search returns relevant results. Some grounding in LLM responses. |
| **6–7** | Adequate | Documents can be uploaded and chunked. Basic search works but may not use embeddings. Limited grounding. |
| **4–5** | Below Average | Document upload works but no embedding or semantic search. Keyword search only. |
| **0–3** | Insufficient | No RAG pipeline or non-functional implementation. |

### What to Look For

- [ ] `IDocumentChunker` with recursive text splitting
- [ ] Configurable chunk size and overlap
- [ ] `IEmbeddingService` that generates vector representations
- [ ] `IVectorStore` with HNSW or similar approximate nearest neighbor index
- [ ] Cosine similarity (or equivalent) for search ranking
- [ ] Top-K retrieval with configurable K
- [ ] Document metadata preserved through the pipeline
- [ ] LLM responses include source document citations
- [ ] Search endpoint returns similarity scores
- [ ] Performance metrics logged (embedding time, search time)

### Common Pitfalls (Point Deductions)

| Pitfall | Deduction |
|---|---|
| No actual embedding — using keyword matching only | -4 |
| No chunking — entire documents passed as single vectors | -3 |
| No similarity scores returned in search results | -1 |
| LLM responses not grounded — no source citations | -2 |
| Vector store is just a linear scan (no indexing) | -1 |
| No handling of empty/malformed documents | -1 |

---

## Category 5: Data Persistence & Modeling (10 Points)

### Criteria

Evaluates EF Core usage, database schema design, migration strategy, query optimization, and caching patterns.

### Point Breakdown

| Points | Level | Description |
|---|---|---|
| **10** | Exceptional | Properly normalized schema (3NF+). Code-first with clean migrations. Fluent API configurations in separate `IEntityTypeConfiguration` classes. Efficient queries using projections (`.Select()`) and includes. Pagination at the database level. Caching strategy for read-heavy data with proper invalidation. Indexes defined for all query patterns. Audit fields (CreatedAt, UpdatedAt) on all entities. |
| **8–9** | Proficient | Good schema design. Migrations work. Most queries are efficient. Some caching. Indexes present. |
| **6–7** | Adequate | Schema works but may have normalization issues. Migrations exist. Some N+1 queries. Basic or no caching. |
| **4–5** | Below Average | Schema is functional but poorly designed. No migrations — manual DB setup. Inefficient queries. No caching. |
| **0–3** | Insufficient | No database implementation or severe design flaws. |

### What to Look For

- [ ] Separate `IEntityTypeConfiguration<T>` classes for each entity
- [ ] Proper relationships defined (one-to-many, many-to-many via join table)
- [ ] Indexes defined for foreign keys and common query patterns
- [ ] Composite indexes where appropriate (e.g., `ProductionLineId + Timestamp`)
- [ ] Migrations committed and functional (`dotnet ef migrations list`)
- [ ] Projections used for list queries (`.Select()` to DTOs, not loading full entities)
- [ ] Pagination implemented at the DB level (`.Skip().Take()`)
- [ ] `IMemoryCache` used for reference data (roles, production line lists)
- [ ] Cache invalidation on writes
- [ ] No N+1 query problems (`.Include()` or explicit joins)
- [ ] Audit fields populated via `SaveChangesAsync` override

### Common Pitfalls (Point Deductions)

| Pitfall | Deduction |
|---|---|
| Loading entire tables into memory for filtering | -3 |
| N+1 queries in loops (no `.Include()` or join) | -2 |
| No migrations — using `EnsureCreated()` | -2 |
| All entity configurations in `OnModelCreating` (not separated) | -1 |
| No indexes beyond primary keys | -1 |
| Storing embeddings as serialized strings without consideration | -1 |

---

## Category 6: Security & Authentication (10 Points)

### Criteria

Evaluates JWT implementation correctness, RBAC enforcement, data boundary policies, input validation, and awareness of common security vulnerabilities.

### Point Breakdown

| Points | Level | Description |
|---|---|---|
| **10** | Exceptional | JWT with proper claims, short expiry, refresh token rotation. RBAC policies enforced on every endpoint. Data boundaries enforced at the query level. Input validation on all endpoints. Passwords hashed with BCrypt. No secrets in source code. CORS configured. Rate limiting on auth endpoints. Demonstrates OWASP Top 10 awareness. |
| **8–9** | Proficient | JWT works correctly. RBAC on most endpoints. Data boundaries implemented. Input validation present. Passwords properly hashed. |
| **6–7** | Adequate | JWT authentication works. Basic role checks. Some input validation. Passwords hashed but configuration could be stronger. |
| **4–5** | Below Average | Authentication exists but has flaws (e.g., no token expiry, secrets in code). Role checks inconsistent. |
| **0–3** | Insufficient | No authentication, or severely broken implementation. |

### What to Look For

- [ ] JWT token generation with proper claims (`sub`, `roles`, `productionLineIds`, `exp`, `iss`, `aud`)
- [ ] Token expiry ≤ 1 hour
- [ ] Refresh token mechanism with single-use rotation
- [ ] BCrypt password hashing (work factor ≥ 10)
- [ ] `[Authorize]` attribute or policy on ALL non-public endpoints
- [ ] Role-based policies: `[Authorize(Roles = "Admin,Engineer")]`
- [ ] Custom `IAuthorizationHandler` for data boundary enforcement
- [ ] Input validation via `FluentValidation` or `DataAnnotations`
- [ ] Secrets loaded from environment variables or user-secrets (NOT hardcoded)
- [ ] CORS configured to restrict origins
- [ ] Rate limiting middleware on `/api/auth/*` endpoints
- [ ] No sensitive data (passwords, tokens) in log output

### Common Pitfalls (Point Deductions)

| Pitfall | Deduction |
|---|---|
| JWT secret key hardcoded in `appsettings.json` | -3 |
| No token expiration or very long expiry (> 24h) | -2 |
| Passwords stored in plain text or weak hash (MD5, SHA) | -4 |
| Endpoints missing `[Authorize]` attribute | -2 |
| No input validation — trusting all client input | -2 |
| CORS set to allow all origins (`*`) | -1 |
| Returning stack traces or internal details in error responses | -1 |

---

## Category 7: Testing & Quality (10 Points)

### Criteria

Evaluates test coverage, test quality, and the overall code quality assurance strategy including unit tests, integration tests, and the agent test harness.

### Point Breakdown

| Points | Level | Description |
|---|---|---|
| **10** | Exceptional | > 70% code coverage. Unit tests for all services, validators, and tools. Integration tests for API endpoints using `WebApplicationFactory`. Agent test harness that validates tool invocations with mock LLM. Tests follow Arrange-Act-Assert. Meaningful test names. Edge cases covered (empty inputs, boundary values, error conditions). |
| **8–9** | Proficient | > 50% coverage. Unit and integration tests present. Good coverage of core business logic. Agent tests exist. Some edge cases. |
| **6–7** | Adequate | > 30% coverage. Some unit tests for key services. Basic integration tests. Agent testing minimal. |
| **4–5** | Below Average | < 30% coverage. Few tests, mostly happy-path. No integration tests. No agent tests. |
| **0–3** | Insufficient | No tests or only auto-generated scaffolding tests. |

### What to Look For

- [ ] Separate test projects: `UnitTests`, `IntegrationTests`, `AgentTests`
- [ ] Unit tests for: services, validators, tools, anomaly detector, chunker
- [ ] Integration tests using `WebApplicationFactory<Program>` with in-memory DB
- [ ] Agent test harness: mock LLM responses, verify tool selection and invocation
- [ ] AAA pattern (Arrange-Act-Assert) consistently followed
- [ ] Descriptive test names: `Should_ReturnNotFound_When_ProductionLineDoesNotExist`
- [ ] Edge cases: null inputs, empty collections, boundary values, unauthorized access
- [ ] Test data generated with Bogus or similar
- [ ] Code coverage report generated and > 70%
- [ ] No test interdependencies (each test is isolated)

### Common Pitfalls (Point Deductions)

| Pitfall | Deduction |
|---|---|
| No test projects at all | -8 |
| Tests exist but don't actually assert anything | -3 |
| Only happy-path tests — no error case coverage | -2 |
| Tests depend on external resources (real DB, real LLM) | -2 |
| No integration tests — only unit tests | -2 |
| Test names are meaningless (`Test1`, `Test2`) | -1 |

---

## Category 8: Containerization & DevOps (5 Points)

### Criteria

Evaluates Docker configuration quality, docker-compose setup, health checks, and environment configuration management.

### Point Breakdown

| Points | Level | Description |
|---|---|---|
| **5** | Exceptional | Multi-stage Dockerfile with clean build/runtime separation. Docker-compose with volumes, health checks, environment variables, and restart policy. `.dockerignore` configured. Container runs as non-root user. Image size optimized. Environment-specific configuration via env vars. |
| **4** | Proficient | Dockerfile works with multi-stage build. Docker-compose functional with volumes and basic health check. |
| **3** | Adequate | Dockerfile builds and runs. Docker-compose exists but minimal configuration. |
| **2** | Below Average | Dockerfile exists but has issues (e.g., includes SDK in runtime image). No docker-compose. |
| **0–1** | Insufficient | No Docker configuration, or it doesn't work. |

### What to Look For

- [ ] Multi-stage Dockerfile (SDK build → ASP.NET runtime)
- [ ] `.dockerignore` excludes `bin/`, `obj/`, `.git/`, etc.
- [ ] Docker-compose with named volumes for data and logs
- [ ] Health check defined in Dockerfile or docker-compose
- [ ] Environment variables for all configuration (no hardcoded values)
- [ ] Container runs as non-root user
- [ ] `EXPOSE` statement for relevant ports
- [ ] Restart policy (`unless-stopped` or `on-failure`)
- [ ] `docker compose up` works from a clean clone

### Common Pitfalls (Point Deductions)

| Pitfall | Deduction |
|---|---|
| Using SDK image for runtime (image > 1GB) | -2 |
| No `.dockerignore` file | -1 |
| No volumes — data lost on container restart | -1 |
| Health check missing | -1 |
| Hardcoded secrets in docker-compose.yml | -2 |

---

## Category 9: Observability (5 Points)

### Criteria

Evaluates structured logging, health check implementation, metrics collection, and diagnostic capabilities.

### Point Breakdown

| Points | Level | Description |
|---|---|---|
| **5** | Exceptional | Serilog configured with structured JSON logging. Request/response logging middleware. Correlation IDs on all requests. Health checks for DB connectivity and critical services. Custom metrics (request count, latency, active connections). Diagnostic endpoint for system status. Sensitive data excluded from logs. |
| **4** | Proficient | Serilog configured with console and file sinks. Health checks implemented. Basic metrics. Correlation IDs present. |
| **3** | Adequate | Logging is structured but basic. Health check exists. No custom metrics. |
| **2** | Below Average | Default `Console.WriteLine` or basic `ILogger` usage. No health checks. |
| **0–1** | Insufficient | No meaningful logging or observability. |

### What to Look For

- [ ] Serilog configured in `Program.cs` with appropriate sinks
- [ ] Structured log messages with properties (not string concatenation)
- [ ] Request logging middleware (`app.UseSerilogRequestLogging()`)
- [ ] Correlation ID middleware for request tracing
- [ ] Sensitive data excluded (passwords, tokens, PII) via destructuring policies
- [ ] Health check endpoint at `/health` with DB connectivity check
- [ ] Readiness endpoint at `/ready` with service dependency checks
- [ ] Log levels used correctly (Debug, Information, Warning, Error, Fatal)
- [ ] Error logging includes exception details and context

### Common Pitfalls (Point Deductions)

| Pitfall | Deduction |
|---|---|
| Using `Console.WriteLine` instead of structured logging | -3 |
| Logging passwords, tokens, or PII | -2 |
| No health check endpoints | -1 |
| String concatenation in log messages instead of structured templates | -1 |

---

## Category 10: Code Quality & Standards (5 Points)

### Criteria

Evaluates naming conventions, SOLID principle adherence, code documentation, and Git commit quality.

### Point Breakdown

| Points | Level | Description |
|---|---|---|
| **5** | Exceptional | Consistent C# naming conventions (PascalCase types, camelCase locals, `_camelCase` fields). XML documentation on all public APIs. SOLID principles evident throughout. Clean Git history with conventional commits. `.editorconfig` or `Directory.Build.props` for consistency. No dead code, no warnings. README is comprehensive. |
| **4** | Proficient | Consistent naming. Some XML documentation. SOLID mostly followed. Reasonable Git history. |
| **3** | Adequate | Naming is mostly consistent. Minimal documentation. Some SOLID violations. Commit messages are descriptive. |
| **2** | Below Average | Inconsistent naming. No documentation. SOLID violations common. Commit messages are vague ("fix stuff"). |
| **0–1** | Insufficient | Code is unreadable. No conventions followed. Single "initial commit" with all code. |

### What to Look For

- [ ] PascalCase for types, methods, properties, and public members
- [ ] `_camelCase` for private fields
- [ ] `camelCase` for local variables and parameters
- [ ] `I` prefix on interfaces (`IToolRegistry`, `IAgentRuntime`)
- [ ] XML doc comments (`///`) on public interfaces and complex methods
- [ ] No magic numbers — use constants or enums
- [ ] Single Responsibility Principle: classes focused on one concern
- [ ] Open/Closed Principle: extensible via interfaces, not modification
- [ ] Git history shows incremental development, not a single commit dump
- [ ] Conventional commit messages: `feat:`, `fix:`, `refactor:`, `test:`, `docs:`
- [ ] No compiler warnings
- [ ] No commented-out code blocks
- [ ] `.editorconfig` present with style rules

### Common Pitfalls (Point Deductions)

| Pitfall | Deduction |
|---|---|
| Entire project in a single commit | -2 |
| Inconsistent naming conventions throughout | -2 |
| Dead code / commented-out blocks throughout | -1 |
| No documentation on public APIs | -1 |
| Compiler warnings ignored | -1 |

---

## Scoring Summary Template

| # | Category | Max Points | Score | Notes |
|---|---|---|---|---|
| 1 | Architecture & System Design | 15 | ___ | |
| 2 | Full-Stack Implementation | 15 | ___ | |
| 3 | Custom AI Agent System | 15 | ___ | |
| 4 | RAG Implementation | 10 | ___ | |
| 5 | Data Persistence & Modeling | 10 | ___ | |
| 6 | Security & Authentication | 10 | ___ | |
| 7 | Testing & Quality | 10 | ___ | |
| 8 | Containerization & DevOps | 5 | ___ | |
| 9 | Observability | 5 | ___ | |
| 10 | Code Quality & Standards | 5 | ___ | |
| | **TOTAL** | **100** | **___** | |

### Final Grade: ___

---

## Micron Skill Mapping

This rubric maps directly to the skills tested in a Micron AI Full Stack Engineer interview:

| Micron Skill Area | Rubric Categories |
|---|---|
| Full-Stack Development (C#/.NET) | Categories 1, 2, 5 |
| AI/ML Integration | Categories 3, 4 |
| System Design & Architecture | Category 1 |
| Data Engineering | Categories 4, 5 |
| Security & Compliance | Category 6 |
| Testing & Quality Assurance | Category 7 |
| DevOps & Containerization | Category 8 |
| Monitoring & Observability | Category 9 |
| Professional Engineering Practices | Category 10 |
| Real-Time Systems | Categories 2 (SignalR), 3 (Event Pipeline) |
