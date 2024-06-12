using System;
using System.ComponentModel;

namespace Lanpuda.Lims.Locations.Dtos;

[Serializable]
public class LocationCreateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("LocationWarehouseId")]
    public Guid WarehouseId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("LocationName")]
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("LocationRemark")]
    public string? Remark { get; set; }
}