using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Core.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<IEnumerable<Product>> GetLowStockProductsAsync();
    Task<IEnumerable<Product>> GetDiscontinuedProductsAsync();
    Task<IEnumerable<Product>> GetProductsWithDetailsAsync();
}