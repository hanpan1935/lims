using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Suppliers.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class SupplierDto : LimsAuditedEntityDto<Guid>
{
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
    public string Manager { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ManagerTel { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Remark { get; set; }
}