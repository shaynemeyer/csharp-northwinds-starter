using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Web.ViewModels;

namespace NorthwindWorkshop.Web.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ICategoryRepository _categoryRepository;

    public IndexModel(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public List<CategoryListViewModel> Categories { get; set; } = new();
    public string? SearchTerm { get; set; }
    public bool ShowEmpty { get; set; }

    public async Task OnGetAsync(string? searchTerm, bool showEmpty = false)
    {
        SearchTerm = searchTerm;
        ShowEmpty = showEmpty;

        var categories = await _categoryRepository.GetCategoriesWithProductCountAsync();

        // Apply filters
        var query = categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(c =>
                c.CategoryName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (c.Description != null && c.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        }

        if (!showEmpty)
        {
            query = query.Where(c => c.Products.Any(p => !p.Discontinued));
        }

        // Project to ViewModel
        Categories = query
            .Select(c => new CategoryListViewModel
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                Description = c.Description,
                ProductCount = c.Products.Count,
                ActiveProductCount = c.Products.Count(p => !p.Discontinued)
            })
            .OrderBy(c => c.CategoryName)
            .ToList();
    }
}