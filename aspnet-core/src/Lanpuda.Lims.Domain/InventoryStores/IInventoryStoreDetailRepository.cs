using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.InventoryStores;

/// <summary>
/// 
/// </summary>
public interface IInventoryStoreDetailRepository : IRepository<InventoryStoreDetail, Guid>
{
}
