using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.Repairs;

/// <summary>
/// 
/// </summary>
public interface IRepairRepository : IRepository<Repair, Guid>
{
}
