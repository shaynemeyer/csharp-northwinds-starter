using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;

namespace NorthwindWorkshop.Infrastructure.Repositories;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(NorthwindDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Supplier>> GetSuppliersWithProductsAsync()
    {
        return await _dbSet
            .Include(s => s.Products)
            .OrderBy(s => s.CompanyName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Supplier>> GetSuppliersWithProductCountAsync()
    {
        return await _dbSet
            .Include(s => s.Products)
            .OrderBy(s => s.CompanyName)
            .ToListAsync();
    }

    public async Task<Supplier?> GetSupplierWithProductsAsync(int supplierId)
    {
        return await _dbSet
            .Include(s => s.Products)
                .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(s => s.Id == supplierId);
    }
}