using Lanpuda.Lims.Equipments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.InspectionItems
{
    public class InspectionItem : LimsAuditedAggregateRoot<Guid>
    {
        [Required]
        [MaxLength(256)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(256)]
        public string FullName { get; set; }


        [Required]
        [MaxLength(256)]
        public string Basis { get; set; }

        [Required]
        [MaxLength(256)]
        public string Unit { get; set; }

        public string? Remark { get; set; }

        public Guid? DefaultEquipmentId { get; set; }
        public Equipment? DefaultEquipment { get; set; }


        protected InspectionItem()
        {
            this.ShortName = string.Empty;
            this.FullName = string.Empty;
            this.Basis = string.Empty;
            this.Unit = string.Empty;
        }

        public InspectionItem(
            Guid id,
            string shortName,
            string fullName,
            string basis,
            string unit
        ) : base(id)
        {
            ShortName = shortName;
            FullName = fullName;
            Basis = basis;
            Unit = unit;
        }
    }
}
