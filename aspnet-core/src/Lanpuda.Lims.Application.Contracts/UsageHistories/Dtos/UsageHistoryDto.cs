using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.UsageHistories.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class UsageHistoryDto : LimsAuditedEntityDto<Guid>
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

    public UsageHistoryType UsageHistoryType { get; set; }
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Person { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Project { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Department { get; set; }

    public UsageHistoryDto()
    {
        this.Number = "";
        this.EquipmentName = "";
    }
}