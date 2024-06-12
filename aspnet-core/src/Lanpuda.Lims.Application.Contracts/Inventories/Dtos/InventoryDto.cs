using System;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.Inventories.Dtos;

/// <summary>
/// 
/// </summary>
[Serializable]
public class InventoryDto : AuditedEntityDto<Guid>
{

    public string? WarehouseName { get; set; }
    public string? LocationName { get; set; }
    public string? ProductName { get; set; }
    public string? ProductUnit { get; set; }

    public string? ProductSpec { get; set; }

    /// <summary>
    /// 库位Id
    /// </summary>
    public Guid LocationId { get; set; }

    /// <summary>
    /// 产品ID
    /// </summary>
    public Guid ProductId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public double Quantity { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? LotNumber { get; set; }

}