using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Standards.Dtos;

[Serializable]
public class StandardDetailGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailStandardId")]
    public Guid? StandardId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailInspectionItemId")]
    public Guid? InspectionItemId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailMinValue")]
    public double? MinValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailHasMinValue")]
    public bool? HasMinValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailMaxValue")]
    public double? MaxValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailHasMaxValue")]
    public bool? HasMaxValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailSort")]
    public int? Sort { get; set; }
}