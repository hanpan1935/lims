using System;
using Lanpuda.Lims.InventoryStores.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.InventoryStores;


/// <summary>
/// 
/// </summary>
public interface IInventoryStoreAppService : IApplicationService
{
    Task<InventoryStoreDto> GetAsync(Guid id);

    Task CreateAsync(InventoryStoreCreateDto input);

    Task<PagedResultDto<InventoryStoreDto>> GetPagedListAsync(InventoryStoreGetListInput input);

    Task UpdateAsync(Guid id, InventoryStoreUpdateDto input);

    Task DeleteAsync(Guid id);
    Task StoragedAsync(Guid id);
}