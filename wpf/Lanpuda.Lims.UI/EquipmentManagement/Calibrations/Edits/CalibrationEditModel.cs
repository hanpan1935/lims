using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Calibrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lanpuda.Lims.UI.EquipmentManagement.Calibrations.Edits
{
    public class CalibrationEditModel : ModelBase
    {
        public Guid? Id { get; set; }
        //设备编号：指示进行维护的设备的编号或标识符。
        public Guid EquipmentId { get; set; }


        [Required(ErrorMessage ="必填")]
        public string? EquipmentName
        {
            get { return GetProperty(() => EquipmentName); }
            set { SetProperty(() => EquipmentName, value); }
        }

        //校准日期
        [Required(ErrorMessage = "必填")]
        public DateTime CalibrationDate
        {
            get { return GetProperty(() => CalibrationDate); }
            set { SetProperty(() => CalibrationDate, value); }
        }

        //校准结果：记录设备校准的结果，如合格、不合格、修复等。
        public CalibrationResult CalibrationResult
        {
            get { return GetProperty(() => CalibrationResult); }
            set { SetProperty(() => CalibrationResult, value); }
        }


        //下次校准日期
        public DateTime? NextCalibrationDate
        {
            get { return GetProperty(() => NextCalibrationDate); }
            set { SetProperty(() => NextCalibrationDate, value); }
        }


        //校准负责人：
        public string? Person
        {
            get { return GetProperty(() => Person); }
            set { SetProperty(() => Person, value); }
        }

        //校准证书编号：校准过程中生成的证书编号，用于追踪和查证校准的有效性。
        public string? CertificateNumber
        {
            get { return GetProperty(() => CertificateNumber); }
            set { SetProperty(() => CertificateNumber, value); }
        }

        //校准费用：记录设备校准的费用。
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Cost
        {
            get { return GetProperty(() => Cost); }
            set { SetProperty(() => Cost, value); }
        }

        //校准标准：指示设备校准所依据的标准或规范。
        public string? Standard
        {
            get { return GetProperty(() => Standard); }
            set { SetProperty(() => Standard, value); }
        }

        //校准备注：记录与设备校准相关的其他信息或说明。
        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }
    }
}
