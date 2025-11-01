# Next.js vs C# Northwind Implementation Comparison

## Architecture Comparison

### Next.js Version
```
next-northwind-starter/
├── app/                      # Next.js App Router (Pages)
├── components/               # React Components
├── db/actions/              # Server Actions (Data Layer)
├── drizzle/                 # ORM Schema & Migrations
└── lib/                     # Utilities & Constants
```

### C# Version
```
NorthwindWorkshop/
├── NorthwindWorkshop.Core/           # Domain Layer
├── NorthwindWorkshop.Infrastructure/ # Data Access Layer
└── NorthwindWorkshop.Web/           # Presentation Layer
```

---

## Technology Stack Comparison

| Aspect | Next.js Version | C# Version |
|--------|----------------|------------|
| **Framework** | Next.js 15 | ASP.NET Core 9.0 |
| **Language** | TypeScript/JavaScript | C# 13 |
| **Runtime** | Node.js 22+ | .NET 9.0 |
| **UI Framework** | React 19 | Razor Pages |
| **Database** | SQLite | SQLite |
| **ORM** | Drizzle ORM | Entity Framework Core |
| **Styling** | Tailwind CSS 4 + shadcn/ui | Tailwind CSS 4 |
| **Data Tables** | TanStack Table (React Table v8) | HTML Tables + Tailwind |
| **Type Safety** | TypeScript | C# (strongly typed) |
| **Icons** | Lucide React | Emojis / Heroicons |

---

## Key Concept Mappings

### 1. Data Fetching

**Next.js (Server Actions):**
```typescript
// db/actions/customers.ts
'use server'
import { db } from '@/db'
import { customers } from '@/drizzle/schema'

export async function getAllCustomers() {
  try {
    const data = await db.select().from(customers)
    return { success: true, data }
  } catch (error) {
    return { success: false, error: 'Failed to fetch customers' }
  }
}
```

**C# (Repository Pattern):**
```csharp
// Infrastructure/Repositories/CustomerRepository.cs
public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
}
```

---

### 2. Schema/Entity Definition

**Next.js (Drizzle Schema):**
```typescript
// drizzle/schema.ts
import { sqliteTable, text, integer } from 'drizzle-orm/sqlite-core'

export const customers = sqliteTable('customers', {
  customerId: integer('customer_id').primaryKey(),
  customerName: text('customer_name'),
  contactName: text('contact_name'),
  city: text('city'),
  country: text('country')
})
```

**C# (Entity Class):**
```csharp
// Core/Entities/Customer.cs
public class Customer : BaseEntity
{
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    
    // Navigation property
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
```

---

### 3. Database Context

**Next.js (Drizzle Connection):**
```typescript
// db/index.ts
import { drizzle } from 'drizzle-orm/better-sqlite3'
import Database from 'better-sqlite3'

const sqlite = new Database('northwind.db')
export const db = drizzle(sqlite)
```

**C# (Entity Framework DbContext):**
```csharp
// Infrastructure/Data/NorthwindDbContext.cs
public class NorthwindDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships and constraints
    }
}
```

---

### 4. Page Components

**Next.js (React Server Component):**
```typescript
// app/customers/page.tsx
import { DataTable } from "@/components/ui/data-table"
import { columns } from "./columns"
import { getAllCustomers } from "@/db/actions/customers"

export default async function CustomersPage() {
  const result = await getAllCustomers()
  
  return (
    <DataTable
      columns={columns}
      data={result.data}
      searchKey="customerName"
      searchPlaceholder="Search customers..."
    />
  )
}
```

**C# (Razor Page Model):**
```csharp
// Web/Pages/Customers/Index.cshtml.cs
public class IndexModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;
    
    public List<CustomerListViewModel> Customers { get; set; } = new();
    
    public async Task OnGetAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        Customers = customers.Select(c => new CustomerListViewModel
        {
            Id = c.Id,
            CompanyName = c.CompanyName,
            // ... map properties
        }).ToList();
    }
}
```

---

### 5. Column Definitions

**Next.js (TanStack Table Columns):**
```typescript
// app/customers/columns.tsx
import { ColumnDef } from "@tanstack/react-table"

export const columns: ColumnDef<Customer>[] = [
  {
    accessorKey: "customerName",
    header: "Customer Name",
  },
  {
    accessorKey: "contactName",
    header: "Contact Name",
  },
]
```

**C# (HTML Table in Razor View):**
```html
<!-- Web/Pages/Customers/Index.cshtml -->
<table class="table">
    <thead>
        <tr>
            <th>Company Name</th>
            <th>Contact Name</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var customer in Model.Customers)
        {
            <tr>
                <td>@customer.CompanyName</td>
                <td>@customer.ContactName</td>
            </tr>
        }
    </tbody>
</table>
```

---

### 6. Dependency Injection

**Next.js:**
Not traditionally used; relies on ES6 module imports and Next.js conventions

**C# (Built-in DI Container):**
```csharp
// Web/Program.cs
builder.Services.AddDbContext<NorthwindDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
```

---

### 7. Routing

**Next.js (File-based routing):**
```
app/
├── customers/
│   └── page.tsx          → /customers
├── employees/
│   └── page.tsx          → /employees
└── products/
    └── page.tsx          → /products
```

**C# (Convention-based routing):**
```
Pages/
├── Customers/
│   └── Index.cshtml      → /Customers/Index
├── Employees/
│   └── Index.cshtml      → /Employees/Index
└── Products/
    └── Index.cshtml      → /Products/Index
```

