using System;
using Lanpuda.Lims.Standards.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.Standards;


/// <summary>
/// 
/// </summary>
public interface IStandardAppService : IApplicationService
{
    Task<StandardDto> GetAsync(Guid id);

    Task CreateAsync(StandardCreateDto input);

    Task<PagedResultDto<StandardDto>> GetPagedListAsync(StandardGetListInput input);

    Task UpdateAsync(Guid id, StandardUpdateDto input);

    Task DeleteAsync(Guid id);
}