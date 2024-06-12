using System;
using System.Linq;
using System.Threading.Tasks;
using Lanpuda.Lims.Permissions;
using Lanpuda.Lims.InspectionTasks.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Lanpuda.Lims.Records;
using Microsoft.AspNetCore.Authorization;

namespace Lanpuda.Lims.InspectionTasks;


/// <summary>
/// 
/// </summary>
/// 
[Authorize]
public class InspectionTaskAppService : LimsAppService, IInspectionTaskAppService
{

    private readonly IInspectionTaskRepository _inspectionTaskRepository;
    private readonly IRecordDetailRepository _recordDetailRepository;
    private readonly RecordManager _recordManager;

    public InspectionTaskAppService(IInspectionTaskRepository repository, IRecordDetailRepository recordDetailRepository, RecordManager recordManager)
    {
        _inspectionTaskRepository = repository;
        _recordDetailRepository = recordDetailRepository;
        _recordManager = recordManager;
    }


    [Authorize(LimsPermissions.InspectionTask_Create)]
    public async Task CreateAsync(InspectionTaskCreateDto input)
    {
        Guid id = GuidGenerator.Create();
        //new InspectionTask and pass input to it
        var inspectionTask = new InspectionTask(id);
        inspectionTask.RecordDetailId = input.RecordDetailId;
        inspectionTask.InspectionDate = input.InspectionDate;
        inspectionTask.Priority = input.Priority;
        inspectionTask.EquipmentId = input.EquipmentId;
        inspectionTask.Inspector = input.Inspector;
        inspectionTask.Remark = input.Remark;

        await _inspectionTaskRepository.InsertAsync(inspectionTask);
    }


    [Authorize(LimsPermissions.InspectionTask_Create)]
    public async Task MultipleCreateAsync(List<InspectionTaskCreateDto> inputs)
    {
        List<InspectionTask> inspectionTasks = new List<InspectionTask>();
        foreach (var input in inputs)
        {
            Guid id = GuidGenerator.Create();
            var inspectionTask = new InspectionTask(id);
            inspectionTask.RecordDetailId = input.RecordDetailId;
            inspectionTask.InspectionDate = input.InspectionDate;
            inspectionTask.Priority = input.Priority;
            inspectionTask.EquipmentId = input.EquipmentId;
            inspectionTask.Inspector = input.Inspector;
            inspectionTask.Remark = input.Remark;
            inspectionTasks.Add(inspectionTask);
        }
        await _inspectionTaskRepository.InsertManyAsync(inspectionTasks);
    }


