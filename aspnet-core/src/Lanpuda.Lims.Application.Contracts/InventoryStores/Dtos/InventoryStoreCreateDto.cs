using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lanpuda.Lims.InventoryStores.Dtos;

[Serializable]
public class InventoryStoreCreateDto
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
    public List<InventoryStoreDetailCreateDto> Details { get; set; }


    public InventoryStoreCreateDto()
    {
        Details = new List<InventoryStoreDetailCreateDto>();
    }
}