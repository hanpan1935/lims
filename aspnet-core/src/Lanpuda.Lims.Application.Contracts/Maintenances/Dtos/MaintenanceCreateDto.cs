using System;
using System.ComponentModel;

namespace Lanpuda.Lims.Maintenances.Dtos;

[Serializable]
public class MaintenanceCreateDto
{

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("MaintenanceEquipmentId")]
    public Guid EquipmentId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("MaintenanceDate")]
    public DateTime Date { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("MaintenanceMaintenanceType")]
    public MaintenanceType MaintenanceType { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("MaintenanceResult")]
    public MaintenanceResult Result { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("MaintenanceDescription")]
    public string? Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("MaintenancePerson")]
    public string? Person { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("MaintenanceSpentDays")]
    public float? SpentTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("MaintenanceCost")]
    public decimal? Cost { get; set; }



    /// <summary>
    /// 
    /// </summary>
    [DisplayName("MaintenanceRemark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("MaintenanceWorkOrderNumber")]
    public string? WorkOrderNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("MaintenanceDepartment")]
    public string? Department { get; set; }
}