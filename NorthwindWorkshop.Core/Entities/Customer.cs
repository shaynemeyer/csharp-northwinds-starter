namespace NorthwindWorkshop.Core.Entities;

/// <summary>
/// Represents a customer in the Northwind system
/// Demonstrates: OOP Encapsulation, Properties, Navigation Properties
/// </summary>
public class Customer : BaseEntity
{
    // Properties - Encapsulation of data
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? ContactTitle { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }

    // Navigation Property - Represents relationship (One-to-Many)
    // One Customer can have many Orders
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    // Computed property - demonstrates logic in entities
    public string DisplayName => $"{CompanyName} ({ContactName})";
}