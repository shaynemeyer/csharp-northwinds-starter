# Northwind Workshop Progress Checklist

Track your progress through the C# and OOP workshop!

## ✅ Setup (30 minutes)

- [ ] Install .NET 9.0 SDK
- [ ] Install Visual Studio Code
- [ ] Install C# Dev Kit extension
- [ ] Install NuGet Package Manager extension
- [ ] Install SQLite Viewer extension (optional)
- [ ] Verify installation: `dotnet --version`
- [ ] Run setup script successfully
- [ ] Open project in VSCode
- [ ] Build solution: `dotnet build`
- [ ] Install EF Core tools: `dotnet tool install --global dotnet-ef`

## ✅ Part 1: Solution Setup (15 minutes)

- [ ] Create solution structure
- [ ] Add all three projects
- [ ] Set up project references
- [ ] Install NuGet packages
- [ ] Verify solution builds

## ✅ Part 2: Domain Entities (1 hour)

- [ ] Create BaseEntity class
- [ ] Understand inheritance concept
- [ ] Create Customer entity
- [ ] Understand encapsulation (properties)
- [ ] Create Product entity
- [ ] Implement business logic methods
- [ ] Create Category entity
- [ ] Create Supplier entity
- [ ] Create Order entity
- [ ] Understand navigation properties
- [ ] Create OrderDetail entity (composite key)
- [ ] Create Employee entity
- [ ] Understand self-referencing relationships
- [ ] Create Shipper entity

**Key Concepts Learned:**
- [ ] OOP: Inheritance
- [ ] OOP: Encapsulation
- [ ] OOP: Properties vs Fields
- [ ] Navigation Properties
- [ ] Business Logic in Entities

## ✅ Part 3: Repository Pattern (45 minutes)

- [ ] Create IRepository<T> interface
- [ ] Understand generic interfaces
- [ ] Understand SOLID: Interface Segregation
- [ ] Create ICustomerRepository interface
- [ ] Create IProductRepository interface
- [ ] Create IEmployeeRepository interface

**Key Concepts Learned:**
- [ ] OOP: Abstraction
- [ ] OOP: Generics
- [ ] SOLID: Single Responsibility
- [ ] SOLID: Dependency Inversion
- [ ] Repository Pattern

## ✅ Part 4: Data Access (1.5 hours)

- [ ] Create NorthwindDbContext
- [ ] Understand DbContext purpose
- [ ] Configure entity relationships
- [ ] Use Fluent API
- [ ] Implement Repository<T> base class
- [ ] Understand OOP: Polymorphism
- [ ] Implement CustomerRepository
- [ ] Understand LINQ queries
- [ ] Use Include for eager loading
- [ ] Implement ProductRepository
- [ ] Implement EmployeeRepository

**Key Concepts Learned:**
- [ ] OOP: Polymorphism
- [ ] Entity Framework Core
- [ ] LINQ
- [ ] Async/Await
- [ ] Eager Loading
- [ ] Lazy Loading

## ✅ Part 5: Database Seeding (30 minutes)

- [ ] Create DbInitializer class
- [ ] Add seed data for Categories
- [ ] Add seed data for Suppliers
- [ ] Add seed data for Products
- [ ] Add seed data for Customers
- [ ] Add seed data for Employees
- [ ] Add seed data for Shippers
- [ ] Understand data initialization

## ✅ Part 6: Web Configuration (30 minutes)

- [ ] Configure Program.cs
- [ ] Register DbContext with DI
- [ ] Understand Dependency Injection
- [ ] Register repositories
- [ ] Configure middleware
- [ ] Set up connection string
- [ ] Call DbInitializer on startup

**Key Concepts Learned:**
- [ ] Dependency Injection
- [ ] SOLID: Dependency Inversion (applied)
- [ ] ASP.NET Core configuration

## ✅ Part 7: ViewModels & Pages (2 hours)

- [ ] Create CustomerListViewModel
- [ ] Understand DTO pattern
- [ ] Create ProductListViewModel
- [ ] Create Customer Index PageModel
- [ ] Inject repository via DI
- [ ] Implement OnGetAsync method
- [ ] Use LINQ projections
- [ ] Create Customer Index view (Razor)
- [ ] Implement search functionality
- [ ] Implement filtering
- [ ] Create Product Index PageModel
- [ ] Create Product Index view
- [ ] Add multiple filters

**Key Concepts Learned:**
- [ ] MVVM Pattern
- [ ] DTO Pattern
- [ ] Razor Syntax
- [ ] LINQ Projections
- [ ] Query Filtering

## ✅ Part 8: Layout & Navigation (45 minutes)

- [ ] Create _Layout.cshtml
- [ ] Create _Sidebar.cshtml
- [ ] Implement navigation menu
- [ ] Style with Bootstrap
- [ ] Make navigation dynamic
- [ ] Add active state highlighting

## ✅ Part 9: Running the App (30 minutes)

