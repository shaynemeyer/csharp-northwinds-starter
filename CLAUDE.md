# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a C# Northwind Workshop project - a comprehensive educational codebase demonstrating ASP.NET Core web development using Clean Architecture principles with the classic Northwind database. The project uses .NET 9.0, Entity Framework Core, and Tailwind CSS 4.

## Architecture

The solution follows **Clean Architecture** with three distinct layers:

```
NorthwindWorkshop/
├── NorthwindWorkshop.Core/           # Domain Layer
│   ├── Entities/                     # Business entities (Product, Customer, Order, etc.)
│   └── Interfaces/                   # Repository abstractions (IRepository<T>, ICustomerRepository)
│
├── NorthwindWorkshop.Infrastructure/ # Data Access Layer
│   ├── Data/                         # DbContext, DbInitializer (seeding)
│   ├── Repositories/                 # Repository implementations
│   └── Migrations/                   # EF Core database migrations
│
└── NorthwindWorkshop.Web/            # Presentation Layer
    ├── Pages/                        # Razor Pages (Index, Customers, Products)
    ├── ViewModels/                   # Page models (CustomerListViewModel, ProductListViewModel)
    └── wwwroot/                      # Static assets, CSS
```

## Technology Stack

- **.NET 9.0** with C# 13 features
- **ASP.NET Core 9.0** using Razor Pages
- **Entity Framework Core 9.0** with SQLite database
- **Tailwind CSS 4.x** for styling
- **Repository Pattern** with Dependency Injection
- **SQLite Database** (`northwind.db` in solution root)

## Development Commands

### Building and Running
```bash
# Build the solution
dotnet build

# Run with hot reload (from Web project directory)
dotnet watch run --project NorthwindWorkshop.Web

# Clean build
dotnet clean && dotnet build
```

### Database Operations
```bash
# Add new migration (run from Web project directory)
dotnet ef migrations add <MigrationName> --project ../NorthwindWorkshop.Infrastructure --startup-project .

# Update database
dotnet ef database update --project ../NorthwindWorkshop.Infrastructure --startup-project .

# Remove last migration
dotnet ef migrations remove --project ../NorthwindWorkshop.Infrastructure --startup-project .
```

### CSS Development (Tailwind CSS 4)
```bash
# Build CSS once (from Web project directory)
npm run css:build

# Watch for CSS changes during development
npm run css:watch
```

### VSCode Tasks
The `.vscode/tasks.json` provides shortcuts for:
- `build` - Build the solution
- `watch` - Run with hot reload
- `clean` - Clean build artifacts
- `restore` - Restore NuGet packages
- `ef-migrations-add` - Add EF Core migration
- `ef-database-update` - Apply migrations

## Key Design Patterns

### Repository Pattern
- **Generic Repository**: `IRepository<T>` with base CRUD operations in `NorthwindWorkshop.Core.Interfaces:1`
- **Specific Repositories**: `ICustomerRepository`, `IProductRepository` extend the generic pattern
- **Implementations**: Located in `NorthwindWorkshop.Infrastructure.Repositories`

### Dependency Injection
Configured in `NorthwindWorkshop.Web/Program.cs:16-20`:
```csharp
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
```

### Domain Entities
- **Base Entity**: All entities inherit from `BaseEntity` with common `Id` property
- **Business Logic**: Domain entities contain business methods (e.g., `Product.IsLowStock()`, `Product.TotalValue`)
- **Navigation Properties**: EF Core relationships configured in `NorthwindDbContext.OnModelCreating()`

## Database Configuration

### Connection String
Located in `NorthwindWorkshop.Web/appsettings.json:2-4`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=northwind.db"
}
```

### Database Seeding
- **Automatic Seeding**: `DbInitializer.Initialize()` runs on application startup in `Program.cs:24-38`
- **Sample Data**: Populates all Northwind entities (Customers, Products, Orders, etc.)

### Entity Relationships
Key relationships configured in `NorthwindDbContext`:
- **Customer → Orders**: One-to-many with SetNull on delete
- **Employee → Employee**: Self-referencing (Manager/Subordinates)
- **Product → Category/Supplier**: Many-to-one relationships
- **Order → OrderDetails**: One-to-many with composite key

## Development Workflow

1. **Starting Development**:
   ```bash
   cd NorthwindWorkshop.Web
   dotnet watch run        # Start web app with hot reload
   npm run css:watch       # Start Tailwind CSS watcher (separate terminal)
   ```

2. **Adding New Entities**:
   - Create entity in `NorthwindWorkshop.Core/Entities/`
   - Add repository interface in `NorthwindWorkshop.Core/Interfaces/`
   - Implement repository in `NorthwindWorkshop.Infrastructure/Repositories/`
   - Register in DI container in `Program.cs`
   - Add migration and update database

3. **Adding New Pages**:
   - Create Razor Page in `NorthwindWorkshop.Web/Pages/`
   - Create ViewModel in `NorthwindWorkshop.Web/ViewModels/`
   - Update navigation in `Pages/Shared/_Layout.cshtml` or `_Sidebar.cshtml`

## Important Files

- **`Program.cs`**: Application startup, DI configuration, database seeding
- **`NorthwindDbContext.cs`**: EF Core configuration, relationships, constraints
- **Entity Classes**: Rich domain models with business logic methods
- **Repository Interfaces**: Define data access contracts following SOLID principles
- **ViewModels**: Page-specific data transfer objects for Razor Pages

## Notes

- This is a **learning/workshop project** focused on demonstrating OOP principles, Clean Architecture, and ASP.NET Core patterns
- The codebase includes extensive comments explaining OOP concepts (encapsulation, inheritance, polymorphism, abstraction)
- Database uses SQLite for portability and ease of setup
- Tailwind CSS 4 provides modern utility-first styling with faster build times
- All migrations are included and database auto-seeds on first run