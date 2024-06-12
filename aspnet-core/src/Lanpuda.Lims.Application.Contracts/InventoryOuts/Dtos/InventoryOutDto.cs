using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.InventoryOuts.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class InventoryOutDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Reason { get; set; }


    public string? Remark { get; set; }

    /// <summary>
    /// 入库状态  false待入库  true已入库
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// 出库时间
    /// </summary>
    public DateTime? SuccessfulTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<InventoryOutDetailDto> Details { get; set; }

    public InventoryOutDto()
    {
        Number = string.Empty;
        Details = new List<InventoryOutDetailDto>();
    }
}