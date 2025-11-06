using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Core.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
    Task<IEnumerable<Order>> GetOrdersByEmployeeAsync(int employeeId);
    Task<IEnumerable<Order>> GetRecentOrdersAsync(int daysBack = 30);
    Task<IEnumerable<Order>> GetOrdersWithDetailsAsync();
    Task<IEnumerable<Order>> GetPendingOrdersAsync();
    Task<IEnumerable<Order>> GetShippedOrdersAsync();
}