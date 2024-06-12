using System;
using Lanpuda.Lims.Maintenances.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.Maintenances;


/// <summary>
/// 
/// </summary>
public interface IMaintenanceAppService : IApplicationService
{
    Task<MaintenanceDto> GetAsync(Guid id);

    Task CreateAsync(MaintenanceCreateDto input);

    Task<PagedResultDto<MaintenanceDto>> GetPagedListAsync(MaintenanceGetListInput input);

    Task UpdateAsync(Guid id, MaintenanceUpdateDto input);

    Task DeleteAsync(Guid id);
}