using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.Maintenances;

/// <summary>
/// 
/// </summary>
public interface IMaintenanceRepository : IRepository<Maintenance, Guid>
{
}
