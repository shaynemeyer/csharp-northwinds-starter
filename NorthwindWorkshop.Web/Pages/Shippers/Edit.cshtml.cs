using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWorkshop.Web.Pages.Shippers;

/// <summary>
/// Page model for editing existing shippers
/// Demonstrates: MVVM pattern, Dependency Injection, Model Binding, Validation, Entity Updates
/// </summary>
public class EditModel : PageModel
{
    private readonly IShipperRepository _shipperRepository;

    public EditModel(IShipperRepository shipperRepository)
    {
        _shipperRepository = shipperRepository;
    }

    [BindProperty]
    public ShipperEditViewModel Shipper { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var shipper = await _shipperRepository.GetByIdAsync(id);
        if (shipper == null)
        {
            return NotFound();
        }

        // Map entity to view model
        Shipper = new ShipperEditViewModel
        {
            Id = shipper.Id,
            CompanyName = shipper.CompanyName,
            Phone = shipper.Phone
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            // Get the existing shipper from database
            var existingShipper = await _shipperRepository.GetByIdAsync(Shipper.Id);
            if (existingShipper == null)
            {
                return NotFound();
            }

            // Update the existing shipper with form data
            existingShipper.CompanyName = Shipper.CompanyName;
            existingShipper.Phone = Shipper.Phone;

            await _shipperRepository.UpdateAsync(existingShipper);

            TempData["SuccessMessage"] = $"Shipper '{existingShipper.CompanyName}' has been updated successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while updating the shipper. Please try again.");
            return Page();
        }
    }
}

/// <summary>
/// View model for shipper editing form
/// Demonstrates: Data Transfer Object pattern, Validation attributes, Entity mapping
/// </summary>
public class ShipperEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Company name is required")]
    [StringLength(40, ErrorMessage = "Company name cannot exceed 40 characters")]
    [Display(Name = "Company Name")]
    public string CompanyName { get; set; } = string.Empty;

    [StringLength(24, ErrorMessage = "Phone cannot exceed 24 characters")]
    public string? Phone { get; set; }
}