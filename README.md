# 🛒 ASP.NET Core Marketplace API

Demo backend showing CRUD operations, Orders system, and modern design patterns.

---

## ⚙️ Tech Stack
- **ASP.NET Core 8**
- **Entity Framework Core**  
  - SQLite (Local)
  - PostgreSQL (Docker)
- **Swagger / OpenAPI**
- **MVC + Web API**
- **Design Patterns:** Repository, UnitOfWork, Singleton, Factory, Decorator, Strategy, Adapter  
- **Helpers:** Extensions, Deadlock demo, Async generator  
- **Docker & docker-compose** support

---

## 🚀 Quick Start (Local)

```bash
# 1. Clone repo
git clone https://github.com/Rufat-Baghirli/MarketplaceDemo.git
cd MarketplaceDemo

# 2. Build & apply migrations
dotnet build
dotnet tool install --global dotnet-ef --version 8.*
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update

# 3. Run app
dotnet run
```

✅ By default, local mode uses **SQLite**  
(DefaultConnection: `"Data Source=marketplace.db"`)

---

## 🐳 Run with Docker (PostgreSQL)

```bash
# Build image and run containers
docker-compose up --build
```

✅ Docker mode automatically uses **PostgreSQL** connection:  
`Host=db;Port=5432;Database=marketplace;Username=marketplace;Password=secret123`

---

## 🧩 Highlights
- Clean separation of layers (`Core`, `Data`, `Controllers`, `Models`)
- Automatic DB migration at startup
- Swagger UI for testing endpoints
- MVC-based and API-based CRUD endpoints
- Thread safety & deadlock demo
- Ready for Docker deployment

---

## 📦 appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "PostgresConnection": "Host=db;Port=5432;Database=marketplace;Username=marketplace;Password=secret123",
    "DefaultConnection": "Data Source=marketplace.db"
  }
}
```

---

## 🧠 Author
**Rufat Bagirli**  
Backend Developer (.NET / Python / Solidity)  
🌐 [github.com/Rufat-Baghirli](https://github.com/Rufat-Baghirli)
