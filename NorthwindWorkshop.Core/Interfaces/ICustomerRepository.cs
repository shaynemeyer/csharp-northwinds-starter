using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Core.Interfaces;

/// <summary>
/// Customer-specific repository operations
/// Extends the generic repository with domain-specific methods
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetCustomersByCountryAsync(string country);
    Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync();
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerWithOrdersAsync(int customerId);
    Task<IEnumerable<string>> GetDistinctCountriesAsync();
}