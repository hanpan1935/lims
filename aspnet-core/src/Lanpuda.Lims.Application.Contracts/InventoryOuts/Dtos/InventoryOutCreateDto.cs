using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lanpuda.Lims.InventoryOuts.Dtos;

[Serializable]
public class InventoryOutCreateDto
{
   

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryOutReason")]
    public string? Reason { get; set; }

    public string? Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryOutDetails")]
    public List<InventoryOutDetailCreateDto> Details { get; set; }


    public InventoryOutCreateDto()
    {
        this.Details = new List<InventoryOutDetailCreateDto>();
    }
}