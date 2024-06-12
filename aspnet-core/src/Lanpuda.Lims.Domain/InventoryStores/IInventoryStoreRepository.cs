using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.InventoryStores;

/// <summary>
/// 
/// </summary>
public interface IInventoryStoreRepository : IRepository<InventoryStore, Guid>
{
}
