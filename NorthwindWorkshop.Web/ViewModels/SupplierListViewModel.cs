namespace NorthwindWorkshop.Web.ViewModels;

public class SupplierListViewModel
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public int ProductCount { get; set; }
    public int ActiveProductCount { get; set; }
}