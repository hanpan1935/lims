using System;
using System.ComponentModel;

namespace Lanpuda.Lims.Samples.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class SampleUpdateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SampleProductId")]
    public Guid ProductId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SampleDicSampleTypeId")]
    public int? DicSampleTypeId { get; set; }

 
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SampleDicSamplePropertyId")]
    public int? DicSamplePropertyId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SampleSampleTime")]
    public DateTime SampleTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SampleSampleCount")]
    public double? SampleCount { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SampleExpireTime")]
    public DateTime? ExpireTime { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SampleSender")]
    public string? Sender { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SampleRemark")]
    public string? Remark { get; set; }

    public Guid? CustomerId { get; set; }
    public Guid? SupplierId { get; set; }
}