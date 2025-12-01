# Sevann Portfolio - Technical Showcase

[![CI/CD](https://github.com/sevann-radhak/sevann-portfolio/actions/workflows/ci.yml/badge.svg)](https://github.com/sevann-radhak/sevann-portfolio/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Professional technical portfolio with modern software architecture, design patterns, and best practices in full-stack development with .NET and React.

## 🎯 Purpose

This project showcases:

- Modern software architecture (DDD, CQRS, Repository Pattern)
- Full-stack development (.NET 8 + React 18)
- Azure deployment with free-tier resources
- CI/CD with GitHub Actions
- Testing (Unit, Integration, Contract)
- Observability and security

## 🏗️ Architecture

```
Frontend (React SPA) → API Gateway (.NET 8) → Business Logic → Data Access → Azure Services
```

See [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) for architecture details.

## 🏛️ Architecture Layers

The solution follows a **Layered Architecture** with **Domain-Driven Design (DDD)**:

- **✅ Domain Layer**: Core business logic with Entities, Value Objects, Aggregates, Domain Events, and Domain Services (100% test coverage)
- **Application Layer**: Application services, CQRS handlers (coming soon)
- **Infrastructure Layer**: Data access, external services (coming soon)
- **API Layer**: REST API endpoints (coming soon)

## 📦 Demonstrative Modules

Each module demonstrates a specific pattern or concept:

- **Repository Pattern**: Data access abstraction (coming soon)
- **CQRS**: Command Query Responsibility Segregation (coming soon)
- **Domain Events**: Event-driven architecture (integrated in Domain Layer)
- **Authentication**: JWT Authentication (coming soon)
- **Testing**: Unit, Integration, Contract tests (Domain Layer complete)

## 🚀 Quick Start

### Prerequisites

- .NET 8 SDK
- Node.js 18+
- SQL Server (LocalDB or Azure SQL)
- Azure CLI (optional, for deployment)

### Run Locally

**Backend**
```bash
cd Backend
dotnet restore
dotnet build
dotnet test
dotnet run --project src/Portfolio.API
```

**Frontend** (coming soon)
```bash
cd Frontend
npm install
npm run dev
```

## 📚 Documentation

- [Architecture](docs/ARCHITECTURE.md)

## 🧪 Testing

```bash
# Backend tests
cd Backend
dotnet test Portfolio.sln

# Run with coverage
dotnet test Portfolio.sln --collect:"XPlat Code Coverage"

# Frontend tests (coming soon)
cd Frontend && npm test
```

**Test Coverage**: Domain layer has 100% code coverage with 150+ unit tests.

**Live Demo**: [Coming soon]

## 📊 Current Status

**✅ Completed:**
- Repository structure setup
- Documentation (Architecture)
- CI/CD templates (GitHub Actions)
- GitHub templates (PR, Issues)
- Infrastructure templates (Azure Bicep)
- Project structure (Backend, Frontend, modules folders)
- **Domain Layer (US-01)**: Complete DDD implementation
  - Entities: PortfolioProject, Skill, Experience
  - Value Objects: ProjectName, EmailAddress, Url
  - Aggregates: PortfolioProjectAggregate
  - Domain Events: PortfolioProjectCreatedEvent, PortfolioProjectFeaturedEvent, PortfolioProjectArchivedEvent
  - Domain Services: PortfolioDomainService
  - Centralized constants (ErrorMessages, FieldNames, ValidationConstants)
  - **150+ unit tests with 100% code coverage**

**🚧 Next Steps:**
- [ ] Implement Repository Pattern (US-02)
- [ ] Implement CQRS (US-03)
- [ ] Create basic REST API (US-04)
- [ ] Create basic React frontend (US-05)
- [ ] Deploy to Azure

## 👤 Author

**Sevann Radhak Triztan**
- LinkedIn: [linkedin.com/in/sevann-radhak](https://linkedin.com/in/sevann-radhak)
- Email: sevann.radhak@gmail.com
- GitHub: [github.com/sevann-radhak](https://github.com/sevann-radhak)

## 📄 License

MIT License - see [LICENSE](LICENSE) for details.
