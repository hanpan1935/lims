using System;
using Lanpuda.Lims.Samples.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace Lanpuda.Lims.Samples;


/// <summary>
/// 
/// </summary>
public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync(Guid id);

    Task CreateAsync(SampleCreateDto input);

    Task<PagedResultDto<SampleDto>> GetPagedListAsync(SampleGetListInput input);

    Task UpdateAsync(Guid id, SampleUpdateDto input);

    Task DeleteAsync(Guid id);

    Task<List<SampleCountAnalysisDto>> GetSampleCountAsync();
}