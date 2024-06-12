using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Products.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class ProductDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? DicProductTypeId { get; set; }

    public string? DicProductTypeDisplayValue { get; set; }



    /// <summary>
    /// 
    /// </summary>
    public string? Spec { get; set; }


    public Guid? StandardId { get; set; }


    public string? StandardDescription { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }


    public ProductDto()
    {
        this.Name = string.Empty;
        this.Unit = string.Empty;
    }
}