using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Web.ViewModels;

namespace NorthwindWorkshop.Web.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderRepository _orderRepository;

    public IndexModel(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public List<OrderListViewModel> Orders { get; set; } = new();
    public string? SearchTerm { get; set; }
    public bool ShowPendingOnly { get; set; }
    public bool ShowOverdueOnly { get; set; }
    public string? FilterByCustomer { get; set; }

    public async Task OnGetAsync(string? searchTerm, bool showPendingOnly = false, bool showOverdueOnly = false, string? filterByCustomer = null)
    {
        SearchTerm = searchTerm;
        ShowPendingOnly = showPendingOnly;
        ShowOverdueOnly = showOverdueOnly;
        FilterByCustomer = filterByCustomer;

        var orders = await _orderRepository.GetOrdersWithDetailsAsync();

        // Apply filters
        var query = orders.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(o =>
                (o.Customer != null && o.Customer.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                (o.Employee != null && (o.Employee.FirstName + " " + o.Employee.LastName).Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                (o.ShipCity != null && o.ShipCity.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        }

        if (!string.IsNullOrWhiteSpace(filterByCustomer))
        {
            query = query.Where(o => o.Customer != null &&
                o.Customer.CompanyName.Contains(filterByCustomer, StringComparison.OrdinalIgnoreCase));
        }

        if (showPendingOnly)
        {
            query = query.Where(o => o.ShippedDate == null && o.OrderDate != null);
        }

        if (showOverdueOnly)
        {
            query = query.Where(o => o.RequiredDate < DateTime.Now && o.ShippedDate == null && o.OrderDate != null);
        }

        // Project to ViewModel
        Orders = query
            .Select(o => new OrderListViewModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequiredDate,
                ShippedDate = o.ShippedDate,
                CustomerName = o.Customer != null ? o.Customer.CompanyName : null,
                EmployeeName = o.Employee != null ? $"{o.Employee.FirstName} {o.Employee.LastName}" : null,
                ShipperName = o.Shipper != null ? o.Shipper.CompanyName : null,
                Freight = o.Freight,
                ShipCity = o.ShipCity,
                ShipCountry = o.ShipCountry,
                OrderTotal = o.CalculateTotal()
            })
            .OrderByDescending(o => o.OrderDate)
            .ToList();
    }
}