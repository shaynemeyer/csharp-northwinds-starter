# Orders Repository Implementation

**Detailed Documentation for Order Repository Pattern Implementation**

This document provides comprehensive coverage of the Order repository implementation, following Clean Architecture principles and the Repository Pattern established in the Northwind Workshop project.

## 🏗️ Architecture Overview

### Repository Pattern Structure

```
┌─────────────────────────────────────┐
│           Domain Layer              │
│                                     │
│  ┌─────────────────────────────────┐│
│  │      IOrderRepository           ││  ← Interface Definition
│  │  ┌─────────────────────────────┐││
│  │  │     IRepository<Order>      │││  ← Base Generic Interface
│  │  └─────────────────────────────┘││
│  └─────────────────────────────────┘│
└─────────────────────────────────────┘
                    │
                    │ implements
                    ▼
┌─────────────────────────────────────┐
│        Infrastructure Layer         │
│                                     │
│  ┌─────────────────────────────────┐│
│  │       OrderRepository           ││  ← Concrete Implementation
│  │  ┌─────────────────────────────┐││
│  │  │    Repository<Order>        │││  ← Base Generic Repository
│  │  └─────────────────────────────┘││
│  └─────────────────────────────────┘│
└─────────────────────────────────────┘
```

## 📋 Interface Definition

### File: `NorthwindWorkshop.Core/Interfaces/IOrderRepository.cs`

```csharp
using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Core.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
    Task<IEnumerable<Order>> GetOrdersByEmployeeAsync(int employeeId);
    Task<IEnumerable<Order>> GetRecentOrdersAsync(int daysBack = 30);
    Task<IEnumerable<Order>> GetOrdersWithDetailsAsync();
    Task<IEnumerable<Order>> GetPendingOrdersAsync();
    Task<IEnumerable<Order>> GetShippedOrdersAsync();
}
```

### Interface Design Principles

1. **Inheritance**: Extends `IRepository<Order>` for base CRUD operations
2. **Async/Await**: All methods are asynchronous for better performance
3. **Specific Queries**: Methods tailored to business requirements
4. **Optional Parameters**: Default values where appropriate (e.g., `daysBack = 30`)

### Method Purposes

| Method | Business Purpose | Use Case |
|--------|------------------|----------|
| `GetOrdersByCustomerAsync` | Customer order history | Customer service, sales analysis |
| `GetOrdersByEmployeeAsync` | Employee performance tracking | Sales reporting, commission calculation |
| `GetRecentOrdersAsync` | Recent activity monitoring | Dashboard, recent activity feeds |
| `GetOrdersWithDetailsAsync` | Complete order information | Order management, detailed views |
| `GetPendingOrdersAsync` | Fulfillment management | Warehouse operations, shipping |
| `GetShippedOrdersAsync` | Completion tracking | Delivery confirmation, analytics |

## 🔧 Implementation Details

### File: `NorthwindWorkshop.Infrastructure/Repositories/OrderRepository.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;

