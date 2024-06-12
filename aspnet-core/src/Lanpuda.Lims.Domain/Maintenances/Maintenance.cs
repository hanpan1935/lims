using Lanpuda.Lims.Calibrations;
using Lanpuda.Lims.Equipments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Maintenances
{
    public class Maintenance : LimsAuditedAggregateRoot<Guid>
    {
        //记录编号：用于唯一标识每个设备维护记录的编号或标识符。
        public string Number { get; set; }

        //设备编号：指示进行维护的设备的编号或标识符。
        public Guid EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }

        //维护日期：记录设备进行维护的日期和时间。
        public DateTime Date { get; set; }

        //维护类型：指示维护的类型，如预防性维护、保养
        public MaintenanceType MaintenanceType { get; set; }

        //维护描述：提供对维护工作的详细描述，包括维护的具体内容和方法。
        public string? Description { get; set; }

        //维护人员：进行设备维护的人员姓名或标识。
        public string? Person { get; set; }

        //维护耗时：记录维护所花费的时间
        [Range(float.MinValue, float.MaxValue)]
        public float? SpentTime { get; set; }

        //维护费用：记录维护所产生的费用，包括人工费、零件费用等。
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Cost { get; set; }

        //维护结果：记录维护的结果，例如维护完成、维护部分完成等。
        public MaintenanceResult Result { get; set; }

        //维护备注：用于记录与设备维护相关的其他信息或说明。
        public string? Remark { get; set; }

        //维护工单编号：如果有维护工单系统，可以记录维护工单的编号，以便进行关联和追溯。
        public string? WorkOrderNumber { get; set; }

        //维护部门：负责进行设备维护的部门或团队的名称或标识。
        public string? Department { get; set; }

        //校准记录附件：记录与设备校准相关的附件信息。
        //public virtual ICollection<MaintenanceAttachment> Attachments { get; set; }

        protected Maintenance()
        {
            this.Number = string.Empty;
        }

        public Maintenance(
            Guid id,
            string number
        ) : base(id)
        {
            Number = number;
        }
    }
}
