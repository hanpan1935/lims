using Lanpuda.Lims.Locations.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Warehouses.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class WarehouseDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<LocationDto> Locations { get; set; }
}