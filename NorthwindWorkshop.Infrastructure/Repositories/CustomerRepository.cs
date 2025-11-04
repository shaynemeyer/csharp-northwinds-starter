using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;

namespace NorthwindWorkshop.Infrastructure.Repositories;

/// <summary>
/// Customer repository with domain-specific queries
/// Demonstrates: Method override, LINQ queries, Eager loading
/// </summary>
public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(NorthwindDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Customer>> GetCustomersByCountryAsync(string country)
    {
        return await _dbSet
            .Where(c => c.Country == country)
            .OrderBy(c => c.CompanyName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync()
    {
        return await _dbSet
            .Include(c => c.Orders)
            .Where(c => c.Orders.Any())
            .ToListAsync();
    }

    public async Task<Customer?> GetCustomerWithOrdersAsync(int customerId)
    {
        return await _dbSet
            .Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(c => c.Id == customerId);
    }

    public async Task<IEnumerable<string>> GetDistinctCountriesAsync()
    {
        return await _dbSet
            .Select(c => c.Country)
            .OfType<string>()
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }
}