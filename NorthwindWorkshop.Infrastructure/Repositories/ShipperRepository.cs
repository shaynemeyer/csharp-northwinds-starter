using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;

namespace NorthwindWorkshop.Infrastructure.Repositories;

public class ShipperRepository : Repository<Shipper>, IShipperRepository
{
    public ShipperRepository(NorthwindDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Shipper>> GetShippersWithOrdersAsync()
    {
        return await _dbSet
            .Include(s => s.Orders)
            .OrderBy(s => s.CompanyName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Shipper>> GetActiveShippersAsync()
    {
        return await _dbSet
            .Where(s => s.Orders.Any())
            .OrderBy(s => s.CompanyName)
            .ToListAsync();
    }

    public async Task<Shipper?> GetShipperWithOrdersAsync(int shipperId)
    {
        return await _dbSet
            .Include(s => s.Orders)
                .ThenInclude(o => o.Customer)
            .FirstOrDefaultAsync(s => s.Id == shipperId);
    }
}