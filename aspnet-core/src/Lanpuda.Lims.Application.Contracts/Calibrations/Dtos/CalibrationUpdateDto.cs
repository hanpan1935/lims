using System;
using System.ComponentModel;

namespace Lanpuda.Lims.Calibrations.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class CalibrationUpdateDto
{

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationEquipmentId")]
    public Guid EquipmentId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationCalibrationDate")]
    public DateTime CalibrationDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationNextCalibrationDate")]
    public DateTime? NextCalibrationDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationCalibrationResult")]
    public CalibrationResult CalibrationResult { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationPerson")]
    public string? Person { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationCertificateNumber")]
    public string? CertificateNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationCost")]
    public decimal? Cost { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationStandard")]
    public string? Standard { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("CalibrationRemark")]
    public string? Remark { get; set; }
}