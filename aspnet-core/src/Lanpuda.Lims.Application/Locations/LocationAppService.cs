using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Locations.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Lanpuda.Lims.Inventories;
using Lanpuda.Lims.InventoryOuts;
using Lanpuda.Lims.InventoryLogs;
using Lanpuda.Lims.InventoryStores;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Locations;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class LocationAppService : LimsAppService, ILocationAppService
{

    private readonly ILocationRepository _locationRepository;
    private readonly IInventoryStoreDetailRepository _inventoryStoreDetailRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;
    private readonly IInventoryOutDetailRepository _inventoryOutDetailRepository;
    private readonly IInventoryRepository _inventoryRepository;

    public LocationAppService(
        ILocationRepository repository ,
        IInventoryStoreDetailRepository inventoryStoreDetailRepository,
        IInventoryLogRepository inventoryLogRepository,
        IInventoryOutDetailRepository inventoryOutDetailRepository,
        IInventoryRepository inventoryRepository
        )
    {
        _locationRepository = repository;
        _inventoryStoreDetailRepository = inventoryStoreDetailRepository;
        _inventoryLogRepository = inventoryLogRepository;
        _inventoryOutDetailRepository = inventoryOutDetailRepository;
        _inventoryRepository = inventoryRepository;
    }

    [Authorize(LimsPermissions.Location_Create)]
    public async Task CreateAsync(LocationCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        //new Location and pass input to it
        var location = ObjectMapper.Map<LocationCreateDto, Location>(input);
        await _locationRepository.InsertAsync(location);
    }

    [Authorize(LimsPermissions.Location_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Location location = await _locationRepository.FindAsync(id);
        if (location == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //判断是否存在入库单

        var queryStore = await _inventoryStoreDetailRepository.WithDetailsAsync();
        var hasStore = queryStore.Any(x => x.LocationId == id);
        if (hasStore)
        {
            throw new UserFriendlyException("已经存在此库位的入库单,请先删除应用的入库单");
        }
        //判断是否存在出库单
        var queryOut = await _inventoryOutDetailRepository.WithDetailsAsync();
        var hasOut = queryOut.Any(x => x.LocationId == id);
        if (hasOut)
        {
            throw new UserFriendlyException("已经存在此库位的出库单,请先删除应用的出库单");
        }

        //判断是否存在库存
        var queryInventory = await _inventoryRepository.WithDetailsAsync();
        var hasInventory = queryInventory.Any(x => x.LocationId == id);
        if (hasInventory)
        {
            throw new UserFriendlyException("已经存在此库位的库存,请先删除应用的库存");
        }

        //判断是否存在库存日志
        var queryLog = await _inventoryLogRepository.WithDetailsAsync();
        var hasLog = queryLog.Any(x => x.LocationId == id);
        if (hasLog)
        {
            throw new UserFriendlyException("已经存在此库位的库存日志,请先删除应用的库存日志");
        }

        await _locationRepository.DeleteAsync(location);
    }



    [Authorize(LimsPermissions.Location_Default)]
    public async Task<LocationDto> GetAsync(Guid id)
    {
        var result = await _locationRepository.FindAsync(id);
        return ObjectMapper.Map<Location, LocationDto>(result);
    }

    [Authorize(LimsPermissions.Location_Default)]
    public async Task<PagedResultDto<LocationDto>> GetPagedListAsync(LocationGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _locationRepository.WithDetailsAsync();

        query = query
            .WhereIf(input.WarehouseId != null, x => x.WarehouseId == input.WarehouseId)
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Remark.IsNullOrWhiteSpace(), x => x.Remark.Contains(input.Remark))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<LocationDto>(totalCount, ObjectMapper.Map<List<Location>, List<LocationDto>>(result));
    }

    [Authorize(LimsPermissions.Location_Update)]
    public async Task UpdateAsync(Guid id, LocationUpdateDto input)
    {
        Location location = await _locationRepository.FindAsync(id);
        if (location == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        location.WarehouseId = input.WarehouseId;
        location.Name = input.Name;
        location.Remark = input.Remark;
        var result = await _locationRepository.UpdateAsync(location);
    }
}
