using System;
using Lanpuda.Lims.Repairs.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.Repairs;


/// <summary>
/// 
/// </summary>
public interface IRepairAppService : IApplicationService
{
    Task<RepairDto> GetAsync(Guid id);

    Task CreateAsync(RepairCreateDto input);

    Task<PagedResultDto<RepairDto>> GetPagedListAsync(RepairGetListInput input);

    Task UpdateAsync(Guid id, RepairUpdateDto input);

    Task DeleteAsync(Guid id);
}