using Lanpuda.Lims.Equipments;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.UsageHistories
{
    public class UsageHistory : LimsAuditedAggregateRoot<Guid>
    {
        //记录编号：用于唯一标识每个设备使用记录的编号或标识符。
        public string Number { get; set; }

        public Guid EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }

        /// <summary>
        /// 记录类型
        /// </summary>
        public UsageHistoryType UsageHistoryType { get; set; }


        //开始使用时间：
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        //使用人员：进行设备使用的人员姓名或标识。
        public string? Person { get; set; }

        //使用项目：指示设备使用的具体项目或任务。
        public string? Project { get; set; }


        //使用描述：提供对设备使用情况的详细描述，包括使用目的、操作步骤等。
        public string? Description { get; set; }

        //使用备注：用于记录与设备使用相关的其他信息或说明。
        public string? Remark { get; set; }
        //使用部门：使用设备的部门或团队的名称或标识。
        public string? Department { get; set; }



        protected UsageHistory()
        {
            this.Number = string.Empty;
        }

        public UsageHistory(
            Guid id,
            string number
        ) : base(id)
        {
            Number = number;
        }
    }
}
