using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWorkshop.Web.Pages.Categories;

/// <summary>
/// Page model for creating new categories
/// Demonstrates: MVVM pattern, Dependency Injection, Model Binding, Validation
/// </summary>
public class CreateModel : PageModel
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateModel(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [BindProperty]
    public CategoryCreateViewModel Category { get; set; } = new();

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
            var category = new Category
            {
                CategoryName = Category.CategoryName,
                Description = Category.Description
            };

            await _categoryRepository.AddAsync(category);

            TempData["SuccessMessage"] = $"Category '{category.CategoryName}' has been created successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while creating the category. Please try again.");
            return Page();
        }
    }
}

/// <summary>
/// View model for category creation form
/// Demonstrates: Data Transfer Object pattern, Validation attributes
/// </summary>
public class CategoryCreateViewModel
{
    [Required(ErrorMessage = "Category name is required")]
    [StringLength(15, ErrorMessage = "Category name cannot exceed 15 characters")]
    [Display(Name = "Category Name")]
    public string CategoryName { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [Display(Name = "Description")]
    public string? Description { get; set; }
}