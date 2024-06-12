using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.InspectionItems;

/// <summary>
/// 
/// </summary>
public interface IInspectionItemRepository : IRepository<InspectionItem, Guid>
{
}
