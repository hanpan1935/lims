using System;
using Lanpuda.Lims.Locations.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.Locations;


/// <summary>
/// 
/// </summary>
public interface ILocationAppService : IApplicationService
{
    Task<LocationDto> GetAsync(Guid id);

    Task CreateAsync(LocationCreateDto input);

    Task<PagedResultDto<LocationDto>> GetPagedListAsync(LocationGetListInput input);

    Task UpdateAsync(Guid id, LocationUpdateDto input);

    Task DeleteAsync(Guid id);
}