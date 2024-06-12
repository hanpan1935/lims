using Lanpuda.Lims.DataDictionaries;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Lanpuda.Lims.Equipments
{
    public class Equipment : LimsAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; } // 设备名称

        public EquipmentStatus Status { get; set; } // 设备状态

        //维护周期：指示预防性维护的周期，例如每月、每季度、每年等。
        public MaintenancePeriodType MaintenancePeriod { get; set; }

        //设备编号
        public string? Number { get; set; }

        //设备类型 例如仪器、工具、仪表等。  字典表
        public int? DicEquipmentTypeId { get; set; }
        public DicEquipmentType? DicEquipmentType { get; set; }


        public string? Spec { get; set; } // 设备规格

        public string? Manufacturer { get; set; } // 制造商

        public DateTime? AcquisitionDate { get; set; } //购置日期

        public string? OperationManual { get; set; } // 操作手册版本号

        public string? InstallationLocation { get; set; } // 安装位置

        //校准标准
        public string? CalibrationStandard { get; set; }

        //维护标准：指示设备维护所依据的标准或规范。
        public string? MaintenanceStandard { get; set; }

        public string? Remark { get; set; } // 备注

        //设备附件
        // public List<EquipmentAttachment> Attachments { get; set; }

        protected Equipment()
        {
            this.Name = string.Empty;
        }

        public Equipment(
            Guid id,
            string name
        ) : base(id)
        {
            Name = name;
        }
    }
}
