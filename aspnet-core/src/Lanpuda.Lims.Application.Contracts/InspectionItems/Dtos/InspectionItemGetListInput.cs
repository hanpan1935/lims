using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.InspectionItems.Dtos;

[Serializable]
public class InspectionItemGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionItemShortName")]
    public string? ShortName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionItemFullName")]
    public string? FullName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionItemBasis")]
    public string? Basis { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionItemUnit")]
    public string? Unit { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionItemRemark")]
    public string? Remark { get; set; }
}