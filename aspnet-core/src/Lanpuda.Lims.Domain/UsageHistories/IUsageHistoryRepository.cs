using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.UsageHistories;

/// <summary>
/// 
/// </summary>
public interface IUsageHistoryRepository : IRepository<UsageHistory, Guid>
{
}
