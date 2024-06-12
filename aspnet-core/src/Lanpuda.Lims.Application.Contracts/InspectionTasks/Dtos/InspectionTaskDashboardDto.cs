using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.Lims.InspectionTasks.Dtos
{
    public class InspectionTaskDashboardDto
    {
        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public List<InspectionTaskDto> Details { get; set; }

        public InspectionTaskDashboardDto()
        {
            Details = new List<InspectionTaskDto>();
            EquipmentName = string.Empty;
        }
    }
}
