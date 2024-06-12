using System;
using System.ComponentModel;

namespace Lanpuda.Lims.InventoryOuts.Dtos;

[Serializable]
public class InventoryOutDetailCreateDto
{

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryOutDetailProductId")]
    public Guid ProductId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryOutDetailLocationId")]
    public Guid LocationId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryOutDetailLotNumber")]
    public string? LotNumber { get; set; }

    /// <summary>
    /// 出库数量
    /// </summary>
    [DisplayName("InventoryOutDetailQuantity")]
    public double Quantity { get; set; }

  
}