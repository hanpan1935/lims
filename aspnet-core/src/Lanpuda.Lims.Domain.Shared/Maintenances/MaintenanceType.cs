using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.Lims.Maintenances
{
    public enum MaintenanceType
    {
        [Display(Name = "未知", Description = "未知", Order = 0)]
        Unknown = 0,
        //1. 计划维护(Scheduled Maintenance)
        [Display(Name = "计划维护", Description = "计划维护", Order = 1)]
        Scheduled = 1,
        //2. 预防性维护(Preventive Maintenance)
        [Display(Name = "预防性维护", Description = "预防性维护", Order = 2)]
        Preventive = 2,
        //3. 纠正性维护(Corrective Maintenance)
        [Display(Name = "纠正性维护", Description = "纠正性维护", Order = 3)]
        Corrective = 3,
        //4. 优先级维护(Priority Maintenance)
        [Display(Name = "优先级维护", Description = "优先级维护", Order = 4)]
        Priority = 4,
        //5. 应急维护(Emergency Maintenance)
        [Display(Name = "应急维护", Description = "应急维护", Order = 5)]
        Emergency = 5,
        //6. 故障修复维护(Fault Repair Maintenance)
        [Display(Name = "故障修复维护", Description = "故障修复维护", Order = 6)]
        FaultRepair = 6,
        //7. 安全维护(Safety Maintenance)
        [Display(Name = "安全维护", Description = "安全维护", Order = 7)]
        Safety = 7,
        //8. 可操作性维护(Operability Maintenance)
        [Display(Name = "可操作性维护", Description = "可操作性维护", Order = 8)]
        Operability = 8,
        //9. 可靠性维护(Reliability Maintenance)
        [Display(Name = "可靠性维护", Description = "可靠性维护", Order = 9)]
        Reliability = 9,
        //10. 能源效率维护(Energy Efficiency Maintenance)
        [Display(Name = "能源效率维护", Description = "能源效率维护", Order = 10)]
        EnergyEfficiency = 10,
    }
}
