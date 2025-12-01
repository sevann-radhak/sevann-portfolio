# Portfolio Architecture

## Overview

This portfolio demonstrates modern software architecture patterns and best practices in full-stack development.

## Architecture Diagram

```
Frontend (React SPA) → API Gateway (.NET 8) → Business Logic → Data Access → Azure Services
```

## Technology Stack

- **Backend**: .NET 8, C#, ASP.NET Core
- **Frontend**: React 18, TypeScript, Vite
- **Database**: SQL Server (Azure SQL or LocalDB)
- **Cloud**: Azure (App Service, Static Web Apps, SQL Database, Storage, Key Vault)
- **CI/CD**: GitHub Actions

## Architecture Patterns

- **Layered Architecture**: Clear separation of concerns (API, BLL, DAL)
- **Domain-Driven Design (DDD)**: Entities, Value Objects, Aggregates, Domain Events
- **Repository Pattern**: Data access abstraction
- **CQRS**: Command Query Responsibility Segregation
- **Dependency Injection**: .NET DI Container
- **Event-Driven**: Domain Events with MediatR

## Project Structure

```
sevann-portfolio/
├── Backend/          # .NET 8 API
│   ├── src/         # Source code
│   └── tests/       # Unit and integration tests
├── Frontend/        # React SPA
│   ├── src/         # Source code
│   └── public/      # Static assets
├── modules/         # Demonstrative modules
│   ├── ddd-demo/
│   ├── repository-demo/
│   └── ...
├── docs/            # Documentation
├── ops/             # Infrastructure as Code
└── ci/              # CI/CD workflows
```

## Modules

Each module demonstrates a specific pattern or concept:

- **DDD Demo**: Domain-Driven Design implementation
- **Repository Demo**: Repository Pattern with data abstraction
- **CQRS Demo**: Command Query Responsibility Segregation
- **Domain Events Demo**: Event-driven architecture
- **Auth Demo**: JWT Authentication
- **Testing Demo**: Unit, Integration, Contract tests

## Deployment

See [DEPLOYMENT.md](DEPLOYMENT.md) for deployment instructions.

## References

- [.NET Documentation](https://learn.microsoft.com/en-us/dotnet/)
- [React Documentation](https://react.dev/)
- [Domain-Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)

