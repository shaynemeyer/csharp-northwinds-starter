using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWorkshop.Web.Pages.Customers;

/// <summary>
/// Page model for creating new customers
/// Demonstrates: MVVM pattern, Dependency Injection, Model Binding, Validation
/// </summary>
public class CreateModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public CreateModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [BindProperty]
    public CustomerCreateViewModel Customer { get; set; } = new();

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
            var customer = new Customer
            {
                CompanyName = Customer.CompanyName,
                ContactName = Customer.ContactName,
                ContactTitle = Customer.ContactTitle,
                Address = Customer.Address,
                City = Customer.City,
                Region = Customer.Region,
                PostalCode = Customer.PostalCode,
                Country = Customer.Country,
                Phone = Customer.Phone,
                Fax = Customer.Fax
            };

            await _customerRepository.AddAsync(customer);

            TempData["SuccessMessage"] = $"Customer '{customer.CompanyName}' has been created successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while creating the customer. Please try again.");
            return Page();
        }
    }
}

/// <summary>
/// View model for customer creation form
/// Demonstrates: Data Transfer Object pattern, Validation attributes
/// </summary>
public class CustomerCreateViewModel
{
    [Required(ErrorMessage = "Company name is required")]
    [StringLength(40, ErrorMessage = "Company name cannot exceed 40 characters")]
    [Display(Name = "Company Name")]
    public string CompanyName { get; set; } = string.Empty;

    [StringLength(30, ErrorMessage = "Contact name cannot exceed 30 characters")]
    [Display(Name = "Contact Name")]
    public string? ContactName { get; set; }

    [StringLength(30, ErrorMessage = "Contact title cannot exceed 30 characters")]
    [Display(Name = "Contact Title")]
    public string? ContactTitle { get; set; }

    [StringLength(60, ErrorMessage = "Address cannot exceed 60 characters")]
    public string? Address { get; set; }

    [StringLength(15, ErrorMessage = "City cannot exceed 15 characters")]
    public string? City { get; set; }

    [StringLength(15, ErrorMessage = "Region cannot exceed 15 characters")]
    public string? Region { get; set; }

    [StringLength(10, ErrorMessage = "Postal code cannot exceed 10 characters")]
    [Display(Name = "Postal Code")]
    public string? PostalCode { get; set; }

    [StringLength(15, ErrorMessage = "Country cannot exceed 15 characters")]
    public string? Country { get; set; }

    [StringLength(24, ErrorMessage = "Phone cannot exceed 24 characters")]
    public string? Phone { get; set; }

    [StringLength(24, ErrorMessage = "Fax cannot exceed 24 characters")]
    public string? Fax { get; set; }
}