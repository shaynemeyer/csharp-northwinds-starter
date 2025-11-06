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

        // Seed Orders
        var orders = new[]
        {
            new Order { CustomerId = 1, EmployeeId = 1, OrderDate = DateTime.Now.AddDays(-30), RequiredDate = DateTime.Now.AddDays(-23), ShipVia = 1, Freight = 12.50m, ShipName = "Alfreds Futterkiste", ShipCity = "Berlin", ShipCountry = "Germany" },
            new Order { CustomerId = 1, EmployeeId = 2, OrderDate = DateTime.Now.AddDays(-15), RequiredDate = DateTime.Now.AddDays(-8), ShipVia = 2, Freight = 8.75m, ShipName = "Alfreds Futterkiste", ShipCity = "Berlin", ShipCountry = "Germany" },
            new Order { CustomerId = 2, EmployeeId = 1, OrderDate = DateTime.Now.AddDays(-20), RequiredDate = DateTime.Now.AddDays(-13), ShipVia = 1, Freight = 15.30m, ShipName = "Ana Trujillo Emparedados", ShipCity = "México D.F.", ShipCountry = "Mexico" },
            new Order { CustomerId = 3, EmployeeId = 3, OrderDate = DateTime.Now.AddDays(-25), RequiredDate = DateTime.Now.AddDays(-18), ShipVia = 3, Freight = 22.40m, ShipName = "Antonio Moreno Taquería", ShipCity = "México D.F.", ShipCountry = "Mexico" },
            new Order { CustomerId = 4, EmployeeId = 2, OrderDate = DateTime.Now.AddDays(-10), RequiredDate = DateTime.Now.AddDays(-3), ShipVia = 1, Freight = 18.90m, ShipName = "Around the Horn", ShipCity = "London", ShipCountry = "UK" },
            new Order { CustomerId = 5, EmployeeId = 1, OrderDate = DateTime.Now.AddDays(-5), RequiredDate = DateTime.Now.AddDays(2), ShipVia = 2, Freight = 9.65m, ShipName = "Berglunds snabbköp", ShipCity = "Luleå", ShipCountry = "Sweden" }
        };
        context.Orders.AddRange(orders);
        context.SaveChanges();

        // Seed Order Details
        var orderDetails = new[]
        {
            // Order 1 details (Alfreds Futterkiste - first order)
            new OrderDetail { OrderId = 1, ProductId = 1, UnitPrice = 18.00m, Quantity = 5, Discount = 0 },
            new OrderDetail { OrderId = 1, ProductId = 3, UnitPrice = 10.00m, Quantity = 3, Discount = 0.05m },

            // Order 2 details (Alfreds Futterkiste - second order)
            new OrderDetail { OrderId = 2, ProductId = 2, UnitPrice = 19.00m, Quantity = 2, Discount = 0 },

            // Order 3 details (Ana Trujillo Emparedados)
            new OrderDetail { OrderId = 3, ProductId = 4, UnitPrice = 22.00m, Quantity = 4, Discount = 0 },
            new OrderDetail { OrderId = 3, ProductId = 6, UnitPrice = 25.00m, Quantity = 2, Discount = 0.10m },

            // Order 4 details (Antonio Moreno Taquería)
            new OrderDetail { OrderId = 4, ProductId = 7, UnitPrice = 30.00m, Quantity = 1, Discount = 0 },
            new OrderDetail { OrderId = 4, ProductId = 8, UnitPrice = 40.00m, Quantity = 2, Discount = 0 },

            // Order 5 details (Around the Horn)
            new OrderDetail { OrderId = 5, ProductId = 9, UnitPrice = 97.00m, Quantity = 1, Discount = 0 },

            // Order 6 details (Berglunds snabbköp)
            new OrderDetail { OrderId = 6, ProductId = 10, UnitPrice = 31.00m, Quantity = 3, Discount = 0 },
            new OrderDetail { OrderId = 6, ProductId = 1, UnitPrice = 18.00m, Quantity = 2, Discount = 0 }
        };
        context.OrderDetails.AddRange(orderDetails);
        context.SaveChanges();
    }
}