using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.DataDictionaries
{
    public class DicSampleProperty : LimsAuditedAggregateRoot<int>
    {
        public string DisplayValue { get; set; }

        public int Sort { get; set; }

        protected DicSampleProperty()
        {
            DisplayValue = string.Empty;
        }

        public DicSampleProperty(
          
            string displayValue
        ) 
        {
            DisplayValue = displayValue;
        }
    }
}
