# Project Structure Diagram

## Solution Structure

```
NorthwindWorkshop.sln
│
├── NorthwindWorkshop.Core/              (Domain Layer - Class Library)
│   ├── Entities/
│   ├── Interfaces/
│   ├── Services/
│   └── NorthwindWorkshop.Core.csproj
│
├── NorthwindWorkshop.Infrastructure/    (Data Access Layer - Class Library)
│   ├── Data/
│   ├── Repositories/
│   ├── Migrations/
│   └── NorthwindWorkshop.Infrastructure.csproj
│
└── NorthwindWorkshop.Web/               (Presentation Layer - Web App)
    ├── Pages/
    ├── ViewModels/
    ├── wwwroot/
    ├── Program.cs
    └── NorthwindWorkshop.Web.csproj
```

## Detailed Project Breakdown

### 1. NorthwindWorkshop.Core (Domain Layer)

```
NorthwindWorkshop.Core/
│
├── Entities/                           # Domain Models
│   ├── BaseEntity.cs                   # Abstract base class
│   │   └── Properties: Id, CreatedDate, ModifiedDate
│   │
│   ├── Customer.cs
│   │   ├── CustomerId (PK)
│   │   ├── CompanyName, ContactName, ContactTitle
│   │   ├── Address, City, Region, PostalCode, Country
│   │   ├── Phone, Fax
│   │   ├── DisplayName (computed property)
│   │   └── Navigation: Orders (ICollection<Order>)
│   │
│   ├── Product.cs
│   │   ├── ProductId (PK)
│   │   ├── ProductName
│   │   ├── UnitPrice, UnitsInStock, UnitsOnOrder
│   │   ├── ReorderLevel, Discontinued
│   │   ├── IsLowStock() method
│   │   └── Navigation: Category, Supplier, OrderDetails
│   │
│   ├── Order.cs
│   │   ├── OrderId (PK)
│   │   ├── OrderDate, RequiredDate, ShippedDate
│   │   ├── Freight, ShipName, ShipAddress
│   │   ├── TotalAmount (computed)
│   │   └── Navigation: Customer, Employee, OrderDetails, Shipper
│   │
│   ├── OrderDetail.cs
│   │   ├── OrderDetailId (PK)
│   │   ├── UnitPrice, Quantity, Discount
│   │   ├── LineTotal (computed)
│   │   └── Navigation: Order, Product
│   │
│   ├── Category.cs
│   │   ├── CategoryId (PK)
│   │   ├── CategoryName, Description
│   │   └── Navigation: Products
│   │
│   ├── Supplier.cs
│   │   ├── SupplierId (PK)
│   │   ├── CompanyName, ContactName
│   │   ├── Address, City, Country
│   │   └── Navigation: Products
│   │
│   ├── Employee.cs
│   │   ├── EmployeeId (PK)
│   │   ├── FirstName, LastName, Title
│   │   ├── BirthDate, HireDate
│   │   ├── ReportsTo (FK, self-referencing)
│   │   └── Navigation: Orders, Manager, Subordinates
│   │
│   └── Shipper.cs
│       ├── ShipperId (PK)
│       ├── CompanyName, Phone
│       └── Navigation: Orders
│
├── Interfaces/                         # Contracts/Abstractions
│   ├── IRepository.cs                  # Generic repository interface
│   │   ├── GetByIdAsync(int id)
│   │   ├── GetAllAsync()
│   │   ├── AddAsync(T entity)
│   │   ├── UpdateAsync(T entity)
│   │   ├── DeleteAsync(int id)
│   │   └── ExistsAsync(int id)
│   │
│   ├── ICustomerRepository.cs          # Customer-specific methods
│   │   ├── Inherits: IRepository<Customer>
│   │   ├── GetByIdWithOrdersAsync()
│   │   ├── GetByCompanyNameAsync()
│   │   ├── GetByCityAsync()
│   │   └── SearchAsync()
│   │
│   ├── IProductRepository.cs           # Product-specific methods
│   │   ├── Inherits: IRepository<Product>
│   │   ├── GetLowStockProductsAsync()
│   │   ├── GetByCategoryAsync()
│   │   ├── GetBySupplierAsync()
│   │   └── SearchAsync()
│   │
│   ├── IOrderRepository.cs             # Order-specific methods
│   │   ├── Inherits: IRepository<Order>
│   │   ├── GetByCustomerAsync()
│   │   ├── GetRecentOrdersAsync()
│   │   └── GetPendingOrdersAsync()
│   │
│   └── IEmployeeRepository.cs          # Employee-specific methods
│       ├── Inherits: IRepository<Employee>
│       └── GetByManagerAsync()
│
├── Services/                           # Business Logic (Optional)
│   ├── ICustomerService.cs
│   └── CustomerService.cs
│       ├── ValidateCustomer()
│       ├── CalculateCustomerLifetimeValue()
│       └── Business rules and workflows
│
└── NorthwindWorkshop.Core.csproj
    └── Target Framework: net9.0
```

