using System;
using System.ComponentModel;

namespace Lanpuda.Lims.Repairs.Dtos;

[Serializable]
public class RepairCreateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairEquipmentId")]
    public Guid EquipmentId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairRepairDate")]
    public DateTime RepairDate { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairRepairResult")]
    public RepairResult RepairResult { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairDescription")]
    public string? Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairPerson")]
    public string? Person { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairRepairTime")]
    public float? RepairTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairRepairCost")]
    public decimal? RepairCost { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairRepairWorkOrderNumber")]
    public string? RepairWorkOrderNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairRepairDepartment")]
    public string? RepairDepartment { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairRepairStandard")]
    public string? RepairStandard { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairConfirmPerson")]
    public string? ConfirmPerson { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairRepairRemark")]
    public string? Remark { get; set; }
}