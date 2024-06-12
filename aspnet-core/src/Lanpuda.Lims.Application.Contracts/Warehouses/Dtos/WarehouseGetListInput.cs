using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Warehouses.Dtos;

[Serializable]
public class WarehouseGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("WarehouseName")]
    public string? Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("WarehouseNumber")]
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("WarehouseRemark")]
    public string? Remark { get; set; }

}