using System;
using System.Threading.Tasks;
using Lanpuda.Lims.UsageHistories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.UsageHistories;


/// <summary>
/// 
/// </summary>
public interface IUsageHistoryAppService : IApplicationService
{
    Task<UsageHistoryDto> GetAsync(Guid id);

    Task CreateAsync(UsageHistoryCreateDto input);

    Task<PagedResultDto<UsageHistoryDto>> GetPagedListAsync(UsageHistoryGetListInput input);

    Task UpdateAsync(Guid id, UsageHistoryUpdateDto input);

    Task DeleteAsync(Guid id);
}