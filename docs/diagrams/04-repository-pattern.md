# Repository Pattern Diagram

## Repository Pattern Overview

The Repository Pattern abstracts data access logic and provides a collection-like interface for accessing domain objects.

```
┌─────────────────────────────────────────────────────────────────┐
│                    PRESENTATION LAYER                           │
│                  (Controllers/PageModels)                       │
└─────────────────────────────────────────────────────────────────┘
                            │
                            │ Depends on
                            │ (Interface only)
                            ↓
┌─────────────────────────────────────────────────────────────────┐
│                      DOMAIN LAYER                               │
│                                                                 │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │            IRepository<T> (Generic Interface)             │  │
│  ├───────────────────────────────────────────────────────────┤  │
│  │ + Task<T> GetByIdAsync(int id)                            │  │
│  │ + Task<IEnumerable<T>> GetAllAsync()                      │  │
│  │ + Task<T> AddAsync(T entity)                              │  │
│  │ + Task UpdateAsync(T entity)                              │  │
│  │ + Task DeleteAsync(int id)                                │  │
│  │ + Task<bool> ExistsAsync(int id)                          │  │
│  └───────────────────────────────────────────────────────────┘  │
│                            ▲                                    │
│                            │ Extends                            │
│         ┌──────────────────┼──────────────────┐                 │
│         │                  │                  │                 │
│  ┌──────────────┐   ┌─────────────┐   ┌───────────────┐         │
│  │ICustomer     │   │ IProduct    │   │ IOrder        │         │
│  │Repository    │   │ Repository  │   │ Repository    │         │
│  ├──────────────┤   ├─────────────┤   ├───────────────┤         │
│  │+ GetByName   │   │+ GetLowStock│   │+ GetByCustomer│         │
│  │+ GetByCity   │   │+ GetByCategory│ │+ GetRecent    │         │
│  │+ SearchAsync │   │+ SearchAsync│   │+ GetPending   │         │
│  └──────────────┘   └─────────────┘   └───────────────┘         │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
                            ▲
                            │ Implements
                            │
┌─────────────────────────────────────────────────────────────────┐
│                 DATA ACCESS LAYER                               │
│                                                                 │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │         Repository<T> (Generic Implementation)            │  │
│  ├───────────────────────────────────────────────────────────┤  │
│  │ - NorthwindDbContext _context                             │  │
│  │ - DbSet<T> _dbSet                                         │  │
│  ├───────────────────────────────────────────────────────────┤  │
│  │ + async Task<T> GetByIdAsync(int id)                      │  │
│  │   {                                                       │  │
│  │     return await _dbSet.FindAsync(id);                    │  │
│  │   }                                                       │  │
│  │                                                           │  │
│  │ + async Task<IEnumerable<T>> GetAllAsync()                │  │
│  │   {                                                       │  │
│  │     return await _dbSet.ToListAsync();                    │  │
│  │   }                                                       │  │
│  │                                                           │  │
│  │ + async Task<T> AddAsync(T entity)                        │  │
│  │   {                                                       │  │
│  │     await _dbSet.AddAsync(entity);                        │  │
│  │     await _context.SaveChangesAsync();                    │  │
│  │     return entity;                                        │  │
│  │   }                                                       │  │
│  └───────────────────────────────────────────────────────────┘  │
│                            ▲                                    │
│                            │ Inherits                           │
│         ┌──────────────────┼──────────────────┐                 │
│         │                  │                  │                 │
│  ┌───────────────┐   ┌───────────────┐   ┌───────────────┐      │
│  │Customer       │   │ Product       │   │ Order         │      │
│  │Repository     │   │ Repository    │   │ Repository    │      │
│  ├───────────────┤   ├───────────────┤   ├───────────────┤      │
│  │+ GetByName    │   │+ GetLowStock  │   │+ GetByCustomer│      │
│  │  {            │   │  {            │   │  {            │      │
│  │   return await│   │   return await│   │   return await│      │
│  │   _context    │   │   _context    │   │   _context    │      │
│  │   .Customers  │   │   .Products   │   │   .Orders     │      │
│  │   .Where(...) │   │   .Where(...) │   │   .Where(...) │      │
│  │  }            │   │  }            │   │  }            │      │
│  └───────────────┘   └───────────────┘   └───────────────┘      │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
                            │
                            │ Uses
                            ↓
┌─────────────────────────────────────────────────────────────────┐
│                    DATABASE (SQLite)                            │
└─────────────────────────────────────────────────────────────────┘
```

