using System;
using System.ComponentModel;

namespace Lanpuda.Lims.UsageHistories.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class UsageHistoryUpdateDto
{

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("UsageHistoryEquipmentId")]
    public Guid EquipmentId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("UsageHistoryPerson")]
    public string? Person { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("UsageHistoryProject")]
    public string? Project { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("UsageHistoryDescription")]
    public string? Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("UsageHistoryRemark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("UsageHistoryDepartment")]
    public string? Department { get; set; }
}