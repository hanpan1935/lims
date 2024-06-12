using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.DataDictionaries
{
    public class DicSampleType : LimsAuditedAggregateRoot<int>
    {
        public string DisplayValue { get; set; }

        public int Sort { get; set; }

        protected DicSampleType()
        {
            DisplayValue = string.Empty;
        }

        public DicSampleType(
      
            string displayValue
        ) 
        {
            DisplayValue = displayValue;
        }
    }
}
