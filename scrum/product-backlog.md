# MicroForge — Product Backlog

> **Project:** MicroForge — AI-Powered Manufacturing Intelligence Platform
> **Last Updated:** 2026-07-14
> **Product Owner / Solo Developer:** Mike
> **AI Pair:** Nemotron3-Super (Local)
> **Estimation Scale:** Fibonacci (1, 2, 3, 5, 8, 13)
> **Prioritization:** MoSCoW (Must Have · Should Have · Could Have)

---

## Table of Contents

1. [Epic 1 — Platform Foundation](#epic-1--platform-foundation)
2. [Epic 2 — Manufacturing Dashboard](#epic-2--manufacturing-dashboard)
3. [Epic 3 — AI Agent Framework](#epic-3--ai-agent-framework)
4. [Epic 4 — RAG Document Intelligence](#epic-4--rag-document-intelligence)
5. [Epic 5 — Production Event Pipeline](#epic-5--production-event-pipeline)
6. [Epic 6 — Observability & Hardening](#epic-6--observability--hardening)
7. [Epic 7 — Testing & Quality](#epic-7--testing--quality)
8. [Backlog Summary](#backlog-summary)

---

## Epic 1 — Platform Foundation

> *Establish the project skeleton, authentication, database schema, and containerization so that all subsequent features build on a stable, secure, containerized base.*

---

### MF-001 · Solution Scaffolding & Project Structure

| Field | Value |
|---|---|
| **Epic** | Platform Foundation |
| **Story** | As a **developer**, I want a well-organized .NET 8 solution with separate projects for API, Blazor UI, Domain, Infrastructure, and Tests, so that the codebase is maintainable and follows clean architecture. |
| **Story Points** | 3 |
| **Priority** | Must Have |
| **Dependencies** | None |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the repo is cloned, **When** `dotnet build` is run at the solution root, **Then** the solution compiles with zero errors and zero warnings treated as errors. |
| AC-2 | **Given** the solution structure, **When** inspected, **Then** it contains at minimum: `MicroForge.Api`, `MicroForge.Blazor`, `MicroForge.Domain`, `MicroForge.Infrastructure`, `MicroForge.Tests.Unit`, `MicroForge.Tests.Integration`. |
| AC-3 | **Given** the solution, **When** project references are reviewed, **Then** dependency flow is Domain ← Infrastructure ← Api/Blazor (no reverse references). |

---

### MF-002 · SQLite Database Schema & EF Core Setup

| Field | Value |
|---|---|
| **Epic** | Platform Foundation |
| **Story** | As a **developer**, I want Entity Framework Core configured with SQLite and initial migrations for core entities (ProductionLine, ProductionRun, DefectRecord, User, Role), so that the system has a relational data store ready for feature development. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-001 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the Infrastructure project, **When** `dotnet ef migrations list` is run, **Then** at least one initial migration exists and applies cleanly to a fresh SQLite database. |
| AC-2 | **Given** the schema, **When** inspected, **Then** tables exist for `ProductionLines`, `ProductionRuns`, `DefectRecords`, `Users`, `Roles`, and `UserRoles` with appropriate foreign keys and indexes. |
| AC-3 | **Given** a seed script, **When** the application starts for the first time, **Then** demo data (≥ 3 production lines, ≥ 1 admin user) is populated. |

---

### MF-003 · JWT Authentication Service

| Field | Value |
|---|---|
| **Epic** | Platform Foundation |
| **Story** | As a **user**, I want to authenticate with a username and password and receive a JWT token, so that I can securely access protected API endpoints. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-002 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** valid credentials, **When** `POST /api/v1/auth/login` is called, **Then** a 200 response is returned containing `accessToken` (JWT) and `expiresAt` (UTC timestamp). |
| AC-2 | **Given** invalid credentials, **When** `POST /api/v1/auth/login` is called, **Then** a 401 response is returned with a generic error message (no credential leakage). |
| AC-3 | **Given** a valid JWT, **When** any `[Authorize]` endpoint is called with the token in the `Authorization: Bearer` header, **Then** the request is authenticated and the user's claims are available. |
| AC-4 | **Given** an expired JWT, **When** any protected endpoint is called, **Then** a 401 response is returned. |

---

### MF-004 · Role-Based Authorization & Data Boundaries

| Field | Value |
|---|---|
| **Epic** | Platform Foundation |
| **Story** | As an **admin**, I want role-based access control (Operator, Engineer, Admin) with data boundary enforcement, so that users only see production lines they are assigned to. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-003 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** an Operator assigned to Line-A, **When** they request production data, **Then** only Line-A data is returned. |
| AC-2 | **Given** an Engineer, **When** they attempt to access an Admin-only endpoint (e.g., user management), **Then** a 403 Forbidden response is returned. |
| AC-3 | **Given** an Admin, **When** they request production data, **Then** data for all lines is returned. |
| AC-4 | **Given** a user with role `Operator`, **When** their JWT is decoded, **Then** claims include `role: Operator` and `assignedLines: [...]`. |

---

### MF-005 · Docker & docker-compose Configuration

| Field | Value |
|---|---|
| **Epic** | Platform Foundation |
| **Story** | As a **developer**, I want a multi-stage Dockerfile and docker-compose.yml that builds and runs the entire MicroForge stack (API + Blazor + SQLite volume), so that the system is portable and reproducible. |
| **Story Points** | 3 |
| **Priority** | Must Have |
| **Dependencies** | MF-001 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** a clean machine with Docker installed, **When** `docker-compose up --build` is run, **Then** the API is reachable at `http://localhost:5000` and Blazor at `http://localhost:5001` within 60 seconds. |
| AC-2 | **Given** the Dockerfile, **When** the image is built, **Then** a multi-stage build is used (SDK → Runtime) and the final image is ≤ 250 MB. |
| AC-3 | **Given** `docker-compose down && docker-compose up`, **When** the database volume is mounted, **Then** data persists across restarts. |

---

### MF-006 · In-Memory Cache Service

| Field | Value |
|---|---|
| **Epic** | Platform Foundation |
| **Story** | As a **developer**, I want a simple in-memory caching service with TTL support (Redis-style interface), so that hot data (e.g., current line status) is served without repeated database hits. |
| **Story Points** | 3 |
| **Priority** | Should Have |
| **Dependencies** | MF-001 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** an `ICacheService` interface, **When** `SetAsync<T>(key, value, ttl)` is called, **Then** the value is retrievable via `GetAsync<T>(key)` within the TTL window. |
| AC-2 | **Given** a cached item with a 30-second TTL, **When** 31 seconds elapse, **Then** `GetAsync` returns `null`. |
| AC-3 | **Given** the cache service, **When** registered in DI, **Then** it is registered as a singleton and is thread-safe. |

---

## Epic 2 — Manufacturing Dashboard

> *Build a real-time Blazor Server dashboard showing production line status, defect rates, and yield metrics with live SignalR updates.*

---

### MF-007 · Blazor Server Shell & Navigation

| Field | Value |
|---|---|
| **Epic** | Manufacturing Dashboard |
| **Story** | As a **user**, I want a responsive Blazor Server application with a sidebar navigation (Dashboard, Documents, Events, Admin), so that I can navigate the system intuitively. |
| **Story Points** | 3 |
| **Priority** | Must Have |
| **Dependencies** | MF-001 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the Blazor app, **When** loaded in a browser, **Then** a sidebar with navigation links for Dashboard, Documents, Events, and Admin is visible. |
| AC-2 | **Given** a non-admin user, **When** they view the sidebar, **Then** the Admin link is hidden. |
| AC-3 | **Given** any page, **When** the browser is resized to mobile width (< 768px), **Then** the sidebar collapses into a hamburger menu. |

---

### MF-008 · Production Line Status Cards

| Field | Value |
|---|---|
| **Epic** | Manufacturing Dashboard |
| **Story** | As an **operator**, I want to see status cards for each production line I'm assigned to — showing current status (Running/Idle/Down), current yield %, and defect count for the shift — so that I have an at-a-glance view. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-004, MF-007 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** an authenticated operator assigned to 2 lines, **When** the Dashboard loads, **Then** exactly 2 status cards are rendered. |
| AC-2 | **Given** a status card, **When** rendered, **Then** it displays: Line Name, Status badge (color-coded: green/yellow/red), Yield %, Defect Count, Last Updated timestamp. |
| AC-3 | **Given** a line status change, **When** a SignalR event is received, **Then** the affected card updates within 2 seconds without a full page reload. |

---

### MF-009 · Real-Time SignalR Hub for Dashboard Updates

| Field | Value |
|---|---|
| **Epic** | Manufacturing Dashboard |
| **Story** | As a **developer**, I want a SignalR hub (`/hubs/dashboard`) that pushes production line status updates and defect events to connected Blazor clients, so that the dashboard reflects near-real-time state. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-007 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** a Blazor client, **When** the Dashboard page is loaded, **Then** a SignalR connection to `/hubs/dashboard` is established and confirmed via connection status indicator. |
| AC-2 | **Given** a connected client, **When** a `LineStatusChanged` event is broadcast, **Then** the client receives the payload containing `lineId`, `status`, `yield`, `defectCount`, `timestamp`. |
| AC-3 | **Given** a SignalR connection drop, **When** the connection is lost, **Then** the client attempts automatic reconnection with exponential backoff (max 3 retries) and displays a "Reconnecting..." banner. |

---

### MF-010 · Yield & Defect Charts

| Field | Value |
|---|---|
| **Epic** | Manufacturing Dashboard |
| **Story** | As an **engineer**, I want interactive charts showing yield trends (line chart) and defect distribution by category (bar chart) for the past 7 days, so that I can identify patterns. |
| **Story Points** | 5 |
| **Priority** | Should Have |
| **Dependencies** | MF-008 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the Dashboard, **When** an engineer views the charts section, **Then** a yield trend line chart with data points for the last 7 days is rendered. |
| AC-2 | **Given** the charts section, **When** rendered, **Then** a defect distribution bar chart groups defects by category (Cosmetic, Functional, Dimensional, Contamination). |
| AC-3 | **Given** a chart, **When** a user hovers over a data point, **Then** a tooltip displays the exact value and date. |
| AC-4 | **Given** the charts, **When** data boundary enforcement is active, **Then** charts only display data for the user's assigned lines. |

---

### MF-011 · Shift Report Summary View

| Field | Value |
|---|---|
| **Epic** | Manufacturing Dashboard |
| **Story** | As a **shift supervisor**, I want to view a summary report for the current or previous shift (total units produced, total defects, yield, top defect categories), so that I can hand off to the next shift. |
| **Story Points** | 3 |
| **Priority** | Should Have |
| **Dependencies** | MF-008 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the Dashboard, **When** the Shift Report tab is selected, **Then** summary metrics (Total Units, Total Defects, Yield %, Top 3 Defect Categories) are displayed for the current shift. |
| AC-2 | **Given** the Shift Report, **When** the user selects "Previous Shift" from a dropdown, **Then** the metrics refresh to show the prior shift's data. |
| AC-3 | **Given** the Shift Report data, **When** the API is queried (`GET /api/v1/reports/shift`), **Then** the response returns within 500ms. |

---

## Epic 3 — AI Agent Framework

> *Build a pluggable agent framework with MCP-style structured tool interfaces that an LLM can invoke to query, predict, and recommend.*

---

### MF-012 · Agent Runtime & Tool Registry

| Field | Value |
|---|---|
| **Epic** | AI Agent Framework |
| **Story** | As a **developer**, I want an agent runtime that discovers, registers, and manages tools via an `IToolRegistry`, where each tool is a class implementing `IAgentTool` with a JSON schema describing inputs/outputs, so that the agent framework is extensible. |
| **Story Points** | 8 |
| **Priority** | Must Have |
| **Dependencies** | MF-001 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the `IToolRegistry`, **When** `GetAllTools()` is called, **Then** a list of all registered tools is returned, each with `Name`, `Description`, `InputSchema` (JSON Schema), and `OutputSchema` (JSON Schema). |
| AC-2 | **Given** a tool class implementing `IAgentTool`, **When** it is placed in the designated assembly, **Then** it is auto-discovered and registered at startup via reflection or explicit registration. |
| AC-3 | **Given** a tool invocation request `{ "tool": "QueryDefectHistory", "input": {...} }`, **When** `ExecuteToolAsync(name, input)` is called on the runtime, **Then** the correct tool is resolved, input is validated against the schema, and the tool's `ExecuteAsync` method is invoked. |
| AC-4 | **Given** invalid input for a tool, **When** `ExecuteToolAsync` is called, **Then** a structured validation error is returned (not an unhandled exception). |

---

### MF-013 · QueryDefectHistory Tool

| Field | Value |
|---|---|
| **Epic** | AI Agent Framework |
| **Story** | As an **AI agent**, I want a `QueryDefectHistory` tool that accepts `lineId`, `startDate`, `endDate`, and optional `defectCategory`, and returns historical defect records, so that I can answer questions about defect trends. |
| **Story Points** | 3 |
| **Priority** | Must Have |
| **Dependencies** | MF-012, MF-002 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** valid inputs (`lineId`, date range), **When** the tool is executed, **Then** it returns an array of defect records with `id`, `lineId`, `category`, `severity`, `timestamp`, `description`. |
| AC-2 | **Given** a `defectCategory` filter, **When** applied, **Then** only defects matching that category are returned. |
| AC-3 | **Given** no defects in the date range, **When** the tool is executed, **Then** an empty array is returned (not an error). |
| AC-4 | **Given** the tool's JSON schema, **When** inspected, **Then** `lineId` (string, required), `startDate` (ISO 8601, required), `endDate` (ISO 8601, required), `defectCategory` (string, optional) are documented. |

---

### MF-014 · PredictYield Tool

| Field | Value |
|---|---|
| **Epic** | AI Agent Framework |
| **Story** | As an **AI agent**, I want a `PredictYield` tool that accepts `lineId` and `shiftDate` and returns a predicted yield percentage based on historical averages and recent trends, so that I can provide forward-looking insights. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-012, MF-002 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** a `lineId` and `shiftDate`, **When** the tool is executed, **Then** it returns `{ predictedYield: 94.2, confidence: "medium", basis: "7-day rolling average" }`. |
| AC-2 | **Given** insufficient historical data (< 3 data points), **When** the tool is executed, **Then** it returns `{ predictedYield: null, confidence: "insufficient_data", basis: "N/A" }`. |
| AC-3 | **Given** the prediction algorithm, **When** executed, **Then** it uses a weighted 7-day rolling average (recent days weighted higher). |

---

### MF-015 · SummarizeShiftReport Tool

| Field | Value |
|---|---|
| **Epic** | AI Agent Framework |
| **Story** | As an **AI agent**, I want a `SummarizeShiftReport` tool that accepts `lineId` and `shiftDate` and returns a structured summary (total units, defects by category, yield, anomalies detected), so that I can generate natural-language shift summaries. |
| **Story Points** | 3 |
| **Priority** | Must Have |
| **Dependencies** | MF-012, MF-002 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** valid inputs, **When** the tool is executed, **Then** it returns a JSON object with `totalUnits`, `totalDefects`, `yieldPercent`, `defectsByCategory` (object), `anomalies` (array of strings). |
| AC-2 | **Given** no production data for the requested shift, **When** executed, **Then** it returns a structured response with zeroed metrics and an empty anomalies list. |

---

### MF-016 · RecommendMaintenance Tool

| Field | Value |
|---|---|
| **Epic** | AI Agent Framework |
| **Story** | As an **AI agent**, I want a `RecommendMaintenance` tool that accepts `lineId` and analyzes recent defect trends and anomaly frequency to generate maintenance recommendations, so that I can proactively suggest preventive actions. |
| **Story Points** | 5 |
| **Priority** | Should Have |
| **Dependencies** | MF-012, MF-013 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** a `lineId` with rising defect trends, **When** the tool is executed, **Then** it returns recommendations like `{ priority: "high", action: "Inspect calibration on Station 3", reason: "Dimensional defects up 40% over 5 days" }`. |
| AC-2 | **Given** a healthy line (no anomalies), **When** the tool is executed, **Then** it returns `{ priority: "none", action: "No maintenance recommended", reason: "All metrics within normal range" }`. |
| AC-3 | **Given** the recommendations, **When** returned, **Then** each recommendation includes `priority` (none/low/medium/high), `action` (string), and `reason` (string). |

---

### MF-017 · Agent Chat API Endpoint

| Field | Value |
|---|---|
| **Epic** | AI Agent Framework |
| **Story** | As a **user**, I want a `POST /api/v1/agent/chat` endpoint that accepts a natural language query and returns the agent's response (which may include tool invocations and results), so that I can interact with the AI agent programmatically. |
| **Story Points** | 8 |
| **Priority** | Must Have |
| **Dependencies** | MF-012, MF-013, MF-014, MF-015 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** an authenticated user, **When** `POST /api/v1/agent/chat` with `{ "message": "What were the defects on Line-A last week?" }` is called, **Then** the response includes the agent's answer grounded in tool results. |
| AC-2 | **Given** the response format, **When** inspected, **Then** it includes `{ answer: string, toolsInvoked: [{ tool: string, input: object, output: object }], sources: [] }`. |
| AC-3 | **Given** an unauthenticated request, **When** the endpoint is called, **Then** a 401 response is returned. |
| AC-4 | **Given** data boundaries, **When** an operator asks about a line they are not assigned to, **Then** the agent responds that they do not have access to that data. |

---

### MF-018 · Agent Chat UI Component

| Field | Value |
|---|---|
| **Epic** | AI Agent Framework |
| **Story** | As a **user**, I want a chat panel in the Blazor UI where I can type questions and see the agent's responses (including which tools were invoked), so that I have a conversational interface to the AI system. |
| **Story Points** | 5 |
| **Priority** | Should Have |
| **Dependencies** | MF-017, MF-007 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the Dashboard, **When** the chat icon is clicked, **Then** a slide-out chat panel appears. |
| AC-2 | **Given** the chat panel, **When** a message is submitted, **Then** a loading indicator is shown and the agent's response appears within 10 seconds. |
| AC-3 | **Given** the agent response, **When** tools were invoked, **Then** an expandable "Tools Used" section shows tool names and summarized outputs. |
| AC-4 | **Given** the chat panel, **When** the user scrolls up, **Then** previous messages in the session are visible (client-side message history). |

---

## Epic 4 — RAG Document Intelligence

> *Enable uploading, chunking, embedding, and semantic search of manufacturing SOPs/runbooks to ground AI agent responses in domain documents.*

---

### MF-019 · Document Upload API

| Field | Value |
|---|---|
| **Epic** | RAG Document Intelligence |
| **Story** | As an **engineer**, I want to upload PDF documents (SOPs, runbooks) via `POST /api/v1/documents/upload`, so that they are ingested into the knowledge base for AI-powered search. |
| **Story Points** | 3 |
| **Priority** | Must Have |
| **Dependencies** | MF-003 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** a valid PDF (≤ 10 MB), **When** uploaded via multipart form, **Then** a 201 response is returned with `{ documentId, filename, status: "processing", uploadedAt }`. |
| AC-2 | **Given** a non-PDF file, **When** uploaded, **Then** a 400 response is returned with `"Only PDF files are accepted"`. |
| AC-3 | **Given** a file > 10 MB, **When** uploaded, **Then** a 413 response is returned. |
| AC-4 | **Given** a successful upload, **When** the document record is inspected in the DB, **Then** it contains `filename`, `uploadedBy` (from JWT), `sizeBytes`, `status`, `uploadedAt`. |

---

### MF-020 · PDF Text Extraction & Chunking

| Field | Value |
|---|---|
| **Epic** | RAG Document Intelligence |
| **Story** | As a **system**, I want uploaded PDFs to be parsed into plain text and split into overlapping chunks (500 tokens, 50-token overlap), so that the text is ready for embedding. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-019 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** a multi-page PDF, **When** processing completes, **Then** the text content is extracted with page boundaries preserved as metadata. |
| AC-2 | **Given** extracted text, **When** chunked, **Then** each chunk is ≤ 500 tokens with a 50-token overlap between consecutive chunks. |
| AC-3 | **Given** each chunk, **When** stored, **Then** it includes metadata: `documentId`, `chunkIndex`, `pageNumber`, `tokenCount`. |
| AC-4 | **Given** processing completion, **When** the document status is updated, **Then** it transitions from `processing` to `ready` (or `failed` with an error message). |

---

### MF-021 · Embedding Generation & Vector Store

| Field | Value |
|---|---|
| **Epic** | RAG Document Intelligence |
| **Story** | As a **system**, I want each text chunk to be embedded into a vector representation and stored in an in-memory HNSW index (or SQLite-backed vector store), so that semantic search is possible. |
| **Story Points** | 8 |
| **Priority** | Must Have |
| **Dependencies** | MF-020 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** a text chunk, **When** embedding is generated, **Then** a float vector of dimension ≥ 384 is produced using a local embedding model or a lightweight sentence-transformer approach. |
| AC-2 | **Given** embeddings, **When** stored in the vector index, **Then** they are retrievable via approximate nearest neighbor (ANN) search. |
| AC-3 | **Given** the vector store, **When** the application restarts, **Then** previously stored embeddings are reloaded (persisted to disk). |
| AC-4 | **Given** 1000 chunks in the index, **When** a query embedding is searched, **Then** the top-K results (K=5) are returned in ≤ 200ms. |

---

### MF-022 · Semantic Search API

| Field | Value |
|---|---|
| **Epic** | RAG Document Intelligence |
| **Story** | As an **engineer**, I want to query `GET /api/v1/documents/search?q=calibration procedure` and receive the top-K most relevant document chunks ranked by similarity, so that I can find information in SOPs quickly. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-021 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** a search query, **When** `GET /api/v1/documents/search?q=...&topK=5` is called, **Then** the response contains an array of ≤ 5 results, each with `chunkText`, `documentId`, `documentName`, `pageNumber`, `similarityScore`. |
| AC-2 | **Given** results, **When** sorted, **Then** they are ordered by descending `similarityScore`. |
| AC-3 | **Given** no documents in the store, **When** searched, **Then** an empty array is returned (not an error). |
| AC-4 | **Given** the agent framework, **When** the `SummarizeShiftReport` or agent chat tool needs context, **Then** the semantic search service is callable internally as a grounding source. |

---

### MF-023 · Document Management UI

| Field | Value |
|---|---|
| **Epic** | RAG Document Intelligence |
| **Story** | As an **engineer**, I want a Documents page in the Blazor UI where I can upload PDFs, see a list of uploaded documents with their processing status, and delete documents, so that I can manage the knowledge base. |
| **Story Points** | 5 |
| **Priority** | Should Have |
| **Dependencies** | MF-019, MF-007 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the Documents page, **When** loaded, **Then** a table lists all documents with columns: Name, Uploaded By, Size, Status (processing/ready/failed), Uploaded At, Actions (Delete). |
| AC-2 | **Given** the upload form, **When** a PDF is selected and submitted, **Then** the file is uploaded and the table refreshes to show the new document with status "processing". |
| AC-3 | **Given** a document in "ready" state, **When** the Delete button is clicked and confirmed, **Then** the document and its chunks/embeddings are removed. |

---

## Epic 5 — Production Event Pipeline

> *Build an event-driven pipeline for ingesting simulated sensor data, detecting anomalies, and triggering agent-driven alerts.*

---

### MF-024 · Simulated Sensor Data Generator

| Field | Value |
|---|---|
| **Epic** | Production Event Pipeline |
| **Story** | As a **developer**, I want a background service that generates simulated sensor events (temperature, vibration, pressure) for each production line at configurable intervals, so that the pipeline has a data source for development and demo. |
| **Story Points** | 3 |
| **Priority** | Must Have |
| **Dependencies** | MF-002 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the application starts, **When** the `SensorSimulatorService` (IHostedService) is running, **Then** it publishes sensor events at a configurable interval (default: every 5 seconds per line). |
| AC-2 | **Given** a sensor event, **When** generated, **Then** it contains `lineId`, `sensorType` (Temperature/Vibration/Pressure), `value` (float), `unit`, `timestamp`. |
| AC-3 | **Given** the simulator configuration, **When** `AnomalyProbability` is set to 0.05, **Then** ~5% of events contain anomalous values (outside normal range). |

---

### MF-025 · Event Ingestion Pipeline (Channels)

| Field | Value |
|---|---|
| **Epic** | Production Event Pipeline |
| **Story** | As a **developer**, I want an event ingestion pipeline using `System.Threading.Channels` that decouples event producers (sensor simulator) from consumers (anomaly detector, persistence, SignalR broadcaster), so that the system processes events asynchronously and resiliently. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-024 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the pipeline, **When** a sensor event is written to the channel, **Then** it is consumed by all registered consumers (fan-out pattern or sequential pipeline). |
| AC-2 | **Given** a burst of 100 events in 1 second, **When** written to the bounded channel (capacity: 1000), **Then** no events are dropped. |
| AC-3 | **Given** a consumer throws an exception, **When** processing an event, **Then** the error is logged and the pipeline continues processing subsequent events. |

---

### MF-026 · Anomaly Detection Service

| Field | Value |
|---|---|
| **Epic** | Production Event Pipeline |
| **Story** | As the **system**, I want an anomaly detection consumer that evaluates incoming sensor events against configurable thresholds and flags anomalies, so that unusual conditions are detected in near-real-time. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-025 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** a temperature event of 105°C (threshold: 100°C), **When** processed by the anomaly detector, **Then** it is flagged as anomalous with `{ isAnomaly: true, sensorType: "Temperature", value: 105, threshold: 100, severity: "warning" }`. |
| AC-2 | **Given** a critical anomaly (value > 120% of threshold), **When** detected, **Then** severity is set to `"critical"`. |
| AC-3 | **Given** an anomaly, **When** flagged, **Then** an `AnomalyDetected` event is published to a separate anomaly channel for downstream processing (alerting, agent notification). |
| AC-4 | **Given** thresholds, **When** configured via `appsettings.json`, **Then** they are reloadable without restarting the application. |

---

### MF-027 · Alert Notification Service

| Field | Value |
|---|---|
| **Epic** | Production Event Pipeline |
| **Story** | As the **system**, I want anomaly events to trigger alerts that are broadcast via SignalR to connected dashboard clients and persisted to the database, so that operators are notified immediately. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-026, MF-009 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** a critical anomaly event, **When** processed by the alert service, **Then** a SignalR message is sent to the `/hubs/dashboard` group for the affected line. |
| AC-2 | **Given** an alert, **When** persisted, **Then** it is stored in the `Alerts` table with `lineId`, `sensorType`, `value`, `threshold`, `severity`, `timestamp`, `acknowledged` (bool). |
| AC-3 | **Given** a dashboard client, **When** a critical alert is received, **Then** a toast notification with sound appears. |

---

### MF-028 · Events History API & UI

| Field | Value |
|---|---|
| **Epic** | Production Event Pipeline |
| **Story** | As an **engineer**, I want to view historical sensor events and anomaly alerts via an Events page in the Blazor UI (with API backing: `GET /api/v1/events`), so that I can investigate past incidents. |
| **Story Points** | 5 |
| **Priority** | Should Have |
| **Dependencies** | MF-027, MF-007 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** `GET /api/v1/events?lineId=1&from=...&to=...&page=1&pageSize=50`, **When** called, **Then** paginated sensor events are returned with `totalCount`, `page`, `pageSize`, `items[]`. |
| AC-2 | **Given** `GET /api/v1/events?anomaliesOnly=true`, **When** called, **Then** only anomalous events are returned. |
| AC-3 | **Given** the Events page in Blazor, **When** loaded, **Then** a filterable, sortable table of events is displayed with columns: Timestamp, Line, Sensor, Value, Anomaly (Yes/No), Severity. |
| AC-4 | **Given** an alert row, **When** the "Acknowledge" button is clicked, **Then** the alert's `acknowledged` field is set to `true` via `PATCH /api/v1/alerts/{id}/acknowledge`. |

---

## Epic 6 — Observability & Hardening

> *Add structured logging, health checks, metrics, and security hardening to make the system production-aware.*

---

### MF-029 · Serilog Structured Logging

| Field | Value |
|---|---|
| **Epic** | Observability & Hardening |
| **Story** | As a **developer**, I want Serilog configured with structured JSON logging to console and file sinks, with enrichers for request ID, user ID, and correlation ID, so that logs are queryable and traceable. |
| **Story Points** | 3 |
| **Priority** | Must Have |
| **Dependencies** | MF-001 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** any API request, **When** processed, **Then** a structured log entry is written containing `Timestamp`, `Level`, `Message`, `RequestId`, `UserId` (if authenticated), `CorrelationId`, `SourceContext`. |
| AC-2 | **Given** Serilog configuration, **When** inspected, **Then** it writes to both Console (colored, structured) and File (rolling, JSON, 50 MB limit per file, 7-day retention). |
| AC-3 | **Given** an unhandled exception, **When** it occurs, **Then** it is logged at `Error` level with the full exception details and the request context. |

---

### MF-030 · Health Check Endpoints

| Field | Value |
|---|---|
| **Epic** | Observability & Hardening |
| **Story** | As a **DevOps engineer**, I want health check endpoints (`/health/live` and `/health/ready`) that verify database connectivity, cache availability, and vector store status, so that container orchestrators can monitor the application. |
| **Story Points** | 3 |
| **Priority** | Must Have |
| **Dependencies** | MF-002, MF-006 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** `GET /health/live`, **When** the application is running, **Then** a 200 response with `{ status: "Healthy" }` is returned (liveness only — no dependency checks). |
| AC-2 | **Given** `GET /health/ready`, **When** the database is reachable, **Then** a 200 response with `{ status: "Healthy", checks: { database: "Healthy", cache: "Healthy" } }` is returned. |
| AC-3 | **Given** `GET /health/ready`, **When** the database is unreachable, **Then** a 503 response with `{ status: "Unhealthy", checks: { database: "Unhealthy" } }` is returned. |

---

### MF-031 · Global Exception Handling Middleware

| Field | Value |
|---|---|
| **Epic** | Observability & Hardening |
| **Story** | As a **developer**, I want a global exception handling middleware that catches unhandled exceptions, logs them, and returns a standardized Problem Details (RFC 7807) JSON response, so that API errors are consistent and informative without leaking internals. |
| **Story Points** | 3 |
| **Priority** | Must Have |
| **Dependencies** | MF-029 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** an unhandled exception in a controller, **When** it occurs, **Then** the client receives a `500` response with `{ type, title, status: 500, detail: "An unexpected error occurred.", traceId }`. |
| AC-2 | **Given** a `ValidationException`, **When** thrown, **Then** the client receives a `400` response with Problem Details including `errors` field. |
| AC-3 | **Given** any error response, **When** in Production environment, **Then** the `detail` field does NOT contain stack traces or internal exception messages. |

---

### MF-032 · API Rate Limiting

| Field | Value |
|---|---|
| **Epic** | Observability & Hardening |
| **Story** | As a **security engineer**, I want rate limiting applied to authentication endpoints (10 req/min) and general API endpoints (100 req/min per user), so that the system is protected against brute-force and abuse. |
| **Story Points** | 3 |
| **Priority** | Should Have |
| **Dependencies** | MF-003 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the login endpoint, **When** the 11th request in a 1-minute window is made from the same IP, **Then** a 429 Too Many Requests response is returned with `Retry-After` header. |
| AC-2 | **Given** a general API endpoint, **When** the 101st request in a 1-minute window is made by the same authenticated user, **Then** a 429 response is returned. |
| AC-3 | **Given** rate limit configuration, **When** set via `appsettings.json`, **Then** limits are adjustable without code changes. |

---

### MF-033 · Security Headers & CORS Configuration

| Field | Value |
|---|---|
| **Epic** | Observability & Hardening |
| **Story** | As a **security engineer**, I want security headers (HSTS, X-Content-Type-Options, X-Frame-Options, CSP) and CORS policy configured, so that the API follows security best practices. |
| **Story Points** | 2 |
| **Priority** | Should Have |
| **Dependencies** | MF-001 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** any API response, **When** headers are inspected, **Then** `X-Content-Type-Options: nosniff`, `X-Frame-Options: DENY`, and `Referrer-Policy: strict-origin-when-cross-origin` are present. |
| AC-2 | **Given** a CORS request from an allowed origin, **When** a preflight OPTIONS request is sent, **Then** a 204 response with appropriate `Access-Control-Allow-*` headers is returned. |
| AC-3 | **Given** a CORS request from a disallowed origin, **When** sent, **Then** the CORS headers are absent and the browser blocks the request. |

---

### MF-034 · Metrics Endpoint

| Field | Value |
|---|---|
| **Epic** | Observability & Hardening |
| **Story** | As a **DevOps engineer**, I want a `/metrics` endpoint that exposes basic application metrics (request count, error rate, active SignalR connections, events processed), so that monitoring tools can scrape them. |
| **Story Points** | 3 |
| **Priority** | Could Have |
| **Dependencies** | MF-029 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** `GET /metrics`, **When** called, **Then** a response with key metrics in a structured format (JSON or Prometheus-compatible text) is returned. |
| AC-2 | **Given** the metrics, **When** inspected, **Then** they include: `http_requests_total`, `http_errors_total`, `signalr_connections_active`, `pipeline_events_processed_total`, `agent_tool_invocations_total`. |

---

## Epic 7 — Testing & Quality

> *Establish a comprehensive test suite covering unit, integration, and agent tool testing.*

---

### MF-035 · Unit Test Foundation & Coverage Target

| Field | Value |
|---|---|
| **Epic** | Testing & Quality |
| **Story** | As a **developer**, I want the `MicroForge.Tests.Unit` project configured with xUnit, Moq, FluentAssertions, and a minimum of 30 unit tests covering domain logic (yield calculation, anomaly detection, token validation), so that core logic is verified. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-001 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the test project, **When** `dotnet test --filter Category=Unit` is run, **Then** ≥ 30 unit tests execute and pass. |
| AC-2 | **Given** the test suite, **When** code coverage is measured, **Then** Domain project coverage is ≥ 70%. |
| AC-3 | **Given** unit tests, **When** inspected, **Then** they cover: yield prediction logic, anomaly threshold evaluation, JWT token generation, input validation, cache TTL behavior. |

---

### MF-036 · Integration Tests for API Endpoints

| Field | Value |
|---|---|
| **Epic** | Testing & Quality |
| **Story** | As a **developer**, I want integration tests using `WebApplicationFactory<Program>` that test key API flows end-to-end (auth → CRUD → agent chat), so that API contracts are verified against a real test server. |
| **Story Points** | 8 |
| **Priority** | Must Have |
| **Dependencies** | MF-003, MF-017 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the integration test project, **When** `dotnet test --filter Category=Integration` is run, **Then** ≥ 10 integration tests execute and pass. |
| AC-2 | **Given** an integration test, **When** it runs, **Then** it uses an in-memory or temp SQLite database (not the production database). |
| AC-3 | **Given** the test suite, **When** inspected, **Then** it covers: login flow (success/failure), authorized/unauthorized access, CRUD operations on production data, agent chat endpoint, document upload. |

---

### MF-037 · Agent Tool Test Harness

| Field | Value |
|---|---|
| **Epic** | Testing & Quality |
| **Story** | As a **developer**, I want a dedicated test harness that tests each AI agent tool in isolation (mocked dependencies) and in integration (with seeded data), verifying input validation, output schema compliance, and edge cases. |
| **Story Points** | 5 |
| **Priority** | Must Have |
| **Dependencies** | MF-012, MF-013, MF-014, MF-015, MF-016 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** each tool (QueryDefectHistory, PredictYield, SummarizeShiftReport, RecommendMaintenance), **When** tested, **Then** at least 3 test cases exist per tool: valid input, invalid input, edge case. |
| AC-2 | **Given** a tool test, **When** executed, **Then** the output is validated against the tool's declared output JSON schema. |
| AC-3 | **Given** the test harness, **When** run, **Then** a summary report lists each tool, number of tests, pass/fail count. |

---

### MF-038 · Load & Performance Baseline

| Field | Value |
|---|---|
| **Epic** | Testing & Quality |
| **Story** | As a **developer**, I want a simple load test script (using `BenchmarkDotNet` or a script) that establishes baseline performance metrics for key endpoints (login, dashboard data, agent chat, search), so that regressions can be detected. |
| **Story Points** | 3 |
| **Priority** | Could Have |
| **Dependencies** | MF-017, MF-022 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the load test, **When** run against a local instance, **Then** it reports p50, p95, and p99 response times for each tested endpoint. |
| AC-2 | **Given** the dashboard data endpoint, **When** tested with 10 concurrent requests, **Then** p95 response time is ≤ 500ms. |
| AC-3 | **Given** the agent chat endpoint, **When** tested, **Then** p95 response time is ≤ 5000ms (agent processing is expected to be slower). |

---

### MF-039 · API Documentation & OpenAPI Spec

| Field | Value |
|---|---|
| **Epic** | Testing & Quality |
| **Story** | As a **developer/consumer**, I want auto-generated OpenAPI/Swagger documentation available at `/swagger` with all endpoints, schemas, and authentication requirements documented, so that the API is self-describing. |
| **Story Points** | 3 |
| **Priority** | Must Have |
| **Dependencies** | MF-001, MF-003 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** the API, **When** navigating to `/swagger`, **Then** the Swagger UI is displayed with all endpoints grouped by controller. |
| AC-2 | **Given** the Swagger UI, **When** inspecting an endpoint, **Then** request/response schemas, required authentication (Bearer token), query parameters, and example payloads are visible. |
| AC-3 | **Given** the Swagger UI, **When** the "Authorize" button is used with a valid JWT, **Then** subsequent "Try it out" requests include the token. |

---

### MF-040 · CI Pipeline Definition (GitHub Actions)

| Field | Value |
|---|---|
| **Epic** | Testing & Quality |
| **Story** | As a **developer**, I want a GitHub Actions workflow (`.github/workflows/ci.yml`) that builds the solution, runs all tests, and builds the Docker image on every push to `main` or PR, so that quality is enforced automatically. |
| **Story Points** | 3 |
| **Priority** | Could Have |
| **Dependencies** | MF-035, MF-036, MF-005 |

**Acceptance Criteria**

| # | Criteria |
|---|---|
| AC-1 | **Given** a push to `main`, **When** the workflow runs, **Then** it executes: `dotnet restore` → `dotnet build` → `dotnet test` → `docker build`. |
| AC-2 | **Given** any test failure, **When** the workflow runs, **Then** the pipeline fails and reports the failing tests. |
| AC-3 | **Given** a successful pipeline, **When** completed, **Then** the Docker image is tagged with the commit SHA. |

---

## Backlog Summary

| Epic | Stories | Total Points | Must Have | Should Have | Could Have |
|---|---|---|---|---|---|
| Platform Foundation | MF-001 – MF-006 | 24 | 21 | 3 | 0 |
| Manufacturing Dashboard | MF-007 – MF-011 | 21 | 13 | 8 | 0 |
| AI Agent Framework | MF-012 – MF-018 | 37 | 27 | 10 | 0 |
| RAG Document Intelligence | MF-019 – MF-023 | 26 | 21 | 5 | 0 |
| Production Event Pipeline | MF-024 – MF-028 | 23 | 18 | 5 | 0 |
| Observability & Hardening | MF-029 – MF-034 | 17 | 9 | 5 | 3 |
| Testing & Quality | MF-035 – MF-040 | 27 | 21 | 0 | 6 |
| **TOTAL** | **40** | **175** | **130** | **36** | **9** |

> [!NOTE]
> The velocity estimate for a solo developer with an AI coding assistant is approximately **25–35 story points per 2-week sprint**, depending on complexity and familiarity. The 175-point backlog is scoped for 6 sprints at ~29 points/sprint average.
