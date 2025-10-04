# ASP.NET Core Marketplace API

Demo backend showing CRUD + orders + design patterns.

## Tech stack
- ASP.NET Core 8
- Entity Framework Core (SQLite)
- Swagger
- MVC + Web API
- Patterns: Repository, UnitOfWork, Singleton, Factory, Decorator, Strategy, Adapter
- Helpers: Extensions, Deadlock demo, Async generator
- Docker / docker-compose (optional)

## Quick start (local)
1. Clone repo:
```bash
git clone https://github.com/<your-user>/aspnetcore-marketplace-api.git
cd aspnetcore-marketplace-api
2.Build & run migrations:
dotnet build
dotnet tool install --global dotnet-ef --version 8.*
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
3.Run:
dotnet run

## Docker quick start

```bash
# Build & run (SQLite)
docker build -t marketplace-demo .
docker run -p 8080:80 marketplace-demo

# or with PostgreSQL
docker-compose up --build
