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