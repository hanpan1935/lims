using Lanpuda.Lims.Equipments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Lanpuda.Lims.Calibrations
{
    public class Calibration : LimsAuditedAggregateRoot<Guid>
    {
        //记录编号：用于唯一标识每个设备维护记录的编号或标识符。
        public string Number { get; set; }

        //设备编号：指示进行维护的设备的编号或标识符。
        public Guid EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }

        //校准日期
        public DateTime CalibrationDate { get; set; }

        //校准结果：记录设备校准的结果，如合格、不合格、修复等。
        public CalibrationResult CalibrationResult { get; set; }


        //下次校准日期
        public DateTime? NextCalibrationDate { get; set; }
       

        //校准负责人：
        public string? Person { get; set; }

        //校准证书编号：校准过程中生成的证书编号，用于追踪和查证校准的有效性。
        public string? CertificateNumber { get; set; }

        //校准费用：记录设备校准的费用。
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Cost { get; set; }

        //校准标准：指示设备校准所依据的标准或规范。
        public string? Standard { get; set; }

        //校准备注：记录与设备校准相关的其他信息或说明。
        public string? Remark { get; set; }

        //校准记录附件：记录与设备校准相关的附件信息。
        //public virtual ICollection<CalibrationAttachment> Attachments { get; set; }


      

        protected Calibration()
        {
            Number = string.Empty;
        }

        public Calibration(Guid id) : base(id)
        {
            Number = string.Empty;
        }
    }
}