namespace NorthwindWorkshop.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(NorthwindDbContext context) : base(context)
    {
    }

    // Implementation methods...
}
```

### Constructor Pattern

- **Inheritance**: Inherits from `Repository<Order>` for base functionality
- **Dependency Injection**: Accepts `NorthwindDbContext` via constructor
- **Base Call**: Passes context to base repository class

## 📊 Method Implementations

### 1. GetOrdersByCustomerAsync

```csharp
public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
{
    return await _dbSet
        .Where(o => o.CustomerId == customerId)
        .Include(o => o.Customer)
        .Include(o => o.Employee)
        .Include(o => o.Shipper)
        .OrderByDescending(o => o.OrderDate)
        .ToListAsync();
}
```

**Key Features:**
- **Filtering**: By customer ID
- **Eager Loading**: Customer, Employee, and Shipper data
- **Sorting**: Most recent orders first
- **Performance**: Single query with joins

### 2. GetOrdersByEmployeeAsync

```csharp
public async Task<IEnumerable<Order>> GetOrdersByEmployeeAsync(int employeeId)
{
    return await _dbSet
        .Where(o => o.EmployeeId == employeeId)
        .Include(o => o.Customer)
        .Include(o => o.Employee)
        .Include(o => o.Shipper)
        .OrderByDescending(o => o.OrderDate)
        .ToListAsync();
}
```

**Key Features:**
- **Employee Focus**: Filter by sales employee
- **Complete Data**: All related entities included
- **Chronological**: Newest orders first
- **Sales Reporting**: Enables commission calculations

### 3. GetRecentOrdersAsync

```csharp
public async Task<IEnumerable<Order>> GetRecentOrdersAsync(int daysBack = 30)
{
    var cutoffDate = DateTime.Now.AddDays(-daysBack);
    return await _dbSet
        .Where(o => o.OrderDate >= cutoffDate)
        .Include(o => o.Customer)
        .Include(o => o.Employee)
        .Include(o => o.Shipper)
        .OrderByDescending(o => o.OrderDate)
        .ToListAsync();
}
```

**Key Features:**
- **Configurable Timeframe**: Default 30 days, customizable
- **Date Calculation**: Dynamic cutoff date
- **Dashboard Ready**: Perfect for activity feeds
- **Performance Optimized**: Database-level date filtering

### 4. GetOrdersWithDetailsAsync

```csharp
public async Task<IEnumerable<Order>> GetOrdersWithDetailsAsync()
{
    return await _dbSet
        .Include(o => o.Customer)
        .Include(o => o.Employee)
        .Include(o => o.Shipper)
        .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
        .OrderByDescending(o => o.OrderDate)
        .ToListAsync();
}
```

**Key Features:**
- **Complete Data**: All relationships included
- **Order Details**: Line items with products
- **Nested Includes**: `ThenInclude` for product data
- **Comprehensive**: Used for detailed order views

### 5. GetPendingOrdersAsync

```csharp
public async Task<IEnumerable<Order>> GetPendingOrdersAsync()
{
    return await _dbSet
        .Where(o => o.ShippedDate == null && o.OrderDate != null)
        .Include(o => o.Customer)
        .Include(o => o.Employee)
        .Include(o => o.Shipper)
        .OrderBy(o => o.RequiredDate ?? o.OrderDate)
        .ToListAsync();
}
```

**Key Features:**
- **Status Logic**: Not shipped but ordered
- **Business Rules**: Must have order date
- **Priority Sorting**: By required date (urgent first)
- **Null Handling**: Fallback to order date
- **Operations Focus**: Warehouse and fulfillment teams

### 6. GetShippedOrdersAsync

```csharp
public async Task<IEnumerable<Order>> GetShippedOrdersAsync()
{
    return await _dbSet
        .Where(o => o.ShippedDate != null)
        .Include(o => o.Customer)
        .Include(o => o.Employee)
        .Include(o => o.Shipper)
        .OrderByDescending(o => o.ShippedDate)
        .ToListAsync();
}
```

**Key Features:**
- **Completion Focus**: Only shipped orders
- **Delivery Tracking**: Sorted by ship date
- **Analytics Ready**: For completion metrics
- **Customer Service**: Delivery confirmations

## 🔍 Query Optimization Strategies

### 1. Eager Loading vs. Lazy Loading

**Chosen Approach: Eager Loading**
```csharp
.Include(o => o.Customer)
.Include(o => o.Employee)
.Include(o => o.Shipper)
```

**Benefits:**
- Prevents N+1 query problems
- Single database round trip
- Better performance for display scenarios
- Predictable memory usage

### 2. Projection Considerations

**Current**: Full entity loading
```csharp
return await _dbSet.Include(o => o.Customer).ToListAsync();
```

**Future Optimization**: Direct projection to ViewModel
```csharp
return await _dbSet
    .Select(o => new OrderListViewModel
    {
        Id = o.Id,
        CustomerName = o.Customer.CompanyName,
        // ... other properties
    })
    .ToListAsync();
```

### 3. Filtering at Database Level

**Efficient Approach:**
```csharp
.Where(o => o.OrderDate >= cutoffDate)  // Applied in SQL
```

**Avoid:**
```csharp
.ToList().Where(o => o.OrderDate >= cutoffDate)  // Applied in memory
```

## 📈 Performance Considerations

### Query Performance

| Method | Complexity | Performance Notes |
|--------|------------|-------------------|
| `GetOrdersByCustomerAsync` | Medium | Indexed on CustomerId |
| `GetOrdersByEmployeeAsync` | Medium | Indexed on EmployeeId |
| `GetRecentOrdersAsync` | Low | Date index recommended |
| `GetOrdersWithDetailsAsync` | High | Most expensive (OrderDetails join) |
| `GetPendingOrdersAsync` | Low-Medium | Simple status filter |
| `GetShippedOrdersAsync` | Low-Medium | Simple status filter |

### Database Indexes

**Recommended Indexes:**
```sql
-- Customer lookup
CREATE INDEX IX_Orders_CustomerId ON Orders (CustomerId);

