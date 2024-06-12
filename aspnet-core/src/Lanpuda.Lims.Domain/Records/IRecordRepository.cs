using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.Records;

/// <summary>
/// 
/// </summary>
public interface IRecordRepository : IRepository<Record, Guid>
{
}
