# MicroForge — Technology Stack

> **Version:** 1.0  
> **Last Updated:** 2026-07-14

---

## Overview

MicroForge is built entirely in **C#** on the **.NET 8** platform. Every component — from the UI to the AI agent runtime to the data layer — uses C# and the .NET ecosystem. This document catalogs every technology, library, and tool used in the project, along with version requirements and rationale.

---

## Core Platform

| Technology | Version | Purpose | Rationale |
|---|---|---|---|
| **.NET 8 SDK** | `8.0.x` (LTS) | Runtime and build platform | Long-term support release; latest C# 12 features; performance improvements; native AOT readiness |
| **C# 12** | (part of .NET 8) | Primary language | Required by .NET 8 SDK; primary expressions, collection expressions, inline arrays |
| **ASP.NET Core 8** | `8.0.x` | Web framework for API + Blazor | Industry standard for building high-performance C# web applications |

---

## Frontend

| Technology | Version | Purpose | Rationale |
|---|---|---|---|
| **Blazor Server** | `8.0.x` (built-in) | Interactive UI framework | Enables rich C# UI without JavaScript; keeps the project 100% C#; real-time via SignalR built-in |
| **SignalR** | `8.0.x` (built-in) | Real-time communication | Built into Blazor Server; used for live sensor dashboards and alert notifications |
| **Bootstrap 5** | `5.3.x` | CSS framework | Included with Blazor templates; provides responsive layout and component styling out of the box |
| **Chart.js** (via JS interop) | `4.x` | Data visualization | Lightweight charting library for sensor data plots and yield trends; minimal JS interop required |

---

## API Layer

| Technology | Version | Purpose | Rationale |
|---|---|---|---|
| **ASP.NET Core Controllers** | `8.0.x` (built-in) | RESTful API endpoints | Clean resource-oriented endpoint organization with attribute routing |
| **ASP.NET Core Minimal API** | `8.0.x` (built-in) | Lightweight utility endpoints | Used for health, metrics, and simple utility endpoints with minimal ceremony |
| **Swashbuckle (Swagger)** | `6.x` | API documentation | Auto-generates OpenAPI spec and interactive documentation UI |
| **FluentValidation** | `11.x` | Request validation | Expressive, testable validation rules separated from model classes |

---

## AI & Machine Learning

| Technology | Version | Purpose | Rationale |
|---|---|---|---|
| **Custom Agent Runtime** | N/A (project code) | MCP-style AI agent orchestration | Full control over tool registration, invocation, and error handling; no heavy framework overhead |
| **System.Text.Json** | `8.0.x` (built-in) | JSON schema validation for tools | Native .NET JSON library; high performance; schema validation for MCP tool definitions |
| **HttpClient** | `8.0.x` (built-in) | LLM API communication | Used to call local LLM (Nemotron3-Super via Ollama) or remote API endpoints |
| **Custom Vector Store** | N/A (project code) | In-memory HNSW index | Demonstrates understanding of vector search internals without external DB dependency |
| **Custom Embedding Service** | N/A (project code) | Text-to-vector embedding | Wraps local embedding model API or implements simple bag-of-words fallback |

---

## Event Pipeline

| Technology | Version | Purpose | Rationale |
|---|---|---|---|
| **System.Threading.Channels** | `8.0.x` (built-in) | Async producer-consumer queues | High-performance bounded channels for sensor data streaming; built into .NET |
| **MediatR** | `12.x` | In-process event mediation | Decouples event producers from handlers; clean CQRS-style patterns |
| **IHostedService** | `8.0.x` (built-in) | Background task management | Manages the sensor simulator lifecycle; integrates with host startup/shutdown |

---

## Data Layer

| Technology | Version | Purpose | Rationale |
|---|---|---|---|
| **Entity Framework Core 8** | `8.0.x` | ORM / Data access | Code-first, LINQ queries, migrations; the standard .NET ORM |
| **Microsoft.EntityFrameworkCore.Sqlite** | `8.0.x` | SQLite EF Core provider | Zero-infrastructure database; single file; perfect for self-contained projects |
| **SQLite** | `3.x` (via provider) | Relational database | No server process required; file-based; cross-platform; embedded in the application |
| **IMemoryCache** | `8.0.x` (built-in) | In-memory caching | Reduces database load for frequently accessed read-heavy data (production line lists, role lookups) |

---

## Authentication & Security

| Technology | Version | Purpose | Rationale |
|---|---|---|---|
| **Microsoft.AspNetCore.Authentication.JwtBearer** | `8.0.x` | JWT authentication middleware | Standard bearer token authentication for ASP.NET Core |
| **System.IdentityModel.Tokens.Jwt** | `7.x` | JWT token generation and validation | Create and validate JWT tokens with claims |
| **BCrypt.Net-Next** | `4.x` | Password hashing | Industry-standard adaptive hashing algorithm; resistant to brute-force attacks |
| **Custom RBAC** | N/A (project code) | Role-based access control | Clean implementation using ASP.NET Core policies and authorization handlers |

---

## Testing

