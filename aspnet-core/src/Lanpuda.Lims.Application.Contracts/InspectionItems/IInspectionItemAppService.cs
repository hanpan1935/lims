using System;
using Lanpuda.Lims.InspectionItems.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Collections.Generic;

namespace Lanpuda.Lims.InspectionItems;


/// <summary>
/// 
/// </summary>
public interface IInspectionItemAppService : IApplicationService
{
    Task<InspectionItemDto> GetAsync(Guid id);

    Task CreateAsync(InspectionItemCreateDto input);

    Task<PagedResultDto<InspectionItemDto>> GetPagedListAsync(InspectionItemGetListInput input);

    Task UpdateAsync(Guid id, InspectionItemUpdateDto input);

    Task DeleteAsync(Guid id);
    Task<List<InspectionItemDto>> GetAllAsync();
}