### 2. NorthwindWorkshop.Infrastructure (Data Access Layer)

```
NorthwindWorkshop.Infrastructure/
│
├── Data/                               # EF Core Configuration
│   ├── NorthwindDbContext.cs           # Main DbContext
│   │   ├── DbSet<Customer> Customers
│   │   ├── DbSet<Product> Products
│   │   ├── DbSet<Order> Orders
│   │   ├── DbSet<OrderDetail> OrderDetails
│   │   ├── DbSet<Category> Categories
│   │   ├── DbSet<Supplier> Suppliers
│   │   ├── DbSet<Employee> Employees
│   │   ├── DbSet<Shipper> Shippers
│   │   ├── OnModelCreating() - Fluent API configuration
│   │   │   ├── Entity configurations
│   │   │   ├── Relationships (FK constraints)
│   │   │   ├── Indexes
│   │   │   └── Default values
│   │   └── SaveChangesAsync() override
│   │
│   └── DbInitializer.cs                # Database seeding
│       ├── SeedCategories()
│       ├── SeedSuppliers()
│       ├── SeedProducts()
│       ├── SeedCustomers()
│       ├── SeedEmployees()
│       └── SeedOrders()
│
├── Repositories/                       # Repository Implementations
│   ├── Repository.cs                   # Generic base repository
│   │   ├── NorthwindDbContext _context
│   │   ├── DbSet<T> _dbSet
│   │   ├── GetByIdAsync()
│   │   ├── GetAllAsync()
│   │   ├── AddAsync()
│   │   ├── UpdateAsync()
│   │   └── DeleteAsync()
│   │
│   ├── CustomerRepository.cs
│   │   ├── Inherits: Repository<Customer>
│   │   ├── Implements: ICustomerRepository
│   │   └── Customer-specific LINQ queries
│   │
│   ├── ProductRepository.cs
│   │   ├── Inherits: Repository<Product>
│   │   ├── Implements: IProductRepository
│   │   └── Product-specific LINQ queries
│   │
│   ├── OrderRepository.cs
│   │   ├── Inherits: Repository<Order>
│   │   ├── Implements: IOrderRepository
│   │   └── Order-specific LINQ queries
│   │
│   └── EmployeeRepository.cs
│       ├── Inherits: Repository<Employee>
│       ├── Implements: IEmployeeRepository
│       └── Employee-specific LINQ queries
│
├── Migrations/                         # EF Core Migrations
│   ├── 20250101000000_InitialCreate.cs
│   ├── 20250102000000_AddIndexes.cs
│   └── NorthwindDbContextModelSnapshot.cs
│
└── NorthwindWorkshop.Infrastructure.csproj
    ├── Target Framework: net9.0
    ├── Project Reference: NorthwindWorkshop.Core
    └── NuGet Packages:
        ├── Microsoft.EntityFrameworkCore (9.0)
        ├── Microsoft.EntityFrameworkCore.Sqlite (9.0)
        └── Microsoft.EntityFrameworkCore.Design (9.0)
```

