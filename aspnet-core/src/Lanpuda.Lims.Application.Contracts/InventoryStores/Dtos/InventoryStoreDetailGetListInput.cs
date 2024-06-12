using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.InventoryStores.Dtos;

[Serializable]
public class InventoryStoreDetailGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryStoreDetailInventoryStoreId")]
    public Guid? InventoryStoreId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryStoreDetailProductId")]
    public Guid? ProductId { get; set; }


    public Guid? WarehouseId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryStoreDetailLocationId")]
    public Guid? LocationId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryStoreDetailLotNumber")]
    public string? LotNumber { get; set; }

    /// <summary>
    /// 出库数量
    /// </summary>
    [DisplayName("InventoryStoreDetailQuantity")]
    public double? Quantity { get; set; }

}