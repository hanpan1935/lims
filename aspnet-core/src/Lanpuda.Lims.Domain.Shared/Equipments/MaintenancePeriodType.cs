using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.Lims.Equipments
{
    public enum MaintenancePeriodType
    {
        [Display(Name = "未知", Description = "未知", Order = 0)]
        Unknown = 0,
        //1. 每月
        [Display(Name = "每月", Description = "每月", Order = 1)]
        Monthly = 1,
        //2. 每季度
        [Display(Name = "每季度", Description = "每季度", Order = 2)]
        Quarterly = 2,
        //3. 每半年
        [Display(Name = "每半年", Description = "每半年", Order = 3)]
        Semiannual = 3,
        //4. 每年
        [Display(Name = "每年", Description = "每年", Order = 4)]
        Annual = 4,
        //5. 其他
        [Display(Name = "其他", Description = "其他", Order = 5)]
        Other = 5,
    }
}
