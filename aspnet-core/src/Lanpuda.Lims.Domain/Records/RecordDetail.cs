using Lanpuda.Lims.InspectionItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Lanpuda.Lims.InspectionTasks;

namespace Lanpuda.Lims.Records
{
    public class RecordDetail : LimsAuditedAggregateRoot<Guid>
    {
        public Guid RecordId { get; set; }
        public Record? Record { get; set; }

        [Required]
        public Guid InspectionItemId { get; set; }
        [ForeignKey(nameof(InspectionItemId))]
        public InspectionItem? InspectionItem { get; set; }


        public double? MinValue { get; set; }

        public bool HasMinValue { get; set; }

        public double? MaxValue { get; set; }

        public bool HasMaxValue { get; set; }

        public double? ResultValue { get; set; }

        public bool? IsQualified { get; set; }

        public List<InspectionTask> InspectionTaskList { get; set; }

        public int Sort { get; set; }


        protected RecordDetail()
        {
            this.InspectionTaskList = new List<InspectionTask>();
        }

        public RecordDetail(Guid id) : base(id)
        {
            this.InspectionTaskList = new List<InspectionTask>();
        }
    }
}
