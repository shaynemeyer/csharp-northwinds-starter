# Data Flow Diagram

## Request/Response Flow: Customer List Page

```
┌──────────────────────────────────────────────────────────────────┐
│                           USER BROWSER                           │
└──────────────────────────────────────────────────────────────────┘
                              │
                              │ 1. HTTP GET /Customers
                              ↓
┌──────────────────────────────────────────────────────────────────┐
│                    ASP.NET CORE MIDDLEWARE                       │
├──────────────────────────────────────────────────────────────────┤
│  • Routing Middleware                                            │
│  • Static Files Middleware                                       │
│  • Authentication/Authorization                                  │
└──────────────────────────────────────────────────────────────────┘
                              │
                              │ 2. Route to Razor Page
                              ↓
┌──────────────────────────────────────────────────────────────────┐
│              PRESENTATION LAYER (Web Project)                    │
├──────────────────────────────────────────────────────────────────┤
│                                                                  │
│   ┌──────────────────────────────────────────┐                   │
│   │   Customers/Index.cshtml.cs              │                   │
│   │   (PageModel)                            │                   │
│   ├──────────────────────────────────────────┤                   │
│   │ 3. OnGetAsync()                          │                   │
│   │    {                                     │                   │
│   │      var customers = await               │                   │
│   │        _customerRepository               │                   │
│   │          .GetAllAsync();                 │ ───────┐          │
│   │    }                                     │        │          │
│   └──────────────────────────────────────────┘        │          │
│                                                       │          │
└───────────────────────────────────────────────────────┼──────────┘
                                                        │
                                        4. Repository call
                                                        │
                                                        ↓
┌──────────────────────────────────────────────────────────────────┐
│                   DOMAIN LAYER (Core Project)                    │
├──────────────────────────────────────────────────────────────────┤
│                                                                  │
│   ┌──────────────────────────────────────────┐                   │
│   │   ICustomerRepository.cs                 │                   │
│   │   (Interface - Contract)                 │                   │
│   ├──────────────────────────────────────────┤                   │
│   │ Task<IEnumerable<Customer>>              │                   │
│   │   GetAllAsync();                         │                   │
│   │                                          │                   │
│   │ Task<Customer>                           │                   │
│   │   GetByIdAsync(int id);                  │                   │
│   └──────────────────────────────────────────┘                   │
│                                                                  │
└──────────────────────────────────────────────────────────────────┘
                              ↓
                              │ 5. Interface implemented by
                              │
┌──────────────────────────────────────────────────────────────────┐
│            DATA ACCESS LAYER (Infrastructure Project)            │
├──────────────────────────────────────────────────────────────────┤
│                                                                  │
│   ┌──────────────────────────────────────────┐                   │
│   │   CustomerRepository.cs                  │                   │
│   │   (Implementation)                       │                   │
│   ├──────────────────────────────────────────┤                   │
│   │ 6. GetAllAsync()                         │                   │
│   │    {                                     │                   │
│   │      return await _context               │                   │
│   │        .Customers                        │                   │
│   │        .Include(c => c.Orders)           │ ─────┐            │
│   │        .ToListAsync();                   │      │            │
│   │    }                                     │      │            │
│   └──────────────────────────────────────────┘      │            │
│                                                     │            │
│   ┌──────────────────────────────────────────┐      │            │
│   │   NorthwindDbContext.cs                  │      │            │
│   │   (EF Core DbContext)                    │ ◄────┘            │
│   ├──────────────────────────────────────────┤  7. LINQ Query    │
│   │ DbSet<Customer> Customers                │                   │
│   │ DbSet<Product> Products                  │                   │
│   │ DbSet<Order> Orders                      │                   │
│   └──────────────────────────────────────────┘                   │
│                                                                  │
└──────────────────────────────────────────────────────────────────┘
                              │
                              │ 8. SQL Query via EF Core
                              ↓
┌──────────────────────────────────────────────────────────────────┐
│                        DATABASE (SQLite)                         │
├──────────────────────────────────────────────────────────────────┤
│  SELECT * FROM Customers                                         │
│  LEFT JOIN Orders ON Customers.CustomerId = Orders.CustomerId    │
└──────────────────────────────────────────────────────────────────┘
                              │
                              │ 9. Return data rows
                              ↓
┌──────────────────────────────────────────────────────────────────┐
│            DATA ACCESS LAYER (Infrastructure Project)            │
├──────────────────────────────────────────────────────────────────┤
│  10. EF Core materializes data into Customer entities            │
│      List<Customer> with navigation properties populated         │
└──────────────────────────────────────────────────────────────────┘
                              │
                              │ 11. Return Customer objects
                              ↓
┌──────────────────────────────────────────────────────────────────┐
│              PRESENTATION LAYER (Web Project)                    │
├──────────────────────────────────────────────────────────────────┤
│                                                                  │
│   ┌──────────────────────────────────────────┐                   │
│   │   Customers/Index.cshtml.cs              │                   │
│   ├──────────────────────────────────────────┤                   │
│   │ 12. Map to ViewModel                     │                   │
│   │     var viewModel = customers            │                   │
│   │       .Select(c => new                   │                   │
│   │         CustomerListViewModel {          │                   │
│   │           Id = c.CustomerId,             │                   │
│   │           Name = c.DisplayName,          │                   │
│   │           City = c.City                  │                   │
│   │         });                              │                   │
│   └──────────────────────────────────────────┘                   │
│                       │                                          │
│                       │ 13. Pass to View                         │
│                       ↓                                          │
│   ┌──────────────────────────────────────────┐                   │
│   │   Customers/Index.cshtml                 │                   │
│   │   (Razor View)                           │                   │
│   ├──────────────────────────────────────────┤                   │
│   │ @model CustomerListViewModel             │                   │
│   │                                          │                   │
│   │ <table>                                  │                   │
│   │   @foreach(var customer in Model)        │                   │
│   │   {                                      │                   │
│   │     <tr>                                 │                   │
│   │       <td>@customer.Name</td>            │                   │
│   │       <td>@customer.City</td>            │                   │
│   │     </tr>                                │                   │
│   │   }                                      │                   │
│   │ </table>                                 │                   │
│   └──────────────────────────────────────────┘                   │
│                                                                  │
└──────────────────────────────────────────────────────────────────┘
                              │
                              │ 14. Render HTML
                              ↓
┌──────────────────────────────────────────────────────────────────┐
│                    ASP.NET CORE MIDDLEWARE                       │
├──────────────────────────────────────────────────────────────────┤
│  • Apply layout (_Layout.cshtml)                                 │
│  • Include CSS and JavaScript                                    │
│  • Generate final HTML response                                  │
└──────────────────────────────────────────────────────────────────┘
                              │
                              │ 15. HTTP 200 OK + HTML
                              ↓
┌──────────────────────────────────────────────────────────────────┐
│                           USER BROWSER                           │
│                    Displays Customer List                        │
└──────────────────────────────────────────────────────────────────┘
```

