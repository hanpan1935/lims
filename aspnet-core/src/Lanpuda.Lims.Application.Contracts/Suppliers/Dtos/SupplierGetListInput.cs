using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Suppliers.Dtos;

[Serializable]
public class SupplierGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SupplierFullName")]
    public string? FullName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("SupplierShortName")]
    public string? ShortName { get; set; }

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