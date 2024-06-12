using System;
using Lanpuda.Lims.Warehouses.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace Lanpuda.Lims.Warehouses;


/// <summary>
/// 
/// </summary>
public interface IWarehouseAppService : IApplicationService
{
    Task<WarehouseDto> GetAsync(Guid id);

    Task CreateAsync(WarehouseCreateDto input);

    Task<PagedResultDto<WarehouseDto>> GetPagedListAsync(WarehouseGetListInput input);

    Task UpdateAsync(Guid id, WarehouseUpdateDto input);

    Task DeleteAsync(Guid id);

    Task<List<WarehouseLookupDto>> LookupAsync();
}