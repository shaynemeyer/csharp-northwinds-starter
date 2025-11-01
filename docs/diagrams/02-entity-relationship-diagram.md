# Entity Relationship Diagram (ERD)

## Northwind Database Schema

```
┌─────────────────────────────┐
│         Suppliers           │
├─────────────────────────────┤
│ PK  SupplierId              │
│     CompanyName             │
│     ContactName             │
│     ContactTitle            │
│     Address                 │
│     City, Region            │
│     PostalCode, Country     │
│     Phone, Fax              │
│     HomePage                │
└─────────────────────────────┘
              │
              │ 1
              │
              │ Supplies
              │
              │ N
              ↓
┌─────────────────────────────┐         ┌─────────────────────────────┐
│         Categories          │         │         Products            │
├─────────────────────────────┤         ├─────────────────────────────┤
│ PK  CategoryId              │ 1       │ PK  ProductId               │
│     CategoryName            │─────────│ FK  SupplierId              │
│     Description             │         │ FK  CategoryId              │
│     Picture                 │ N       │     ProductName             │
└─────────────────────────────┘         │     QuantityPerUnit         │
                                        │     UnitPrice               │
              Categorizes               │     UnitsInStock            │
                                        │     UnitsOnOrder            │
                                        │     ReorderLevel            │
                                        │     Discontinued            │
                                        └─────────────────────────────┘
                                                    │
                                                    │ N
                                                    │
                                                    │ In
                                                    │
                                                    │ 1
                                                    ↓
┌─────────────────────────────┐         ┌─────────────────────────────┐
│         Customers           │         │       OrderDetails          │
├─────────────────────────────┤         ├─────────────────────────────┤
│ PK  CustomerId              │         │ PK  OrderDetailId           │
│     CompanyName             │         │ FK  OrderId                 │
│     ContactName             │         │ FK  ProductId               │
│     ContactTitle            │         │     UnitPrice               │
│     Address                 │         │     Quantity                │
│     City, Region            │         │     Discount                │
│     PostalCode, Country     │         └─────────────────────────────┘
│     Phone, Fax              │                     │
└─────────────────────────────┘                     │ N
              │                                     │
              │ 1                                   │ Part Of
              │                                     │
              │ Places                              │ 1
              │                                     │
              │ N                                   ↓
              ↓                         ┌─────────────────────────────┐
┌─────────────────────────────┐         │          Orders             │
│          Orders             │◄────────├─────────────────────────────┤
├─────────────────────────────┤         │ PK  OrderId                 │
│ PK  OrderId                 │         │ FK  CustomerId              │
│ FK  CustomerId              │         │ FK  EmployeeId              │
│ FK  EmployeeId              │         │ FK  ShipperId               │
│ FK  ShipperId               │         │     OrderDate               │
│     OrderDate               │         │     RequiredDate            │
│     RequiredDate            │         │     ShippedDate             │
│     ShippedDate             │         │     Freight                 │
│     Freight                 │         │     ShipName                │
│     ShipName                │         │     ShipAddress             │
│     ShipAddress             │         │     ShipCity, ShipRegion    │
│     ShipCity, ShipRegion    │         │     ShipPostalCode          │
│     ShipPostalCode          │         │     ShipCountry             │
│     ShipCountry             │         └─────────────────────────────┘
└─────────────────────────────┘                     │
              ↑                                     │ N
              │ N                                   │
              │                                     │
              │ Processes                           │ Ships
              │                                     │
              │ 1                                   │ 1
              │                                     │
┌─────────────────────────────┐                     ↓
│         Employees           │         ┌─────────────────────────────┐
├─────────────────────────────┤         │         Shippers            │
│ PK  EmployeeId              │         ├─────────────────────────────┤
│     LastName                │         │ PK  ShipperId               │
│     FirstName               │         │     CompanyName             │
│     Title                   │         │     Phone                   │
│     TitleOfCourtesy         │         └─────────────────────────────┘
│     BirthDate               │
│     HireDate                │
│     Address                 │
│     City, Region            │
│     PostalCode, Country     │
│     HomePhone               │
│     Extension               │
│     Photo                   │
│     Notes                   │
│ FK  ReportsTo               │
│     PhotoPath               │
└─────────────────────────────┘
              │
              │
              │ Self-referencing
              │ (Manager hierarchy)
              │
              └─────────────────┐
                                ↓
                        Reports To Manager
```

## Relationship Cardinalities

### One-to-Many Relationships

| Parent Entity | Child Entity | Description |
|--------------|-------------|-------------|
| **Customer** | Orders | One customer can place many orders |
| **Employee** | Orders | One employee can process many orders |
| **Shipper** | Orders | One shipper can ship many orders |
| **Order** | OrderDetails | One order can contain many line items |
| **Product** | OrderDetails | One product can appear in many order lines |
| **Category** | Products | One category can contain many products |
| **Supplier** | Products | One supplier can supply many products |
| **Employee** | Employees | One employee (manager) can supervise many employees |

## Key Entity Properties and Behaviors

### Customer
```csharp
Properties:
- CustomerId (PK)
- CompanyName
- ContactName, ContactTitle
- Address, City, Region, PostalCode, Country
- Phone, Fax

Computed:
- DisplayName (ContactName + CompanyName)

Navigation:
- Orders (ICollection<Order>)
```

### Product
```csharp
Properties:
- ProductId (PK)
- ProductName
- UnitPrice, UnitsInStock
- UnitsOnOrder, ReorderLevel
- Discontinued

Methods:
- IsLowStock() → bool

Navigation:
- Category
- Supplier
- OrderDetails (ICollection<OrderDetail>)
```

### Order
```csharp
Properties:
- OrderId (PK)
- OrderDate, RequiredDate, ShippedDate
- Freight
- ShipName, ShipAddress, ShipCity, etc.

Computed:
- TotalAmount (sum of OrderDetails)
- Status (based on dates)

Navigation:
- Customer
- Employee
- Shipper
- OrderDetails (ICollection<OrderDetail>)
```

### OrderDetail
```csharp
Properties:
- OrderDetailId (PK)
- UnitPrice
- Quantity
- Discount

Computed:
- LineTotal (UnitPrice * Quantity * (1 - Discount))

Navigation:
- Order
- Product
```

## Domain Model Inheritance

```
┌─────────────────────────┐
│      BaseEntity         │
│    (Abstract Class)     │
├─────────────────────────┤
│ + Id (int)              │
│ + CreatedDate (DateTime)│
│ + ModifiedDate (DateTime)│
└─────────────────────────┘
            ▲
            │ Inherits from
            │
     ┌──────┴──────┬──────────┬──────────┬──────────┐
     │             │          │          │          │
┌─────────┐  ┌─────────┐  ┌──────┐  ┌─────────┐  ┌─────────┐
│Customer │  │Product  │  │Order │  │Category │  │Supplier │
└─────────┘  └─────────┘  └──────┘  └─────────┘  └─────────┘
```

## Business Rules

1. **Product Inventory**
   - UnitsInStock should not go below 0
   - IsLowStock() triggers when UnitsInStock < ReorderLevel

2. **Order Processing**
   - OrderDate must be before or equal to RequiredDate
   - ShippedDate should be after OrderDate
   - Freight must be >= 0

3. **Customer Orders**
   - Customer must exist before placing an order
   - Customer can have zero or many orders

4. **Order Details**
   - Quantity must be > 0
   - Discount must be between 0 and 1 (0% to 100%)
   - UnitPrice must be >= 0

5. **Employee Hierarchy**
   - Employee can report to one manager (ReportsTo)
   - Manager must be another valid Employee
   - Circular references not allowed
