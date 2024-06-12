using System;
using System.Threading.Tasks;
using Lanpuda.Lims.Calibrations.Dtos;
using Lanpuda.Lims.Products.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.Calibrations;


/// <summary>
/// 
/// </summary>
public interface ICalibrationAppService : IApplicationService
{
    Task<CalibrationDto> GetAsync(Guid id);

    Task CreateAsync(CalibrationCreateDto input);

    Task<PagedResultDto<CalibrationDto>> GetPagedListAsync(CalibrationGetListInput input);

    Task UpdateAsync(Guid id, CalibrationUpdateDto input);

    Task DeleteAsync(Guid id);
}