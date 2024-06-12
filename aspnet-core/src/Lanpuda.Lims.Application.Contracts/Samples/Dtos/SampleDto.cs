using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Samples.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class SampleDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } 

    public Guid? ProductStandardId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public int? DicSampleTypeId { get; set; }
    public string? DicSampleTypeDisplayValue { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public int? DicSamplePropertyId { get; set; }
    public string? DicSamplePropertyDisplayValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime SampleTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double? SampleCount { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Sender { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }

    public Guid? CustomerId { get; set; }
    public string? CustomerShortName { get; set; }


    public Guid? SupplierId { get; set; }
    public string? SupplierShortName { get; set; }

    public SampleDto()
    {
        this.Number = string.Empty;
        this.ProductName = string.Empty;
    }

}