using System;
using System.ComponentModel;

namespace Lanpuda.Lims.InventoryStores.Dtos;

[Serializable]
public class InventoryStoreDetailCreateDto
{

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryStoreDetailProductId")]
    public Guid ProductId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryStoreDetailLocationId")]
    public Guid LocationId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryStoreDetailLotNumber")]
    public string? LotNumber { get; set; }

    /// <summary>
    /// 出库数量
    /// </summary>
    [DisplayName("InventoryStoreDetailQuantity")]
    public double Quantity { get; set; }

}