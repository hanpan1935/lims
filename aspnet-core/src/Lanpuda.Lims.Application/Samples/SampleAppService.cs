using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.Samples.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Lanpuda.Lims.Records;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Lanpuda.UniqueCode;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.Samples;

[Authorize]
public class SampleAppService : LimsAppService, ISampleAppService
{

    private readonly ISampleRepository _sampleRepository;
    private readonly IUniqueCodeGenerator _uniqueCodeGenerator;
    private readonly IRecordRepository _recordRepository;


    public SampleAppService(ISampleRepository repository, IUniqueCodeGenerator uniqueCodeGenerator, IRecordRepository recordRepository)
    {
        _sampleRepository = repository;
        _uniqueCodeGenerator = uniqueCodeGenerator;
        _recordRepository = recordRepository;
    }

    [Authorize(LimsPermissions.Sample_Create)]
    public async Task CreateAsync(SampleCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        string number = await _uniqueCodeGenerator.GetUniqueNumberAsync(LimsNumberPrefix.SamplePrefix);
        Sample sample = new Sample(id, number);
        sample.ProductId = input.ProductId;
        sample.DicSampleTypeId = input.DicSampleTypeId;
        sample.DicSamplePropertyId = input.DicSamplePropertyId;
        sample.SampleTime = input.SampleTime;
        sample.SampleCount = input.SampleCount;
        sample.ExpireTime = input.ExpireTime;
        sample.Sender = input.Sender;
        sample.Remark = input.Remark;
        sample.CustomerId = input.CustomerId;
        sample.SupplierId = input.SupplierId;

        await _sampleRepository.InsertAsync(sample);
    }

    [Authorize(LimsPermissions.Sample_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        Sample sample = await _sampleRepository.FindAsync(id);
        if (sample == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        //判断是否存在关联数据
        var has = await _recordRepository.AnyAsync(x => x.SampleId == id);

        if (has)
        {
            throw new UserFriendlyException("无法删除,请先删除对应的检测数据!");
        }

        await _sampleRepository.DeleteAsync(sample);
    }

    [Authorize(LimsPermissions.Sample_Default)]
    public async Task<SampleDto> GetAsync(Guid id)
    {
        var result = await _sampleRepository.FindAsync(id);
        return ObjectMapper.Map<Sample, SampleDto>(result);
    }

    [Authorize(LimsPermissions.Sample_Default)]
    public async Task<PagedResultDto<SampleDto>> GetPagedListAsync(SampleGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _sampleRepository.WithDetailsAsync();

        query = query
            .WhereIf(!input.Number.IsNullOrWhiteSpace(), x => x.Number.Contains(input.Number))
            .WhereIf(input.ProductId != null, x => x.ProductId == input.ProductId)
            .WhereIf(input.DicSampleTypeId != null, x => x.DicSampleTypeId == input.DicSampleTypeId)
            .WhereIf(input.DicSamplePropertyId != null, x => x.DicSamplePropertyId == input.DicSamplePropertyId)
            .WhereIf(input.SampleTime != null, x => x.SampleTime == input.SampleTime)
            .WhereIf(input.SampleCount != null, x => x.SampleCount == input.SampleCount)
            .WhereIf(input.ExpireTime != null, x => x.ExpireTime == input.ExpireTime)
            .WhereIf(!input.Sender.IsNullOrWhiteSpace(), x => x.Sender.Contains(input.Sender))
            .WhereIf(!input.Remark.IsNullOrWhiteSpace(), x => x.Remark.Contains(input.Remark))
            ;
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<SampleDto>(totalCount, ObjectMapper.Map<List<Sample>, List<SampleDto>>(result));
    }

    [Authorize(LimsPermissions.Sample_Update)]
    public async Task UpdateAsync(Guid id, SampleUpdateDto input)
    {
        Sample sample = await _sampleRepository.FindAsync(id);
        if (sample == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        sample.ProductId = input.ProductId;
        sample.DicSampleTypeId = input.DicSampleTypeId;
        sample.DicSamplePropertyId = input.DicSamplePropertyId;
        sample.SampleTime = input.SampleTime;
        sample.SampleCount = input.SampleCount;
        sample.ExpireTime = input.ExpireTime;
        sample.Sender = input.Sender;
        sample.Remark = input.Remark;
        sample.CustomerId = input.CustomerId;
        sample.SupplierId = input.SupplierId;
        var result = await _sampleRepository.UpdateAsync(sample);
    }


    //首页来样数量

    [Authorize]
    public async Task<List<SampleCountAnalysisDto>> GetSampleCountAsync()
    {
        int countOfDay = 7;
        var endDate = DateTime.Now;
        var startDate = new DateTime(endDate.AddDays(-countOfDay).Year, endDate.AddDays(-countOfDay).Month, endDate.AddDays(-countOfDay).Day);

        var query = await _sampleRepository.GetQueryableAsync();
        query = query.Where(m => m.SampleTime <= endDate && m.SampleTime > startDate);

        var sampleList = await AsyncExecuter.ToListAsync(query);

        var groups = sampleList.GroupBy(m => m.SampleTime.Date).Select(
            g => new
            {
                Key = g.Key,
                Total = g.Sum(m => m.SampleCount),
                Date = g.First().SampleTime.ToShortDateString(),
            }
            ).ToList();

        List<SampleCountAnalysisDto> resultList = new List<SampleCountAnalysisDto>();

        for (int i = 0; i < countOfDay; i++)
        {
            SampleCountAnalysisDto dto = new SampleCountAnalysisDto();
            dto.SampleDate = new DateTime(startDate.AddDays(i).Year, startDate.AddDays(i).Month, startDate.AddDays(i).Day);

            var group = groups.Where(m => m.Key == dto.SampleDate).FirstOrDefault();
            if (group == null)
            {
                dto.SampleCount = 0;
            }
            else
            {
                if (group.Total != null)
                {
                    dto.SampleCount = (double)group.Total;
                }
                else { dto.SampleCount = 0; }
            }

            resultList.Add(dto);
        }

        return resultList;
    }


}
