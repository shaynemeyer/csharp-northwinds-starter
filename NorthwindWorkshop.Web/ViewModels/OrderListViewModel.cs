namespace NorthwindWorkshop.Web.ViewModels;

public class OrderListViewModel
{
    public int Id { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public string? CustomerName { get; set; }
    public string? EmployeeName { get; set; }
    public string? ShipperName { get; set; }
    public decimal? Freight { get; set; }
    public string? ShipCity { get; set; }
    public string? ShipCountry { get; set; }
    public decimal OrderTotal { get; set; }
    public bool IsPending => ShippedDate == null && OrderDate != null;
    public bool IsOverdue => RequiredDate < DateTime.Now && IsPending;
}