using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.Warehouses;

/// <summary>
/// 
/// </summary>
public interface IWarehouseRepository : IRepository<Warehouse, Guid>
{
}
