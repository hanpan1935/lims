using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.Lims.Repairs
{
    public enum RepairResult
    {
        //维修结果：记录维修的结果，例如修复成功、部分修复、无法修复等。
        [Display(Name = "未知", Description = "未知", Order = 0)]
        Unknown = 0,
        [Display(Name = "维修成功", Description = "维修成功", Order = 1)]
        RepairSuccess = 1,
        [Display(Name = "部分维修成功", Description = "部分维修成功", Order = 2)]
        RepairPartSuccess = 2,
        [Display(Name = "维修失败", Description = "维修失败", Order = 3)]
        RepairFail = 3
    }
}
