using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.Locations;

/// <summary>
/// 
/// </summary>
public interface ILocationRepository : IRepository<Location, Guid>
{
}
