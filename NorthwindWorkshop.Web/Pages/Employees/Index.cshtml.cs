using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Web.ViewModels;

namespace NorthwindWorkshop.Web.Pages.Employees;

public class IndexModel : PageModel
{
    private readonly IEmployeeRepository _employeeRepository;

    public IndexModel(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public List<EmployeeListViewModel> Employees { get; set; } = new();
    public string? SearchTerm { get; set; }
    public bool ShowWithOrders { get; set; }

    public async Task OnGetAsync(string? searchTerm, bool showWithOrders = false)
    {
        SearchTerm = searchTerm;
        ShowWithOrders = showWithOrders;

        var employees = await _employeeRepository.GetEmployeesWithOrdersAsync();

        // Apply filters
        var query = employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(e =>
                e.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                e.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (e.Title != null && e.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        }

        if (showWithOrders)
        {
            query = query.Where(e => e.Orders.Any());
        }

        // Project to ViewModel
        Employees = query
            .Select(e => new EmployeeListViewModel
            {
                Id = e.Id,
                FullName = e.FullName,
                Title = e.Title,
                City = e.City,
                Country = e.Country,
                HireDate = e.HireDate,
                ManagerName = e.Manager != null ? e.Manager.FullName : null,
                OrderCount = e.Orders.Count()
            })
            .OrderBy(e => e.FullName)
            .ToList();
    }
}