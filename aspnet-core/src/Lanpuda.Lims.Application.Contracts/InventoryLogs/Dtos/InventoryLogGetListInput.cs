using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.InventoryLogs.Dtos;

[Serializable]
public class InventoryLogGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 发生单号
    /// </summary>
    [DisplayName("InventoryLogNumber")]
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryLogProductId")]
    public Guid? ProductId { get; set; }


    public Guid? WarehouseId { get; set; }


  
    [DisplayName("InventoryLogLocationId")]
    public Guid? LocationId { get; set; }


    /// <summary>
    ///  出入时间
    /// </summary>
    [DisplayName("InventoryLogLogTime")]
    public DateTime? LogTime { get; set; }

    public string? Reason { get; set; }
    /// <summary>
    /// 批次号
    /// </summary>
    [DisplayName("InventoryLogLotNumber")]
    public string? LotNumber { get; set; }


}