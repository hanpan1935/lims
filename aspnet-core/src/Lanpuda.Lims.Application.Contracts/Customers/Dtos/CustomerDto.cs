using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Customers.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class CustomerDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Manager { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ManagerTel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Consignee { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ConsigneeTel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Address { get; set; }

    public CustomerDto()
    {
        this.FullName = string.Empty;
        this.ShortName = string.Empty;
    }

}