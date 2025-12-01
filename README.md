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

## 📦 Modules

Each module is independent and demonstrative:

- **DDD Demo**: Domain-Driven Design with Entities, Value Objects, Aggregates
- **Repository Demo**: Repository Pattern with data abstraction
- **CQRS Demo**: Command Query Responsibility Segregation
- **Domain Events Demo**: Event-driven architecture
- **Auth Demo**: JWT Authentication
- **Testing Demo**: Unit, Integration, Contract tests

## 🚀 Quick Start

### Prerequisites

- .NET 8 SDK
- Node.js 18+
- SQL Server (LocalDB or Azure SQL)
- Azure CLI (optional, for deployment)

### Run Locally

**Backend** (coming soon)
```bash
cd Backend
dotnet restore
dotnet run
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
# Backend tests (coming soon)
dotnet test Backend/

# Frontend tests (coming soon)
cd Frontend && npm test
```

**Live Demo**: [Coming soon]

## 📊 Current Status

**✅ Completed:**
- Repository structure setup
- Documentation (Architecture)
- CI/CD templates (GitHub Actions)
- GitHub templates (PR, Issues)
- Infrastructure templates (Azure Bicep)
- Project structure (Backend, Frontend, modules folders)

**🚧 Next Steps:**
- [ ] Implement DDD demo module
- [ ] Implement Repository demo module
- [ ] Create basic REST API
- [ ] Create basic React frontend
- [ ] Deploy to Azure

## 👤 Author

**Sevann Radhak Triztan**
- LinkedIn: [linkedin.com/in/sevann-radhak](https://linkedin.com/in/sevann-radhak)
- Email: sevann.radhak@gmail.com
- GitHub: [github.com/sevann-radhak](https://github.com/sevann-radhak)

## 📄 License

MIT License - see [LICENSE](LICENSE) for details.
