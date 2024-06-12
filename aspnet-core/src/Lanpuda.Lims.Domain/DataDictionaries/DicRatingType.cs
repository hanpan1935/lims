using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.DataDictionaries
{
    /// <summary>
    /// 综合判级
    /// </summary>
    public class DicRatingType : LimsAuditedAggregateRoot<int>
    {
        public string DisplayValue { get; set; }

        public int Sort { get; set; }

        protected DicRatingType()
        {
            DisplayValue = string.Empty;
        }

        public DicRatingType(
          
            string displayValue
        ) 
        {
            DisplayValue = displayValue;
        }
    }
}
