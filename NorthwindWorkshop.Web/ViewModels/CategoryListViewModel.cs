namespace NorthwindWorkshop.Web.ViewModels;

public class CategoryListViewModel
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int ProductCount { get; set; }
    public int ActiveProductCount { get; set; }
}