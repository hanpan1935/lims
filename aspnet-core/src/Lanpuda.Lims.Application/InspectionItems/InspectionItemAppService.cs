using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.InspectionItems.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.InspectionItems;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class InspectionItemAppService : LimsAppService, IInspectionItemAppService
{
    private readonly IInspectionItemRepository _inspectionItemRepository;

    public InspectionItemAppService(IInspectionItemRepository repository)
    {
        _inspectionItemRepository = repository;
    }


    [Authorize(LimsPermissions.InspectionItem_Create)]
    public async Task CreateAsync(InspectionItemCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        //new InspectionItem and pass input to it
        var inspectionItem = new InspectionItem(id, input.ShortName, input.FullName, input.Basis, input.Unit);
        inspectionItem.Remark = input.Remark;
        inspectionItem.DefaultEquipmentId = input.DefaultEquipmentId;
        await _inspectionItemRepository.InsertAsync(inspectionItem);
    }

    [Authorize(LimsPermissions.InspectionItem_Delete)]

    public async Task DeleteAsync(Guid id)
    {
        InspectionItem inspectionItem = await _inspectionItemRepository.FindAsync(id);
        if (inspectionItem == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _inspectionItemRepository.DeleteAsync(inspectionItem);
    }


    /// <summary>
    /// 根据id获取实体
    /// 调用: 编辑 ,创建任务,编辑任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// 
    [Authorize(LimsPermissions.InspectionItem_Default)]
    public async Task<InspectionItemDto> GetAsync(Guid id)
    {
        var result = await _inspectionItemRepository.FindAsync(id);
        return ObjectMapper.Map<InspectionItem, InspectionItemDto>(result);
    }


    [Authorize(LimsPermissions.InspectionItem_Default)]
    public async Task<PagedResultDto<InspectionItemDto>> GetPagedListAsync(InspectionItemGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _inspectionItemRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.ShortName.IsNullOrWhiteSpace(), x => x.ShortName.Contains(input.ShortName))
            .WhereIf(!input.FullName.IsNullOrWhiteSpace(), x => x.FullName.Contains(input.FullName))
            .WhereIf(!input.Basis.IsNullOrWhiteSpace(), x => x.Basis.Contains(input.Basis))
            .WhereIf(!input.Unit.IsNullOrWhiteSpace(), x => x.Unit.Contains(input.Unit))
            .WhereIf(!input.Remark.IsNullOrWhiteSpace(), x => x.Remark.Contains(input.Remark))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<InspectionItemDto>(totalCount, ObjectMapper.Map<List<InspectionItem>, List<InspectionItemDto>>(result));
    }


    [Authorize(LimsPermissions.InspectionItem_Update)]
    public async Task UpdateAsync(Guid id, InspectionItemUpdateDto input)
    {
        InspectionItem inspectionItem = await _inspectionItemRepository.FindAsync(id);
        if (inspectionItem == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        inspectionItem.FullName = input.FullName;
        inspectionItem.ShortName = input.ShortName;
        inspectionItem.Basis = input.Basis;
        inspectionItem.Unit = input.Unit;
        inspectionItem.DefaultEquipmentId = input.DefaultEquipmentId;
        inspectionItem.Remark = input.Remark;
        var result = await _inspectionItemRepository.UpdateAsync(inspectionItem);
    }



    public async Task<List<InspectionItemDto>> GetAllAsync()
    {
        var queryable = await _inspectionItemRepository.GetQueryableAsync(); 
        queryable = queryable.OrderBy(x => x.Id);

        var result = await AsyncExecuter.ToListAsync(queryable);
        return ObjectMapper.Map<List<InspectionItem>, List<InspectionItemDto>>(result);
    }
}
