using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.InventoryOuts.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Volo.Abp;
using Lanpuda.Lims.InventoryLogs;
using Lanpuda.Lims.Inventories;
using Lanpuda.UniqueCode;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.InventoryOuts;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class InventoryOutAppService : LimsAppService, IInventoryOutAppService
{
    private readonly IUniqueCodeGenerator _uniqueCodeGenerator;
    private readonly IInventoryOutRepository _inventoryOutRepository;
    private readonly IInventoryOutDetailRepository _inventoryOutDetailRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IInventoryLogRepository _inventoryLogRepository;


    public InventoryOutAppService(
        IUniqueCodeGenerator uniqueCodeGenerator,
        IInventoryOutRepository repository, 
        IInventoryRepository inventoryRepository, 
        IInventoryLogRepository inventoryLogRepository,
        IInventoryOutDetailRepository inventoryOutDetailRepository
        )
    {
        _inventoryOutRepository = repository;
        _inventoryRepository = inventoryRepository;
        _inventoryLogRepository = inventoryLogRepository;
        _uniqueCodeGenerator = uniqueCodeGenerator;
        _inventoryOutDetailRepository = inventoryOutDetailRepository;
    }

    [Authorize(LimsPermissions.InventoryOut_Create)]
    public async Task CreateAsync(InventoryOutCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeGenerator.GetUniqueNumberAsync(LimsNumberPrefix.InventoryOutPrefix);
        var inventoryOut = new InventoryOut(id,number);
        inventoryOut.Reason = input.Reason;
        inventoryOut.Remark = input.Remark;
        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            InventoryOutDetail detail = new InventoryOutDetail(GuidGenerator.Create());
            detail.InventoryOutId = id;
            detail.ProductId = item.ProductId;
            detail.LocationId = item.LocationId;
            detail.LotNumber = item.LotNumber;
            detail.Quantity = item.Quantity;
            detail.Sort = i;
            inventoryOut.Details.Add(detail);
        }
        await _inventoryOutRepository.InsertAsync(inventoryOut);
    }


    [Authorize(LimsPermissions.InventoryOut_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        InventoryOut inventoryOut = await _inventoryOutRepository.FindAsync(id);
        if (inventoryOut == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }


        if (inventoryOut.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库，无法删除！");
        }


        await _inventoryOutRepository.DeleteAsync(inventoryOut);
    }

    [Authorize(LimsPermissions.InventoryOut_Default)]
    public async Task<InventoryOutDto> GetAsync(Guid id)
    {
        var result = await _inventoryOutRepository.FindAsync(id);
        return ObjectMapper.Map<InventoryOut, InventoryOutDto>(result);
    }

    [Authorize(LimsPermissions.InventoryOut_Default)]
    public async Task<PagedResultDto<InventoryOutDto>> GetPagedListAsync(InventoryOutGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _inventoryOutRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(!input.Reason.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Reason))
            .WhereIf(input.IsSuccessful != null, x => x.IsSuccessful == input.IsSuccessful)
            .WhereIf(input.ProductId != null, x => x.Details.Any(a =>a.ProductId == input.ProductId) )
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<InventoryOutDto>(totalCount, ObjectMapper.Map<List<InventoryOut>, List<InventoryOutDto>>(result));
    }

    [Authorize(LimsPermissions.InventoryOut_Update)]
    public async Task UpdateAsync(Guid id, InventoryOutUpdateDto input)
    {
        InventoryOut inventoryOut = await _inventoryOutRepository.FindAsync(id);
        if (inventoryOut == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (inventoryOut.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库！无法编辑");
        }

        inventoryOut.Reason = input.Reason;
        inventoryOut.Remark = input.Remark;

        List<InventoryOutDetail> createList = new List<InventoryOutDetail>();
        List<InventoryOutDetail> updateList = new List<InventoryOutDetail>();
        List<InventoryOutDetail> deleteList = new List<InventoryOutDetail>();
        List<InventoryOutDetail> dbList = await _inventoryOutDetailRepository.GetListAsync(m => m.InventoryOutId == id);


        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                InventoryOutDetail detail = new InventoryOutDetail(detailId);
                detail.InventoryOutId = id;
                detail.ProductId = item.ProductId;
                detail.LocationId = item.LocationId;
                detail.LotNumber = item.LotNumber;
                detail.Quantity = item.Quantity;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //编辑
            {
                InventoryOutDetail detail = dbList.Where(m => m.Id == item.Id).First();
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
        var result = await _inventoryOutRepository.UpdateAsync(inventoryOut);
        await _inventoryOutDetailRepository.InsertManyAsync(createList);
        await _inventoryOutDetailRepository.UpdateManyAsync(updateList);
        await _inventoryOutDetailRepository.DeleteManyAsync(deleteList);
    }


    [Authorize(LimsPermissions.InventoryOut_Out)]
    public async Task OutedAsync(Guid id)
    {
        InventoryOut inventoryOut = await _inventoryOutRepository.GetAsync(id, true);

        if (inventoryOut == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        if (inventoryOut.IsSuccessful == true)
        {
            throw new UserFriendlyException("已经出库");
        }


        if (inventoryOut.Details == null || inventoryOut.Details.Count == 0)
        {
            throw new UserFriendlyException("明细不能为空");
        }

        foreach (var item in inventoryOut.Details)
        {
            if (item.Quantity <= 0)
            {
                throw new UserFriendlyException("数量必须大于0");
            }
        }

        inventoryOut.IsSuccessful = true;
        inventoryOut.SuccessfulTime = Clock.Now;

        foreach (var item in inventoryOut.Details)
        {
            InventoryLog inventoryLog = new InventoryLog(GuidGenerator.Create());
            inventoryLog.Number = inventoryOut.Number;
            inventoryLog.ProductId = item.ProductId;
            inventoryLog.LocationId = item.LocationId;
            inventoryLog.LogTime = Clock.Now;
            inventoryLog.LotNumber = item.LotNumber;
            inventoryLog.Reason = inventoryOut.Reason;
            inventoryLog.OutQuantity = item.Quantity;
            double afterQuantity = await _inventoryRepository.OutAsync(item.LocationId, item.ProductId, item.Quantity, item.LotNumber);
            inventoryLog.AfterQuantity = afterQuantity;
            await _inventoryLogRepository.InsertAsync(inventoryLog);
        }
        
    }
}
