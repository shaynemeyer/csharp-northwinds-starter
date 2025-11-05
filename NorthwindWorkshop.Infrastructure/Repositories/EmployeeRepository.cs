using Microsoft.EntityFrameworkCore;
using NorthwindWorkshop.Core.Entities;
using NorthwindWorkshop.Core.Interfaces;
using NorthwindWorkshop.Infrastructure.Data;

namespace NorthwindWorkshop.Infrastructure.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(NorthwindDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Employee>> GetEmployeesByManagerAsync(int managerId)
    {
        return await _dbSet
            .Where(e => e.ReportsTo == managerId)
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetEmployeesWithOrdersAsync()
    {
        return await _dbSet
            .Include(e => e.Orders)
            .Include(e => e.Manager)
            .OrderBy(e => e.LastName)
            .ToListAsync();
    }
}