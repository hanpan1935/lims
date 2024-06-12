using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Equipments.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class EquipmentDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? DicEquipmentTypeId { get; set; }
    public string? DicEquipmentTypeDisplayValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Spec { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? AcquisitionDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? OperationManual { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? InstallationLocation { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public EquipmentStatus Status { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? CalibrationStandard { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public MaintenancePeriodType MaintenancePeriod { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? MaintenanceStandard { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }

    public EquipmentDto()
    {
        Name = string.Empty;
    }
}