using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Core.Interfaces;

public interface IShipperRepository : IRepository<Shipper>
{
    Task<IEnumerable<Shipper>> GetShippersWithOrdersAsync();
    Task<IEnumerable<Shipper>> GetActiveShippersAsync();
    Task<Shipper?> GetShipperWithOrdersAsync(int shipperId);
}