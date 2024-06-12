using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lanpuda.Lims.UsageHistories
{
    public enum UsageHistoryType
    {
        //使用记录类型：记录使用的类型，例如使用、借用、归还等。
        [Display(Name = "未知", Description = "未知", Order = 0)]
        Unknown = 0,
        [Display(Name = "手动添加", Description = "手动添加", Order = 1)]
        Manual = 1,
        [Display(Name = "系统创建", Description = "系统创建", Order = 2)]
        System = 2,
    }
}
