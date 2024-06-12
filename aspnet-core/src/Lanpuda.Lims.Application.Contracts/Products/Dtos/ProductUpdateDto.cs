using System;
using System.ComponentModel;

namespace Lanpuda.Lims.Products.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class ProductUpdateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("ProductName")]
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("ProductUnit")]
    public string Unit { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("ProductNumber")]
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("ProductDicProductTypeId")]
    public int? DicProductTypeId { get; set; }

  

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("ProductSpec")]
    public string? Spec { get; set; }


    public Guid? StandardId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("ProductRemark")]
    public string? Remark { get; set; }
}