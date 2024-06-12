using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.DataDictionaries
{
    public class DicStandardType : LimsAuditedAggregateRoot<int>
    {
        public string DisplayValue { get; set; }

        public int Sort { get; set; }

        protected DicStandardType()
        {
            DisplayValue = string.Empty;
        }

        public DicStandardType(string displayValue)
        {
            DisplayValue = displayValue;
        }
    }
}