---

## Architectural Pattern Comparison

### Next.js Architecture
1. **Server Components** - React components that run on the server
2. **Server Actions** - Server-side functions for data mutations
3. **Client Components** - Interactive React components
4. **No traditional "layers"** - More feature-oriented structure

### C# Architecture (Clean Architecture / N-Tier)
1. **Core Layer** - Domain entities and business logic (no dependencies)
2. **Infrastructure Layer** - Data access, external services (depends on Core)
3. **Web Layer** - UI and user interaction (depends on Core & Infrastructure)
4. **Clear separation of concerns** - Enforced by project boundaries

---

## OOP Concepts

### Next.js/TypeScript
- **Interfaces** - Type contracts
- **Functional Programming** - Primary paradigm
- **Composition** - Component composition
- **Type Safety** - Via TypeScript

### C# 
- **Classes** - Primary building blocks
- **Inheritance** - BaseEntity, Repository<T>
- **Polymorphism** - Virtual methods, interfaces
- **Encapsulation** - Properties, access modifiers
- **Abstraction** - Interfaces, abstract classes
- **SOLID Principles** - Explicitly demonstrated

---

## Data Access Patterns

### Next.js (Drizzle ORM)
```typescript
// Direct queries
const customers = await db
  .select()
  .from(customersTable)
  .where(eq(customersTable.country, 'USA'))

// Relational queries
const customersWithOrders = await db.query.customers.findMany({
  with: {
    orders: true
  }
})
```

### C# (Entity Framework Core)
```csharp
// LINQ queries
var customers = await _dbSet
    .Where(c => c.Country == "USA")
    .ToListAsync();

// Eager loading
var customersWithOrders = await _dbSet
    .Include(c => c.Orders)
    .ToListAsync();
```

---

## State Management

### Next.js
- **Server State** - Handled by Server Components
- **Client State** - React hooks (useState, useReducer)
- **URL State** - Search params, route params
- **No Redux needed** - Server Components handle most state

### C#
- **ViewModels** - Data for specific views
- **TempData** - Data between redirects
- **Session State** - User session data
- **View State** - Not typically used in Razor Pages

---

## When to Choose Which?

### Choose Next.js/React When:
- Building a highly interactive, SPA-like experience
- Need real-time features
- Want to deploy to edge/serverless
- Team has JavaScript/TypeScript expertise
- Building a public-facing website
- SEO and performance are critical
- Want modern, component-based UI development

### Choose C#/ASP.NET Core When:
- Building enterprise applications
- Need strong typing and compile-time checking
- Team has C# or .NET experience
- Building internal business applications
- Need robust data access with complex queries
- Want mature tooling and ecosystem
- Require Windows integration
- Building microservices or APIs
- Need strong OOP and design patterns

---

## Learning Curve

### Next.js
- **Easier to start** - HTML/CSS/JS knowledge sufficient
- **React ecosystem** - Must learn React patterns
- **Async patterns** - Promises, async/await
- **Modern JavaScript** - ES6+, TypeScript
- **Build tools** - Webpack, bundlers (abstracted)

### C#
- **Steeper initial learning** - Language syntax, OOP concepts
- **Rich IDE support** - IntelliSense, debugging
- **Type system** - Generics, interfaces, inheritance
- **LINQ** - Powerful query syntax
- **Framework conventions** - More to learn upfront
- **Better long-term** - Strong foundations in software engineering

---

## Performance Characteristics

### Next.js
- **Fast initial load** - Server-side rendering
- **Client-side routing** - Instant navigation
- **Code splitting** - Automatic optimization
- **Edge deployment** - Global distribution

### C#
- **Server-rendered** - Traditional request/response
- **Compiled code** - Fast execution
- **Efficient data access** - EF Core optimizations
- **Scalable** - Excellent for high-traffic scenarios

---

## Ecosystem & Tooling

### Next.js
- **npm/yarn** - Package management
- **VS Code** - Primary IDE
- **React DevTools** - Component debugging
- **Vercel** - Optimized hosting platform

### C#
- **NuGet** - Package management
- **VS Code** - Cross-platform IDE (our choice!)
- **Visual Studio** - Full-featured IDE (Windows/Mac)
- **Rider** - Alternative IDE from JetBrains
- **Azure** - Native cloud platform
- **Built-in debugging** - Excellent debugging experience
- **C# Dev Kit** - Official VSCode extension for C# development

---

## Which Should You Learn?

### Learn Both If:
- You want full-stack capabilities
- You're building diverse applications
- You want maximum career flexibility

### Start with Next.js If:
- You know JavaScript already
- Building web-first applications
- Want to see results quickly
- Interested in modern web development

### Start with C# If:
- Want strong CS fundamentals
- Building enterprise software
- Interested in game development (Unity)
- Want a versatile, powerful language
- Preparing for enterprise career

---

## Conclusion

Both approaches are excellent for learning web development and the Northwind database structure. The Next.js version emphasizes modern web development patterns and React, while the C# version provides deeper exposure to object-oriented programming, clean architecture, and enterprise development patterns.

The C# workshop is specifically designed to teach:
- **OOP principles** through practical application
- **SOLID principles** in a real-world context
- **Clean architecture** with clear separation of concerns
- **Type-safe data access** with Entity Framework
- **Design patterns** (Repository, Dependency Injection)

Choose based on your learning goals and career aspirations!
