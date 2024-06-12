using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.UsageHistories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.Lims.Permissions.LimsPermissions;

namespace Lanpuda.Lims.UI.EquipmentManagement.UsageHistories.Edits
{
    public class UsageHistoryEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        //记录编号：用于唯一标识每个设备使用记录的编号或标识符。
        //public string Number { get; set; }

        public Guid EquipmentId { get; set; }

        [Required(ErrorMessage = "必填")]
        public string? EquipmentName
        {
            get { return GetProperty(() => EquipmentName); }
            set { SetProperty(() => EquipmentName, value); }
        }


        //开始使用时间：
        [Required(ErrorMessage = "必填")]
        public DateTime StartTime
        {
            get { return GetProperty(() => StartTime); }
            set { SetProperty(() => StartTime, value); }
        }

        public DateTime? EndTime
        {
            get { return GetProperty(() => EndTime); }
            set { SetProperty(() => EndTime, value); }
        }

        //使用人员：进行设备使用的人员姓名或标识。
        public string? Person
        {
            get { return GetProperty(() => Person); }
            set { SetProperty(() => Person, value); }
        }

        //使用项目：指示设备使用的具体项目或任务。
        public string? Project
        {
            get { return GetProperty(() => Project); }
            set { SetProperty(() => Project, value); }
        }


        //使用描述：提供对设备使用情况的详细描述，包括使用目的、操作步骤等。
        public string? Description
        {
            get { return GetProperty(() => Description); }
            set { SetProperty(() => Description, value); }
        }

        //使用备注：用于记录与设备使用相关的其他信息或说明。
        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }
        //使用部门：使用设备的部门或团队的名称或标识。
        public string? Department
        {
            get { return GetProperty(() => Department); }
            set { SetProperty(() => Department, value); }
        }

    }
}
