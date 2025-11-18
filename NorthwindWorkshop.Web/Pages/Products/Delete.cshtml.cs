using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;

namespace NorthwindWorkshop.Web.Pages.Products;

/// <summary>
/// Page model for deleting products with confirmation
/// Demonstrates: MVVM pattern, Dependency Injection, Safe Delete Operations, User Confirmation
/// </summary>
public class DeleteModel : PageModel
{
    private readonly IProductRepository _productRepository;

    public DeleteModel(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Product? Product { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        // Load product with order details to show relationship information
        Product = await _productRepository.GetProductWithOrderDetailsAsync(id);

        if (Product == null)
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
            // Check if product exists before attempting to delete
            var productExists = await _productRepository.ExistsAsync(id);
            if (!productExists)
            {
                return NotFound();
            }

            // Get product details for success message
            var product = await _productRepository.GetByIdAsync(id);
            var productName = product?.ProductName ?? "Unknown Product";

            // Perform the delete operation
            await _productRepository.DeleteAsync(id);

            TempData["SuccessMessage"] = $"Product '{productName}' has been deleted successfully.";
            return RedirectToPage("./Index");
        }
        catch (Exception)
        {
            // Handle potential foreign key constraints or other database errors
            TempData["ErrorMessage"] = "Unable to delete product. This product may have existing orders or other related data that must be removed first.";
            return RedirectToPage("./Index");
        }
    }
}