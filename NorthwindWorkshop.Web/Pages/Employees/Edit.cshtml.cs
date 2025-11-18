using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWorkshop.Web.Pages.Employees;

/// <summary>
/// Page model for editing existing employees
/// Demonstrates: MVVM pattern, Dependency Injection, Model Binding, Validation, Self-referencing relationships
/// </summary>
public class EditModel : PageModel
{
    private readonly IEmployeeRepository _employeeRepository;

    public EditModel(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [BindProperty]
    public EmployeeEditViewModel Employee { get; set; } = new();

    public List<SelectListItem> Managers { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _employeeRepository.GetByIdAsync(id.Value);
        if (employee == null)
        {
            return NotFound();
        }

        // Map entity to view model
        Employee = new EmployeeEditViewModel
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Title = employee.Title,
            TitleOfCourtesy = employee.TitleOfCourtesy,
            BirthDate = employee.BirthDate,
            HireDate = employee.HireDate,
            Address = employee.Address,
            City = employee.City,
            Region = employee.Region,
            PostalCode = employee.PostalCode,
            Country = employee.Country,
            HomePhone = employee.HomePhone,
            Extension = employee.Extension,
            Notes = employee.Notes,
            ReportsTo = employee.ReportsTo
        };

        // Load managers (excluding self)
        await LoadManagersAsync(id.Value);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Reload managers if validation fails
            await LoadManagersAsync(Employee.Id);
            return Page();
        }

        try
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(Employee.Id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            // Update employee properties
            existingEmployee.FirstName = Employee.FirstName;
            existingEmployee.LastName = Employee.LastName;
            existingEmployee.Title = Employee.Title;
            existingEmployee.TitleOfCourtesy = Employee.TitleOfCourtesy;
            existingEmployee.BirthDate = Employee.BirthDate;
            existingEmployee.HireDate = Employee.HireDate ?? DateTime.Today;
            existingEmployee.Address = Employee.Address;
            existingEmployee.City = Employee.City;
            existingEmployee.Region = Employee.Region;
            existingEmployee.PostalCode = Employee.PostalCode;
            existingEmployee.Country = Employee.Country;
            existingEmployee.HomePhone = Employee.HomePhone;
            existingEmployee.Extension = Employee.Extension;
            existingEmployee.Notes = Employee.Notes;
            existingEmployee.ReportsTo = Employee.ReportsTo;

            await _employeeRepository.UpdateAsync(existingEmployee);

            TempData["SuccessMessage"] = $"Employee '{existingEmployee.FullName}' has been updated successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while updating the employee. Please try again.");
            await LoadManagersAsync(Employee.Id);
            return Page();
        }
    }

    private async Task LoadManagersAsync(int employeeId)
    {
        var allEmployees = await _employeeRepository.GetAllAsync();
        Managers = new List<SelectListItem>
        {
            new() { Value = "", Text = "-- No Manager --" }
        };

        // Exclude the current employee from manager selection (can't manage themselves)
        Managers.AddRange(
            allEmployees
                .Where(e => e.Id != employeeId)
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName
                })
        );
    }
}

/// <summary>
/// View model for employee editing form
/// Demonstrates: Data Transfer Object pattern, Validation attributes, Self-referencing relationships
/// </summary>
public class EmployeeEditViewModel
{
    public int Id { get; set; }

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