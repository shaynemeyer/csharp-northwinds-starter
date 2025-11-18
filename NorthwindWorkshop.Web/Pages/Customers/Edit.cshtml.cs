using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWorkshop.Web.Pages.Customers;

/// <summary>
/// Page model for editing existing customers
/// Demonstrates: MVVM pattern, Dependency Injection, Model Binding, Validation, Entity Updates
/// </summary>
public class EditModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;

    public EditModel(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [BindProperty]
    public CustomerEditViewModel Customer { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        // Map entity to view model
        Customer = new CustomerEditViewModel
        {
            Id = customer.Id,
            CompanyName = customer.CompanyName,
            ContactName = customer.ContactName,
            ContactTitle = customer.ContactTitle,
            Address = customer.Address,
            City = customer.City,
            Region = customer.Region,
            PostalCode = customer.PostalCode,
            Country = customer.Country,
            Phone = customer.Phone,
            Fax = customer.Fax
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
            // Get the existing customer from database
            var existingCustomer = await _customerRepository.GetByIdAsync(Customer.Id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Update the existing customer with form data
            existingCustomer.CompanyName = Customer.CompanyName;
            existingCustomer.ContactName = Customer.ContactName;
            existingCustomer.ContactTitle = Customer.ContactTitle;
            existingCustomer.Address = Customer.Address;
            existingCustomer.City = Customer.City;
            existingCustomer.Region = Customer.Region;
            existingCustomer.PostalCode = Customer.PostalCode;
            existingCustomer.Country = Customer.Country;
            existingCustomer.Phone = Customer.Phone;
            existingCustomer.Fax = Customer.Fax;

            await _customerRepository.UpdateAsync(existingCustomer);

            TempData["SuccessMessage"] = $"Customer '{existingCustomer.CompanyName}' has been updated successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while updating the customer. Please try again.");
            return Page();
        }
    }
}

/// <summary>
/// View model for customer editing form
/// Demonstrates: Data Transfer Object pattern, Validation attributes, Entity mapping
/// </summary>
public class CustomerEditViewModel
{
    public int Id { get; set; }

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