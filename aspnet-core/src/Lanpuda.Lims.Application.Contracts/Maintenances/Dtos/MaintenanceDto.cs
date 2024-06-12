using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Maintenances.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class MaintenanceDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid EquipmentId { get; set; }
    public string? EquipmentName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public MaintenanceType MaintenanceType { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public MaintenanceResult Result { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Person { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public float? SpentTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? Cost { get; set; }

 

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? WorkOrderNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Department { get; set; }
}