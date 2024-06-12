using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Repairs.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class RepairDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid EquipmentId { get; set; }
    public string? EquipmentName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime RepairDate { get; set; }



    /// <summary>
    /// 
    /// </summary>
    public RepairResult RepairResult { get; set; }


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
    public float? RepairTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? RepairCost { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public string? RepairWorkOrderNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? RepairDepartment { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? RepairStandard { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ConfirmPerson { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }

    public RepairDto()
    {
        this.Number = string.Empty;
    }

}