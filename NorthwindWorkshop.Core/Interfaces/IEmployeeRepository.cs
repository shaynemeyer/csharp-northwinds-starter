using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Core.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<IEnumerable<Employee>> GetEmployeesByManagerAsync(int managerId);
    Task<IEnumerable<Employee>> GetEmployeesWithOrdersAsync();
}