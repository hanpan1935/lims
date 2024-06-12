using System;
using System.Threading.Tasks;
using Lanpuda.Lims.Inventories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.Inventories;


/// <summary>
/// 
/// </summary>
public interface IInventoryAppService : IApplicationService
{
    Task<InventoryDto> GetAsync(Guid id);


    Task<PagedResultDto<InventoryDto>> GetPagedListAsync(InventoryGetListInput input);

}