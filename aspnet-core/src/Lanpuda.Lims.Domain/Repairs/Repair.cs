using Lanpuda.Lims.Equipments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Repairs
{
    public class Repair : LimsAuditedAggregateRoot<Guid>
    {
        //记录编号：用于唯一标识每个设备维修记录的编号或标识符。
        public string Number { get; set; }

        //设备编号：用于标识设备的编号。
        public Guid EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }


        //维修日期：记录设备进行维修的日期和时间。
        public DateTime RepairDate { get; set; }

        //维修结果：记录维修的结果，例如修复成功、部分修复、无法修复等。
        public RepairResult RepairResult { get; set; }


        //维修描述：提供对维修工作的详细描述，包括维修的具体问题和解决方案。
        public string? Description { get; set; }


        //维修人员：进行设备维修的人员姓名或标识。
        public string? Person { get; set; }


        //维修耗时：记录维修所花费的时间，例如小时数或天数。
        public float? RepairTime { get; set; }

        //维修费用：记录维修所产生的费用，包括人工费、零件费用等。
        [Column(TypeName = "decimal(18,2)")]
        public decimal? RepairCost { get; set; }
     

        //维修工单编号：如果有维修工单系统，可以记录维修工单的编号，以便进行关联和追溯。
        public string? RepairWorkOrderNumber { get; set; }

        //维修部门：负责进行设备维修的部门或团队的名称或标识。
        public string? RepairDepartment { get; set; }


        //维修标准：指示设备维修所依据的标准或规范。
        public string? RepairStandard { get; set; }


        //维修结果确认人：确认维修结果的人员姓名或标识。
        public string? ConfirmPerson { get; set; }


        //维修备注：用于记录与设备维修相关的其他信息或说明。
        public string? Remark { get; set; }

        //维修附件：记录与设备维修相关的附件信息。
        //public string RepairAttachment { get; set; }


        protected Repair()
        {
            this.Number = string.Empty;
        }

        public Repair(
            Guid id,
            string number
        ) : base(id)
        {
            Number = number;
        }
    }
}