-- Employee lookup
CREATE INDEX IX_Orders_EmployeeId ON Orders (EmployeeId);

-- Date range queries
CREATE INDEX IX_Orders_OrderDate ON Orders (OrderDate);

-- Status queries
CREATE INDEX IX_Orders_ShippedDate ON Orders (ShippedDate);

-- Composite for pending orders
CREATE INDEX IX_Orders_Status ON Orders (OrderDate, ShippedDate);
```

## 🧪 Testing Strategies

### Unit Testing Repository

```csharp
[Test]
public async Task GetPendingOrdersAsync_ShouldReturnUnshippedOrders()
{
    // Arrange
    var context = CreateTestContext();
    var repository = new OrderRepository(context);

    // Add test data
    context.Orders.AddRange(
        new Order { Id = 1, OrderDate = DateTime.Now, ShippedDate = null },
        new Order { Id = 2, OrderDate = DateTime.Now, ShippedDate = DateTime.Now },
        new Order { Id = 3, OrderDate = null, ShippedDate = null }
    );
    await context.SaveChangesAsync();

    // Act
    var result = await repository.GetPendingOrdersAsync();

    // Assert
    Assert.That(result.Count(), Is.EqualTo(1));
    Assert.That(result.First().Id, Is.EqualTo(1));
}
```

### Integration Testing

```csharp
[Test]
public async Task GetOrdersWithDetailsAsync_ShouldIncludeOrderDetails()
{
    // Arrange
    using var context = CreateIntegrationTestContext();
    var repository = new OrderRepository(context);

    // Act
    var orders = await repository.GetOrdersWithDetailsAsync();

    // Assert
    Assert.That(orders.Any(o => o.OrderDetails.Any()), Is.True);
    Assert.That(orders.First().Customer, Is.Not.Null);
}
```

## 🔧 Dependency Injection Setup

### Registration in Program.cs

```csharp
// File: NorthwindWorkshop.Web/Program.cs
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
```

### Usage in Page Models

```csharp
public class IndexModel : PageModel
{
    private readonly IOrderRepository _orderRepository;

    public IndexModel(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task OnGetAsync()
    {
        var orders = await _orderRepository.GetOrdersWithDetailsAsync();
        // Process orders...
    }
}
```

## 🛠️ Extension Methods

### Future Enhancement Ideas

```csharp
public static class OrderRepositoryExtensions
{
    public static async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(
        this IOrderRepository repository,
        DateTime startDate,
        DateTime endDate)
    {
        // Implementation for date range queries
    }

    public static async Task<decimal> GetTotalRevenueAsync(
        this IOrderRepository repository,
        int customerId)
    {
        // Implementation for revenue calculations
    }
}
```

## 📚 Related Patterns

### Generic Repository Base

The `OrderRepository` inherits from `Repository<Order>`, which provides:

- `GetAllAsync()`
- `GetByIdAsync(int id)`
- `AddAsync(Order entity)`
- `UpdateAsync(Order entity)`
- `DeleteAsync(int id)`

### Unit of Work Pattern

**Future Consideration:**
```csharp
public interface IUnitOfWork
{
    IOrderRepository Orders { get; }
    ICustomerRepository Customers { get; }
    Task<int> SaveChangesAsync();
}
```

## 🎯 Best Practices Applied

1. **Async/Await**: All database operations are asynchronous
2. **Interface Segregation**: Specific methods for specific needs
3. **Single Responsibility**: Each method has one clear purpose
4. **Dependency Inversion**: Depends on abstractions, not concretions
5. **Performance**: Eager loading to prevent N+1 queries
6. **Null Safety**: Proper handling of nullable fields
7. **Sorting**: Consistent and business-relevant ordering
8. **Filtering**: Applied at database level for efficiency

---

**Repository Implementation Complete!** ✅

The OrderRepository provides a robust, performant, and maintainable data access layer that follows established patterns and supports all business requirements for order management.