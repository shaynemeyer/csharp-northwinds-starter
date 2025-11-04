namespace NorthwindWorkshop.Core.Entities;

/// <summary>
/// Represents a line item in an order
/// Composite key: OrderId + ProductId
/// </summary>
public class OrderDetail
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    public decimal Discount { get; set; }

    // Navigation properties
    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;

    // Computed property
    public decimal LineTotal => Quantity * UnitPrice * (1 - Discount);
}