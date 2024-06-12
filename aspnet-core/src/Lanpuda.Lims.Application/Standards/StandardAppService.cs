using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Standards.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Standards;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class StandardAppService : LimsAppService, IStandardAppService
{

    private readonly IStandardRepository _standardRepository;
    private readonly IStandardDetailRepository _standardDetailRepository;

    public StandardAppService(IStandardRepository repository, IStandardDetailRepository standardDetailRepository)
    {
        _standardRepository = repository;
        _standardDetailRepository = standardDetailRepository;
    }

    [Authorize(LimsPermissions.Standard_Create)]
    public async Task CreateAsync(StandardCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        //new Standard and pass input to it
        var standard = new Standard(id, input.Description)
        {
            DicStandardTypeId = input.DicStandardTypeId,
            Remark = input.Remark
        };

        for (int i = 0; i < input.Details.Count; i++)
        {
            Guid detailId = GuidGenerator.Create();
            StandardDetail detail = new StandardDetail(detailId);
            detail.StandardId = id;
            detail.InspectionItemId = input.Details[i].InspectionItemId;
            detail.MinValue = input.Details[i].MinValue;
            detail.HasMinValue = input.Details[i].HasMinValue;
            detail.MaxValue = input.Details[i].MaxValue;
            detail.HasMaxValue = input.Details[i].HasMaxValue;
            detail.Sort = i;
            standard.Details.Add(detail);
        }
        await _standardRepository.InsertAsync(standard);
    }

    [Authorize(LimsPermissions.Standard_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Standard standard = await _standardRepository.FindAsync(id);
        if (standard == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _standardRepository.DeleteAsync(standard);
    }

    [Authorize(LimsPermissions.Standard_Default)]
    public async Task<StandardDto> GetAsync(Guid id)
    {
        var result = await _standardRepository.FindAsync(id);
        return ObjectMapper.Map<Standard, StandardDto>(result);
    }

    [Authorize(LimsPermissions.Standard_Default)]
    public async Task<PagedResultDto<StandardDto>> GetPagedListAsync(StandardGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _standardRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Description.IsNullOrWhiteSpace(), x => x.Description.Contains(input.Description))
            .WhereIf(input.DicStandardTypeId != null, x => x.DicStandardTypeId == input.DicStandardTypeId)
            .WhereIf(!input.Remark.IsNullOrWhiteSpace(), x => x.Remark.Contains(input.Remark))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<StandardDto>(totalCount, ObjectMapper.Map<List<Standard>, List<StandardDto>>(result));
    }

    [Authorize(LimsPermissions.Standard_Update)]
    public async Task UpdateAsync(Guid id, StandardUpdateDto input)
    {
        Standard standard = await _standardRepository.FindAsync(id, false);
        if (standard == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        standard.Description = input.Description;
        standard.DicStandardTypeId = input.DicStandardTypeId;
        standard.Remark = input.Remark;


        List<StandardDetail> createList = new List<StandardDetail>();
        List<StandardDetail> updateList = new List<StandardDetail>();
        List<StandardDetail> deleteList = new List<StandardDetail>();
        List<StandardDetail> dbList = await _standardDetailRepository.GetListAsync(m => m.StandardId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //ÐÂ½¨
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                StandardDetail detail = new StandardDetail(detailId);
                detail.StandardId = id;
                detail.InspectionItemId = input.Details[i].InspectionItemId;
                detail.MinValue = input.Details[i].MinValue;
                detail.HasMinValue = input.Details[i].HasMinValue;
                detail.MaxValue = input.Details[i].MaxValue;
                detail.HasMaxValue = input.Details[i].HasMaxValue;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //±à¼­
            {
                StandardDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.StandardId = id;
                detail.InspectionItemId = input.Details[i].InspectionItemId;
                detail.MinValue = input.Details[i].MinValue;
                detail.HasMinValue = input.Details[i].HasMinValue;
                detail.MaxValue = input.Details[i].MaxValue;
                detail.HasMaxValue = input.Details[i].HasMaxValue;
                detail.Sort = i;
                updateList.Add(detail);
            }
        }
        //É¾³ý
        foreach (var dbItem in dbList)
        {
            bool isExsiting = input.Details.Any(m => m.Id == dbItem.Id);
            if (isExsiting == false)
            {
                deleteList.Add(dbItem);
            }
        }
        await _standardDetailRepository.InsertManyAsync(createList);
        await _standardDetailRepository.UpdateManyAsync(updateList);
        await _standardDetailRepository.DeleteManyAsync(deleteList);
        var result = await _standardRepository.UpdateAsync(standard);
    }
}
