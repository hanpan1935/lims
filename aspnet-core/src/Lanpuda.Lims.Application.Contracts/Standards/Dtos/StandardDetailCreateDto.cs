using System;
using System.ComponentModel;

namespace Lanpuda.Lims.Standards.Dtos;

[Serializable]
public class StandardDetailCreateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailInspectionItemId")]
    public Guid InspectionItemId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailMinValue")]
    public double? MinValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailHasMinValue")]
    public bool HasMinValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailMaxValue")]
    public double? MaxValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("StandardDetailHasMaxValue")]
    public bool HasMaxValue { get; set; }

}