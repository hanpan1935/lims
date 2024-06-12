using System;
using Lanpuda.Lims.InventoryOuts.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.InventoryOuts;


/// <summary>
/// 
/// </summary>
public interface IInventoryOutAppService : IApplicationService
{
    Task<InventoryOutDto> GetAsync(Guid id);

    Task CreateAsync(InventoryOutCreateDto input);

    Task<PagedResultDto<InventoryOutDto>> GetPagedListAsync(InventoryOutGetListInput input);

    Task UpdateAsync(Guid id, InventoryOutUpdateDto input);

    Task DeleteAsync(Guid id);

    Task OutedAsync(Guid id);
}