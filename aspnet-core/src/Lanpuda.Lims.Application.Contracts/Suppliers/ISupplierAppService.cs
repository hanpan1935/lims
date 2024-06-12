using System;
using Lanpuda.Lims.Suppliers.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.Suppliers;


/// <summary>
/// 
/// </summary>
public interface ISupplierAppService : IApplicationService
{
    Task<SupplierDto> GetAsync(Guid id);

    Task CreateAsync(SupplierCreateDto input);

    Task<PagedResultDto<SupplierDto>> GetPagedListAsync(SupplierGetListInput input);

    Task UpdateAsync(Guid id, SupplierUpdateDto input);

    Task DeleteAsync(Guid id);
}