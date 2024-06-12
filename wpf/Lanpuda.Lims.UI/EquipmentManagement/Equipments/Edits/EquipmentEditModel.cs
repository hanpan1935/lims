using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Equipments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lanpuda.Lims.UI.EquipmentManagement.Equipments.Edits
{
    public class EquipmentEditModel : ModelBase
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Required(ErrorMessage = "必填")]
        [DisplayName("EquipmentName")]
        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }


        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentStatus")]
        public EquipmentStatus Status
        {
            get { return GetProperty(() => Status); }
            set { SetProperty(() => Status, value); }
        }


        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentMaintenancePeriod")]
        public MaintenancePeriodType MaintenancePeriod
        {
            get { return GetProperty(() => MaintenancePeriod); }
            set { SetProperty(() => MaintenancePeriod, value); }
        }


        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentNumber")]
        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }



        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentDicEquipmentTypeId")]
        public int? DicEquipmentTypeId
        {
            get { return GetProperty(() => DicEquipmentTypeId); }
            set { SetProperty(() => DicEquipmentTypeId, value); }
        }


        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentSpec")]
        public string? Spec
        {
            get { return GetProperty(() => Spec); }
            set { SetProperty(() => Spec, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentManufacturer")]
        public string? Manufacturer
        {
            get { return GetProperty(() => Manufacturer); }
            set { SetProperty(() => Manufacturer, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentAcquisitionDate")]
        public DateTime? AcquisitionDate
        {
            get { return GetProperty(() => AcquisitionDate); }
            set { SetProperty(() => AcquisitionDate, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentOperationManual")]
        public string? OperationManual
        {
            get { return GetProperty(() => OperationManual); }
            set { SetProperty(() => OperationManual, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentInstallationLocation")]
        public string? InstallationLocation
        {
            get { return GetProperty(() => InstallationLocation); }
            set { SetProperty(() => InstallationLocation, value); }
        }


        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentCalibrationStandard")]
        public string? CalibrationStandard
        {
            get { return GetProperty(() => CalibrationStandard); }
            set { SetProperty(() => CalibrationStandard, value); }
        }



        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentMaintenanceStandard")]
        public string? MaintenanceStandard
        {
            get { return GetProperty(() => MaintenanceStandard); }
            set { SetProperty(() => MaintenanceStandard, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("EquipmentRemark")]
        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public EquipmentEditModel()
        {

        }
    }

}
