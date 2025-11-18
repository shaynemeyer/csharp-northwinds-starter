using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWorkshop.Web.Pages.Shippers;

/// <summary>
/// Page model for creating new shippers
/// Demonstrates: MVVM pattern, Dependency Injection, Model Binding, Validation
/// </summary>
public class CreateModel : PageModel
{
    private readonly IShipperRepository _shipperRepository;

    public CreateModel(IShipperRepository shipperRepository)
    {
        _shipperRepository = shipperRepository;
    }

    [BindProperty]
    public ShipperCreateViewModel Shipper { get; set; } = new();

    public IActionResult OnGet()
    {
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
            var shipper = new Shipper
            {
                CompanyName = Shipper.CompanyName,
                Phone = Shipper.Phone
            };

            await _shipperRepository.AddAsync(shipper);

            TempData["SuccessMessage"] = $"Shipper '{shipper.CompanyName}' has been created successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while creating the shipper. Please try again.");
            return Page();
        }
    }
}

/// <summary>
/// View model for shipper creation form
/// Demonstrates: Data Transfer Object pattern, Validation attributes
/// </summary>
public class ShipperCreateViewModel
{
    [Required(ErrorMessage = "Company name is required")]
    [StringLength(40, ErrorMessage = "Company name cannot exceed 40 characters")]
    [Display(Name = "Company Name")]
    public string CompanyName { get; set; } = string.Empty;

    [StringLength(24, ErrorMessage = "Phone cannot exceed 24 characters")]
    public string? Phone { get; set; }
}