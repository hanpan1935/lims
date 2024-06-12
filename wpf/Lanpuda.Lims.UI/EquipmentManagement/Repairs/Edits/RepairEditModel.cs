using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Repairs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.Lims.Permissions.LimsPermissions;
using System.Windows.Media;
using System.ComponentModel.DataAnnotations;

namespace Lanpuda.Lims.UI.EquipmentManagement.Repairs.Edits
{
    public class RepairEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        //设备编号：用于标识设备的编号。
        public Guid EquipmentId { get; set; }


        [Required(ErrorMessage = "必填")]
        public string? EquipmentName
        {
            get { return GetProperty(() => EquipmentName); }
            set { SetProperty(() => EquipmentName, value); }
        }


        //维修日期：记录设备进行维修的日期和时间。
        [Required(ErrorMessage = "必填")]
        public DateTime RepairDate
        {
            get { return GetProperty(() => RepairDate); }
            set { SetProperty(() => RepairDate, value); }
        }

        //维修结果：记录维修的结果，例如修复成功、部分修复、无法修复等。
        public RepairResult RepairResult
        {
            get { return GetProperty(() => RepairResult); }
            set { SetProperty(() => RepairResult, value); }
        }


        //维修描述：提供对维修工作的详细描述，包括维修的具体问题和解决方案。
        public string? Description
        {
            get { return GetProperty(() => Description); }
            set { SetProperty(() => Description, value); }
        }


        //维修人员：进行设备维修的人员姓名或标识。
        public string? Person
        {
            get { return GetProperty(() => Person); }
            set { SetProperty(() => Person, value); }
        }


        //维修耗时：记录维修所花费的时间，例如小时数或天数。
        public float? RepairTime
        {
            get { return GetProperty(() => RepairTime); }
            set { SetProperty(() => RepairTime, value); }
        }

        //维修费用：记录维修所产生的费用，包括人工费、零件费用等。
        [Column(TypeName = "decimal(18,2)")]
        public decimal? RepairCost
        {
            get { return GetProperty(() => RepairCost); }
            set { SetProperty(() => RepairCost, value); }
        }


        //维修工单编号：如果有维修工单系统，可以记录维修工单的编号，以便进行关联和追溯。
        public string? RepairWorkOrderNumber
        {
            get { return GetProperty(() => RepairWorkOrderNumber); }
            set { SetProperty(() => RepairWorkOrderNumber, value); }
        }

        //维修部门：负责进行设备维修的部门或团队的名称或标识。
        public string? RepairDepartment
        {
            get { return GetProperty(() => RepairDepartment); }
            set { SetProperty(() => RepairDepartment, value); }
        }


        //维修标准：指示设备维修所依据的标准或规范。
        public string? RepairStandard
        {
            get { return GetProperty(() => RepairStandard); }
            set { SetProperty(() => RepairStandard, value); }
        }


        //维修结果确认人：确认维修结果的人员姓名或标识。
        public string? ConfirmPerson
        {
            get { return GetProperty(() => ConfirmPerson); }
            set { SetProperty(() => ConfirmPerson, value); }
        }


        //维修备注：用于记录与设备维修相关的其他信息或说明。
        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

    }
}
