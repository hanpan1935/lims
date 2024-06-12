using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lanpuda.Lims.InventoryStores.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class InventoryStoreUpdateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryStoreReason")]
    public string? Reason { get; set; }


    public string? Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryStoreDetails")]
    public List<InventoryStoreDetailUpdateDto> Details { get; set; }


    public InventoryStoreUpdateDto()
    {
        Details = new List<InventoryStoreDetailUpdateDto>();
    }
}