### 3. NorthwindWorkshop.Web (Presentation Layer)

```
NorthwindWorkshop.Web/
│
├── Pages/                              # Razor Pages
│   ├── Customers/
│   │   ├── Index.cshtml                # Customer list view
│   │   ├── Index.cshtml.cs             # PageModel - GetAllAsync()
│   │   ├── Details.cshtml              # Customer details view
│   │   ├── Details.cshtml.cs           # PageModel - GetByIdWithOrdersAsync()
│   │   ├── Create.cshtml
│   │   ├── Create.cshtml.cs
│   │   ├── Edit.cshtml
│   │   ├── Edit.cshtml.cs
│   │   ├── Delete.cshtml
│   │   └── Delete.cshtml.cs
│   │
│   ├── Products/
│   │   ├── Index.cshtml
│   │   ├── Index.cshtml.cs
│   │   ├── Details.cshtml
│   │   ├── Details.cshtml.cs
│   │   └── [CRUD pages...]
│   │
│   ├── Orders/
│   │   ├── Index.cshtml
│   │   ├── Index.cshtml.cs
│   │   ├── Details.cshtml
│   │   ├── Details.cshtml.cs
│   │   └── [CRUD pages...]
│   │
│   ├── Employees/
│   │   └── [Employee management pages...]
│   │
│   ├── Categories/
│   │   └── [Category management pages...]
│   │
│   ├── Suppliers/
│   │   └── [Supplier management pages...]
│   │
│   ├── Shared/
│   │   ├── _Layout.cshtml              # Master layout
│   │   │   ├── <head> section
│   │   │   ├── Navigation bar
│   │   │   ├── @RenderBody()
│   │   │   └── Scripts
│   │   │
│   │   ├── _Sidebar.cshtml             # Navigation sidebar
│   │   │   ├── Dashboard link
│   │   │   ├── Customers link
│   │   │   ├── Products link
│   │   │   ├── Orders link
│   │   │   └── Other entity links
│   │   │
│   │   ├── _ValidationScriptsPartial.cshtml
│   │   └── Error.cshtml
│   │
│   ├── Index.cshtml                    # Home/Dashboard
│   ├── Index.cshtml.cs
│   ├── Privacy.cshtml
│   ├── _ViewImports.cshtml             # Global using statements
│   └── _ViewStart.cshtml               # Sets default layout
│
├── ViewModels/                         # DTOs for views
│   ├── CustomerListViewModel.cs
│   │   ├── CustomerId
│   │   ├── DisplayName
│   │   ├── City
│   │   ├── Country
│   │   └── OrderCount
│   │
│   ├── CustomerDetailViewModel.cs
│   │   ├── Customer info
│   │   └── List<OrderSummary> RecentOrders
│   │
│   ├── ProductListViewModel.cs
│   ├── ProductDetailViewModel.cs
│   ├── OrderListViewModel.cs
│   └── OrderDetailViewModel.cs
│
├── wwwroot/                            # Static files
│   ├── css/
│   │   ├── app.css                     # Tailwind CSS source
│   │   ├── output.css                  # Generated CSS (git ignored)
│   │   └── site.css                    # Custom styles
│   │
│   ├── js/
│   │   └── site.js                     # Client-side JavaScript
│   │
│   ├── lib/                            # Client libraries
│   │   ├── jquery/
│   │   └── bootstrap/ (optional)
│   │
│   ├── images/
│   └── favicon.ico
│
├── Program.cs                          # Application entry point
│   ├── WebApplicationBuilder creation
│   ├── Service registration
│   │   ├── AddDbContext<NorthwindDbContext>()
│   │   ├── AddScoped<ICustomerRepository, CustomerRepository>()
│   │   ├── AddScoped<IProductRepository, ProductRepository>()
│   │   ├── AddScoped<IOrderRepository, OrderRepository>()
│   │   └── AddRazorPages()
│   │
│   ├── Middleware pipeline configuration
│   │   ├── UseHttpsRedirection()
│   │   ├── UseStaticFiles()
│   │   ├── UseRouting()
│   │   ├── UseAuthorization()
│   │   └── MapRazorPages()
│   │
│   └── Database initialization
│       └── DbInitializer.Initialize()
│
├── appsettings.json                    # Configuration
│   ├── ConnectionStrings
│   │   └── Northwind: "Data Source=northwind.db"
│   └── Logging configuration
│
├── appsettings.Development.json
│
├── package.json                        # Node.js dependencies
│   └── Tailwind CSS packages
│
├── tailwind.config.js                  # Tailwind configuration
│
└── NorthwindWorkshop.Web.csproj
    ├── Target Framework: net9.0
    ├── Project References:
    │   ├── NorthwindWorkshop.Core
    │   └── NorthwindWorkshop.Infrastructure
    └── NuGet Packages:
        ├── Microsoft.AspNetCore.App (9.0)
        └── Microsoft.EntityFrameworkCore.Design (9.0)
```

