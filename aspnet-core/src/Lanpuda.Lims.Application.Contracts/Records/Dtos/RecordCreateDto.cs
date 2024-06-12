using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lanpuda.Lims.Records.Dtos;

[Serializable]
public class RecordCreateDto
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
    public List<RecordDetailCreateDto> Details { get; set; }

    public RecordCreateDto()
    {
        Details = new List<RecordDetailCreateDto>();
    }
}