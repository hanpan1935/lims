using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lanpuda.Lims.InventoryOuts.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class InventoryOutUpdateDto
{
   
    [DisplayName("InventoryOutReason")]
    public string? Reason { get; set; }

    public string? Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryOutDetails")]
    public List<InventoryOutDetailUpdateDto> Details { get; set; }


    public InventoryOutUpdateDto()
    {
        this.Details = new List<InventoryOutDetailUpdateDto>();
    }   
}