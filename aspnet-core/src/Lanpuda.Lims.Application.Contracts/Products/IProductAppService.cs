using System;
using Lanpuda.Lims.Products.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.Products;


/// <summary>
/// 
/// </summary>
public interface IProductAppService : IApplicationService
{
    Task<ProductDto> GetAsync(Guid id);

    Task CreateAsync(ProductCreateDto input);

    Task<PagedResultDto<ProductDto>> GetPagedListAsync(ProductGetListInput input);

    Task UpdateAsync(Guid id, ProductUpdateDto input);

    Task DeleteAsync(Guid id);
}