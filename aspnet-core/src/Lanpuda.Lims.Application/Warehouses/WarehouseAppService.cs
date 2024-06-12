using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Warehouses.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Warehouses;


/// <summary>
/// 
/// </summary>
[Authorize]
public class WarehouseAppService : LimsAppService, IWarehouseAppService
{

    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseAppService(IWarehouseRepository repository)
    {
        _warehouseRepository = repository;
    }

    [Authorize(LimsPermissions.Warehouse_Create)]
    public async Task CreateAsync(WarehouseCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        //new Warehouse and pass input to it
        var warehouse = ObjectMapper.Map<WarehouseCreateDto, Warehouse>(input);
        await _warehouseRepository.InsertAsync(warehouse);
    }

    [Authorize(LimsPermissions.Warehouse_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Warehouse warehouse = await _warehouseRepository.FindAsync(id,true);
        if (warehouse == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (warehouse.Locations != null)
        {
            if (warehouse.Locations.Count >0)
            {
                throw new UserFriendlyException("请先删除仓库对应的库位");
            }
        }

        await _warehouseRepository.DeleteAsync(warehouse);
    }

    [Authorize(LimsPermissions.Warehouse_Default)]
    public async Task<WarehouseDto> GetAsync(Guid id)
    {
        var result = await _warehouseRepository.FindAsync(id);
        return ObjectMapper.Map<Warehouse, WarehouseDto>(result);
    }

    [Authorize(LimsPermissions.Warehouse_Default)]
    public async Task<PagedResultDto<WarehouseDto>> GetPagedListAsync(WarehouseGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _warehouseRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Remark.IsNullOrWhiteSpace(), x => x.Remark.Contains(input.Remark))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<WarehouseDto>(totalCount, ObjectMapper.Map<List<Warehouse>, List<WarehouseDto>>(result));
    }

    [Authorize(LimsPermissions.Warehouse_Update)]
    public async Task UpdateAsync(Guid id, WarehouseUpdateDto input)
    {
        Warehouse warehouse = await _warehouseRepository.FindAsync(id);
        if (warehouse == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        warehouse.Name = input.Name;
        warehouse.Remark = input.Remark;
        var result = await _warehouseRepository.UpdateAsync(warehouse);
    }


    public async Task<List<WarehouseLookupDto>> LookupAsync()
    {
        var query = await _warehouseRepository.WithDetailsAsync();
        var result = await AsyncExecuter.ToListAsync(query);
        var list = ObjectMapper.Map<List<Warehouse>, List<WarehouseLookupDto>>(result);
        return list;
    }
}
