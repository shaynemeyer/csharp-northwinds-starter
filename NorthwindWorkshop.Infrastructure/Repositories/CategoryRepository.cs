using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;

namespace NorthwindWorkshop.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(NorthwindDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> GetCategoriesWithProductsAsync()
    {
        return await _dbSet
            .Include(c => c.Products)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetCategoriesWithProductCountAsync()
    {
        return await _dbSet
            .Include(c => c.Products.Where(p => !p.Discontinued))
            .OrderBy(c => c.CategoryName)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryWithProductsAsync(int categoryId)
    {
        return await _dbSet
            .Include(c => c.Products)
                .ThenInclude(p => p.Supplier)
            .FirstOrDefaultAsync(c => c.Id == categoryId);
    }
}