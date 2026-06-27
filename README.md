# Arcavis Sigillum - Authentication Service

Arcavis Sigillum Authentication Service is the identity and access management component of the Sigillum ecosystem.

It is responsible exclusively for identity-related concerns and communicates with other services via APIs and event-driven messaging.

---

## Bounded Context

This service represents a single bounded context:

**Identity & Access Management**

### Out of Scope
- User profile management
- Billing / subscription logic
- Application-specific authorization rules

---

## Responsibilities

- User registration and authentication
- JWT token generation and validation
- Password hashing and verification (Argon2id)
- Email verification workflow
- User lifecycle management (activate / deactivate)
- Security event publishing

---

## Architecture Overview

Arcavis is built following Clean Architecture principles with strict separation of concerns.

### Dependency Flow

Presentation → Application → Domain  
Infrastructure provides implementations for contracts defined by the Application layer and persistence mappings for the Domain model.

The Domain layer has no external dependencies.

---

## Layers

### Domain Layer (`Core.Domain`)

Contains the core business logic and invariants.

- User aggregate root
- Email and Password value objects
- Domain events (e.g. UserRegistered)
- Business rules and invariants

---

### Application Layer (`Core.Application`)

Orchestrates use cases and application workflows.

- CQRS command and query handlers
- Mediator.SourceGenerator pipeline behaviors
- Transaction pipeline behaviors
- Outbox coordination
- Security abstractions (e.g. password hashing, user context)

---

### Infrastructure Layer

Implements external system integrations.

- Entity Framework Core (write model persistence)
- RepoDb (read-optimized queries)
- MassTransit + RabbitMQ (event bus)
- Redis (caching layer)
- Argon2id password hashing implementation

> EF Core is used for transactional consistency, while RepoDb is used for optimized read performance.

---

### Presentation Layer

- ASP.NET Core Web API (v1)
- Background worker services (Outbox processing)
- Authentication endpoints
- Health checks and API versioning

---

## Key Features

### Authentication

- JWT-based stateless authentication
- Secure login and registration flows
- Token validation middleware

---

### Identity Management

- Email verification workflow
- User lifecycle state management
- Domain-driven user model (aggregate consistency)

---

### Reliability

- Outbox pattern for reliable event publishing
- Transactional consistency between database and events
- Background worker-based event publishing
- Designed for idempotent event consumption by downstream services.

---

## Event-Driven Architecture

### Event Flow

1. Client sends a command (e.g. RegisterUser)
2. The application executes the use case
3. The aggregate performs the business operation
4. The aggregate raises one or more domain events
5. A pipeline behavior collects the domain events
6. Domain events are mapped to integration events
7. Integration events are added to the Outbox within the current transaction
8. The transaction is committed atomically
9. The background worker publishes integration events to RabbitMQ

---

### Delivery Guarantees

- At-least-once delivery
- Retry-based processing
- Idempotent consumer expectations

---

## Security Model

- Argon2id password hashing (memory-hard algorithm)
- JWT-based authentication (stateless)
- Claims-based identity model
- Email verification enforced before activation

---

## Data Strategy

- EF Core for transactional write operations
- RepoDb for high-performance read queries
- CQRS separation for scalability and clarity

---

## Deployment Model

- Fully containerized (Docker-ready)
- Stateless API design
- Horizontally scalable
- Separate API and Worker processes

---

## Observability (Extensible)

- Structured logging support
- Health check endpoints
- Event pipeline hooks via MassTransit

---

## Architecture Decision Records (ADRs)

### Why CQRS?

To keep transactional writes isolated while serving read workloads through optimized projections using RepoDb.

---

### Why Outbox Pattern?

To solve the dual-write problem and ensure reliable event delivery in distributed systems.

---

### Why EF Core + RepoDb?

- EF Core is responsible for aggregate persistence and transactional consistency.
- RepoDb is used exclusively for read-side projections where change tracking is unnecessary.

---

### Why Event-Driven Communication?

To reduce service coupling and enable independent deployment of services.

---

## Failure Handling

- RabbitMQ failure → Outbox backlog accumulates, retried by worker
- Database transaction failure → no event is persisted (atomic consistency)
- Consumer failure → at-least-once delivery with idempotent handling

---

## Technology Stack

- .NET 10
- ASP.NET Core
- Entity Framework Core
- RepoDb
- MassTransit + RabbitMQ
- Redis
- Mediator.SourceGenerator
- Argon2id password hashing

---

## Non-Functional Requirements

- Stateless service design
- Horizontal scalability
- Eventual consistency across services
- Strong security baseline
- Modular and extensible architecture

---

## Summary

Arcavis Sigillum Authentication Service is a modern identity system designed for high security, scalability, and distributed system reliability.

It provides a clean separation of concerns and is ready for production-grade workloads in an event-driven ecosystem.