    [Authorize(LimsPermissions.InspectionTask_Delete)]
    public async Task DeleteAsync(Guid id)
    {
        InspectionTask inspectionTask = await _inspectionTaskRepository.FindAsync(id);
        if (inspectionTask == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        await _inspectionTaskRepository.DeleteAsync(inspectionTask);
    }


    [Authorize(LimsPermissions.InspectionTask_Default)]
    public async Task<InspectionTaskDto> GetAsync(Guid id)
    {
        var result = await _inspectionTaskRepository.FindAsync(id);
        return ObjectMapper.Map<InspectionTask, InspectionTaskDto>(result);
    }

    [Authorize]
    public async Task<PagedResultDto<InspectionTaskDto>> GetPagedListAsync(InspectionTaskGetListInput input)
    {
        if (string.IsNullOrEmpty(input.Sorting))
        {
            input.Sorting = "CreationTime" + " desc";
        }
        var query = await _inspectionTaskRepository.WithDetailsAsync();

#pragma warning disable CS8602 // 解引用可能出现空引用。
#pragma warning disable CS8604 // 引用类型参数可能为 null。
        query = query
            .WhereIf(!input.RecordNumber.IsNullOrWhiteSpace(), x => x.RecordDetail.Record.Number.Contains(input.RecordNumber))
            .WhereIf(!input.SampleNumber.IsNullOrWhiteSpace(), x => x.RecordDetail.Record.Sample.Number.Contains(input.SampleNumber))
            .WhereIf(input.ProductId != null, x => x.RecordDetail.Record.Sample.ProductId == input.ProductId)
            .WhereIf(input.InspectionItemId != null, x => x.RecordDetail.InspectionItemId == input.InspectionItemId)
            .WhereIf(input.InspectionDateStart != null, x => x.InspectionDate >= input.InspectionDateStart)
            .WhereIf(input.InspectionDateEnd != null, x => x.InspectionDate <= input.InspectionDateEnd)
            .WhereIf(input.EquipmentId != null, x => x.EquipmentId <= input.EquipmentId)
            .WhereIf(!input.Inspector.IsNullOrWhiteSpace(), x => x.Inspector.Contains(input.Inspector))
            ;
#pragma warning restore CS8604 // 引用类型参数可能为 null。
#pragma warning restore CS8602 // 解引用可能出现空引用。
        long totalCount = await AsyncExecuter.CountAsync(query);

        query = query.OrderByDescending(m => m.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);
        var result = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<InspectionTaskDto>(totalCount, ObjectMapper.Map<List<InspectionTask>, List<InspectionTaskDto>>(result));
    }

    [Authorize(LimsPermissions.InspectionTask_Update)]
    public async Task UpdateAsync(Guid id, InspectionTaskUpdateDto input)
    {
        InspectionTask inspectionTask = await _inspectionTaskRepository.FindAsync(id);
        if (inspectionTask == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        inspectionTask.RecordDetailId = input.RecordDetailId;
        inspectionTask.InspectionDate = input.InspectionDate;
        inspectionTask.Priority = input.Priority;
        inspectionTask.EquipmentId = input.EquipmentId;
        inspectionTask.Inspector = input.Inspector;
        inspectionTask.Remark = input.Remark;
        var result = await _inspectionTaskRepository.UpdateAsync(inspectionTask);
    }



    [Authorize(LimsPermissions.InspectionTask_Result)]
    public async Task UpdateResultValueAsync(Guid id, double? resultValue)
    {
        InspectionTask inspectionTask = await _inspectionTaskRepository.FindAsync(id);
        if (inspectionTask == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }
        inspectionTask.ResultValue = resultValue;

        var recordDetail = await _recordDetailRepository.GetAsync(inspectionTask.RecordDetailId);

        if (recordDetail == null)
        {
            throw new EntityNotFoundException(L["Message:DoesNotExist"]);
        }

        _recordManager.SetResultValue(recordDetail, resultValue);

        await _recordDetailRepository.UpdateAsync(recordDetail);
        await _inspectionTaskRepository.UpdateAsync(inspectionTask);
    }


    public async Task<List<InspectionTaskDashboardDto>> GetListByDateAsync(DateTime dateTime)
    {

        DateTime localDateTime = dateTime.ToLocalTime();
        DateTime startDate = new DateTime(localDateTime.Year, localDateTime.Month, localDateTime.Day);
        DateTime endDate = new DateTime(localDateTime.Year, localDateTime.Month, localDateTime.Day, 23, 59, 59);

        var query = await _inspectionTaskRepository.WithDetailsAsync();
        query = query.Where(m => m.InspectionDate >= startDate && m.InspectionDate <= endDate);
        query = query.OrderBy(m => m.EquipmentId).ThenBy(m => m.Priority);
        var result = await AsyncExecuter.ToListAsync(query);

        var groups = result.GroupBy(x => x.EquipmentId);
        List<InspectionTaskDashboardDto> dashboardDtos = new List<InspectionTaskDashboardDto>();
        foreach (var item in groups)
        {
            InspectionTaskDashboardDto dashboardDto = new InspectionTaskDashboardDto();
            dashboardDto.EquipmentId = item.Key;
            dashboardDto.EquipmentName = item.First().Equipment.Name;
            dashboardDto.Details = ObjectMapper.Map<List<InspectionTask>, List<InspectionTaskDto>>(item.ToList());
            dashboardDtos.Add(dashboardDto);
        }
        return dashboardDtos;
    }

}
