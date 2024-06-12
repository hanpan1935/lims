using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Locations.Dtos;


[Serializable]
public class LocationDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public Guid WarehouseId { get; set; }

    public string WarehouseName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }

    public LocationDto()
    {
        WarehouseName = string.Empty;
        Name = string.Empty;
    }
}