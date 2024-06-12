using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Standards.Dtos;

[Serializable]
public class StandardGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDescription")]
    public string? Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDicStandardTypeId")]
    public int? DicStandardTypeId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardProductId")]
    public Guid? ProductId { get; set; }

 
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardRemark")]
    public string? Remark { get; set; }
}