using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;

namespace NorthwindWorkshop.Web.Pages.Categories;

/// <summary>
/// Page model for deleting categories with confirmation
/// Demonstrates: MVVM pattern, Dependency Injection, Safe Delete Operations, User Confirmation
/// </summary>
public class DeleteModel : PageModel
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteModel(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Category? Category { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        // Load category with products to show relationship information
        Category = await _categoryRepository.GetCategoryWithProductsAsync(id);

        if (Category == null)
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
            // Check if category exists before attempting to delete
            var categoryExists = await _categoryRepository.ExistsAsync(id);
            if (!categoryExists)
            {
                return NotFound();
            }

            // Get category details for success message
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryName = category?.CategoryName ?? "Unknown Category";

            // Perform the delete operation
            await _categoryRepository.DeleteAsync(id);

            TempData["SuccessMessage"] = $"Category '{categoryName}' has been deleted successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            // Handle potential foreign key constraints or other database errors
            TempData["ErrorMessage"] = "Unable to delete category. This category may have existing products or other related data that must be removed first.";
            return RedirectToPage("./Index");
        }
    }
}