## Concrete Example: CustomerRepository

### Interface Definition (Core)

```csharp
// NorthwindWorkshop.Core/Interfaces/ICustomerRepository.cs

public interface ICustomerRepository : IRepository<Customer>
{
    // Inherits base CRUD operations from IRepository<Customer>:
    // - GetByIdAsync(int id)
    // - GetAllAsync()
    // - AddAsync(Customer entity)
    // - UpdateAsync(Customer entity)
    // - DeleteAsync(int id)

    // Additional customer-specific methods:
    Task<Customer> GetByIdWithOrdersAsync(int customerId);
    Task<IEnumerable<Customer>> GetByCompanyNameAsync(string companyName);
    Task<IEnumerable<Customer>> GetByCityAsync(string city);
    Task<IEnumerable<Customer>> GetByCountryAsync(string country);
    Task<IEnumerable<Customer>> SearchAsync(string searchTerm);
}
```

### Implementation (Infrastructure)

```csharp
// NorthwindWorkshop.Infrastructure/Repositories/CustomerRepository.cs

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    // Inherits base implementation from Repository<Customer>
    // Gets access to _context and _dbSet

    public CustomerRepository(NorthwindDbContext context) : base(context)
    {
    }

    // Implement customer-specific methods
    public async Task<Customer> GetByIdWithOrdersAsync(int customerId)
    {
        return await _context.Customers
            .Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }

    public async Task<IEnumerable<Customer>> GetByCityAsync(string city)
    {
        return await _context.Customers
            .Where(c => c.City == city)
            .OrderBy(c => c.CompanyName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Customer>> SearchAsync(string searchTerm)
    {
        return await _context.Customers
            .Where(c =>
                c.CompanyName.Contains(searchTerm) ||
                c.ContactName.Contains(searchTerm) ||
                c.City.Contains(searchTerm))
            .ToListAsync();
    }
}
```

### Usage in PageModel (Web)

```csharp
// NorthwindWorkshop.Web/Pages/Customers/Index.cshtml.cs

public class IndexModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    // Dependency injection - depends on interface only!
    public IndexModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IEnumerable<Customer> Customers { get; set; }

    public async Task OnGetAsync()
    {
        // Use repository to get data
        Customers = await _customerRepository.GetAllAsync();
    }

    public async Task<IActionResult> OnGetSearchAsync(string searchTerm)
    {
        // Use customer-specific search method
        Customers = await _customerRepository.SearchAsync(searchTerm);
        return Page();
    }
}
```

## Benefits of Repository Pattern

```
┌─────────────────────────────────────────────────────────────────┐
│                      WITHOUT REPOSITORY                         │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  PageModel directly uses DbContext:                             │
│                                                                 │
│  public class IndexModel : PageModel                            │
│  {                                                              │
│      private readonly NorthwindDbContext _context;              │
│                                                                 │
│      public async Task OnGetAsync()                             │
│      {                                                          │
│          // Direct database access - tightly coupled!           │
│          Customers = await _context.Customers                   │
│              .Include(c => c.Orders)                            │
│              .ToListAsync();                                    │
│      }                                                          │
│  }                                                              │
│                                                                 │
│  ❌ Problems:                                                    │
│  • PageModel knows about EF Core                                │
│  • Hard to test (requires real database)                        │
│  • Difficult to change data source                              │
│  • Duplicated queries across multiple pages                     │
│  • Business logic mixed with data access                        │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                       WITH REPOSITORY                           │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  PageModel uses Repository interface:                           │
│                                                                 │
│  public class IndexModel : PageModel                            │
│  {                                                              │
│      private readonly ICustomerRepository _repository;          │
│                                                                 │
│      public async Task OnGetAsync()                             │
│      {                                                          │
│          // Clean abstraction - loosely coupled!                │
│          Customers = await _repository.GetAllAsync();           │
│      }                                                          │
│  }                                                              │
│                                                                 │
│  ✅ Benefits:                                                   │
│  • PageModel only knows about domain interface                  │
│  • Easy to test (can mock ICustomerRepository)                  │
│  • Can swap implementations (SQL → NoSQL)                       │
│  • Centralized queries in one place                             │
│  • Separation of concerns                                       │
│  • Follows SOLID principles                                     │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Testing with Repository Pattern

```
┌─────────────────────────────────────────────────────────────────┐
│                      UNIT TEST EXAMPLE                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  [TestClass]                                                    │
│  public class IndexModelTests                                   │
│  {                                                              │
│      [TestMethod]                                               │
│      public async Task OnGetAsync_ReturnsCustomers()            │
│      {                                                          │
│          // Arrange - Create mock repository                    │
│          var mockRepo = new Mock<ICustomerRepository>();        │
│          mockRepo                                               │
│              .Setup(r => r.GetAllAsync())                       │
│              .ReturnsAsync(new List<Customer>                   │
│              {                                                  │
│                  new Customer { CustomerId = 1,                 │
│                                CompanyName = "Test Co" }        │
│              });                                                │
│                                                                 │
│          var pageModel = new IndexModel(mockRepo.Object);       │
│                                                                 │
│          // Act                                                 │
│          await pageModel.OnGetAsync();                          │
│                                                                 │
│          // Assert                                              │
│          Assert.IsNotNull(pageModel.Customers);                 │
│          Assert.AreEqual(1, pageModel.Customers.Count());       │
│      }                                                          │
│  }                                                              │
│                                                                 │
│  ✅ No database required for testing!                           │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Dependency Injection Registration

