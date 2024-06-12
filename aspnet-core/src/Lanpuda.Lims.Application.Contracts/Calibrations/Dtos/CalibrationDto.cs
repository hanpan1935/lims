using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Calibrations.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class CalibrationDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid EquipmentId { get; set; }
    public string EquipmentName { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public DateTime CalibrationDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? NextCalibrationDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public CalibrationResult CalibrationResult { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Person { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? CertificateNumber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? Cost { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Standard { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }
}