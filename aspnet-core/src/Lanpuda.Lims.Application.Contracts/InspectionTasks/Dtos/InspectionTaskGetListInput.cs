using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.InspectionTasks.Dtos;

[Serializable]
public class InspectionTaskGetListInput : PagedAndSortedResultRequestDto
{
    public string? RecordNumber { get; set; }

    public string? SampleNumber { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? InspectionItemId { get; set; }
  
    public DateTime? InspectionDateStart { get; set; }

    public DateTime? InspectionDateEnd { get; set; }

    public Guid? EquipmentId { get; set; }

    public string? Inspector { get; set; }

}