using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Web.ViewModels;

namespace NorthwindWorkshop.Web.Pages.Customers;

/// <summary>
/// Page model for customer list
/// Demonstrates: MVVM pattern, Dependency Injection, LINQ projections
/// </summary>
public class IndexModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public IndexModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public List<CustomerListViewModel> Customers { get; set; } = new();
    public string? SearchTerm { get; set; }
    public string? CountryFilter { get; set; }
    public bool? HasOrdersFilter { get; set; }
    public List<string> Countries { get; set; } = new();

    public async Task OnGetAsync(string? searchTerm, string? country, bool? hasOrders)
    {
        SearchTerm = searchTerm;
        CountryFilter = country;
        HasOrdersFilter = hasOrders;

        // Get customers based on filter
        var customers = hasOrders == true
            ? await _customerRepository.GetCustomersWithOrdersAsync()
            : await _customerRepository.GetAllCustomersAsync();

        // Apply filters
        var query = customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(c =>
                c.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (c.ContactName != null && c.ContactName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        }

        if (!string.IsNullOrWhiteSpace(country))
        {
            query = query.Where(c => c.Country == country);
        }

        // Project to ViewModel
        Customers = query
            .Select(c => new CustomerListViewModel
            {
                Id = c.Id,
                CompanyName = c.CompanyName,
                ContactName = c.ContactName,
                City = c.City,
                Country = c.Country,
                Phone = c.Phone,
                OrderCount = c.Orders.Count
            })
            .OrderBy(c => c.CompanyName)
            .ToList();

        // Get distinct countries for filter dropdown
        Countries = (await _customerRepository.GetDistinctCountriesAsync()).ToList();
    }
}