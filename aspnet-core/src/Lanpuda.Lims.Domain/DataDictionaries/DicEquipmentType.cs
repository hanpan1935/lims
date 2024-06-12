using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.DataDictionaries
{
    public class DicEquipmentType : LimsAuditedAggregateRoot<int>
    {
        public string DisplayValue { get; set; }

        public int Sort { get; set; }

        protected DicEquipmentType()
        {
            DisplayValue = string.Empty;
        }

        public DicEquipmentType(string displayValue)
        {
            DisplayValue = displayValue;
        }
    }
}
