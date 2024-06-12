using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Inventories.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Inventories;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class InventoryAppService : LimsAppService, IInventoryAppService
{

    private readonly IInventoryRepository _inventoryRepository;

    public InventoryAppService(IInventoryRepository repository)
    {
        _inventoryRepository = repository;
    }

    [Authorize(LimsPermissions.InspectionTask_Default)]
    public async Task<InventoryDto> GetAsync(Guid id)
    {
        var result = await _inventoryRepository.FindAsync(id);
        return ObjectMapper.Map<Inventory, InventoryDto>(result);
    }


    [Authorize(LimsPermissions.InspectionTask_Default)]
    public async Task<PagedResultDto<InventoryDto>> GetPagedListAsync(InventoryGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _inventoryRepository.WithDetailsAsync();

        query = query
            .WhereIf(input.WarehouseId != null, x => x.Location.WarehouseId == input.WarehouseId)
            .WhereIf(input.LocationId != null, x => x.LocationId == input.LocationId)
            .WhereIf(input.ProductId != null, x => x.ProductId == input.ProductId)
            .WhereIf(!input.LotNumber.IsNullOrWhiteSpace(), x => x.LotNumber.Contains(input.LotNumber))
            .WhereIf(!input.ProductName.IsNullOrWhiteSpace(), x => x.Product.Name.Contains(input.ProductName))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<InventoryDto>(totalCount, ObjectMapper.Map<List<Inventory>, List<InventoryDto>>(result));
    }
    
}
