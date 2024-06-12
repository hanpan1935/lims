using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Records.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Lanpuda.UniqueCode;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Records;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class RecordAppService : LimsAppService, IRecordAppService
{
    private readonly IRecordRepository _recordRepository;
    private readonly IRecordDetailRepository _recordDetailRepository;
    private readonly IUniqueCodeGenerator _uniqueCodeGenerator;

    public RecordAppService(IRecordRepository repository, IUniqueCodeGenerator uniqueCodeGenerator, IRecordDetailRepository recordDetailRepository)
    {
        _recordRepository = repository;
        _uniqueCodeGenerator = uniqueCodeGenerator;
        _recordDetailRepository = recordDetailRepository;

    }

    [Authorize(LimsPermissions.Record_Create)]
    public async Task CreateAsync(RecordCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeGenerator.GetUniqueNumberAsync(LimsNumberPrefix.RecordPrefix);
        var record = new Record(id, number);
        record.SampleId = input.SampleId;
        record.Remark = input.Remark;
        record.DicRatingTypeId = input.DicRatingTypeId;

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            var detail = new RecordDetail(GuidGenerator.Create());
            detail.RecordId = id;
            detail.InspectionItemId = item.InspectionItemId;
            detail.MinValue = item.MinValue;
            detail.MaxValue = item.MaxValue;
            detail.HasMinValue = item.HasMinValue;
            detail.HasMaxValue = item.HasMaxValue;
            detail.IsQualified = item.IsQualified;
            detail.ResultValue = item.ResultValue;
            record.Details.Add(detail);
        }

        await _recordRepository.InsertAsync(record);
    }

    [Authorize(LimsPermissions.Record_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Record record = await _recordRepository.FindAsync(id);
        if (record == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _recordRepository.DeleteAsync(record);
    }


    [Authorize(LimsPermissions.Record_Default)]
    public async Task<RecordDto> GetAsync(Guid id)
    {
        var result = await _recordRepository.FindAsync(id);
        return ObjectMapper.Map<Record, RecordDto>(result);
    }


    [Authorize(LimsPermissions.Record_Default)]
    public async Task<PagedResultDto<RecordDto>> GetPagedListAsync(RecordGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _recordRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(!input.SampleNumber.IsNullOrWhiteSpace(), x => x.Sample.Number.Contains(input.SampleNumber))
            .WhereIf(input.ProductId != null, x => x.Sample.ProductId == input.ProductId)
            .WhereIf(input.DicSampleTypeId != null, x => x.Sample.DicSampleTypeId == input.DicSampleTypeId)
            .WhereIf(input.DicSamplePropertyId != null, x => x.Sample.DicSamplePropertyId == input.DicSamplePropertyId)
            .WhereIf(input.DicRatingTypeId != null, x => x.DicRatingTypeId == input.DicRatingTypeId)
            .WhereIf(input.SampleTimeStart != null, x => x.Sample.SampleTime >= input.SampleTimeStart)
            .WhereIf(input.SampleTimeEnd != null, x => x.Sample.SampleTime <= input.SampleTimeEnd)
            .WhereIf(!input.Sender.IsNullOrWhiteSpace(), x => x.Sample.Sender.Contains(input.Sender))
            .WhereIf(input.CustomerId != null, x => x.Sample.CustomerId >= input.CustomerId)
            .WhereIf(input.SupplierId != null, x => x.Sample.SupplierId <= input.SupplierId)
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<RecordDto>(totalCount, ObjectMapper.Map<List<Record>, List<RecordDto>>(result));
    }

    [Authorize(LimsPermissions.Record_Update)]
    public async Task UpdateAsync(Guid id, RecordUpdateDto input)
    {
        Record record = await _recordRepository.FindAsync(id, false);
        if (record == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        record.SampleId = input.SampleId;
        record.Remark = input.Remark;
        record.DicRatingTypeId = input.DicRatingTypeId;

        List<RecordDetail> createList = new List<RecordDetail>();
        List<RecordDetail> updateList = new List<RecordDetail>();
        List<RecordDetail> deleteList = new List<RecordDetail>();
        List<RecordDetail> dbList = await _recordDetailRepository.GetListAsync(m => m.RecordId == id);

        for (int i = 0; i < input.Details.Count; i++)
        {
            var item = input.Details[i];
            //ÐÂ½¨
            if (item.Id == null || item.Id == Guid.Empty)
            {
                Guid detailId = GuidGenerator.Create();
                RecordDetail detail = new RecordDetail(detailId);
                detail.RecordId = id;
                detail.InspectionItemId = input.Details[i].InspectionItemId;
                detail.MinValue = input.Details[i].MinValue;
                detail.HasMinValue = input.Details[i].HasMinValue;
                detail.MaxValue = input.Details[i].MaxValue;
                detail.HasMaxValue = input.Details[i].HasMaxValue;
                detail.ResultValue = input.Details[i].ResultValue;
                detail.IsQualified = input.Details[i].IsQualified;
                detail.Sort = i;
                createList.Add(detail);
            }
            else //±à¼­
            {
                RecordDetail detail = dbList.Where(m => m.Id == item.Id).First();
                detail.RecordId = id;
                detail.InspectionItemId = input.Details[i].InspectionItemId;
                detail.MinValue = input.Details[i].MinValue;
                detail.HasMinValue = input.Details[i].HasMinValue;
                detail.MaxValue = input.Details[i].MaxValue;
                detail.HasMaxValue = input.Details[i].HasMaxValue;
                detail.ResultValue = input.Details[i].ResultValue;
                detail.IsQualified = input.Details[i].IsQualified;
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

        await _recordDetailRepository.InsertManyAsync(createList);
        await _recordDetailRepository.UpdateManyAsync(updateList);
        await _recordDetailRepository.DeleteManyAsync(deleteList);

        var result = await _recordRepository.UpdateAsync(record);
    }


    [Authorize(LimsPermissions.Record_Update)]
    public async Task UpdateResultValueAsync(Guid id, RecordDetailResultValueUpdateDto input)
    {
        var detail = await _recordDetailRepository.FindAsync(id);
        if (detail == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        detail.ResultValue = input.ResultValue;
        detail.IsQualified = input.IsQualified;
        await _recordDetailRepository.UpdateAsync(detail);
    }
}
