using System.Linq.Expressions;

namespace NorthwindWorkshop.Core.Interfaces;

/// <summary>
/// Generic repository interface
/// Demonstrates: OOP Abstraction, Generics, SOLID (Interface Segregation)
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IRepository<T> where T : class
{
    // Query methods
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    // Command methods
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);

    // Check existence
    Task<bool> ExistsAsync(int id);

    // Count
    Task<int> CountAsync();
}