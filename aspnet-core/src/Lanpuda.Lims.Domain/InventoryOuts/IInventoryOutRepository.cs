using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.InventoryOuts;

/// <summary>
/// 
/// </summary>
public interface IInventoryOutRepository : IRepository<InventoryOut, Guid>
{
}
