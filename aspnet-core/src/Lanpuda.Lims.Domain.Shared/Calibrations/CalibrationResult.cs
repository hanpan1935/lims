using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.Lims.Calibrations
{
    public enum CalibrationResult
    {
        [Display(Name = "未知", Description = "未知", Order = 0)]
        Unknown = 0,
        //1. 合格(Qualified):表示该项校准结果在正常范围内，符合要求。
        [Display(Name = "合格", Description = "合格", Order = 1)]
        Qualified = 1,
        //2. 不合格(Not Qualified):表示该项校准结果超出了正常范围，不符合要求。
        [Display(Name = "不合格", Description = "不合格", Order = 2)]
        NotQualified = 2,
        //3. 待确认(To Be Confirmed):表示该项校准结果需要进一步确认，不能确定是否合格。
        [Display(Name = "待确认", Description = "待确认", Order = 3)]
        ToBeConfirmed = 3,
        //4. 已修正(Fixed):表示该项校准结果已进行修正，可以重新进行校准并再次确认。
        [Display(Name = "已修正", Description = "已修正", Order = 4)]
        Fixed = 4,
        //5. 需要重新校准(Recalibration Required):表示该项校准结果需要重新进行校准才能确定是否合格。
        [Display(Name = "需要重新校准", Description = "需要重新校准", Order = 5)]
        RecalibrationRequired = 5,
    }
}
