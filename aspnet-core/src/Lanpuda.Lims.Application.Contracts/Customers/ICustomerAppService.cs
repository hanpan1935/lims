using System;
using Lanpuda.Lims.Customers.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.Customers;


/// <summary>
/// 
/// </summary>
public interface ICustomerAppService : IApplicationService
{
    Task<CustomerDto> GetAsync(Guid id);

    Task CreateAsync(CustomerCreateDto input);

    Task<PagedResultDto<CustomerDto>> GetPagedListAsync(CustomerGetListInput input);

    Task UpdateAsync(Guid id, CustomerUpdateDto input);

    Task DeleteAsync(Guid id);
}