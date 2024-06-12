using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.InventoryStores.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class InventoryStoreDetailDto : AuditedEntityDto<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public Guid InventoryStoreId { get; set; }


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

    public string? LocationName { get; set; }

    public Guid WarehouseId { get; set; }
    public string? WarehouseName { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public string? LotNumber { get; set; }

    /// <summary>
    /// 出库数量
    /// </summary>
    public double Quantity { get; set; }


    public InventoryStoreDetailDto()
    {
        this.ProductName = string.Empty;
        this.ProductUnit = string.Empty;
    }

}