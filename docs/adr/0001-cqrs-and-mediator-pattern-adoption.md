# ADR 0001: Adoption of CQRS Pattern via MediatR

## Status
Accepted

## Context & Problem Statement
In traditional ASP.NET Core applications, business logic is frequently concentrated within bloated service classes (`ArticleService.cs`) containing dozens of unrelated methods. As applications evolve, these classes suffer from:
- Massive dependency injection graphs leading to circular references and initialization overhead.
- Entangled read and write logic, making optimization (such as query caching or read replica routing) difficult.
- Lack of centralized cross-cutting pipeline controls for input validation, metrics logging, and performance auditing.

## Decision
We adopted the **Command Query Responsibility Segregation (CQRS)** pattern implemented via the **MediatR** mediator pipeline.

Key architectural mechanics:
- **Commands:** Explicit mutations (`CreateArticleCommand`, `PublishArticleCommand`) implementing `IRequest<TResponse>`.
- **Queries:** Read-only data projections (`GetArticlesQuery`) implementing `IRequest<TResponse>`.
- **Pipeline Behaviors:** Generic pipeline middlewares (`ValidationBehavior`, `LoggingBehavior`) intercepting all requests before handler invocation.

## Consequences

### Positive
- **Single Responsibility Principle (SRP):** Each command and query lives in an isolated class with dedicated request/response contracts and single-purpose dependencies.
- **Centralized Pipeline Verification:** Payload validation (via FluentValidation) executes automatically across all commands without repetitive controller-level boilerplate.
- **Performance Optimization:** Read queries can bypass change-tracking overhead (`AsNoTracking()`) or integrate Redis cache-aside patterns independently from write operations.

### Negative / Trade-Offs
- Introduces additional class files per use case compared to monolithic service classes.
- Navigation requires searching handlers rather than direct method definitions within a single service file.

