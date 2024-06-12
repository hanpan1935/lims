using System;
using Lanpuda.Lims.InspectionTasks.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace Lanpuda.Lims.InspectionTasks;


/// <summary>
/// 
/// </summary>
public interface IInspectionTaskAppService : IApplicationService
{
    Task<InspectionTaskDto> GetAsync(Guid id);

    Task CreateAsync(InspectionTaskCreateDto input);

    Task MultipleCreateAsync(List<InspectionTaskCreateDto> inputs);

    Task<PagedResultDto<InspectionTaskDto>> GetPagedListAsync(InspectionTaskGetListInput input);

    Task UpdateAsync(Guid id, InspectionTaskUpdateDto input);

    Task UpdateResultValueAsync(Guid id, double? resultValue);

    Task DeleteAsync(Guid id);

    Task<List<InspectionTaskDashboardDto>> GetListByDateAsync(DateTime dateTime);
}