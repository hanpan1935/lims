using Lanpuda.Lims.Equipments;
using Lanpuda.Lims.InspectionItems;
using Lanpuda.Lims.Records;
using Lanpuda.Lims.Samples;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.InspectionTasks
{
    public class InspectionTask : LimsAuditedAggregateRoot<Guid>
    {
        public Guid RecordDetailId { get; set; }
        public RecordDetail? RecordDetail { get; set; }

        //检验日期
        public DateTime InspectionDate { get; set; }

        //优先级
        public int Priority { get; set; }

        public Guid EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }

        //检验人
        public string? Inspector { get; set; }

        public double? ResultValue { get; set; }


        public string? Remark { get; set; }

        protected InspectionTask()
        {

        }

        public InspectionTask(Guid id) : base(id)
        {
           
        }
    }
}
