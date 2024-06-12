using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Inventories.Dtos;

[Serializable]
public class InventoryGetListInput : PagedAndSortedResultRequestDto
{


    public Guid? WarehouseId { get; set; }


    /// <summary>
    /// 库位Id
    /// </summary>
    [DisplayName("InventoryLocationId")]
    public Guid? LocationId { get; set; }


    /// <summary>
    /// 产品ID
    /// </summary>
    [DisplayName("InventoryProductId")]
    public Guid? ProductId { get; set; }

    public string? ProductName { get; set; }


    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryLotNumber")]
    public string? LotNumber { get; set; }

}