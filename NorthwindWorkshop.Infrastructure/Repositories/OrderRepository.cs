using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;

namespace NorthwindWorkshop.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(NorthwindDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
    {
        return await _dbSet
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.Customer)
            .Include(o => o.Employee)
            .Include(o => o.Shipper)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByEmployeeAsync(int employeeId)
    {
        return await _dbSet
            .Where(o => o.EmployeeId == employeeId)
            .Include(o => o.Customer)
            .Include(o => o.Employee)
            .Include(o => o.Shipper)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetRecentOrdersAsync(int daysBack = 30)
    {
        var cutoffDate = DateTime.Now.AddDays(-daysBack);
        return await _dbSet
            .Where(o => o.OrderDate >= cutoffDate)
            .Include(o => o.Customer)
            .Include(o => o.Employee)
            .Include(o => o.Shipper)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersWithDetailsAsync()
    {
        return await _dbSet
            .Include(o => o.Customer)
            .Include(o => o.Employee)
            .Include(o => o.Shipper)
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetPendingOrdersAsync()
    {
        return await _dbSet
            .Where(o => o.ShippedDate == null && o.OrderDate != null)
            .Include(o => o.Customer)
            .Include(o => o.Employee)
            .Include(o => o.Shipper)
            .OrderBy(o => o.RequiredDate ?? o.OrderDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetShippedOrdersAsync()
    {
        return await _dbSet
            .Where(o => o.ShippedDate != null)
            .Include(o => o.Customer)
            .Include(o => o.Employee)
            .Include(o => o.Shipper)
            .OrderByDescending(o => o.ShippedDate)
            .ToListAsync();
    }
}