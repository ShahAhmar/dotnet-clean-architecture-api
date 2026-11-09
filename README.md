# .NET 8 Enterprise Clean Architecture & CQRS Web API

[![.NET CI](https://github.com/ShahAhmar/dotnet-clean-architecture-api/actions/workflows/ci.yml/badge.svg)](https://github.com/ShahAhmar/dotnet-clean-architecture-api/actions)
[![.NET Version](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat-square&logo=csharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Docker Ready](https://img.shields.io/badge/Docker-Ready-2496ED?style=flat-square&logo=docker&logoColor=white)](docker-compose.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-0F172A.svg?style=flat-square)](LICENSE)

A production-grade RESTful API engineered with **ASP.NET Core 8**, adhering strictly to **Clean Architecture**, **CQRS (Command Query Responsibility Segregation)** with MediatR, and **Domain-Driven Design (DDD)** principles.

[Architecture Topology](#clean-architecture-structure) &bull; [Core Capabilities](#core-system-functionalities) &bull; [Quick Start](#quick-start-docker)

---

## System Objective: Why This System Exists

Enterprise web backends frequently fail to scale because business domain logic becomes tightly coupled to web frameworks, direct database models, or external third-party SDKs. When requirements change, refactoring creates cascading breaking changes across the codebase.

This project exists as a reference implementation of **Clean Architecture** in .NET 8:
1. **Decoupled Business Rules:** The Domain and Application layers maintain zero dependencies on database engines, ORMs, or UI frameworks.
2. **Predictable CQRS Pipelines:** Read and write responsibilities are cleanly bifurcated using MediatR request handlers with automatic pipeline behaviors for validation and logging.
3. **Enterprise Maintainability:** Standardized patterns allowing seamless swapping of infrastructure adapters (e.g., PostgreSQL, SQL Server, Redis) without touching domain logic.

---

## Technical Stack Breakdown

- **Runtime & Language:** .NET 8.0 SDK, C# 12 (Nullable reference types, records, pattern matching)
- **Web API Framework:** ASP.NET Core Web API (Kestrel HTTP server, dependency injection container)
- **Mediator Pattern & CQRS:** MediatR 12 (Decoupled command and query dispatcher)
- **Data Validation:** FluentValidation (Pipeline-level strongly typed rule validation)
- **Persistence & ORM:** Entity Framework Core 8 (Code-First migrations, auditable entities)
- **Database Driver:** Npgsql PostgreSQL EntityFrameworkCore provider
- **Distributed Caching:** StackExchange.Redis (Cache-aside pattern, distributed lock abstraction)
- **API Specification:** Swashbuckle OpenAPI / Swagger UI
- **Testing Subsystem:** xUnit, FluentAssertions, Moq
- **Containerization:** Multi-stage Linux Docker build targeting Alpine-based ASP.NET runtime

---

## Core System Functionalities

1. **CQRS Command & Query Separation:** Mutating actions (`CreateArticleCommand`, `PublishArticleCommand`) and read operations (`GetArticlesQuery`) are modeled as isolated, single-responsibility handlers.
2. **Pipeline Middleware Behaviors:** MediatR pipeline automatically intercepts all commands before reaching handlers, validating payload schemas via FluentValidation and halting invalid requests immediately.
3. **Auditable Domain Entities:** Base entity abstractions automatically track state transitions (`CreatedAt`, `ModifiedAt`, `CreatedBy`, `Status`).
4. **Distributed Cache-Aside Pattern:** Redis caching layer with automated cache invalidation executed whenever domain mutations occur.
5. **Global Error Interception:** Custom exception handling middleware that catches domain and validation errors, transforming them into RFC 7807 compliant Problem Details responses.
6. **Containerized Multi-Service Orchestration:** Single-command local environment bootstrapping with isolated containers for the Web API, PostgreSQL database, and Redis cache.

---

## Clean Architecture Structure

```mermaid
flowchart TD
    subgraph WebApi["1. Presentation Layer (WebApi)"]
        Controllers[API Controllers]
        Middlewares[Global Exception & Authentication Middlewares]
        Swagger[Swagger / OpenAPI Specification]
    end

    subgraph Application["2. Application Layer (Use Cases)"]
        Commands[Commands & Command Handlers]
        Queries[Queries & Query Handlers]
        Behaviors[MediatR Pipeline Behaviors (Validation, Logging)]
        Validators[FluentValidation Rules]
    end

    subgraph Domain["3. Domain Layer (Enterprise Business Rules)"]
        Entities[Entities & Aggregates]
        ValueObjects[Value Objects]
        DomainEvents[Domain Events]
        Interfaces[Repository & Domain Service Contracts]
    end

    subgraph Infrastructure["4. Infrastructure Layer (External Adapters)"]
        EFCore[EF Core 8 DbContext & Migrations]
        PostgreSQL[(PostgreSQL / MS SQL)]
        RedisCache[(Redis Distributed Cache)]
        JWT[JWT Identity Service & Token Handler]
    end

    WebApi --> Application
    Application --> Domain
    Infrastructure -.->|Implements| Domain
    Infrastructure -.->|Implements| Application
    WebApi --> Infrastructure
```

---

## Security & Enterprise Standards

- **Parameter Tamper Prevention:** Domain entities enforce invariant checks; invalid states cannot be instantiated.
- **SQL Injection Elimination:** 100% parameterized queries generated through Entity Framework Core.
- **Strict Layer Isolation:** Architecture guidelines prevent outer infrastructure code from leaking into inner domain contracts.
- **Config Security:** Sensitive configuration parameters (connection strings, tokens) are extracted to environment variables.

---

## Quick Start (Docker)

```bash
# 1. Clone repository
git clone https://github.com/ShahAhmar/dotnet-clean-architecture-api.git
cd dotnet-clean-architecture-api

# 2. Start services via Docker Compose (App + PostgreSQL + Redis)
docker compose up -d --build
```

Access interactive Swagger documentation at: `http://localhost:5000/swagger`

---

## Testing

```bash
# Execute automated test suite
dotnet test --logger "console;verbosity=detailed"
```

---

## Author

**Syed Ahmar Ali**  
*Senior Full-Stack Engineer & Solutions Architect*  
- Portfolio: [ahmar.techgitsolutions.com](https://ahmar.techgitsolutions.com)  
- GitHub: [@ShahAhmar](https://github.com/ShahAhmar)
