namespace NorthwindWorkshop.Web.ViewModels;

public class ProductListViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
    public string? SupplierName { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public bool Discontinued { get; set; }
    public bool IsLowStock { get; set; }
}