using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Suppliers
{
    public class Supplier : LimsAuditedAggregateRoot<Guid>
    {
        [Required]
        [MaxLength(256)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(256)]
        public string ShortName { get; set; }

        [MaxLength(256)]
        public string? Manager { get; set; }


        public string? ManagerTel { get; set; }


        [MaxLength(256)]
        public string? Number { get; set; }


        [MaxLength(256)]
        public string? Remark { get; set; }

        protected Supplier()
        {
            this.FullName = string.Empty;
            this.ShortName = string.Empty;
        }

        public Supplier(
            Guid id,
            string fullName,
            string shortName
        ) : base(id)
        {
            FullName = fullName;
            ShortName = shortName;
        }
    }
}