| Technology | Version | Purpose | Rationale |
|---|---|---|---|
| **xUnit** | `2.x` | Unit testing framework | Most popular .NET testing framework; clean assertion syntax; extensible |
| **Moq** | `4.x` | Mocking framework | Create test doubles for interfaces; verify interactions |
| **FluentAssertions** | `6.x` | Assertion library | Readable, expressive assertions; better failure messages |
| **Microsoft.AspNetCore.Mvc.Testing** | `8.0.x` | Integration testing | In-memory test server for API integration tests without Docker |
| **Bogus** | `35.x` | Test data generation | Deterministic fake data for manufacturing scenarios (sensor readings, defects) |
| **Coverlet** | `6.x` | Code coverage | Generate coverage reports; enforce coverage thresholds in CI |

---

## Observability

| Technology | Version | Purpose | Rationale |
|---|---|---|---|
| **Serilog** | `3.x` | Structured logging | De facto .NET logging library; structured JSON output; rich sink ecosystem |
| **Serilog.AspNetCore** | `8.x` | ASP.NET Core integration | Request logging, enrichment, middleware integration |
| **Serilog.Sinks.Console** | `5.x` | Console output | Formatted console logging for development and Docker |
| **Serilog.Sinks.File** | `5.x` | File output | Persistent logs in `/app/logs` for production diagnostics |
| **ASP.NET Core Health Checks** | `8.0.x` (built-in) | Health monitoring | Liveness and readiness probes for Docker health checks |
| **Custom Metrics Middleware** | N/A (project code) | Runtime metrics | Request count, latency histograms, active connections |

---

## Containerization & DevOps

| Technology | Version | Purpose | Rationale |
|---|---|---|---|
| **Docker** | `24.x+` | Containerization | Standardized deployment; environment consistency; reproducible builds |
| **Docker Compose** | `2.x+` | Multi-service orchestration | Single command to start the entire platform with volumes and networking |
| **Multi-stage Dockerfile** | N/A | Optimized container builds | Separates build (SDK) from runtime (ASP.NET); smaller production images |

---

## Development Tools

| Tool | Version | Purpose | Rationale |
|---|---|---|---|
| **Visual Studio 2022 / VS Code** | Latest | IDE | Primary development environment; rich C# tooling |
| **C# Dev Kit** (VS Code extension) | Latest | VS Code C# support | IntelliSense, debugging, testing integration for VS Code users |
| **dotnet CLI** | `8.0.x` | Build, test, run, publish | Command-line interface for all .NET operations |
| **Git** | `2.x+` | Version control | Source code management with meaningful commit history |
| **Nemotron3-Super** (via Ollama) | Latest | Local LLM | AI coding assistant; powers the agent runtime in development |
| **Ollama** | `0.3.x+` | Local LLM runtime | Runs Nemotron3-Super locally; provides OpenAI-compatible API |

---

## NuGet Package Summary

```xml
<!-- Core -->
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.*" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.*" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.*" />

<!-- Validation & Mediation -->
<PackageReference Include="FluentValidation.AspNetCore" Version="11.*" />
<PackageReference Include="MediatR" Version="12.*" />

<!-- Security -->
<PackageReference Include="BCrypt.Net-Next" Version="4.*" />

<!-- Logging -->
<PackageReference Include="Serilog.AspNetCore" Version="8.*" />
<PackageReference Include="Serilog.Sinks.Console" Version="5.*" />
<PackageReference Include="Serilog.Sinks.File" Version="5.*" />

<!-- API Documentation -->
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.*" />

<!-- Testing -->
<PackageReference Include="xunit" Version="2.*" />
<PackageReference Include="Moq" Version="4.*" />
<PackageReference Include="FluentAssertions" Version="6.*" />
<PackageReference Include="Bogus" Version="35.*" />
<PackageReference Include="coverlet.collector" Version="6.*" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.*" />
```

---

## Version Compatibility Matrix

| Component | Minimum Version | Recommended Version | Notes |
|---|---|---|---|
| .NET SDK | 8.0.100 | 8.0.300+ | LTS release; check with `dotnet --version` |
| Docker Engine | 20.10 | 24.x+ | Required for containerized deployment |
| Docker Compose | 2.0 | 2.20+ | V2 syntax required |
| SQLite | 3.35 | 3.45+ | JSON functions require 3.38+ |
| Ollama | 0.1.0 | 0.3.x+ | For local LLM support |
| Node.js | N/A | N/A | Not required — 100% C# project |

---

## Why This Stack?

### Single-Language Simplicity
The entire platform is C# — frontend, backend, AI runtime, data access, tests. This reduces cognitive overhead and demonstrates deep mastery of the .NET ecosystem.

### Zero External Infrastructure
SQLite eliminates the need for a database server. In-memory vector store eliminates the need for a vector database. Ollama runs locally. The entire platform can run on a single developer machine.

### Production-Grade Patterns at Dev Scale
The architecture uses patterns from enterprise manufacturing systems (event pipelines, CQRS, structured logging) but sized for a solo developer to build and demonstrate competency.

### Framework Choices Are Intentional
Every dependency was chosen because it's either built into .NET 8 (reducing external dependencies) or is the clear industry standard for its purpose (Serilog, xUnit, MediatR). No exotic or experimental libraries.
