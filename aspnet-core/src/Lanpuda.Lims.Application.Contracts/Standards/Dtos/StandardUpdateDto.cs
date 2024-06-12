using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lanpuda.Lims.Standards.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class StandardUpdateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDescription")]
    public string Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDicStandardTypeId")]
    public int? DicStandardTypeId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardRemark")]
    public string? Remark { get; set; }


    public List<StandardDetailUpdateDto> Details { get; set; }


    public StandardUpdateDto()
    {
        Description = string.Empty;
        Details = new List<StandardDetailUpdateDto>();
    }
}