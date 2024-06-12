using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.InventoryLogs.Dtos;

/// <summary>
/// 库存流水
/// </summary>
[Serializable]
public class InventoryLogDto : LimsAuditedEntityDto<Guid>
{
    /// <summary>
    /// 发生单号
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductUnit { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public Guid LocationId { get; set; }
    public string LocationName { get; set; }
    public string WarehouseName { get; set; }

 
    /// <summary>
    ///  出入时间
    /// </summary>
    public DateTime LogTime { get; set; }

    public string? Reason { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    public string? LotNumber { get; set; }

    /// <summary>
    /// 入库数量
    /// </summary>
    public double InQuantity { get; set; }

    /// <summary>
    /// 出库数量
    /// </summary>
    public double OutQuantity { get; set; }

    /// <summary>
    /// 发生后数量
    /// </summary>
    public double AfterQuantity { get; set; }


    public InventoryLogDto()
    {
        this.ProductUnit = string.Empty;
        this.ProductName = string.Empty;
        this.Number = string.Empty;
        this.WarehouseName = string.Empty;
        this.LocationName = string.Empty;
    }
}