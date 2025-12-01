# Portfolio Backend

.NET 8 backend implementation showcasing modern software architecture patterns and best practices.

## 🏗️ Architecture

The backend follows a **Layered Architecture** with **Domain-Driven Design (DDD)** principles:

```
Portfolio.API (Presentation Layer)
    ↓
Portfolio.Application (Application Layer)
    ↓
Portfolio.Domain (Domain Layer) ✅ COMPLETE
    ↓
Portfolio.Infrastructure (Infrastructure Layer)
```

## 📦 Projects

- **Portfolio.API**: ASP.NET Core Web API (Presentation Layer)
- **Portfolio.Application**: Application services, CQRS handlers (Application Layer)
- **Portfolio.Domain**: Domain entities, value objects, aggregates, domain events (Domain Layer) ✅
- **Portfolio.Infrastructure**: Data access, external services (Infrastructure Layer)

## ✅ Domain Layer (US-01 - Complete)

The Domain layer is fully implemented with:

### Entities
- `PortfolioProject`: Represents a portfolio project
- `Skill`: Represents a technical skill
- `Experience`: Represents work experience

### Value Objects
- `ProjectName`: Immutable project name with validation
- `EmailAddress`: Immutable email address with validation
- `Url`: Immutable URL with validation

### Aggregates
- `PortfolioProjectAggregate`: Aggregate root managing PortfolioProject lifecycle

### Domain Events
- `PortfolioProjectCreatedEvent`: Fired when a project is created
- `PortfolioProjectFeaturedEvent`: Fired when a project is featured
- `PortfolioProjectArchivedEvent`: Fired when a project is archived

### Domain Services
- `PortfolioDomainService`: Business logic for portfolio operations

### Constants
- `ErrorMessages`: Centralized error messages
- `FieldNames`: Centralized field names for error messages
- `ValidationConstants`: Validation rules (min/max lengths)

## 🧪 Testing

### Run Tests

```bash
# Run all tests
dotnet test Portfolio.sln

# Run with verbosity
dotnet test Portfolio.sln --verbosity normal

# Run specific test project
dotnet test tests/Portfolio.Domain.Tests
```

### Test Coverage

- **150+ unit tests** covering all domain components
- **100% code coverage** for Domain layer
- Tests organized by component type (Entities, ValueObjects, Aggregates, Services)

### Test Structure

```
tests/
└── Portfolio.Domain.Tests/
    ├── Entities/
    │   ├── PortfolioProjectTests.cs
    │   ├── SkillTests.cs
    │   └── ExperienceTests.cs
    ├── ValueObjects/
    │   ├── ProjectNameTests.cs
    │   ├── EmailAddressTests.cs
    │   └── UrlTests.cs
    ├── Aggregates/
    │   └── PortfolioProjectAggregateTests.cs
    └── Services/
        └── PortfolioDomainServiceTests.cs
```

## 🚀 Quick Start

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or Azure SQL) - for future use

### Build

```bash
dotnet restore Portfolio.sln
dotnet build Portfolio.sln
```

### Run API

```bash
dotnet run --project src/Portfolio.API
```

The API will be available at `https://localhost:5001` or `http://localhost:5000` (check `launchSettings.json` for exact ports).

## 📋 Development Workflow

This project follows a **Scrum-like workflow**:

1. Create GitHub Issue (User Story)
2. Create feature branch: `features/US-XX--Description`
3. Implement feature
4. Write/update tests
5. Create Pull Request
6. Code review
7. Merge to main

## 🎯 Next Steps

- [ ] **US-02**: Implement Repository Pattern
- [ ] **US-03**: Implement CQRS (Commands and Queries)
- [ ] **US-04**: Create REST API endpoints
- [ ] **US-05**: Add Integration Tests

## 📚 References

- [Domain-Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)
- [Value Objects](https://martinfowler.com/bliki/ValueObject.html)
- [Aggregates](https://martinfowler.com/bliki/DDD_Aggregate.html)
- [.NET Documentation](https://learn.microsoft.com/en-us/dotnet/)

