using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.DataDictionaries
{
    public class DicProductType : LimsAuditedAggregateRoot<int>
    {
        public string DisplayValue { get; set; }

        public int Sort { get; set; }

        protected DicProductType()
        {
            DisplayValue = string.Empty;
        }

        public DicProductType(
          
            string displayValue
        ) 
        {
            DisplayValue = displayValue;
        }
    }
}
