using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Repairs.Dtos;

[Serializable]
public class RepairGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairNumber")]
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairEquipmentId")]
    public Guid? EquipmentId { get; set; }
 
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("RepairRepairDate")]
    public DateTime? RepairDate { get; set; }


    //维修结果：记录维修的结果，例如修复成功、部分修复、无法修复等。
    public RepairResult? RepairResult { get; set; }

}