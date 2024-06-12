using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.Samples;

/// <summary>
/// 
/// </summary>
public interface ISampleRepository : IRepository<Sample, Guid>
{
}
