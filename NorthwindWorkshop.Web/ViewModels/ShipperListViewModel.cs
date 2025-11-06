namespace NorthwindWorkshop.Web.ViewModels;

public class ShipperListViewModel
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public int TotalOrders { get; set; }
    public bool HasActiveOrders { get; set; }
}