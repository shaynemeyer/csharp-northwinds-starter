using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Web.ViewModels;

namespace NorthwindWorkshop.Web.Pages.Shippers;

public class IndexModel : PageModel
{
    private readonly IShipperRepository _shipperRepository;

    public IndexModel(IShipperRepository shipperRepository)
    {
        _shipperRepository = shipperRepository;
    }

    public List<ShipperListViewModel> Shippers { get; set; } = new();
    public string? SearchTerm { get; set; }
    public bool ShowActiveOnly { get; set; }

    public async Task OnGetAsync(string? searchTerm, bool showActiveOnly = false)
    {
        SearchTerm = searchTerm;
        ShowActiveOnly = showActiveOnly;

        var shippers = await _shipperRepository.GetShippersWithOrdersAsync();

        // Apply filters
        var query = shippers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(s =>
                s.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (s.Phone != null && s.Phone.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        }

        if (showActiveOnly)
        {
            query = query.Where(s => s.Orders.Any());
        }

        // Project to ViewModel
        Shippers = query
            .Select(s => new ShipperListViewModel
            {
                Id = s.Id,
                CompanyName = s.CompanyName,
                Phone = s.Phone,
                TotalOrders = s.Orders.Count,
                HasActiveOrders = s.Orders.Any()
            })
            .OrderBy(s => s.CompanyName)
            .ToList();
    }
}