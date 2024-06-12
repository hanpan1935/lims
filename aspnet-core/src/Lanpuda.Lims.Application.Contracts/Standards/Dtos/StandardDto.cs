using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Standards.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class StandardDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? DicStandardTypeId { get; set; }
    public string? DicStandardTypeDisplayValue { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }


    public List<StandardDetailDto> Details { get; set; }

    public StandardDto()
    {
        Description = string.Empty;
        Details = new List<StandardDetailDto>();
    }
}