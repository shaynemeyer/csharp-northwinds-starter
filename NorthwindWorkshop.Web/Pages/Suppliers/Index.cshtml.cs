using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Web.ViewModels;

namespace NorthwindWorkshop.Web.Pages.Suppliers;

public class IndexModel : PageModel
{
    private readonly ISupplierRepository _supplierRepository;

    public IndexModel(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public List<SupplierListViewModel> Suppliers { get; set; } = new();
    public string? SearchTerm { get; set; }
    public bool ShowEmpty { get; set; }

    /// <summary>
    /// Gets a user-friendly description of the current filter settings
    /// </summary>
    public string FilterDescription => ShowEmpty ? "Showing all suppliers" : "Showing suppliers with active products only";

    public async Task OnGetAsync(string? searchTerm, bool showEmpty = false)
    {
        SearchTerm = searchTerm;
        ShowEmpty = showEmpty;

        var suppliers = await _supplierRepository.GetSuppliersWithProductCountAsync();

        // Apply filters
        var query = suppliers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(s =>
                s.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (s.ContactName != null && s.ContactName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                (s.City != null && s.City.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                (s.Country != null && s.Country.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        }

        // Filter suppliers based on display option
        if (!showEmpty)
        {
            // Only show suppliers that have at least one active (non-discontinued) product
            query = query.Where(s => s.Products.Any(p => !p.Discontinued));
        }
        // If showEmpty is true, we show all suppliers regardless of product status

        // Project to ViewModel
        Suppliers = query
            .Select(s => new SupplierListViewModel
            {
                Id = s.Id,
                CompanyName = s.CompanyName,
                ContactName = s.ContactName,
                City = s.City,
                Country = s.Country,
                Phone = s.Phone,
                ProductCount = s.Products.Count,
                ActiveProductCount = s.Products.Count(p => !p.Discontinued)
            })
            .OrderBy(s => s.CompanyName)
            .ToList();
    }
}