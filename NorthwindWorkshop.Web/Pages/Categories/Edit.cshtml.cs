using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWorkshop.Web.Pages.Categories;

/// <summary>
/// Page model for editing existing categories
/// Demonstrates: MVVM pattern, Dependency Injection, Model Binding, Validation, Entity Updates
/// </summary>
public class EditModel : PageModel
{
    private readonly ICategoryRepository _categoryRepository;

    public EditModel(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [BindProperty]
    public CategoryEditViewModel Category { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        // Map entity to view model
        Category = new CategoryEditViewModel
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            Description = category.Description
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
            // Get the existing category from database
            var existingCategory = await _categoryRepository.GetByIdAsync(Category.Id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            // Update the existing category with form data
            existingCategory.CategoryName = Category.CategoryName;
            existingCategory.Description = Category.Description;

            await _categoryRepository.UpdateAsync(existingCategory);

            TempData["SuccessMessage"] = $"Category '{existingCategory.CategoryName}' has been updated successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while updating the category. Please try again.");
            return Page();
        }
    }
}

/// <summary>
/// View model for category editing form
/// Demonstrates: Data Transfer Object pattern, Validation attributes, Entity mapping
/// </summary>
public class CategoryEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [StringLength(15, ErrorMessage = "Category name cannot exceed 15 characters")]
    [Display(Name = "Category Name")]
    public string CategoryName { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [Display(Name = "Description")]
    public string? Description { get; set; }
}