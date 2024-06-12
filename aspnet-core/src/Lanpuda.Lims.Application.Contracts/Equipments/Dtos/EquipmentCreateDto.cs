using System;
using System.ComponentModel;

namespace Lanpuda.Lims.Equipments.Dtos;

[Serializable]
public class EquipmentCreateDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentName")]
    public string Name { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentStatus")]
    public EquipmentStatus Status { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentMaintenancePeriod")]
    public MaintenancePeriodType MaintenancePeriod { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentNumber")]
    public string? Number { get; set; }

  

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentDicEquipmentTypeId")]
    public int? DicEquipmentTypeId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentSpec")]
    public string? Spec { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentManufacturer")]
    public string? Manufacturer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentAcquisitionDate")]
    public DateTime? AcquisitionDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentOperationManual")]
    public string? OperationManual { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentInstallationLocation")]
    public string? InstallationLocation { get; set; }

    
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentCalibrationStandard")]
    public string? CalibrationStandard { get; set; }

  

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentMaintenanceStandard")]
    public string? MaintenanceStandard { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("EquipmentRemark")]
    public string? Remark { get; set; }

    public EquipmentCreateDto()
    {
        this.Name = string.Empty;
    }
}