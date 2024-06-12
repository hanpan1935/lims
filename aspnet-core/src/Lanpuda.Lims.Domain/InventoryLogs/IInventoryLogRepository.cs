using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.InventoryLogs;

/// <summary>
/// 库存流水
/// </summary>
public interface IInventoryLogRepository : IRepository<InventoryLog, Guid>
{
}
