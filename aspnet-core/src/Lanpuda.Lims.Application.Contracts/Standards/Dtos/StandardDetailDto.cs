using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Standards.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class StandardDetailDto : AuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public Guid StandardId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid InspectionItemId { get; set; }
    public string? InspectionItemShortName { get; set; }
    public string? InspectionItemFullName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double? MinValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool HasMinValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double? MaxValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool HasMaxValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Sort { get; set; }
}