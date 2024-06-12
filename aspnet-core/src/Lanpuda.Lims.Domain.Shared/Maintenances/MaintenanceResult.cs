using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.Lims.Maintenances
{
    public enum MaintenanceResult
    {
        [Display(Name = "未知", Description = "未知", Order = 0)]
        Unkonwn = 0,
        //1. 维护完成(Maintenance Completed)
        [Display(Name = "维护完成", Description = "维护完成", Order = 1)]
        Completed = 1,
        //2. 维护部分完成(Maintenance Partially Completed)
        [Display(Name = "维护部分完成", Description = "维护部分完成", Order = 2)]
        PartiallyCompleted = 2,
        //3. 维护未完成(Maintenance Not Completed)
        [Display(Name = "维护未完成", Description = "维护未完成", Order = 3)]
        NotCompleted = 3,
    }
}
