using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.InventoryLogs.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.InventoryLogs;


/// <summary>
/// 库存流水
/// </summary>
/// 
[Authorize]
public class InventoryLogAppService : LimsAppService, IInventoryLogAppService
{

    private readonly IInventoryLogRepository _inventoryLogRepository;

    public InventoryLogAppService(IInventoryLogRepository repository)
    {
        _inventoryLogRepository = repository;
    }





    //public async Task DeleteAsync(Guid id)
    //{
    //    InventoryLog inventoryLog = await _inventoryLogRepository.FindAsync(id);
    //    if (inventoryLog == null)
    //    {
    //        throw new EntityNotFoundException(L["Message:DoesNotExist"]);
    //    }
    //    await _inventoryLogRepository.DeleteAsync(inventoryLog);
    //}


    [Authorize(LimsPermissions.InventoryLog_Default)]
    public async Task<InventoryLogDto> GetAsync(Guid id)
    {
        var result = await _inventoryLogRepository.FindAsync(id);
        return ObjectMapper.Map<InventoryLog, InventoryLogDto>(result);
    }

    [Authorize(LimsPermissions.InventoryLog_Default)]
    public async Task<PagedResultDto<InventoryLogDto>> GetPagedListAsync(InventoryLogGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _inventoryLogRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.ProductId != null, x => x.ProductId == input.ProductId)
            .WhereIf(input.WarehouseId != null, x => x.Location.WarehouseId == input.WarehouseId)
            .WhereIf(input.LocationId != null, x => x.LocationId == input.LocationId)
            .WhereIf(input.LogTime != null, x => x.LogTime == input.LogTime)
            .WhereIf(!input.LotNumber.IsNullOrWhiteSpace(), x => x.LotNumber.Contains(input.LotNumber))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<InventoryLogDto>(totalCount, ObjectMapper.Map<List<InventoryLog>, List<InventoryLogDto>>(result));
    }
}
