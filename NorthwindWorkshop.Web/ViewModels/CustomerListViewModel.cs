namespace NorthwindWorkshop.Web.ViewModels;

/// <summary>
/// ViewModel for displaying customers in a list
/// Demonstrates: DTO pattern, separation of concerns
/// </summary>
public class CustomerListViewModel
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public int OrderCount { get; set; }
}