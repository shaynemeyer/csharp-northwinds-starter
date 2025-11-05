namespace NorthwindWorkshop.Core.Entities;

public class Shipper : BaseEntity
{
    public string CompanyName { get; set; } = string.Empty;
    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}