using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;

namespace NorthwindWorkshop.Web.Pages.Customers;

/// <summary>
/// Page model for deleting customers with confirmation
/// Demonstrates: MVVM pattern, Dependency Injection, Safe Delete Operations, User Confirmation
/// </summary>
public class DeleteModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteModel(ICustomerRepository customerRepository)
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

        // Load customer with orders to show relationship information
        Customer = await _customerRepository.GetCustomerWithOrdersAsync(id);

        if (Customer == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        try
        {
            // Check if customer exists before attempting to delete
            var customerExists = await _customerRepository.ExistsAsync(id);
            if (!customerExists)
            {
                return NotFound();
            }

            // Get customer details for success message
            var customer = await _customerRepository.GetByIdAsync(id);
            var customerName = customer?.CompanyName ?? "Unknown Customer";

            // Perform the delete operation
            await _customerRepository.DeleteAsync(id);

            TempData["SuccessMessage"] = $"Customer '{customerName}' has been deleted successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            // Handle potential foreign key constraints or other database errors
            TempData["ErrorMessage"] = "Unable to delete customer. This customer may have existing orders or other related data that must be removed first.";
            return RedirectToPage("./Index");
        }
    }
}