# .NET 8 Enterprise Clean Architecture & CQRS Web API

[![.NET CI](https://github.com/ShahAhmar/dotnet-clean-architecture-api/actions/workflows/ci.yml/badge.svg)](https://github.com/ShahAhmar/dotnet-clean-architecture-api/actions)
[![.NET Version](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=flat-square&logo=csharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Docker Ready](https://img.shields.io/badge/Docker-Ready-2496ED?style=flat-square&logo=docker&logoColor=white)](docker-compose.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-0F172A.svg?style=flat-square)](LICENSE)

A production-grade RESTful API engineered with **ASP.NET Core 8**, adhering to **Clean Architecture**, **CQRS (Command Query Responsibility Segregation)** with MediatR, and **Domain-Driven Design (DDD)** principles.

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

## Engineering Standards & Architecture Highlights

- **MediatR Pipeline Behaviors:** Automatic request validation (FluentValidation), unhandled exception interception, and structured performance metrics logging.
- **Entity Framework Core 8:** Code-First migrations, soft delete interceptors, and auditable entity base abstractions (`CreatedAt`, `ModifiedAt`, `CreatedBy`).
- **Distributed Caching:** Redis cache-aside pattern with automated cache invalidation on command mutations.
- **Security:** JWT authentication with refresh token rotation and claim-based authorization policies.
- **Containerization:** Multi-stage production `Dockerfile` and `docker-compose.yml` for isolated container orchestration.

---

## Quick Start (Docker)

```bash
# Clone repository
git clone https://github.com/ShahAhmar/dotnet-clean-architecture-api.git
cd dotnet-clean-architecture-api

# Start services via Docker Compose (App + PostgreSQL + Redis)
docker compose up -d --build
```

Access Swagger UI documentation at: `http://localhost:5000/swagger`

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
