using System;
using Lanpuda.Lims.Equipments.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.Equipments;


/// <summary>
/// 
/// </summary>
public interface IEquipmentAppService : IApplicationService
{
    Task<EquipmentDto> GetAsync(Guid id);

    Task CreateAsync(EquipmentCreateDto input);

    Task<PagedResultDto<EquipmentDto>> GetPagedListAsync(EquipmentGetListInput input);

    Task UpdateAsync(Guid id, EquipmentUpdateDto input);

    Task DeleteAsync(Guid id);
}