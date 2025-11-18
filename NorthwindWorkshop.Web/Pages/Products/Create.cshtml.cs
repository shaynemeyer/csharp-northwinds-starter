using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NorthwindWorkshop.Web.Pages.Products;

/// <summary>
/// Page model for creating new products
/// Demonstrates: MVVM pattern, Dependency Injection, Model Binding, Validation
/// </summary>
public class CreateModel : PageModel
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISupplierRepository _supplierRepository;

    public CreateModel(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ISupplierRepository supplierRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _supplierRepository = supplierRepository;
    }

    [BindProperty]
    public ProductCreateViewModel Product { get; set; } = new();

    public SelectList Categories { get; set; } = new(new List<Category>(), "Id", "CategoryName");
    public SelectList Suppliers { get; set; } = new(new List<Supplier>(), "Id", "CompanyName");

    public async Task<IActionResult> OnGetAsync()
    {
        await LoadDropdownDataAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownDataAsync();
            return Page();
        }

        try
        {
            var product = new Product
            {
                ProductName = Product.ProductName,
                CategoryId = Product.CategoryId,
                SupplierId = Product.SupplierId,
                QuantityPerUnit = Product.QuantityPerUnit,
                UnitPrice = Product.UnitPrice,
                UnitsInStock = Product.UnitsInStock,
                UnitsOnOrder = Product.UnitsOnOrder,
                ReorderLevel = Product.ReorderLevel,
                Discontinued = Product.Discontinued
            };

            await _productRepository.AddAsync(product);

            TempData["SuccessMessage"] = $"Product '{product.ProductName}' has been created successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while creating the product. Please try again.");
            await LoadDropdownDataAsync();
            return Page();
        }
    }

    private async Task LoadDropdownDataAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var suppliers = await _supplierRepository.GetAllAsync();

        Categories = new SelectList(categories.OrderBy(c => c.CategoryName), "Id", "CategoryName");
        Suppliers = new SelectList(suppliers.OrderBy(s => s.CompanyName), "Id", "CompanyName");
    }
}

/// <summary>
/// View model for product creation form
/// Demonstrates: Data Transfer Object pattern, Validation attributes
/// </summary>
public class ProductCreateViewModel
{
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(40, ErrorMessage = "Product name cannot exceed 40 characters")]
    [Display(Name = "Product Name")]
    public string ProductName { get; set; } = string.Empty;

    [Display(Name = "Category")]
    public int? CategoryId { get; set; }

    [Display(Name = "Supplier")]
    public int? SupplierId { get; set; }

    [StringLength(20, ErrorMessage = "Quantity per unit cannot exceed 20 characters")]
    [Display(Name = "Quantity Per Unit")]
    public string? QuantityPerUnit { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Unit price must be a positive number")]
    [Display(Name = "Unit Price")]
    public decimal? UnitPrice { get; set; }

    [Range(0, short.MaxValue, ErrorMessage = "Units in stock must be a positive number")]
    [Display(Name = "Units In Stock")]
    public short? UnitsInStock { get; set; }

    [Range(0, short.MaxValue, ErrorMessage = "Units on order must be a positive number")]
    [Display(Name = "Units On Order")]
    public short? UnitsOnOrder { get; set; }

    [Range(0, short.MaxValue, ErrorMessage = "Reorder level must be a positive number")]
    [Display(Name = "Reorder Level")]
    public short? ReorderLevel { get; set; }

    public bool Discontinued { get; set; }
}