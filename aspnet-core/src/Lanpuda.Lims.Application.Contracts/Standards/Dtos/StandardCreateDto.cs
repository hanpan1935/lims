using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lanpuda.Lims.Standards.Dtos;

[Serializable]
public class StandardCreateDto
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


    public List<StandardDetailCreateDto> Details { get; set; }

    public StandardCreateDto()
    {
        Description = string.Empty;
        Details = new List<StandardDetailCreateDto>();
    }
}