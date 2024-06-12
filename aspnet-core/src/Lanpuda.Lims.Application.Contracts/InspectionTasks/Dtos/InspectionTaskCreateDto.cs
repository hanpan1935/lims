using System;
using System.ComponentModel;

namespace Lanpuda.Lims.InspectionTasks.Dtos;

[Serializable]
public class InspectionTaskCreateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionTaskRecordDetailId")]
    public Guid RecordDetailId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionTaskInspectionDate")]
    public DateTime InspectionDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionTaskPriority")]
    public int Priority { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionTaskEquipmentId")]
    public Guid EquipmentId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionTaskInspector")]
    public string? Inspector { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InspectionTaskRemark")]
    public string? Remark { get; set; }
}