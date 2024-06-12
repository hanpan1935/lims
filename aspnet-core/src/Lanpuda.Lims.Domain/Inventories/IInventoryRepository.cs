using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.Inventories;

/// <summary>
/// 
/// </summary>
public interface IInventoryRepository : IRepository<Inventory, Guid>
{

    Task<Inventory> FindExistingAsync(Guid productId, Guid locationId, string lotNumber);

    Task<double> GetSumQuantityByProductIdAsync(Guid productId);

    Task<Inventory> StorageAsync(Guid locationId, Guid productId, double quantity, string? lotNumber);

    Task<double> OutAsync(Guid locationId, Guid productId, double quantity, string? lotNumber);
}
