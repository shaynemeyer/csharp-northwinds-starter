namespace NorthwindWorkshop.Web.ViewModels;

public class EmployeeListViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public DateTime? HireDate { get; set; }
    public string? ManagerName { get; set; }
    public int OrderCount { get; set; }
}