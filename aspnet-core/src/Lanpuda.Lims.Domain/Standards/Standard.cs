using Lanpuda.Lims.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.Lims.DataDictionaries;

namespace Lanpuda.Lims.Standards
{
    public class Standard : LimsAuditedAggregateRoot<Guid>
    {
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        public int? DicStandardTypeId { get; set; }
        public DicStandardType? DicStandardType { get; set; }

     
        [MaxLength(256)]
        public string? Remark { get; set; }


        public List<StandardDetail> Details { get; set; }

        protected Standard()
        {
            this.Description = string.Empty;
            this.Details = new List<StandardDetail>();
        }

        public Standard(Guid id,string description) : base(id)
        {
            this.Description = description;
            this.Details = new List<StandardDetail>();
        }
    }
}
