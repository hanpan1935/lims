using System;
using Volo.Abp.Domain.Repositories;

namespace Lanpuda.Lims.Equipments;

/// <summary>
/// 
/// </summary>
public interface IEquipmentRepository : IRepository<Equipment, Guid>
{
}
