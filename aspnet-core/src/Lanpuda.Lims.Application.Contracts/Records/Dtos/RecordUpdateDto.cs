using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lanpuda.Lims.Records.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class RecordUpdateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RecordSampleId")]
    public Guid SampleId { get; set; }

    public int? DicRatingTypeId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RecordRemark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RecordDetails")]
    public List<RecordDetailUpdateDto> Details { get; set; }

    public RecordUpdateDto()
    {
        Details = new List<RecordDetailUpdateDto>();
    }
}