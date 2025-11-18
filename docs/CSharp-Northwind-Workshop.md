# C# Northwind Workshop - Mastering OOP and Web Development

## Overview

This workshop is a C# equivalent of the Next.js Northwind Starter, designed to teach you modern C# development, Object-Oriented Programming principles, and ASP.NET Core web application development using the classic Northwind database.

## What You'll Learn

- **ASP.NET Core 9.0** - Modern web framework
- **Entity Framework Core** - Type-safe ORM for database operations
- **Clean Architecture** - Separation of concerns and SOLID principles
- **Razor Pages** - Server-side rendering with minimal JavaScript
- **Repository Pattern** - Data access abstraction
- **Dependency Injection** - Inversion of Control container
- **DTOs and ViewModels** - Data transfer and presentation logic
- **LINQ** - Language-Integrated Query for data manipulation
- **Async/Await** - Asynchronous programming patterns

## Technology Stack

- **Framework**: ASP.NET Core 9.0 (Web App with Razor Pages)
- **Database**: SQLite via Entity Framework Core
- **UI**: Tailwind CSS + Razor Pages
- **Language**: C# 13
- **Architecture**: Clean Architecture / N-Tier

## Prerequisites

- .NET 9.0 SDK ([Download](https://dotnet.microsoft.com/download))
- Visual Studio Code ([Download](https://code.visualstudio.com/))
- VSCode Extensions (install these):
  - C# Dev Kit (Microsoft)
  - C# (Microsoft)
  - NuGet Package Manager
  - SQLite Viewer (optional but helpful)
- Basic understanding of C# syntax
- SQL fundamentals (helpful but not required)
- Works on: macOS, Linux, and Windows

## Setting Up Your Development Environment

### 1. Install .NET SDK

**macOS (using Homebrew):**
```bash
brew install --cask dotnet-sdk
```

**Linux (Ubuntu/Debian):**
```bash
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 9.0
```

**Windows:**
Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download)

**Verify installation:**
```bash
dotnet --version
# Should output: 9.0.x or higher
```

### 2. Install VSCode Extensions

Open VSCode and install these extensions:

1. **C# Dev Kit** - Official Microsoft extension for C# development
   - Provides IntelliSense, debugging, and project management
   - Press `Cmd+Shift+X` (Mac) or `Ctrl+Shift+X` (Windows/Linux)
   - Search for "C# Dev Kit" and install

2. **NuGet Package Manager** - Manage NuGet packages
   - Search for "NuGet Package Manager" and install

3. **SQLite Viewer** (Optional) - View your SQLite database
   - Search for "SQLite Viewer" and install

### 3. Configure VSCode for .NET

VSCode will automatically detect your .NET projects and create a `.vscode` folder with:
- `launch.json` - Debug configurations
- `tasks.json` - Build tasks

These will be auto-generated when you first open the project!

## Project Structure

```
NorthwindWorkshop/
├── NorthwindWorkshop.Web/              # Presentation Layer (Razor Pages)
│   ├── Pages/
│   │   ├── Customers/
│   │   │   ├── Index.cshtml            # Customer list view
│   │   │   ├── Index.cshtml.cs         # Page model (code-behind)
│   │   │   ├── Details.cshtml          # Customer details
│   │   │   └── Details.cshtml.cs
│   │   ├── Employees/
│   │   ├── Products/
│   │   ├── Orders/
│   │   └── Shared/
│   │       ├── _Layout.cshtml          # Master layout
│   │       └── _Sidebar.cshtml         # Navigation sidebar
│   ├── ViewModels/                     # View-specific models
│   ├── wwwroot/                        # Static files (CSS, JS, images)
│   ├── Program.cs                      # Application entry point
│   └── appsettings.json                # Configuration
│
├── NorthwindWorkshop.Core/             # Domain Layer (Business Logic)
│   ├── Entities/                       # Domain entities
│   │   ├── Customer.cs
│   │   ├── Employee.cs
│   │   ├── Product.cs
│   │   ├── Order.cs
│   │   ├── OrderDetail.cs
│   │   ├── Category.cs
│   │   ├── Supplier.cs
│   │   └── Shipper.cs
│   ├── Interfaces/                     # Repository interfaces
│   │   ├── IRepository.cs
│   │   ├── ICustomerRepository.cs
│   │   ├── IEmployeeRepository.cs
│   │   └── IProductRepository.cs
│   └── Services/                       # Business logic services
│       ├── ICustomerService.cs
│       └── CustomerService.cs
│
└── NorthwindWorkshop.Infrastructure/   # Data Access Layer
    ├── Data/
    │   ├── NorthwindDbContext.cs       # EF Core DbContext
    │   └── DbInitializer.cs            # Database seeding
    ├── Repositories/                    # Repository implementations
    │   ├── Repository.cs                # Generic repository
    │   ├── CustomerRepository.cs
    │   ├── EmployeeRepository.cs
    │   └── ProductRepository.cs
    └── Migrations/                      # EF Core migrations
```

## Architecture Principles

### 1. **Separation of Concerns**
- **Web Layer**: Handles HTTP requests, UI rendering
- **Core Layer**: Contains business logic and domain models
- **Infrastructure Layer**: Manages data access and external dependencies

### 2. **Dependency Inversion**
- High-level modules don't depend on low-level modules
- Both depend on abstractions (interfaces)

### 3. **Repository Pattern**
- Abstracts data access logic
- Makes testing easier with mock repositories

---

## Workshop Steps

## Part 1: Setting Up the Solution

### Step 1.1: Create the Solution Structure

```bash
# Create solution
dotnet new sln -n NorthwindWorkshop

# Create projects
dotnet new classlib -n NorthwindWorkshop.Core
dotnet new classlib -n NorthwindWorkshop.Infrastructure
dotnet new webapp -n NorthwindWorkshop.Web

# Add projects to solution
dotnet sln add NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj
dotnet sln add NorthwindWorkshop.Infrastructure/NorthwindWorkshop.Infrastructure.csproj
dotnet sln add NorthwindWorkshop.Web/NorthwindWorkshop.Web.csproj

# Set up project references
cd NorthwindWorkshop.Infrastructure
dotnet add reference ../NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj

cd ../NorthwindWorkshop.Web
dotnet add reference ../NorthwindWorkshop.Core/NorthwindWorkshop.Core.csproj
dotnet add reference ../NorthwindWorkshop.Infrastructure/NorthwindWorkshop.Infrastructure.csproj
```

### Step 1.2: Install Required NuGet Packages

```bash
# Infrastructure project
cd NorthwindWorkshop.Infrastructure
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design

# Web project
cd ../NorthwindWorkshop.Web
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### Step 1.3: Open in VSCode

```bash
# From the solution root directory
cd ..
code .
```

**VSCode will automatically:**
- Detect your .NET solution
- Create `.vscode/launch.json` for debugging
- Create `.vscode/tasks.json` for building
- Enable IntelliSense and code completion

**Verify everything works:**
1. Press `F5` to start debugging (select "C#" if prompted)
2. VSCode should build and run the Web project
3. Your default browser should open to the app

### Step 1.4: Install and Configure Tailwind CSS

Tailwind CSS can be integrated into ASP.NET Core using the Tailwind CLI.

**Install Node.js (if not already installed):**
- **macOS**: `brew install node`
- **Linux**: `sudo apt install nodejs npm` or `sudo yum install nodejs npm`
- **Windows**: Download from [nodejs.org](https://nodejs.org)

**Setup Tailwind in your Web project:**

```bash
cd NorthwindWorkshop.Web

# Initialize npm
npm init -y

# Install Tailwind CSS v4
npm install tailwindcss @tailwindcss/cli

# Create the CSS directory
mkdir -p wwwroot/css
```

**Create the Tailwind input file:**

Create `wwwroot/css/app.css`:
```css
/* Import Tailwind's base styles */
@import "tailwindcss";

/* Configure theme (replaces tailwind.config.js) */
@theme {
  /* Customize colors */
  --color-primary: #3b82f6;
  --color-secondary: #6b7280;
}

/* Configure content paths (what files to scan) */
@source "../../Pages/**/*.cshtml";
@source "../../Views/**/*.cshtml";

/* Custom Component Classes */
@layer components {
  .btn-primary {
    @apply bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition-colors;
  }
  
  .btn-secondary {
    @apply bg-gray-600 text-white px-4 py-2 rounded-lg hover:bg-gray-700 transition-colors;
  }
  
  .card {
    @apply bg-white rounded-lg shadow-md overflow-hidden;
  }
  
  .badge {
    @apply inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium;
  }
  
  .badge-primary {
    @apply bg-blue-100 text-blue-800;
  }
  
  .badge-success {
    @apply bg-green-100 text-green-800;
  }
  
  .badge-warning {
    @apply bg-yellow-100 text-yellow-800;
  }
  
  .badge-danger {
    @apply bg-red-100 text-red-800;
  }
  
  .badge-info {
    @apply bg-cyan-100 text-cyan-800;
  }
}

**Add build script to package.json:**

Update your `package.json` to include:
```json
{
  "name": "northwindworkshop.web",
  "version": "1.0.0",
  "scripts": {
    "css:build": "tailwindcss -i ./wwwroot/css/app.css -o ./wwwroot/css/output.css --minify",
    "css:watch": "tailwindcss -i ./wwwroot/css/app.css -o ./wwwroot/css/output.css --watch"
  },
  "devDependencies": {
    "@tailwindcss/cli": "^4.0.0",
    "tailwindcss": "^4.0.0"
  }
}
```

**Build Tailwind CSS:**

```bash
# Build for production
npm run css:build

# Or watch for changes during development
npm run css:watch
```

**Add to .gitignore:**
```
node_modules/
wwwroot/css/output.css
```

**Useful VSCode Shortcuts:**
- `Cmd+Shift+P` (Mac) / `Ctrl+Shift+P` (Win/Linux) - Command Palette
- `F5` - Start Debugging
- `Ctrl+F5` - Run Without Debugging  
- `Cmd+.` (Mac) / `Ctrl+.` (Win/Linux) - Quick Fix/Code Actions
- `F12` - Go to Definition
- `Shift+F12` - Find All References

---

## Part 2: Creating Domain Entities

### Step 2.1: Create Base Entity Class

**File**: `NorthwindWorkshop.Core/Entities/BaseEntity.cs`

```csharp
namespace NorthwindWorkshop.Core.Entities;

/// <summary>
/// Base class for all entities in the domain
/// Demonstrates OOP principle: Inheritance
/// </summary>
public abstract class BaseEntity
{
    // Primary key - common to all entities
    public int Id { get; set; }
}
```

### Step 2.2: Create Customer Entity

**File**: `NorthwindWorkshop.Core/Entities/Customer.cs`

```csharp
namespace NorthwindWorkshop.Core.Entities;

/// <summary>
/// Represents a customer in the Northwind system
/// Demonstrates: OOP Encapsulation, Properties, Navigation Properties
/// </summary>
public class Customer : BaseEntity
{
    // Properties - Encapsulation of data
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? ContactTitle { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    
    // Navigation Property - Represents relationship (One-to-Many)
    // One Customer can have many Orders
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    
    // Computed property - demonstrates logic in entities
    public string DisplayName => $"{CompanyName} ({ContactName})";
}
```

### Step 2.3: Create Product Entity

**File**: `NorthwindWorkshop.Core/Entities/Product.cs`

```csharp
namespace NorthwindWorkshop.Core.Entities;

/// <summary>
/// Represents a product in inventory
/// Demonstrates: Value objects, business rules
/// </summary>
public class Product : BaseEntity
{
    public string ProductName { get; set; } = string.Empty;
    public int? SupplierId { get; set; }
    public int? CategoryId { get; set; }
    public string? QuantityPerUnit { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }
    
    // Navigation Properties
    public virtual Supplier? Supplier { get; set; }
    public virtual Category? Category { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    
    // Business logic method - demonstrates behavior in domain models
    public bool IsLowStock()
    {
        return UnitsInStock.HasValue && 
               ReorderLevel.HasValue && 
               UnitsInStock < ReorderLevel;
    }
    
    // Computed property
    public decimal TotalValue => (UnitPrice ?? 0) * (UnitsInStock ?? 0);
}
```

### Step 2.4: Create Category, Supplier, and Other Entities

**File**: `NorthwindWorkshop.Core/Entities/Category.cs`

```csharp
namespace NorthwindWorkshop.Core.Entities;

public class Category : BaseEntity
{
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public byte[]? Picture { get; set; }
    
    // Navigation property
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
```

**File**: `NorthwindWorkshop.Core/Entities/Supplier.cs`

```csharp
namespace NorthwindWorkshop.Core.Entities;

public class Supplier : BaseEntity
{
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? ContactTitle { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? HomePage { get; set; }
    
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
```

**File**: `NorthwindWorkshop.Core/Entities/Order.cs`

```csharp
namespace NorthwindWorkshop.Core.Entities;

public class Order : BaseEntity
{
    public int? CustomerId { get; set; }
    public int? EmployeeId { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public int? ShipVia { get; set; }
    public decimal? Freight { get; set; }
    public string? ShipName { get; set; }
    public string? ShipAddress { get; set; }
    public string? ShipCity { get; set; }
    public string? ShipRegion { get; set; }
    public string? ShipPostalCode { get; set; }
    public string? ShipCountry { get; set; }
    
    // Navigation properties
    public virtual Customer? Customer { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual Shipper? Shipper { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    
    // Business method
    public decimal CalculateTotal()
    {
        return OrderDetails.Sum(od => od.Quantity * od.UnitPrice * (1 - od.Discount));
    }
}
```

**File**: `NorthwindWorkshop.Core/Entities/OrderDetail.cs`

```csharp
namespace NorthwindWorkshop.Core.Entities;

/// <summary>
/// Represents a line item in an order
/// Composite key: OrderId + ProductId
/// </summary>
public class OrderDetail
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    public float Discount { get; set; }
    
    // Navigation properties
    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
    
    // Computed property
    public decimal LineTotal => Convert.ToDecimal(Quantity * UnitPrice * (1 - Discount));
}
```

**File**: `NorthwindWorkshop.Core/Entities/Employee.cs`

```csharp
namespace NorthwindWorkshop.Core.Entities;

public class Employee : BaseEntity
{
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? TitleOfCourtesy { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? HireDate { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? HomePhone { get; set; }
    public string? Extension { get; set; }
    public byte[]? Photo { get; set; }
    public string? Notes { get; set; }
    public int? ReportsTo { get; set; }
    public string? PhotoPath { get; set; }
    
    // Self-referencing relationship (Manager)
    public virtual Employee? Manager { get; set; }
    public virtual ICollection<Employee> Subordinates { get; set; } = new List<Employee>();
    
    // Navigation property
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    
    // Computed property
    public string FullName => $"{FirstName} {LastName}";
}
```

**File**: `NorthwindWorkshop.Core/Entities/Shipper.cs`

```csharp
namespace NorthwindWorkshop.Core.Entities;

public class Shipper : BaseEntity
{
    public string CompanyName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
```

---

## Part 3: Repository Pattern & Interfaces

### Step 3.1: Create Generic Repository Interface

**File**: `NorthwindWorkshop.Core/Interfaces/IRepository.cs`

```csharp
using System.Linq.Expressions;

namespace NorthwindWorkshop.Core.Interfaces;

/// <summary>
/// Generic repository interface
/// Demonstrates: OOP Abstraction, Generics, SOLID (Interface Segregation)
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IRepository<T> where T : class
{
    // Query methods
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    
    // Command methods
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    
    // Check existence
    Task<bool> ExistsAsync(int id);
    
    // Count
    Task<int> CountAsync();
}
```

### Step 3.2: Create Specific Repository Interfaces

**File**: `NorthwindWorkshop.Core/Interfaces/ICustomerRepository.cs`

```csharp
using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Core.Interfaces;

/// <summary>
/// Customer-specific repository operations
/// Extends the generic repository with domain-specific methods
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetCustomersByCountryAsync(string country);
    Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync();
    Task<Customer?> GetCustomerWithOrdersAsync(int customerId);
    Task<IEnumerable<string>> GetDistinctCountriesAsync();
}
```

**File**: `NorthwindWorkshop.Core/Interfaces/IProductRepository.cs`

```csharp
using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Core.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<IEnumerable<Product>> GetLowStockProductsAsync();
    Task<IEnumerable<Product>> GetDiscontinuedProductsAsync();
    Task<IEnumerable<Product>> GetProductsWithDetailsAsync();
}
```

**File**: `NorthwindWorkshop.Core/Interfaces/IEmployeeRepository.cs`

```csharp
using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Core.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<IEnumerable<Employee>> GetEmployeesByManagerAsync(int managerId);
    Task<IEnumerable<Employee>> GetEmployeesWithOrdersAsync();
}
```

---

## Part 4: Data Access with Entity Framework Core

### Step 4.1: Create DbContext

**File**: `NorthwindWorkshop.Infrastructure/Data/NorthwindDbContext.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Infrastructure.Data;

/// <summary>
/// Database context for Northwind
/// Demonstrates: EF Core configuration, DbSets, Fluent API
/// </summary>
public class NorthwindDbContext : DbContext
{
    public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options)
        : base(options)
    {
    }
    
    // DbSets represent tables in the database
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
    public DbSet<Shipper> Shippers { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure composite key for OrderDetail
        modelBuilder.Entity<OrderDetail>()
            .HasKey(od => new { od.OrderId, od.ProductId });
        
        // Configure relationships
        ConfigureCustomerRelationships(modelBuilder);
        ConfigureEmployeeRelationships(modelBuilder);
        ConfigureProductRelationships(modelBuilder);
        ConfigureOrderRelationships(modelBuilder);
        
        // Configure decimal precision
        ConfigureDecimalProperties(modelBuilder);
    }
    
    private void ConfigureCustomerRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
    
    private void ConfigureEmployeeRelationships(ModelBuilder modelBuilder)
    {
        // Self-referencing relationship (Manager-Subordinate)
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Manager)
            .WithMany(e => e.Subordinates)
            .HasForeignKey(e => e.ReportsTo)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Orders)
            .WithOne(o => o.Employee)
            .HasForeignKey(o => o.EmployeeId);
    }
    
    private void ConfigureProductRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
        
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId);
    }
    
    private void ConfigureOrderRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId);
        
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.ProductId);
        
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Shipper)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.ShipVia);
    }
    
    private void ConfigureDecimalProperties(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.UnitPrice)
            .HasPrecision(18, 2);
        
        modelBuilder.Entity<Order>()
            .Property(o => o.Freight)
            .HasPrecision(18, 2);
        
        modelBuilder.Entity<OrderDetail>()
            .Property(od => od.UnitPrice)
            .HasPrecision(18, 2);
    }
}
```

### Step 4.2: Implement Generic Repository

**File**: `NorthwindWorkshop.Infrastructure/Repositories/Repository.cs`

```csharp
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;

namespace NorthwindWorkshop.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation
/// Demonstrates: OOP Polymorphism, Async/Await, LINQ
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly NorthwindDbContext _context;
    protected readonly DbSet<T> _dbSet;
    
    public Repository(NorthwindDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    
    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
    
    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public virtual async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    
    public virtual async Task<bool> ExistsAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }
    
    public virtual async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }
}
```

### Step 4.3: Implement Customer Repository

**File**: `NorthwindWorkshop.Infrastructure/Repositories/CustomerRepository.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;

namespace NorthwindWorkshop.Infrastructure.Repositories;

/// <summary>
/// Customer repository with domain-specific queries
/// Demonstrates: Method override, LINQ queries, Eager loading
/// </summary>
public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(NorthwindDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Customer>> GetCustomersByCountryAsync(string country)
    {
        return await _dbSet
            .Where(c => c.Country == country)
            .OrderBy(c => c.CompanyName)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync()
    {
        return await _dbSet
            .Include(c => c.Orders)
            .Where(c => c.Orders.Any())
            .ToListAsync();
    }
    
    public async Task<Customer?> GetCustomerWithOrdersAsync(int customerId)
    {
        return await _dbSet
            .Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(c => c.Id == customerId);
    }
    
    public async Task<IEnumerable<string>> GetDistinctCountriesAsync()
    {
        return await _dbSet
            .Select(c => c.Country)
            .Where(c => c != null)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync()!;
    }
}
```

### Step 4.4: Implement Product Repository

**File**: `NorthwindWorkshop.Infrastructure/Repositories/ProductRepository.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;

namespace NorthwindWorkshop.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(NorthwindDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Supplier)
            .OrderBy(p => p.ProductName)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Product>> GetLowStockProductsAsync()
    {
        return await _dbSet
            .Where(p => p.UnitsInStock < p.ReorderLevel && !p.Discontinued)
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .OrderBy(p => p.ProductName)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Product>> GetDiscontinuedProductsAsync()
    {
        return await _dbSet
            .Where(p => p.Discontinued)
            .Include(p => p.Category)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Product>> GetProductsWithDetailsAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .OrderBy(p => p.ProductName)
            .ToListAsync();
    }
}
```

### Step 4.5: Implement Employee Repository

**File**: `NorthwindWorkshop.Infrastructure/Repositories/EmployeeRepository.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;

namespace NorthwindWorkshop.Infrastructure.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(NorthwindDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Employee>> GetEmployeesByManagerAsync(int managerId)
    {
        return await _dbSet
            .Where(e => e.ReportsTo == managerId)
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Employee>> GetEmployeesWithOrdersAsync()
    {
        return await _dbSet
            .Include(e => e.Orders)
            .Include(e => e.Manager)
            .OrderBy(e => e.LastName)
            .ToListAsync();
    }
}
```

---

## Part 5: Database Seeding

**File**: `NorthwindWorkshop.Infrastructure/Data/DbInitializer.cs`

```csharp
using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Infrastructure.Data;

/// <summary>
/// Seeds the database with initial data
/// </summary>
public static class DbInitializer
{
    public static void Initialize(NorthwindDbContext context)
    {
        // Ensure database is created
        context.Database.EnsureCreated();
        
        // Check if already seeded
        if (context.Customers.Any())
        {
            return; // DB has been seeded
        }
        
        // Seed Categories
        var categories = new[]
        {
            new Category { CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" },
            new Category { CategoryName = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings" },
            new Category { CategoryName = "Confections", Description = "Desserts, candies, and sweet breads" },
            new Category { CategoryName = "Dairy Products", Description = "Cheeses" },
            new Category { CategoryName = "Grains/Cereals", Description = "Breads, crackers, pasta, and cereal" },
            new Category { CategoryName = "Meat/Poultry", Description = "Prepared meats" },
            new Category { CategoryName = "Produce", Description = "Dried fruit and bean curd" },
            new Category { CategoryName = "Seafood", Description = "Seaweed and fish" }
        };
        context.Categories.AddRange(categories);
        context.SaveChanges();
        
        // Seed Suppliers
        var suppliers = new[]
        {
            new Supplier { CompanyName = "Exotic Liquids", ContactName = "Charlotte Cooper", City = "London", Country = "UK", Phone = "(171) 555-2222" },
            new Supplier { CompanyName = "New Orleans Cajun Delights", ContactName = "Shelley Burke", City = "New Orleans", Country = "USA", Phone = "(100) 555-4822" },
            new Supplier { CompanyName = "Grandma Kelly's Homestead", ContactName = "Regina Murphy", City = "Ann Arbor", Country = "USA", Phone = "(313) 555-5735" },
            new Supplier { CompanyName = "Tokyo Traders", ContactName = "Yoshi Nagase", City = "Tokyo", Country = "Japan", Phone = "(03) 3555-5011" },
            new Supplier { CompanyName = "Cooperativa de Quesos", ContactName = "Antonio del Valle Saavedra", City = "Oviedo", Country = "Spain", Phone = "(98) 598 76 54" }
        };
        context.Suppliers.AddRange(suppliers);
        context.SaveChanges();
        
        // Seed Products
        var products = new[]
        {
            new Product { ProductName = "Chai", CategoryId = 1, SupplierId = 1, UnitPrice = 18.00m, UnitsInStock = 39, ReorderLevel = 10 },
            new Product { ProductName = "Chang", CategoryId = 1, SupplierId = 1, UnitPrice = 19.00m, UnitsInStock = 17, ReorderLevel = 25 },
            new Product { ProductName = "Aniseed Syrup", CategoryId = 2, SupplierId = 1, UnitPrice = 10.00m, UnitsInStock = 13, ReorderLevel = 25 },
            new Product { ProductName = "Chef Anton's Cajun Seasoning", CategoryId = 2, SupplierId = 2, UnitPrice = 22.00m, UnitsInStock = 53, ReorderLevel = 0 },
            new Product { ProductName = "Gumbo Mix", CategoryId = 2, SupplierId = 2, UnitPrice = 21.35m, UnitsInStock = 0, ReorderLevel = 0, Discontinued = true },
            new Product { ProductName = "Grandma's Boysenberry Spread", CategoryId = 2, SupplierId = 3, UnitPrice = 25.00m, UnitsInStock = 120, ReorderLevel = 25 },
            new Product { ProductName = "Uncle Bob's Organic Dried Pears", CategoryId = 7, SupplierId = 3, UnitPrice = 30.00m, UnitsInStock = 15, ReorderLevel = 10 },
            new Product { ProductName = "Northwoods Cranberry Sauce", CategoryId = 2, SupplierId = 3, UnitPrice = 40.00m, UnitsInStock = 6, ReorderLevel = 0 },
            new Product { ProductName = "Mishi Kobe Niku", CategoryId = 6, SupplierId = 4, UnitPrice = 97.00m, UnitsInStock = 29, ReorderLevel = 0 },
            new Product { ProductName = "Ikura", CategoryId = 8, SupplierId = 4, UnitPrice = 31.00m, UnitsInStock = 31, ReorderLevel = 0 }
        };
        context.Products.AddRange(products);
        context.SaveChanges();
        
        // Seed Customers
        var customers = new[]
        {
            new Customer { CompanyName = "Alfreds Futterkiste", ContactName = "Maria Anders", City = "Berlin", Country = "Germany", Phone = "030-0074321" },
            new Customer { CompanyName = "Ana Trujillo Emparedados", ContactName = "Ana Trujillo", City = "México D.F.", Country = "Mexico", Phone = "(5) 555-4729" },
            new Customer { CompanyName = "Antonio Moreno Taquería", ContactName = "Antonio Moreno", City = "México D.F.", Country = "Mexico", Phone = "(5) 555-3932" },
            new Customer { CompanyName = "Around the Horn", ContactName = "Thomas Hardy", City = "London", Country = "UK", Phone = "(171) 555-7788" },
            new Customer { CompanyName = "Berglunds snabbköp", ContactName = "Christina Berglund", City = "Luleå", Country = "Sweden", Phone = "0921-12 34 65" },
            new Customer { CompanyName = "Blauer See Delikatessen", ContactName = "Hanna Moos", City = "Mannheim", Country = "Germany", Phone = "0621-08460" },
            new Customer { CompanyName = "Blondel père et fils", ContactName = "Frédérique Citeaux", City = "Strasbourg", Country = "France", Phone = "88.60.15.31" },
            new Customer { CompanyName = "Bólido Comidas preparadas", ContactName = "Martín Sommer", City = "Madrid", Country = "Spain", Phone = "(91) 555 22 82" },
            new Customer { CompanyName = "Bon app'", ContactName = "Laurence Lebihan", City = "Marseille", Country = "France", Phone = "91.24.45.40" },
            new Customer { CompanyName = "Bottom-Dollar Markets", ContactName = "Elizabeth Lincoln", City = "Tsawassen", Country = "Canada", Phone = "(604) 555-4729" }
        };
        context.Customers.AddRange(customers);
        context.SaveChanges();
        
        // Seed Employees
        var employees = new[]
        {
            new Employee { FirstName = "Nancy", LastName = "Davolio", Title = "Sales Representative", HireDate = new DateTime(1992, 5, 1), City = "Seattle", Country = "USA" },
            new Employee { FirstName = "Andrew", LastName = "Fuller", Title = "Vice President, Sales", HireDate = new DateTime(1992, 8, 14), City = "Tacoma", Country = "USA" },
            new Employee { FirstName = "Janet", LastName = "Leverling", Title = "Sales Representative", HireDate = new DateTime(1992, 4, 1), City = "Kirkland", Country = "USA", ReportsTo = 2 },
            new Employee { FirstName = "Margaret", LastName = "Peacock", Title = "Sales Representative", HireDate = new DateTime(1993, 5, 3), City = "Redmond", Country = "USA", ReportsTo = 2 },
            new Employee { FirstName = "Steven", LastName = "Buchanan", Title = "Sales Manager", HireDate = new DateTime(1993, 10, 17), City = "London", Country = "UK", ReportsTo = 2 }
        };
        context.Employees.AddRange(employees);
        context.SaveChanges();
        
        // Seed Shippers
        var shippers = new[]
        {
            new Shipper { CompanyName = "Speedy Express", Phone = "(503) 555-9831" },
            new Shipper { CompanyName = "United Package", Phone = "(503) 555-3199" },
            new Shipper { CompanyName = "Federal Shipping", Phone = "(503) 555-9931" }
        };
        context.Shippers.AddRange(shippers);
        context.SaveChanges();
    }
}
```

---

## Part 6: Web Application Configuration

### Step 6.1: Configure Program.cs

**File**: `NorthwindWorkshop.Web/Program.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;
using NorthwindWorkshop.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();

// Configure SQLite Database
builder.Services.AddDbContext<NorthwindDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories with Dependency Injection
// Demonstrates: SOLID (Dependency Inversion Principle)
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<NorthwindDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
```

### Step 6.2: Configure Connection String

**File**: `NorthwindWorkshop.Web/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=northwind.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

---

## Part 7: Creating ViewModels and Pages

### Step 7.1: Create ViewModels

**File**: `NorthwindWorkshop.Web/ViewModels/CustomerListViewModel.cs`

```csharp
namespace NorthwindWorkshop.Web.ViewModels;

/// <summary>
/// ViewModel for displaying customers in a list
/// Demonstrates: DTO pattern, separation of concerns
/// </summary>
public class CustomerListViewModel
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public int OrderCount { get; set; }
}
```

**File**: `NorthwindWorkshop.Web/ViewModels/ProductListViewModel.cs`

```csharp
namespace NorthwindWorkshop.Web.ViewModels;

public class ProductListViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
    public string? SupplierName { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public bool Discontinued { get; set; }
    public bool IsLowStock { get; set; }
}
```

### Step 7.2: Create Customer List Page

**File**: `NorthwindWorkshop.Web/Pages/Customers/Index.cshtml.cs`

```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Web.ViewModels;

namespace NorthwindWorkshop.Web.Pages.Customers;

/// <summary>
/// Page model for customer list
/// Demonstrates: MVVM pattern, Dependency Injection, LINQ projections
/// </summary>
public class IndexModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;
    
    public IndexModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public List<CustomerListViewModel> Customers { get; set; } = new();
    public string? SearchTerm { get; set; }
    public string? CountryFilter { get; set; }
    public List<string> Countries { get; set; } = new();
    
    public async Task OnGetAsync(string? searchTerm, string? country)
    {
        SearchTerm = searchTerm;
        CountryFilter = country;
        
        // Get all customers with orders
        var customers = await _customerRepository.GetCustomersWithOrdersAsync();
        
        // Apply filters
        var query = customers.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(c => 
                c.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (c.ContactName != null && c.ContactName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        }
        
        if (!string.IsNullOrWhiteSpace(country))
        {
            query = query.Where(c => c.Country == country);
        }
        
        // Project to ViewModel
        Customers = query
            .Select(c => new CustomerListViewModel
            {
                Id = c.Id,
                CompanyName = c.CompanyName,
                ContactName = c.ContactName,
                City = c.City,
                Country = c.Country,
                Phone = c.Phone,
                OrderCount = c.Orders.Count
            })
            .OrderBy(c => c.CompanyName)
            .ToList();
        
        // Get distinct countries for filter dropdown
        Countries = (await _customerRepository.GetDistinctCountriesAsync()).ToList();
    }
}
```

**File**: `NorthwindWorkshop.Web/Pages/Customers/Index.cshtml`

```html
@page
@model NorthwindWorkshop.Web.Pages.Customers.IndexModel
@{
    ViewData["Title"] = "Customers";
}

<div class="max-w-7xl mx-auto">
    <div class="flex justify-between items-center mb-6">
        <h1 class="text-3xl font-bold text-gray-900">Customers</h1>
        <span class="badge badge-primary text-lg">@Model.Customers.Count customers</span>
    </div>
    
    <!-- Search and Filter -->
    <div class="card mb-6">
        <div class="p-6">
            <form method="get" class="grid grid-cols-1 md:grid-cols-4 gap-4">
                <div class="md:col-span-2">
                    <label for="searchTerm" class="block text-sm font-medium text-gray-700 mb-2">Search</label>
                    <input type="text" 
                           class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent" 
                           id="searchTerm" 
                           name="searchTerm" 
                           value="@Model.SearchTerm" 
                           placeholder="Search by company or contact name...">
                </div>
                <div>
                    <label for="country" class="block text-sm font-medium text-gray-700 mb-2">Country</label>
                    <select class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent" 
                            id="country" 
                            name="country">
                        <option value="">All Countries</option>
                        @foreach (var country in Model.Countries)
                        {
                            <option value="@country" selected="@(country == Model.CountryFilter)">@country</option>
                        }
                    </select>
                </div>
                <div class="flex items-end">
                    <button type="submit" class="btn-primary w-full">Filter</button>
                </div>
            </form>
        </div>
    </div>
    
    <!-- Customer Table -->
    <div class="card">
        <div class="overflow-x-auto">
            <table class="min-w-full divide-y divide-gray-200">
                <thead class="bg-gray-50">
                    <tr>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Company Name
                        </th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Contact Name
                        </th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            City
                        </th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Country
                        </th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Phone
                        </th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Orders
                        </th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Actions
                        </th>
                    </tr>
                </thead>
                <tbody class="bg-white divide-y divide-gray-200">
                    @foreach (var customer in Model.Customers)
                    {
                        <tr class="hover:bg-gray-50 transition-colors">
                            <td class="px-6 py-4 whitespace-nowrap">
                                <div class="text-sm font-medium text-gray-900">@customer.CompanyName</div>
                            </td>
                            <td class="px-6 py-4 whitespace-nowrap">
                                <div class="text-sm text-gray-500">@customer.ContactName</div>
                            </td>
                            <td class="px-6 py-4 whitespace-nowrap">
                                <div class="text-sm text-gray-500">@customer.City</div>
                            </td>
                            <td class="px-6 py-4 whitespace-nowrap">
                                <div class="text-sm text-gray-500">@customer.Country</div>
                            </td>
                            <td class="px-6 py-4 whitespace-nowrap">
                                <div class="text-sm text-gray-500">@customer.Phone</div>
                            </td>
                            <td class="px-6 py-4 whitespace-nowrap text-center">
                                <span class="badge badge-info">@customer.OrderCount</span>
                            </td>
                            <td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
                                <a asp-page="Details" 
                                   asp-route-id="@customer.Id" 
                                   class="text-blue-600 hover:text-blue-900">
                                    View Details
                                </a>
                            </td>
                        </tr>
                    }
                </tbody>
            </table>
            
            @if (!Model.Customers.Any())
            {
                <div class="text-center py-12">
                    <svg class="mx-auto h-12 w-12 text-gray-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M20 13V6a2 2 0 00-2-2H6a2 2 0 00-2 2v7m16 0v5a2 2 0 01-2 2H6a2 2 0 01-2-2v-5m16 0h-2.586a1 1 0 00-.707.293l-2.414 2.414a1 1 0 01-.707.293h-3.172a1 1 0 01-.707-.293l-2.414-2.414A1 1 0 006.586 13H4" />
                    </svg>
                    <h3 class="mt-2 text-sm font-medium text-gray-900">No customers found</h3>
                    <p class="mt-1 text-sm text-gray-500">Try adjusting your search or filter criteria.</p>
                </div>
            }
        </div>
    </div>
</div>
```

### Step 7.3: Create Product List Page

**File**: `NorthwindWorkshop.Web/Pages/Products/Index.cshtml.cs`

```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Web.ViewModels;

namespace NorthwindWorkshop.Web.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductRepository _productRepository;
    
    public IndexModel(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public List<ProductListViewModel> Products { get; set; } = new();
    public string? SearchTerm { get; set; }
    public bool ShowDiscontinued { get; set; }
    public bool ShowLowStock { get; set; }
    
    public async Task OnGetAsync(string? searchTerm, bool showDiscontinued = false, bool showLowStock = false)
    {
        SearchTerm = searchTerm;
        ShowDiscontinued = showDiscontinued;
        ShowLowStock = showLowStock;
        
        var products = await _productRepository.GetProductsWithDetailsAsync();
        
        // Apply filters
        var query = products.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => 
                p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
        
        if (!showDiscontinued)
        {
            query = query.Where(p => !p.Discontinued);
        }
        
        if (showLowStock)
        {
            query = query.Where(p => p.IsLowStock());
        }
        
        // Project to ViewModel
        Products = query
            .Select(p => new ProductListViewModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                CategoryName = p.Category?.CategoryName,
                SupplierName = p.Supplier?.CompanyName,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                Discontinued = p.Discontinued,
                IsLowStock = p.IsLowStock()
            })
            .OrderBy(p => p.ProductName)
            .ToList();
    }
}
```

**File**: `NorthwindWorkshop.Web/Pages/Products/Index.cshtml`

```html
@page
@model NorthwindWorkshop.Web.Pages.Products.IndexModel
@{
    ViewData["Title"] = "Products";
}

<div class="max-w-7xl mx-auto">
    <div class="flex justify-between items-center mb-6">
        <h1 class="text-3xl font-bold text-gray-900">Products</h1>
        <span class="badge badge-primary text-lg">@Model.Products.Count products</span>
    </div>
    
    <!-- Search and Filter -->
    <div class="card mb-6">
        <div class="p-6">
            <form method="get" class="grid grid-cols-1 md:grid-cols-3 gap-4">
                <div>
                    <label for="searchTerm" class="block text-sm font-medium text-gray-700 mb-2">Search Products</label>
                    <input type="text" 
                           class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent" 
                           id="searchTerm" 
                           name="searchTerm" 
                           value="@Model.SearchTerm" 
                           placeholder="Search by product name...">
                </div>
                <div class="flex items-end gap-4">
                    <div class="flex items-center">
                        <input class="w-4 h-4 text-blue-600 rounded focus:ring-blue-500" 
                               type="checkbox" 
                               id="showLowStock" 
                               name="showLowStock" 
                               value="true" 
                               checked="@Model.ShowLowStock">
                        <label class="ml-2 text-sm text-gray-700" for="showLowStock">
                            Show Low Stock Only
                        </label>
                    </div>
                    <div class="flex items-center">
                        <input class="w-4 h-4 text-blue-600 rounded focus:ring-blue-500" 
                               type="checkbox" 
                               id="showDiscontinued" 
                               name="showDiscontinued" 
                               value="true" 
                               checked="@Model.ShowDiscontinued">
                        <label class="ml-2 text-sm text-gray-700" for="showDiscontinued">
                            Include Discontinued
                        </label>
                    </div>
                </div>
                <div class="flex items-end">
                    <button type="submit" class="btn-primary w-full">Filter</button>
                </div>
            </form>
        </div>
    </div>
    
    <!-- Product Table -->
    <div class="card">
        <div class="overflow-x-auto">
            <table class="min-w-full divide-y divide-gray-200">
                <thead class="bg-gray-50">
                    <tr>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Product Name
                        </th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Category
                        </th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Supplier
                        </th>
                        <th class="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Unit Price
                        </th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Stock
                        </th>
                        <th class="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
                            Status
                        </th>
                    </tr>
                </thead>
                <tbody class="bg-white divide-y divide-gray-200">
                    @foreach (var product in Model.Products)
                    {
                        <tr class="@(product.Discontinued ? "bg-gray-100" : "hover:bg-gray-50") transition-colors">
                            <td class="px-6 py-4 whitespace-nowrap">
                                <div class="text-sm font-medium text-gray-900">@product.ProductName</div>
                            </td>
                            <td class="px-6 py-4 whitespace-nowrap">
                                <div class="text-sm text-gray-500">@product.CategoryName</div>
                            </td>
                            <td class="px-6 py-4 whitespace-nowrap">
                                <div class="text-sm text-gray-500">@product.SupplierName</div>
                            </td>
                            <td class="px-6 py-4 whitespace-nowrap text-right">
                                <div class="text-sm font-medium text-gray-900">@product.UnitPrice?.ToString("C")</div>
                            </td>
                            <td class="px-6 py-4 whitespace-nowrap text-center">
                                <span class="badge @(product.IsLowStock ? "badge-warning" : "badge-success")">
                                    @product.UnitsInStock
                                </span>
                            </td>
                            <td class="px-6 py-4 whitespace-nowrap text-center">
                                @if (product.Discontinued)
                                {
                                    <span class="badge badge-danger">Discontinued</span>
                                }
                                else if (product.IsLowStock)
                                {
                                    <span class="badge badge-warning">Low Stock</span>
                                }
                                else
                                {
                                    <span class="badge badge-success">Available</span>
                                }
                            </td>
                        </tr>
                    }
                </tbody>
            </table>
            
            @if (!Model.Products.Any())
            {
                <div class="text-center py-12">
                    <svg class="mx-auto h-12 w-12 text-gray-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4" />
                    </svg>
                    <h3 class="mt-2 text-sm font-medium text-gray-900">No products found</h3>
                    <p class="mt-1 text-sm text-gray-500">Try adjusting your search or filter criteria.</p>
                </div>
            }
        </div>
    </div>
</div>
```

---

## Part 8: Layout and Navigation

### Step 8.1: Create Layout

**File**: `NorthwindWorkshop.Web/Pages/Shared/_Layout.cshtml`

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - Northwind Traders</title>
    <link rel="stylesheet" href="~/css/output.css" asp-append-version="true" />
</head>
<body class="bg-gray-50">
    <div class="flex min-h-screen">
        <!-- Sidebar -->
        <partial name="_Sidebar" />
        
        <!-- Main Content -->
        <main class="flex-1 overflow-x-hidden">
            <!-- Header -->
            <header class="bg-white border-b border-gray-200 px-6 py-4 mb-6">
                <div class="flex justify-between items-center">
                    <h2 class="text-2xl font-bold text-gray-800">@ViewData["Title"]</h2>
                    <div class="text-sm text-gray-500">
                        Northwind Traders Management System
                    </div>
                </div>
            </header>
            
            <!-- Page Content -->
            <div class="px-6 pb-6">
                @RenderBody()
            </div>
        </main>
    </div>
    
    <script src="~/js/site.js" asp-append-version="true"></script>
    
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```

### Step 8.2: Create Sidebar

**File**: `NorthwindWorkshop.Web/Pages/Shared/_Sidebar.cshtml`

```html
<nav class="w-64 bg-gray-900 text-white min-h-screen p-4">
    <div class="mb-8">
        <h4 class="text-center text-xl font-bold">Northwind</h4>
        <p class="text-center text-gray-400 text-sm">Traders</p>
    </div>
    
    <hr class="border-gray-700 mb-6" />
    
    <!-- Catalog Section -->
    <div class="mb-6">
        <h6 class="text-xs uppercase text-gray-400 font-semibold mb-3 px-2">Catalog</h6>
        <ul class="space-y-1">
            <li>
                <a class="flex items-center gap-3 px-3 py-2 rounded-lg transition-colors @(ViewContext.RouteData.Values["page"]?.ToString()?.Contains("Products") == true ? "bg-blue-600 text-white" : "text-gray-300 hover:bg-gray-800")" 
                   asp-page="/Products/Index">
                    <span>📦</span>
                    <span>Products</span>
                </a>
            </li>
            <li>
                <a class="flex items-center gap-3 px-3 py-2 rounded-lg transition-colors text-gray-300 hover:bg-gray-800" 
                   asp-page="/Categories/Index">
                    <span>🏷️</span>
                    <span>Categories</span>
                </a>
            </li>
            <li>
                <a class="flex items-center gap-3 px-3 py-2 rounded-lg transition-colors text-gray-300 hover:bg-gray-800" 
                   asp-page="/Suppliers/Index">
                    <span>🏭</span>
                    <span>Suppliers</span>
                </a>
            </li>
        </ul>
    </div>
    
    <!-- Management Section -->
    <div class="mb-6">
        <h6 class="text-xs uppercase text-gray-400 font-semibold mb-3 px-2">Management</h6>
        <ul class="space-y-1">
            <li>
                <a class="flex items-center gap-3 px-3 py-2 rounded-lg transition-colors @(ViewContext.RouteData.Values["page"]?.ToString()?.Contains("Customers") == true ? "bg-blue-600 text-white" : "text-gray-300 hover:bg-gray-800")" 
                   asp-page="/Customers/Index">
                    <span>👥</span>
                    <span>Customers</span>
                </a>
            </li>
            <li>
                <a class="flex items-center gap-3 px-3 py-2 rounded-lg transition-colors @(ViewContext.RouteData.Values["page"]?.ToString()?.Contains("Employees") == true ? "bg-blue-600 text-white" : "text-gray-300 hover:bg-gray-800")" 
                   asp-page="/Employees/Index">
                    <span>👔</span>
                    <span>Employees</span>
                </a>
            </li>
            <li>
                <a class="flex items-center gap-3 px-3 py-2 rounded-lg transition-colors text-gray-300 hover:bg-gray-800" 
                   asp-page="/Orders/Index">
                    <span>📋</span>
                    <span>Orders</span>
                </a>
            </li>
            <li>
                <a class="flex items-center gap-3 px-3 py-2 rounded-lg transition-colors text-gray-300 hover:bg-gray-800" 
                   asp-page="/Shippers/Index">
                    <span>🚚</span>
                    <span>Shippers</span>
                </a>
            </li>
        </ul>
    </div>
    
    <!-- Analytics Section -->
    <div class="mb-6">
        <h6 class="text-xs uppercase text-gray-400 font-semibold mb-3 px-2">Analytics</h6>
        <ul class="space-y-1">
            <li>
                <a class="flex items-center gap-3 px-3 py-2 rounded-lg transition-colors text-gray-300 hover:bg-gray-800" 
                   asp-page="/Reports/Sales">
                    <span>📊</span>
                    <span>Sales Report</span>
                </a>
            </li>
            <li>
                <a class="flex items-center gap-3 px-3 py-2 rounded-lg transition-colors text-gray-300 hover:bg-gray-800" 
                   asp-page="/Reports/Inventory">
                    <span>📈</span>
                    <span>Inventory Report</span>
                </a>
            </li>
        </ul>
    </div>
</nav>
```

---

## Part 9: Running the Application

### Step 9.1: Install EF Core Tools

First, install the Entity Framework Core tools globally:

```bash
dotnet tool install --global dotnet-ef
```

**Verify installation:**
```bash
dotnet ef --version
# Should output: 9.0.x or higher
```

### Step 9.2: Create and Apply Migrations

From your solution root directory:

```bash
# Navigate to the Web project (where your DbContext registration is)
cd NorthwindWorkshop.Web

# Create initial migration
dotnet ef migrations add InitialCreate --project ../NorthwindWorkshop.Infrastructure

# Apply migration to create database
dotnet ef database update --project ../NorthwindWorkshop.Infrastructure
```

**What just happened?**
1. EF Core analyzed your `DbContext` and entities
2. Generated migration code in `Infrastructure/Migrations/`
3. Created `northwind.db` SQLite database file
4. Applied the schema to create all tables

**View your database in VSCode:**
1. Install "SQLite Viewer" extension if you haven't
2. Right-click `northwind.db` in Explorer
3. Select "Open Database"

### Step 9.3: Run the Application

**Option 1: Using the terminal**
```bash
# Make sure you're in the Web project directory
cd NorthwindWorkshop.Web

# Run the application
dotnet run
```

**Option 2: Using VSCode debugger**
1. Press `F5` to start debugging
2. Or press `Ctrl+F5` to run without debugging

The application will start and display:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
```

Navigate to `https://localhost:5001` in your browser.

### Step 9.4: Verify Everything Works

You should see:
1. The Northwind Traders homepage with sidebar navigation
2. Click "Customers" - should show a list of customers
3. Click "Products" - should show a list of products
4. Click "Employees" - should show employee list

**If you see errors:**
- Check that all projects built successfully: `dotnet build`
- Verify database was created: `ls northwind.db`
- Check migrations were applied: `dotnet ef migrations list --project ../NorthwindWorkshop.Infrastructure`

### Step 9.5: Development Workflow

**Hot Reload is enabled by default!**

1. Run the app: `dotnet run` or `F5`
2. Make changes to `.cshtml` or `.cs` files
3. Save the file
4. Refresh your browser - changes appear automatically!

**Common Commands:**
```bash
# Build the solution
dotnet build

# Run tests (when you add them)
dotnet test

# Clean build artifacts
dotnet clean

# Restore NuGet packages
dotnet restore

# Watch mode (auto-restart on file changes)
dotnet watch run
```

---

## Part 10: Advanced Topics & Exercises

### Exercise 1: Add CRUD Operations

Implement Create, Update, and Delete operations for Customers:

1. ✅ Add `Create.cshtml` and `Create.cshtml.cs` for adding new customers (Implemented)
2. ✅ Add `Edit.cshtml` and `Edit.cshtml.cs` for updating customers (Implemented)
3. ✅ Add delete functionality with confirmation (Implemented)

**Exercise 1 Complete!** ✅ Full CRUD operations implemented for Customer management.

> **Pattern Extension Example**: The same CRUD patterns have been successfully extended to complete Shipper management (Create, Edit, Delete), demonstrating how Clean Architecture enables rapid feature development across different entities. The Shipper implementation showcases:
> - **Entity-Appropriate Design**: Form and confirmation complexity matched to entity simplicity (2 fields vs Customer's 10+ fields)
> - **Pattern Scalability**: Same safety and validation frameworks scaled appropriately
> - **Infrastructure Reuse**: 80%+ code reuse through existing repository and architecture patterns
> - **Consistent User Experience**: Professional interface standards maintained while optimizing for simplicity

### Exercise 2: Implement Order Management

Create a complete order management system:

1. List orders with customer and employee information
2. View order details with line items
3. Calculate order totals
4. Filter orders by date range and status

### Exercise 3: Add Search Functionality

Implement advanced search across multiple entities:

1. Global search across products, customers, and orders
2. Use LINQ for complex queries
3. Implement pagination for large result sets

### Exercise 4: Create a Dashboard

Build a dashboard showing:

1. Total sales by month
2. Top-selling products
3. Low stock alerts
4. Recent orders
5. Use Chart.js for visualizations

### Exercise 5: Implement Unit of Work Pattern

Refactor to use Unit of Work:

```csharp
public interface IUnitOfWork : IDisposable
{
    ICustomerRepository Customers { get; }
    IProductRepository Products { get; }
    IEmployeeRepository Employees { get; }
    Task<int> SaveChangesAsync();
}
```

---

## OOP Concepts Covered

### 1. **Encapsulation**
- Properties hide internal data
- Private fields with public accessors
- ViewModels separate presentation from domain

### 2. **Inheritance**
- BaseEntity class
- Repository<T> as base class
- Entities inherit common properties

### 3. **Polymorphism**
- Interface implementations
- Virtual methods for overriding
- Generic repository pattern

### 4. **Abstraction**
- IRepository interfaces
- Service layer abstractions
- DbContext abstraction over database

### 5. **SOLID Principles**
- **S**ingle Responsibility: Each class has one purpose
- **O**pen/Closed: Open for extension, closed for modification
- **L**iskov Substitution: Derived classes can replace base classes
- **I**nterface Segregation: Small, focused interfaces
- **D**ependency Inversion: Depend on abstractions, not concretions

---

## Best Practices Demonstrated

1. **Async/Await** - All database operations are asynchronous
2. **Dependency Injection** - Services registered in Program.cs
3. **Repository Pattern** - Data access abstraction
4. **DTO/ViewModel Pattern** - Separate concerns between layers
5. **LINQ** - Type-safe queries
6. **Navigation Properties** - EF Core relationships
7. **Eager Loading** - Include related entities
8. **Code Organization** - Clean architecture layers

---

## Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [C# Programming Guide](https://docs.microsoft.com/dotnet/csharp)
- [SOLID Principles](https://www.digitalocean.com/community/conceptual_articles/s-o-l-i-d-the-first-five-principles-of-object-oriented-design)

---

## Next Steps

1. Complete all exercises
2. Add authentication and authorization
3. Implement API endpoints (Web API)
4. Add client-side interactivity with Blazor
5. Deploy to Azure or AWS

Happy coding! 🚀
