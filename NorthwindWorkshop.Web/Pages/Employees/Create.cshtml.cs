using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWorkshop.Web.Pages.Employees;

/// <summary>
/// Page model for creating new employees
/// Demonstrates: MVVM pattern, Dependency Injection, Model Binding, Validation, Self-referencing relationships
/// </summary>
public class CreateModel : PageModel
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateModel(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [BindProperty]
    public EmployeeCreateViewModel Employee { get; set; } = new();

    public List<SelectListItem> Managers { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        // Load existing employees for manager selection
        await LoadManagersAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Reload managers if validation fails
            await LoadManagersAsync();
            return Page();
        }

        try
        {
            var employee = new Employee
            {
                FirstName = Employee.FirstName,
                LastName = Employee.LastName,
                Title = Employee.Title,
                TitleOfCourtesy = Employee.TitleOfCourtesy,
                BirthDate = Employee.BirthDate,
                HireDate = Employee.HireDate ?? DateTime.Today,
                Address = Employee.Address,
                City = Employee.City,
                Region = Employee.Region,
                PostalCode = Employee.PostalCode,
                Country = Employee.Country,
                HomePhone = Employee.HomePhone,
                Extension = Employee.Extension,
                Notes = Employee.Notes,
                ReportsTo = Employee.ReportsTo
            };

            await _employeeRepository.AddAsync(employee);

            TempData["SuccessMessage"] = $"Employee '{employee.FullName}' has been created successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while creating the employee. Please try again.");
            await LoadManagersAsync();
            return Page();
        }
    }

    private async Task LoadManagersAsync()
    {
        var allEmployees = await _employeeRepository.GetAllAsync();
        Managers = new List<SelectListItem>
        {
            new() { Value = "", Text = "-- No Manager --" }
        };
        Managers.AddRange(
            allEmployees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.FullName
            })
        );
    }
}

/// <summary>
/// View model for employee creation form
/// Demonstrates: Data Transfer Object pattern, Validation attributes, Self-referencing relationships
/// </summary>
public class EmployeeCreateViewModel
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(20, ErrorMessage = "Last name cannot exceed 20 characters")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [StringLength(30, ErrorMessage = "Title cannot exceed 30 characters")]
    public string? Title { get; set; }

    [StringLength(25, ErrorMessage = "Title of courtesy cannot exceed 25 characters")]
    [Display(Name = "Title of Courtesy")]
    public string? TitleOfCourtesy { get; set; }

    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [Display(Name = "Hire Date")]
    [DataType(DataType.Date)]
    public DateTime? HireDate { get; set; }

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

    [StringLength(24, ErrorMessage = "Home phone cannot exceed 24 characters")]
    [Display(Name = "Home Phone")]
    public string? HomePhone { get; set; }

    [StringLength(4, ErrorMessage = "Extension cannot exceed 4 characters")]
    public string? Extension { get; set; }

    [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
    public string? Notes { get; set; }

    [Display(Name = "Manager")]
    public int? ReportsTo { get; set; }
}