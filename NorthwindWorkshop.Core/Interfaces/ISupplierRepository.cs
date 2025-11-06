using NorthwindWorkshop.Core.Entities;

namespace NorthwindWorkshop.Core.Interfaces;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<IEnumerable<Supplier>> GetSuppliersWithProductsAsync();
    Task<IEnumerable<Supplier>> GetSuppliersWithProductCountAsync();
    Task<Supplier?> GetSupplierWithProductsAsync(int supplierId);
}