```
┌─────────────────────────────────────────────────────────────────┐
│                        Program.cs                               │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  var builder = WebApplication.CreateBuilder(args);              │
│                                                                 │
│  // Register DbContext                                          │
│  builder.Services.AddDbContext<NorthwindDbContext>(options =>   │
│      options.UseSqlite(                                         │
│          builder.Configuration.GetConnectionString("Northwind"))│
│  );                                                             │
│                                                                 │
│  // Register repositories                                       │
│  builder.Services.AddScoped<ICustomerRepository,                │
│                              CustomerRepository>();             │
│  builder.Services.AddScoped<IProductRepository,                 │
│                              ProductRepository>();              │
│  builder.Services.AddScoped<IOrderRepository,                   │
│                              OrderRepository>();                │
│                                                                 │
│  // OR use generic registration:                                │
│  builder.Services.AddScoped(typeof(IRepository<>),              │
│                              typeof(Repository<>));             │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
                            │
                            │ When request arrives
                            ↓
┌─────────────────────────────────────────────────────────────────┐
│                      DI Container                               │
│                                                                 │
│  Request: ICustomerRepository                                   │
│     ↓                                                           │
│  Returns: CustomerRepository instance                           │
│     ↓                                                           │
│  Injects: NorthwindDbContext into constructor                   │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Repository Pattern + Unit of Work

For complex scenarios with multiple repositories:

```
┌─────────────────────────────────────────────────────────────────┐
│                      IUnitOfWork                                │
├─────────────────────────────────────────────────────────────────┤
│ + ICustomerRepository Customers { get; }                        │
│ + IProductRepository Products { get; }                          │
│ + IOrderRepository Orders { get; }                              │
│ + Task<int> SaveChangesAsync();                                 │
│ + Task BeginTransactionAsync();                                 │
│ + Task CommitAsync();                                           │
│ + Task RollbackAsync();                                         │
└─────────────────────────────────────────────────────────────────┘
                            ▲
                            │ Implements
                            │
┌─────────────────────────────────────────────────────────────────┐
│                        UnitOfWork                               │
├─────────────────────────────────────────────────────────────────┤
│ - NorthwindDbContext _context                                   │
│ - ICustomerRepository _customers                                │
│ - IProductRepository _products                                  │
│ - IOrderRepository _orders                                      │
│                                                                 │
│ + async Task<int> SaveChangesAsync()                            │
│   {                                                             │
│     return await _context.SaveChangesAsync();                   │
│   }                                                             │
└─────────────────────────────────────────────────────────────────┘

Usage:
  await _unitOfWork.Customers.AddAsync(customer);
  await _unitOfWork.Orders.AddAsync(order);
  await _unitOfWork.SaveChangesAsync(); // Single transaction!
```

## Key Takeaways

1. **Abstraction**: PageModels depend on interfaces, not implementations
2. **Testability**: Easy to mock repositories for unit testing
3. **Maintainability**: Centralized data access logic
4. **Flexibility**: Can swap data sources without changing business logic
5. **SOLID**: Follows Interface Segregation and Dependency Inversion principles
