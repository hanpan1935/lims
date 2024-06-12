using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.Lims.Equipments
{
    public enum EquipmentStatus
    {
        [Display(Name = "未知", Description = "未知", Order = 0)]
        Unknown = 0,
        //1. 正常运行状态(Normal Operation)
        [Display(Name = "正常运行", Description = "正常运行", Order = 1)]
        Normal = 1,
        //2. 待机状态(Standby)
        [Display(Name = "待机状态", Description = "待机状态", Order = 2)]
        Standby = 2,
        //3. 故障状态(Faulty)
        [Display(Name = "故障状态", Description = "故障状态", Order = 3)]
        Faulty = 3,
        //4. 维修状态(Maintenance)
        [Display(Name = "维修状态", Description = "维修状态", Order = 4)]
        Maintenance = 4,
        //5. 停用状态(Deactivated)
        [Display(Name = "停用状态", Description = "停用状态", Order = 5)]
        Deactivated = 5,
        //6. 报废状态(Obsolete)
        [Display(Name = "报废状态", Description = "报废状态", Order = 6)]
        Obsolete = 6,
    }
}
