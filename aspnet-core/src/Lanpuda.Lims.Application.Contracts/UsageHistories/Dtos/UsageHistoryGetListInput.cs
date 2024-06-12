using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.UsageHistories.Dtos;

[Serializable]
public class UsageHistoryGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("UsageHistoryNumber")]
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("UsageHistoryEquipmentId")]
    public Guid? EquipmentId { get; set; }


    public UsageHistoryType? UsageHistoryType { get; set; }

   
}