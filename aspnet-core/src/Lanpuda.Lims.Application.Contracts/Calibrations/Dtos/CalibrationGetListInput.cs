using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Calibrations.Dtos;

[Serializable]
public class CalibrationGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// </summary>
    [DisplayName("CalibrationNumber")]
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationEquipmentId")]
    public Guid? EquipmentId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationCalibrationDate")]
    public DateTime? CalibrationDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationNextCalibrationDate")]
    public DateTime? NextCalibrationDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationCalibrationResult")]
    public CalibrationResult? CalibrationResult { get; set; }

}