- [ ] Create initial migration
- [ ] Understand migration files
- [ ] Apply migration to database
- [ ] Verify database created
- [ ] View database in SQLite Viewer
- [ ] Run application
- [ ] Debug with breakpoints
- [ ] Use VSCode debugger
- [ ] Test hot reload
- [ ] Navigate through pages

**Key Concepts Learned:**
- [ ] EF Core Migrations
- [ ] Debugging in VSCode
- [ ] Hot Reload

## ✅ Part 10: Exercises (4+ hours)

### Exercise 1: CRUD Operations
- [ ] Create Customer Create page
- [ ] Create Customer Edit page
- [ ] Implement Delete with confirmation
- [ ] Test all CRUD operations

### Exercise 2: Order Management
- [ ] Create Order list page
- [ ] Show order details with line items
- [ ] Display customer information
- [ ] Calculate order totals
- [ ] Filter by date range

### Exercise 3: Search Functionality
- [ ] Global search across entities
- [ ] Implement pagination
- [ ] Add sorting columns
- [ ] Optimize queries

### Exercise 4: Dashboard
- [ ] Create dashboard page
- [ ] Show total sales
- [ ] Display top products
- [ ] Show low stock alerts
- [ ] List recent orders

### Exercise 5: Unit of Work
- [ ] Create IUnitOfWork interface
- [ ] Implement UnitOfWork class
- [ ] Refactor repositories
- [ ] Update Program.cs
- [ ] Test transactions

## 📊 Skill Progress Tracker

### OOP Concepts
- [ ] Encapsulation - Beginner
- [ ] Encapsulation - Intermediate
- [ ] Encapsulation - Advanced
- [ ] Inheritance - Beginner
- [ ] Inheritance - Intermediate
- [ ] Inheritance - Advanced
- [ ] Polymorphism - Beginner
- [ ] Polymorphism - Intermediate
- [ ] Polymorphism - Advanced
- [ ] Abstraction - Beginner
- [ ] Abstraction - Intermediate
- [ ] Abstraction - Advanced

### SOLID Principles
- [ ] Single Responsibility - Understanding
- [ ] Single Responsibility - Application
- [ ] Open/Closed - Understanding
- [ ] Open/Closed - Application
- [ ] Liskov Substitution - Understanding
- [ ] Liskov Substitution - Application
- [ ] Interface Segregation - Understanding
- [ ] Interface Segregation - Application
- [ ] Dependency Inversion - Understanding
- [ ] Dependency Inversion - Application

### Technical Skills
- [ ] C# Syntax - Beginner
- [ ] C# Syntax - Intermediate
- [ ] ASP.NET Core - Beginner
- [ ] ASP.NET Core - Intermediate
- [ ] Entity Framework - Beginner
- [ ] Entity Framework - Intermediate
- [ ] LINQ - Beginner
- [ ] LINQ - Intermediate
- [ ] Async/Await - Understanding
- [ ] Async/Await - Application
- [ ] Dependency Injection - Understanding
- [ ] Dependency Injection - Application

## 🎯 Competency Checklist

After completing the workshop, you should be able to:

### C# Language
- [ ] Write classes with proper encapsulation
- [ ] Use inheritance effectively
- [ ] Implement interfaces
- [ ] Work with generics
- [ ] Use LINQ for data queries
- [ ] Write async/await code
- [ ] Handle exceptions properly

### ASP.NET Core
- [ ] Create a multi-project solution
- [ ] Configure services with DI
- [ ] Build Razor Pages
- [ ] Handle routing
- [ ] Work with ViewModels
- [ ] Implement form handling

### Entity Framework Core
- [ ] Design entity models
- [ ] Configure relationships
- [ ] Write LINQ queries
- [ ] Use eager/lazy loading
- [ ] Create and apply migrations
- [ ] Seed databases

### Software Architecture
- [ ] Implement Repository pattern
- [ ] Apply SOLID principles
- [ ] Use Clean Architecture
- [ ] Separate concerns properly
- [ ] Design for testability

### VSCode & Tooling
- [ ] Debug C# applications
- [ ] Use keyboard shortcuts
- [ ] Run EF migrations
- [ ] Use integrated terminal
- [ ] Configure workspace settings

## 📝 Personal Notes

### Challenges I Faced:
```
Write down any difficulties you encountered and how you solved them






```

### Concepts to Review:
```
List any concepts you want to revisit






```

### Ideas for Extensions:
```
What features would you add to the application?






```

## 🎓 Certificate of Completion

I, _________________, completed the C# Northwind Workshop on ___/___/____

- [ ] Completed all core sections (Parts 1-9)
- [ ] Completed at least 3 exercises
- [ ] Built and ran the application successfully
- [ ] Understood all OOP concepts
- [ ] Applied SOLID principles

**Total Time Spent:** _____ hours

**Key Takeaway:**
```




```

---

## Next Steps

After completing this checklist:

- [ ] Build your own project using what you learned
- [ ] Add unit tests to the Northwind project
- [ ] Explore Web APIs with ASP.NET Core
- [ ] Learn Blazor for interactive UIs
- [ ] Study advanced design patterns
- [ ] Deploy your application to the cloud

**Keep learning and building! 🚀**