## Project Dependencies

```
┌─────────────────────────────────────────┐
│      NorthwindWorkshop.Web              │
│      (ASP.NET Core Web App)             │
└─────────────────────────────────────────┘
              │           │
              │           │ References
              ↓           ↓
    ┌──────────────┐  ┌──────────────────────┐
    │     Core     │  │   Infrastructure     │
    │   (Domain)   │◄─┤   (Data Access)      │
    └──────────────┘  └──────────────────────┘
                              │
                              │ Uses
                              ↓
                    ┌──────────────────┐
                    │  Entity Framework│
                    │      Core        │
                    └──────────────────┘
                              │
                              ↓
                    ┌──────────────────┐
                    │     SQLite       │
                    │   (northwind.db) │
                    └──────────────────┘
```

## Key Files and Their Purpose

| File | Purpose |
|------|---------|
| **BaseEntity.cs** | Abstract base class providing common properties (Id, timestamps) |
| **Customer.cs** | Domain entity representing customers |
| **ICustomerRepository.cs** | Contract defining customer data operations |
| **CustomerRepository.cs** | Implementation of customer data access using EF Core |
| **NorthwindDbContext.cs** | EF Core context managing database connection and entities |
| **DbInitializer.cs** | Seeds database with sample data |
| **Index.cshtml** | Razor view template for displaying customer list |
| **Index.cshtml.cs** | PageModel (code-behind) handling requests for customer list |
| **CustomerListViewModel.cs** | DTO optimized for displaying customer list |
| **Program.cs** | Application startup and dependency injection configuration |
| **_Layout.cshtml** | Master page layout shared across all pages |

## Build and Run Process

```
1. Developer runs: dotnet build
   ↓
2. MSBuild compiles projects in order:
   • Core (no dependencies)
   • Infrastructure (depends on Core)
   • Web (depends on Core and Infrastructure)
   ↓
3. Developer runs: dotnet ef database update
   ↓
4. EF Core applies migrations to create/update database
   ↓
5. Developer runs: dotnet run --project NorthwindWorkshop.Web
   ↓
6. Program.cs executes:
   • Builds WebApplication
   • Configures services (DI)
   • Configures middleware pipeline
   • Initializes database (if needed)
   • Starts Kestrel web server
   ↓
7. Application running at https://localhost:5001
```

## Deployment Structure

```
Published Output/
├── NorthwindWorkshop.Web.dll
├── NorthwindWorkshop.Core.dll
├── NorthwindWorkshop.Infrastructure.dll
├── EntityFrameworkCore.*.dll
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
├── appsettings.json
├── northwind.db
└── web.config (for IIS)
```

This structure follows Clean Architecture principles with clear separation of concerns, making the application maintainable, testable, and scalable.
