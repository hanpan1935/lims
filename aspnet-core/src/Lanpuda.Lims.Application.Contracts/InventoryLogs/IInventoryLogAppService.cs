using System;
using Lanpuda.Lims.InventoryLogs.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.InventoryLogs;


/// <summary>
/// 库存流水
/// </summary>
public interface IInventoryLogAppService : IApplicationService
{
    Task<InventoryLogDto> GetAsync(Guid id);

    Task<PagedResultDto<InventoryLogDto>> GetPagedListAsync(InventoryLogGetListInput input);

}