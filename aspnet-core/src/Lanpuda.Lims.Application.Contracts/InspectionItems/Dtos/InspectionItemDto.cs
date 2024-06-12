using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.InspectionItems.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class InspectionItemDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Basis { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }


    public Guid? DefaultEquipmentId { get; set; }
    public string? DefaultEquipmentName { get; set; }



    public InspectionItemDto()
    {
        this.ShortName = string.Empty;
        this.FullName = string.Empty;
        this.Basis = string.Empty;
        this.Unit = string.Empty;
    }
}