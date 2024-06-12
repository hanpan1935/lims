using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Maintenances;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.EquipmentManagement.Maintenances.Edits
{
    public class MaintenanceEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public Guid EquipmentId { get; set; }

        [Required(ErrorMessage ="必填")]
        public string EquipmentName
        {
            get { return GetProperty(() => EquipmentName); }
            set { SetProperty(() => EquipmentName, value); }
        }


        //维护日期：记录设备进行维护的日期和时间。
        [Required(ErrorMessage = "必填")]
        public DateTime Date
        {
            get { return GetProperty(() => Date); }
            set { SetProperty(() => Date, value); }
        }

        //维护类型：指示维护的类型，如预防性维护、保养
        public MaintenanceType MaintenanceType
        {
            get { return GetProperty(() => MaintenanceType); }
            set { SetProperty(() => MaintenanceType, value); }
        }

        //维护结果：记录维护的结果，例如维护完成、维护部分完成等。
        public MaintenanceResult Result
        {
            get { return GetProperty(() => Result); }
            set { SetProperty(() => Result, value); }
        }

        //维护描述：提供对维护工作的详细描述，包括维护的具体内容和方法。
        public string? Description
        {
            get { return GetProperty(() => Description); }
            set { SetProperty(() => Description, value); }
        }

        //维护人员：进行设备维护的人员姓名或标识。
        public string? Person
        {
            get { return GetProperty(() => Person); }
            set { SetProperty(() => Person, value); }
        }

        //维护耗时
        [Range(float.MinValue, float.MaxValue)]
        public float? SpentTime
        {
            get { return GetProperty(() => SpentTime); }
            set { SetProperty(() => SpentTime, value); }
        }

        //维护费用：记录维护所产生的费用，包括人工费、零件费用等。
        public decimal? Cost
        {
            get { return GetProperty(() => Cost); }
            set { SetProperty(() => Cost, value); }
        }


        //维护工单编号：如果有维护工单系统，可以记录维护工单的编号，以便进行关联和追溯。
        public string? WorkOrderNumber
        {
            get { return GetProperty(() => WorkOrderNumber); }
            set { SetProperty(() => WorkOrderNumber, value); }
        }

        //维护部门：负责进行设备维护的部门或团队的名称或标识。
        public string? Department
        {
            get { return GetProperty(() => Department); }
            set { SetProperty(() => Department, value); }
        }

        //维护备注：用于记录与设备维护相关的其他信息或说明。
        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public MaintenanceEditModel()
        {
            this.EquipmentName = string.Empty;
        }
    }
}
