using Lanpuda.Lims.Locations.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lanpuda.Lims.Warehouses.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class WarehouseUpdateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("WarehouseName")]
    public string Name { get; set; }

 
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("WarehouseRemark")]
    public string? Remark { get; set; }

}