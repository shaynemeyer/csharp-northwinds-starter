using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;

namespace NorthwindWorkshop.Web.Pages.Customers;

/// <summary>
/// Page model for customer details
/// Demonstrates: MVVM pattern, Dependency Injection, Route parameters
/// </summary>
public class DetailsModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public DetailsModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Customer? Customer { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        Customer = await _customerRepository.GetCustomerWithOrdersAsync(id);

        if (Customer == null)
        {
            return NotFound();
        }

        return Page();
    }
}