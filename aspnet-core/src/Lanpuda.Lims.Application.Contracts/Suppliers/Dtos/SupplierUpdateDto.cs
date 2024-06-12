using System;
using System.ComponentModel;

namespace Lanpuda.Lims.Suppliers.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class SupplierUpdateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SupplierFullName")]
    public string FullName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SupplierShortName")]
    public string ShortName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SupplierManager")]
    public string? Manager { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SupplierManagerTel")]
    public string? ManagerTel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SupplierNumber")]
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SupplierRemark")]
    public string? Remark { get; set; }
}