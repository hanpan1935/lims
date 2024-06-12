using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.InventoryStores.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Lanpuda.Lims.Inventories;
using Lanpuda.Lims.InventoryLogs;
using Volo.Abp;
using Lanpuda.UniqueCode;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.InventoryStores;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class InventoryStoreAppService : LimsAppService, IInventoryStoreAppService
{

    private readonly IInventoryStoreRepository _inventoryStoreRepository;
    private readonly IInventoryStoreDetailRepository _inventoryStoreDetailRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;
    private readonly IUniqueCodeGenerator _uniqueCodeGenerator;

    public InventoryStoreAppService(
        IInventoryStoreRepository repository, 
        IInventoryRepository inventoryRepository, 
        IInventoryLogRepository inventoryLogRepository,
        IUniqueCodeGenerator uniqueCodeGenerator,
        IInventoryStoreDetailRepository inventoryStoreDetailRepository
        )
    {
        _inventoryStoreRepository = repository;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
        _uniqueCodeGenerator = uniqueCodeGenerator;
        _inventoryStoreDetailRepository = inventoryStoreDetailRepository;
    }

    [Authorize(LimsPermissions.InventoryStore_Create)]
    public async Task CreateAsync(InventoryStoreCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeGenerator.GetUniqueNumberAsync(LimsNumberPrefix.InventoryInPrefix);
        var inventoryStore = new InventoryStore(id,number);
        inventoryStore.Reason = input.Reason;
        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            InventoryStoreDetail detail = new InventoryStoreDetail(GuidGenerator.Create());
            detail.InventoryStoreId = id;
            detail.ProductId = item.ProductId;
            detail.LocationId = item.LocationId;
            detail.LotNumber = item.LotNumber;
            detail.Quantity = item.Quantity;
            detail.Sort = i;
            inventoryStore.Details.Add(detail);
        }
        await _inventoryStoreRepository.InsertAsync(inventoryStore);
    }

    [Authorize(LimsPermissions.InventoryStore_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        InventoryStore inventoryStore = await _inventoryStoreRepository.FindAsync(id);
        if (inventoryStore == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (inventoryStore.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库，无法删除！");
        }

        await _inventoryStoreRepository.DeleteAsync(inventoryStore);
    }

    [Authorize(LimsPermissions.InventoryStore_Default)]
    public async Task<InventoryStoreDto> GetAsync(Guid id)
    {
        var result = await _inventoryStoreRepository.FindAsync(id);
        return ObjectMapper.Map<InventoryStore, InventoryStoreDto>(result);
    }

    [Authorize(LimsPermissions.InventoryStore_Default)]
    public async Task<PagedResultDto<InventoryStoreDto>> GetPagedListAsync(InventoryStoreGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _inventoryStoreRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(!input.Reason.IsNullOrWhiteSpace(), x => x.Reason.Contains(input.Reason))
            .WhereIf(input.IsSuccessful != null, x => x.IsSuccessful == input.IsSuccessful)
            ;

        if (input.ProductId != null)
        {
            query = query.Where(m => m.Details.Any(p => p.ProductId == input.ProductId));
        }


        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<InventoryStoreDto>(totalCount, ObjectMapper.Map<List<InventoryStore>, List<InventoryStoreDto>>(result));
    }

    [Authorize(LimsPermissions.InventoryStore_Update)]
    public async Task UpdateAsync(Guid id, InventoryStoreUpdateDto input)
    {
        InventoryStore inventoryStore = await _inventoryStoreRepository.FindAsync(id);
        if (inventoryStore == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        //

        if (inventoryStore.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库！无法编辑");
        }


        inventoryStore.Reason = input.Reason;
        inventoryStore.Remark = input.Remark;

        List<InventoryStoreDetail> createList = new List<InventoryStoreDetail>();
        List<InventoryStoreDetail> updateList = new List<InventoryStoreDetail>();
        List<InventoryStoreDetail> deleteList = new List<InventoryStoreDetail>();
        List<InventoryStoreDetail> dbList = await _inventoryStoreDetailRepository.GetListAsync(m => m.InventoryStoreId == id);



        for (int i = 0; i < input.Details.Count; i++)
        {
            var item    = input.Details[i];
            //新建
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                InventoryStoreDetail detail = new InventoryStoreDetail(detailId);
                detail.InventoryStoreId = id;
                detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.LotNumber = item.LotNumber;
                detail.Quantity = item.Quantity;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                InventoryStoreDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.LotNumber = item.LotNumber;
                detail.Quantity = item.Quantity;
                detail.Sort = i;
                updateList.Add(detail);
            }
        }
      
        //删除
        foreach (var dbItem in dbList)
        {
            bool isExsiting = input.Details.Any(m => m.Id == dbItem.Id);
            if (isExsiting == false)
            {
                deleteList.Add(dbItem);
            }
        }
        var result = await _inventoryStoreRepository.UpdateAsync(inventoryStore);
        await _inventoryStoreDetailRepository.InsertManyAsync(createList);
        await _inventoryStoreDetailRepository.UpdateManyAsync(updateList);
        await _inventoryStoreDetailRepository.DeleteManyAsync(deleteList);
    }

    [Authorize(LimsPermissions.InventoryStore_Store)]
    public async Task StoragedAsync(Guid id)
    {
        var entity = await _inventoryStoreRepository.GetAsync(id, true);

        if (entity == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (entity.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经入库了，不要重复入库");
        }

        //行项目部能为空
        if (entity.Details == null || entity.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }


        foreach (var item in entity.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }

        entity.IsSuccessful = true; ;
        entity.SuccessfulTime = Clock.Now;

        foreach (var item in entity.Details)
        {
            var inventory = await _inventoryRepository.StorageAsync(
                locationId: item.LocationId,
                productId: item.ProductId,
                quantity: item.Quantity,
                lotNumber: item.LotNumber
                );
            InventoryLog inventoryLog = new InventoryLog(GuidGenerator.Create());
            inventoryLog.Number = entity.Number;
            inventoryLog.ProductId = item.ProductId;
            inventoryLog.LocationId = item.LocationId;
            inventoryLog.LogTime = Clock.Now;
            inventoryLog.LotNumber = item.LotNumber;
            inventoryLog.Reason = entity.Reason;
            inventoryLog.InQuantity = item.Quantity;
            inventoryLog.OutQuantity = 0;
            inventoryLog.AfterQuantity = inventory.Quantity;
            await _inventoryLogRepository.InsertAsync(inventoryLog);
            await _inventoryStoreRepository.UpdateAsync(entity);
        }
    }
}
