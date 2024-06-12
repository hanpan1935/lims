using Lanpuda.Lims.InspectionTasks.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lanpuda.Lims.Records.Dtos;

[Serializable]
public class RecordDetailCreateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RecordDetailInspectionItemId")]
    public Guid InspectionItemId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RecordDetailMinValue")]
    public double? MinValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RecordDetailHasMinValue")]
    public bool HasMinValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RecordDetailMaxValue")]
    public double? MaxValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RecordDetailHasMaxValue")]
    public bool HasMaxValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RecordDetailResultValue")]
    public double? ResultValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RecordDetailIsQualified")]
    public bool? IsQualified { get; set; }

}