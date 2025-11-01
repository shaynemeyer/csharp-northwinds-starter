# Layered Architecture Diagram

## Three-Layer Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                     PRESENTATION LAYER                          │
│                 (NorthwindWorkshop.Web)                         │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐         │
│  │ Razor Pages  │  │ View Models  │  │   wwwroot    │         │
│  ├──────────────┤  ├──────────────┤  ├──────────────┤         │
│  │ Index.cshtml │  │ CustomerList │  │  CSS/JS      │         │
│  │ Details      │  │ ProductDetail│  │  Images      │         │
│  │ PageModels   │  │ OrderSummary │  │  Tailwind    │         │
│  └──────────────┘  └──────────────┘  └──────────────┘         │
│                                                                 │
│  Responsibilities:                                              │
│  • HTTP request/response handling                              │
│  • User interface rendering                                    │
│  • Input validation                                            │
│  • Navigation and routing                                      │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
                            ↓ depends on ↓
┌─────────────────────────────────────────────────────────────────┐
│                      DOMAIN LAYER                               │
│                 (NorthwindWorkshop.Core)                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐         │
│  │   Entities   │  │  Interfaces  │  │   Services   │         │
│  ├──────────────┤  ├──────────────┤  ├──────────────┤         │
│  │ Customer     │  │ IRepository  │  │ Customer     │         │
│  │ Product      │  │ ICustomer    │  │ Service      │         │
│  │ Order        │  │   Repository │  │ Business     │         │
│  │ Category     │  │ IProduct     │  │ Logic        │         │
│  │ Supplier     │  │   Repository │  │              │         │
│  │ Employee     │  │ IService     │  │              │         │
│  └──────────────┘  └──────────────┘  └──────────────┘         │
│                                                                 │
│  Responsibilities:                                              │
│  • Business logic and rules                                    │
│  • Domain models and entities                                  │
│  • Service contracts (interfaces)                              │
│  • Domain validations                                          │
│                                                                 │
│  Key Principle: NO DEPENDENCIES on other layers                │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
                            ↑ implements ↑
┌─────────────────────────────────────────────────────────────────┐
│                   DATA ACCESS LAYER                             │
│              (NorthwindWorkshop.Infrastructure)                 │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐         │
│  │   DbContext  │  │ Repositories │  │  Migrations  │         │
│  ├──────────────┤  ├──────────────┤  ├──────────────┤         │
│  │ Northwind    │  │ Customer     │  │ Schema       │         │
│  │ DbContext    │  │   Repository │  │ Versioning   │         │
│  │              │  │ Product      │  │ Database     │         │
│  │ Entity       │  │   Repository │  │ Updates      │         │
│  │ Config       │  │ Generic      │  │              │         │
│  │              │  │   Repository │  │              │         │
│  └──────────────┘  └──────────────┘  └──────────────┘         │
│                                                                 │
│  Responsibilities:                                              │
│  • Database connection management                              │
│  • LINQ queries and data access                                │
│  • Entity Framework configuration                              │
│  • Database migrations                                         │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
                            ↓ accesses ↓
┌─────────────────────────────────────────────────────────────────┐
│                        DATABASE                                 │
│                     (northwind.db)                              │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  SQLite Database                                                │
│                                                                 │
│  Tables:                                                        │
│  • Customers                                                    │
│  • Products                                                     │
│  • Orders                                                       │
│  • OrderDetails                                                 │
│  • Categories                                                   │
│  • Suppliers                                                    │
│  • Employees                                                    │
│  • Shippers                                                     │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Dependency Flow

```
┌─────────────┐
│     Web     │ ───────┐
└─────────────┘        │
                       ↓
                ┌─────────────┐
                │    Core     │ (Interfaces & Models)
                └─────────────┘
                       ↑
┌─────────────┐        │
│Infrastructure│───────┘
└─────────────┘
```

**Key Principle: Dependency Inversion**
- Web depends on Core (interfaces)
- Infrastructure depends on Core (implements interfaces)
- Core depends on NOTHING (pure business logic)

## Benefits of This Architecture

1. **Testability**: Each layer can be tested independently
2. **Maintainability**: Changes in one layer don't affect others
3. **Flexibility**: Can swap implementations (e.g., change from SQLite to SQL Server)
4. **Clarity**: Clear separation of concerns
5. **Reusability**: Core logic can be reused across different UIs
