using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Web.ViewModels;

namespace NorthwindWorkshop.Web.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductRepository _productRepository;

    public IndexModel(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<ProductListViewModel> Products { get; set; } = new();
    public string? SearchTerm { get; set; }
    public bool ShowDiscontinued { get; set; }
    public bool ShowLowStock { get; set; }

    public async Task OnGetAsync(string? searchTerm, bool showDiscontinued = false, bool showLowStock = false)
    {
        SearchTerm = searchTerm;
        ShowDiscontinued = showDiscontinued;
        ShowLowStock = showLowStock;

        var products = await _productRepository.GetProductsWithDetailsAsync();

        // Apply filters
        var query = products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p =>
                p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        if (!showDiscontinued)
        {
            query = query.Where(p => !p.Discontinued);
        }

        if (showLowStock)
        {
            query = query.Where(p => p.IsLowStock());
        }

        // Project to ViewModel
        Products = query
            .Select(p => new ProductListViewModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                CategoryName = p.Category != null ? p.Category.CategoryName : null,
                SupplierName = p.Supplier != null ? p.Supplier.CompanyName : null,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                Discontinued = p.Discontinued,
                IsLowStock = p.IsLowStock()
            })
            .OrderBy(p => p.ProductName)
            .ToList();
    }
}