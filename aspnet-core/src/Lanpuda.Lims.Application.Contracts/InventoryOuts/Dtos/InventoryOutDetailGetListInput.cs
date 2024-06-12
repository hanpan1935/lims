using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.InventoryOuts.Dtos;

[Serializable]
public class InventoryOutDetailGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryOutDetailInventoryOutId")]
    public Guid? InventoryOutId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryOutDetailProductId")]
    public Guid? ProductId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryOutDetailLocationId")]
    public Guid? LocationId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryOutDetailLotNumber")]
    public string LotNumber { get; set; }

    /// <summary>
    /// 出库数量
    /// </summary>
    [DisplayName("InventoryOutDetailQuantity")]
    public double? Quantity { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryOutDetailSort")]
    public int? Sort { get; set; }
}