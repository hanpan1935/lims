using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Locations.Dtos;

[Serializable]
public class LocationGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("LocationWarehouseId")]
    public Guid? WarehouseId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("LocationName")]
    public string? Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("LocationRemark")]
    public string? Remark { get; set; }
}