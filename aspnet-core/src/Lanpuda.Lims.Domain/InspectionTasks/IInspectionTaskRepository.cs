using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.InspectionTasks;

/// <summary>
/// 
/// </summary>
public interface IInspectionTaskRepository : IRepository<InspectionTask, Guid>
{
}
