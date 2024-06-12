using System;
using Lanpuda.Lims.Records.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.Records;

/// <summary>
/// 
/// </summary>
public interface IRecordAppService : IApplicationService
{
    Task<RecordDto> GetAsync(Guid id);

    Task CreateAsync(RecordCreateDto input);

    Task<PagedResultDto<RecordDto>> GetPagedListAsync(RecordGetListInput input);

    Task UpdateAsync(Guid id, RecordUpdateDto input);

    Task DeleteAsync(Guid id);

    Task UpdateResultValueAsync(Guid id, RecordDetailResultValueUpdateDto input);
}