using Lanpuda.Lims.DataDictionaries.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Lanpuda.Lims.DataDictionaries
{
    public interface IDataDictionaryAppService : IApplicationService
    {
        //Task<DataDictionaryDto> GetAsync(int id);

        Task CreateAsync(DataDictionaryCreateDto input);

        Task<List<DataDictionaryDto>> GetListAsync(DataDictionaryGetListInput input);

        Task UpdateAsync(DataDictionaryUpdateDto input);

        Task DeleteAsync(DataDictionaryDeleteDto input);

        Task<List<DicEquipmentTypeLookupDto>> LookupEquipmentTypeAsync();
        Task<List<DicProductTypeLookupDto>> LookupProductTypeAsync();
        Task<List<DicRatingTypeLookupDto>> LookupRatingTypeAsync();
        Task<List<DicSampleTypeLookupDto>> LookupSampleTypeAsync();
        Task<List<DicSamplePropertyLookupDto>> LookupSamplePropertyAsync();
        Task<List<DicStandardTypeLookupDto>> LookupStandardTypeAsync();
    }
}
