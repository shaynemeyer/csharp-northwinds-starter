using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;

namespace NorthwindWorkshop.Web.Pages.Shippers;

/// <summary>
/// Page model for deleting shippers with confirmation
/// Demonstrates: MVVM pattern, Dependency Injection, Safe Delete Operations, User Confirmation
/// </summary>
public class DeleteModel : PageModel
{
    private readonly IShipperRepository _shipperRepository;

    public DeleteModel(IShipperRepository shipperRepository)
    {
        _shipperRepository = shipperRepository;
    }

    public Shipper? Shipper { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        // Load shipper with orders to show relationship information
        Shipper = await _shipperRepository.GetShipperWithOrdersAsync(id);

        if (Shipper == null)
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
            // Check if shipper exists before attempting to delete
            var shipperExists = await _shipperRepository.ExistsAsync(id);
            if (!shipperExists)
            {
                return NotFound();
            }

            // Get shipper details for success message
            var shipper = await _shipperRepository.GetByIdAsync(id);
            var shipperName = shipper?.CompanyName ?? "Unknown Shipper";

            // Perform the delete operation
            await _shipperRepository.DeleteAsync(id);

            TempData["SuccessMessage"] = $"Shipper '{shipperName}' has been deleted successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            // Handle potential foreign key constraints or other database errors
            TempData["ErrorMessage"] = "Unable to delete shipper. This shipper may have existing orders or other related data that must be removed first.";
            return RedirectToPage("./Index");
        }
    }
}