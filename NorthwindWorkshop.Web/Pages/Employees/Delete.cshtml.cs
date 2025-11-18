using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;

namespace NorthwindWorkshop.Web.Pages.Employees;

/// <summary>
/// Page model for deleting employees with confirmation
/// Demonstrates: MVVM pattern, Dependency Injection, Safe Delete Operations, Organizational Hierarchy Management
/// </summary>
public class DeleteModel : PageModel
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteModel(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Employee? Employee { get; set; }
    public List<Employee> Subordinates { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        // Load employee details
        Employee = await _employeeRepository.GetByIdAsync(id);

        if (Employee == null)
        {
            return NotFound();
        }

        // Load subordinates (employees who report to this employee)
        Subordinates = (await _employeeRepository.GetEmployeesByManagerAsync(id)).ToList();

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
            // Check if employee exists before attempting to delete
            var employeeExists = await _employeeRepository.ExistsAsync(id);
            if (!employeeExists)
            {
                return NotFound();
            }

            // Get employee details for success message
            var employee = await _employeeRepository.GetByIdAsync(id);
            var employeeName = employee?.FullName ?? "Unknown Employee";

            // Handle subordinates - set their ReportsTo to null (remove manager assignment)
            var subordinates = await _employeeRepository.GetEmployeesByManagerAsync(id);
            foreach (var subordinate in subordinates)
            {
                subordinate.ReportsTo = null;
                await _employeeRepository.UpdateAsync(subordinate);
            }

            // Perform the delete operation
            await _employeeRepository.DeleteAsync(id);

            var subordinateMessage = subordinates.Any()
                ? $" {subordinates.Count()} subordinate employee(s) have been reassigned with no manager."
                : "";

            TempData["SuccessMessage"] = $"Employee '{employeeName}' has been deleted successfully.{subordinateMessage}";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            // Handle potential foreign key constraints or other database errors
            TempData["ErrorMessage"] = "Unable to delete employee. This employee may have existing orders or other related data that must be removed first.";
            return RedirectToPage("./Index");
        }
    }
}