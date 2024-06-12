using Lanpuda.Lims.InspectionItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Standards
{
    public class StandardDetail : LimsAuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid StandardId { get; set; }
        public Standard? Standard { get; set; }


        [Required]
        public Guid InspectionItemId { get; set; }
        [ForeignKey(nameof(InspectionItemId))]
        public InspectionItem? InspectionItem { get; set; }

        public double? MinValue { get; set; }

        public bool HasMinValue { get; set; }

        public double? MaxValue { get; set; }

        public bool HasMaxValue { get; set; }

        public int Sort { get; set; }

        protected StandardDetail()
        {
        }

        public StandardDetail(
            Guid id
        ) : base(id)
        {
        
        }
    }
}