## Dependency Injection Flow

```
┌──────────────────────────────────────────────────────────────────┐
│                        Program.cs                                │
│                     (Application Startup)                        │
├──────────────────────────────────────────────────────────────────┤
│                                                                  │
│  1. Register Services in DI Container                            │
│                                                                  │
│     builder.Services.AddDbContext<NorthwindDbContext>();         │
│                                                                  │
│     builder.Services.AddScoped<                                  │
│       ICustomerRepository,                                       │
│       CustomerRepository>();                                     │
│                                                                  │
│     builder.Services.AddRazorPages();                            │
│                                                                  │
└──────────────────────────────────────────────────────────────────┘
                              │
                              │ 2. Request arrives
                              ↓
┌──────────────────────────────────────────────────────────────────┐
│                    DI Container (Service Provider)               │
├──────────────────────────────────────────────────────────────────┤
│                                                                  │
│  3. Resolve dependencies                                         │
│                                                                  │
│     Request for: IndexModel                                      │
│       ↓                                                          │
│     Requires: ICustomerRepository                                │
│       ↓                                                          │
│     Create: CustomerRepository                                   │
│       ↓                                                          │
│     Requires: NorthwindDbContext                                 │
│       ↓                                                          │
│     Create: NorthwindDbContext                                   │
│                                                                  │
└──────────────────────────────────────────────────────────────────┘
                              │
                              │ 4. Inject into constructor
                              ↓
┌──────────────────────────────────────────────────────────────────┐
│                         IndexModel                               │
├──────────────────────────────────────────────────────────────────┤
│                                                                  │
│  public class IndexModel : PageModel                             │
│  {                                                               │
│      private readonly ICustomerRepository _repository;           │
│                                                                  │
│      public IndexModel(ICustomerRepository repository)           │
│      {                                                           │
│          _repository = repository; // ← Injected                 │
│      }                                                           │
│  }                                                               │
│                                                                  │
└──────────────────────────────────────────────────────────────────┘
```

## Data Transformation Flow

```
Database Row        Entity Object       ViewModel          HTML
─────────────  →  ────────────────  →  ───────────────  →  ─────────

CustomerId: 5      Customer           CustomerList        <tr>
CompanyName:       {                   ViewModel          <td>
  "Alfreds"    →     CustomerId: 5  →  {                →   Alfreds
ContactName:         CompanyName:        Name:           </td>
  "Maria"             "Alfreds",         "Maria          <td>
City:                ContactName:         (Alfreds)",      Berlin
  "Berlin"            "Maria",           City:          </td>
Orders: [...]        City:                "Berlin"      </tr>
                      "Berlin",         }
                     Orders: [...]
                   }
```

## Async/Await Flow

```
User Request
     │
     │ Synchronous
     ↓
PageModel.OnGetAsync()
     │
     │ await (thread released back to pool)
     ↓
Repository.GetAllAsync()
     │
     │ await (thread released back to pool)
     ↓
DbContext.ToListAsync()
     │
     │ await (thread released back to pool)
     ↓
Database Query Executing
     │
     │ (I/O operation in progress)
     │
     ↓ Query completes
     │
DbContext returns data
     │
     │ Continuation (thread assigned from pool)
     ↑
Repository returns entities
     │
     │ Continuation (same or different thread)
     ↑
PageModel receives data
     │
     │ Synchronous
     ↑
Razor View Renders
     │
     ↑
Response to User
```

## Key Benefits of This Architecture

1. **Separation of Concerns**: Each layer has a single responsibility
2. **Dependency Inversion**: High-level modules depend on abstractions
3. **Testability**: Can mock repositories for unit testing
4. **Scalability**: Async/await allows handling more concurrent requests
5. **Maintainability**: Clear data flow makes debugging easier
6. **Flexibility**: Can swap implementations without changing business logic
