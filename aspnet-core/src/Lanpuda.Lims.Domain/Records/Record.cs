
using Lanpuda.Lims.DataDictionaries;
using Lanpuda.Lims.Samples;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Records
{
    public class Record : LimsAuditedAggregateRoot<Guid>
    {
        public string Number { get; set; }

        public Guid SampleId { get; set; }
        public Sample? Sample { get; set; }

        public int? DicRatingTypeId { get; set; }
        public DicRatingType? DicRatingType { get; set; }


        public string? Remark { get; set; }

        public List<RecordDetail> Details { get; set; }

        protected Record()
        {
            this.Details = new List<RecordDetail>();
            this.Number = string.Empty;
        }

        public Record(
            Guid id,
            string number
        ) : base(id)
        {
            Number = number;
            this.Details = new List<RecordDetail>();
        }
    }
}
