using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.InventoryOuts.Dtos;

[Serializable]
public class InventoryOutGetListInput : PagedAndSortedResultRequestDto
{

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryStoreNumber")]
    public string? Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("InventoryStoreReason")]
    public string? Reason { get; set; }

    /// <summary>
    /// 入库状态  false待入库  true已入库
    /// </summary>
    [DisplayName("InventoryStoreIsSuccessful")]
    public bool? IsSuccessful { get; set; }



    public Guid? ProductId { get; set; }

}