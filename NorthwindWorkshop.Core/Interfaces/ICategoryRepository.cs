using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Core.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetCategoriesWithProductsAsync();
    Task<IEnumerable<Category>> GetCategoriesWithProductCountAsync();
    Task<Category?> GetCategoryWithProductsAsync(int categoryId);
}