using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Lanpuda.Lims
{
    public enum DataDictionaryType
    {
        [Display(Name = "设备类型", Description = "设备类型", Order = 1)]
        DicEquipmentType = 1,
        [Display(Name = "样品属性", Description = "样品属性", Order = 2)]
        DicSampleProperty = 2,
        [Display(Name = "样品类型", Description = "样品类型", Order = 3)]
        DicSampleType = 3,
        [Display(Name = "标准类型", Description = "标准类型", Order = 4)]
        DicStandardType = 4,
        [Display(Name = "产品类型", Description = "产品类型", Order = 5)]
        DicProductType = 5,
        [Display(Name = "判级类型", Description = "判级类型", Order = 6)]
        DicRatingType = 6,
    }
}
