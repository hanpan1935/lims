using Lanpuda.Lims.Calibrations.Dtos;
using Lanpuda.Lims.Calibrations;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.Lims.DataDictionaries
{
    [Authorize]
    public class DataDictionaryAppService : LimsAppService, IDataDictionaryAppService
    {
        private readonly IDicEquipmentTypeRepository _dicEquipmentTypeRepository;
        private readonly IDicStandardTypeRepository _dicStandardTypeRepository;
        private readonly IDicProductTypeRepository _dicProductTypeRepository;
        private readonly IDicSampleTypeRepository _dicSampleTypeRepository;
        private readonly IDicSamplePropertyRepository _dicSamplePropertyRepository;
        private readonly IDicRatingTypeRepository _dicRatingTypeRepository;
        public DataDictionaryAppService(
               IDicEquipmentTypeRepository dicEquipmentTypeRepository,
               IDicStandardTypeRepository dicStandardTypeRepository,
               IDicProductTypeRepository dicProductTypeRepository,
               IDicSampleTypeRepository dicSampleTypeRepository,
               IDicSamplePropertyRepository dicSamplePropertyRepository,
               IDicRatingTypeRepository dicRatingTypeRepository)
        {
            _dicEquipmentTypeRepository = dicEquipmentTypeRepository;
            _dicStandardTypeRepository = dicStandardTypeRepository;
            _dicProductTypeRepository = dicProductTypeRepository;
            _dicSampleTypeRepository = dicSampleTypeRepository;
            _dicSamplePropertyRepository = dicSamplePropertyRepository;
            _dicRatingTypeRepository = dicRatingTypeRepository;
        }


        [Authorize(LimsPermissions.DictionaryData_Create)]
        public async Task CreateAsync(DataDictionaryCreateDto input)
        {
            if (input.Type == DataDictionaryType.DicEquipmentType)
            {
                DicEquipmentType entity = new DicEquipmentType(input.DisplayValue);
                entity.Sort = input.Sort;
                await _dicEquipmentTypeRepository.InsertAsync(entity);
            }
            else if (input.Type == DataDictionaryType.DicProductType)
            {
                DicProductType entity = new DicProductType(input.DisplayValue);
                entity.Sort = input.Sort;
                await _dicProductTypeRepository.InsertAsync(entity);
            }
            else if (input.Type == DataDictionaryType.DicRatingType)
            {
                DicRatingType entity = new DicRatingType(input.DisplayValue);
                entity.Sort = input.Sort;
                await _dicRatingTypeRepository.InsertAsync(entity);
            }
            else if (input.Type == DataDictionaryType.DicSampleProperty)
            {
                DicSampleProperty entity = new DicSampleProperty(input.DisplayValue);
                entity.Sort = input.Sort;
                await _dicSamplePropertyRepository.InsertAsync(entity);
            }
            else if (input.Type == DataDictionaryType.DicSampleType)
            {
                DicSampleType entity = new DicSampleType(input.DisplayValue);
                entity.Sort = input.Sort;
                await _dicSampleTypeRepository.InsertAsync(entity);
            }
            else if (input.Type == DataDictionaryType.DicStandardType)
            {
                DicStandardType entity = new DicStandardType(input.DisplayValue);
                entity.Sort = input.Sort;
                await _dicStandardTypeRepository.InsertAsync(entity);
            }
        }


        [Authorize(LimsPermissions.DictionaryData_Delete)]
        public async Task DeleteAsync(DataDictionaryDeleteDto input)
        {
            if (input.Type == DataDictionaryType.DicEquipmentType)
            {
                await _dicEquipmentTypeRepository.DeleteAsync(input.Id);
            }
            else if (input.Type == DataDictionaryType.DicProductType)
            {
                await _dicProductTypeRepository.DeleteAsync(input.Id);
            }
            else if (input.Type == DataDictionaryType.DicRatingType)
            {
                await _dicRatingTypeRepository.DeleteAsync(input.Id);
            }
            else if (input.Type == DataDictionaryType.DicSampleProperty)
            {
                await _dicSamplePropertyRepository.DeleteAsync(input.Id);
            }
            else if (input.Type == DataDictionaryType.DicSampleType)
            {
                await _dicSampleTypeRepository.DeleteAsync(input.Id);
            }
            else if (input.Type == DataDictionaryType.DicStandardType)
            {
                await _dicStandardTypeRepository.DeleteAsync(input.Id);
            }
        }


        [Authorize(LimsPermissions.DictionaryData_Default)]
        public async Task<List<DataDictionaryDto>> GetListAsync(DataDictionaryGetListInput input)
        {
            List<DataDictionaryDto> resultList = new List<DataDictionaryDto>();

            if (input.Type == DataDictionaryType.DicEquipmentType)
            {
                var query = await _dicEquipmentTypeRepository.WithDetailsAsync();
                query = query
                    .WhereIf(!input.DisplayValue.IsNullOrWhiteSpace(), x => x.DisplayValue.Contains(input.DisplayValue))
                    ;
                query = query.OrderBy(m => m.Sort);
                var result = await AsyncExecuter.ToListAsync(query);

                foreach (var item in result)
                {
                    DataDictionaryDto dataDictionaryDto = new DataDictionaryDto();
                    dataDictionaryDto.Id = item.Id;
                    dataDictionaryDto.DisplayValue = item.DisplayValue;
                    dataDictionaryDto.Sort = item.Sort;
                    dataDictionaryDto.Type = DataDictionaryType.DicEquipmentType;
                    dataDictionaryDto.CreationTime = item.CreationTime;
                    dataDictionaryDto.CreatorId = item.CreatorId;
                    dataDictionaryDto.LastModifierId = item.LastModifierId;
                    dataDictionaryDto.LastModificationTime = item.LastModificationTime;
                    if (item.Creator !=null)
                    {
                        dataDictionaryDto.CreatorSurname = item.Creator.Surname;
                        dataDictionaryDto.CreatorName = item.Creator.Name;
                    }
                    if (item.LastModifier !=null)
                    {
                        dataDictionaryDto.LastModifierSurname = item.LastModifier.Surname;
                        dataDictionaryDto.LastModifierName = item.LastModifier.Name;
                    }

                    resultList.Add(dataDictionaryDto);
                }
            }
            else if (input.Type == DataDictionaryType.DicProductType)
            {
                var query = await _dicProductTypeRepository.WithDetailsAsync();
                query = query
                    .WhereIf(!input.DisplayValue.IsNullOrWhiteSpace(), x => x.DisplayValue.Contains(input.DisplayValue))
                    ;
                long totalCount = await AsyncExecuter.CountAsync(query);
                query = query.OrderBy(m => m.Sort);
                var result = await AsyncExecuter.ToListAsync(query);
                foreach (var item in result)
                {
                    DataDictionaryDto dataDictionaryDto = new DataDictionaryDto();
                    dataDictionaryDto.Id = item.Id;
                    dataDictionaryDto.DisplayValue = item.DisplayValue;
                    dataDictionaryDto.Sort = item.Sort;
                    dataDictionaryDto.Type = DataDictionaryType.DicProductType;

                    dataDictionaryDto.CreationTime = item.CreationTime;
                    dataDictionaryDto.CreatorId = item.CreatorId;
                    dataDictionaryDto.LastModifierId = item.LastModifierId;
                    dataDictionaryDto.LastModificationTime = item.LastModificationTime;
                    if (item.Creator != null)
                    {
                        dataDictionaryDto.CreatorSurname = item.Creator.Surname;
                        dataDictionaryDto.CreatorName = item.Creator.Name;
                    }
                    if (item.LastModifier != null)
                    {
                        dataDictionaryDto.LastModifierSurname = item.LastModifier.Surname;
                        dataDictionaryDto.LastModifierName = item.LastModifier.Name;
                    }
                    resultList.Add(dataDictionaryDto);
                }
            }
            else if (input.Type == DataDictionaryType.DicRatingType)
            {
                var query = await _dicRatingTypeRepository.WithDetailsAsync();
                query = query
                    .WhereIf(!input.DisplayValue.IsNullOrWhiteSpace(), x => x.DisplayValue.Contains(input.DisplayValue))
                    ;
                long totalCount = await AsyncExecuter.CountAsync(query);
                query = query.OrderBy(m => m.Sort);
                var result = await AsyncExecuter.ToListAsync(query);
                foreach (var item in result)
                {
                    DataDictionaryDto dataDictionaryDto = new DataDictionaryDto();
                    dataDictionaryDto.Id = item.Id;
                    dataDictionaryDto.DisplayValue = item.DisplayValue;
                    dataDictionaryDto.Sort = item.Sort;
                    dataDictionaryDto.Type = DataDictionaryType.DicRatingType;


                    dataDictionaryDto.CreationTime = item.CreationTime;
                    dataDictionaryDto.CreatorId = item.CreatorId;
                    dataDictionaryDto.LastModifierId = item.LastModifierId;
                    dataDictionaryDto.LastModificationTime = item.LastModificationTime;
                    if (item.Creator != null)
                    {
                        dataDictionaryDto.CreatorSurname = item.Creator.Surname;
                        dataDictionaryDto.CreatorName = item.Creator.Name;
                    }
                    if (item.LastModifier != null)
                    {
                        dataDictionaryDto.LastModifierSurname = item.LastModifier.Surname;
                        dataDictionaryDto.LastModifierName = item.LastModifier.Name;
                    }
                    resultList.Add(dataDictionaryDto);
                }
            }
            else if (input.Type == DataDictionaryType.DicSampleProperty)
            {
                var query = await _dicSamplePropertyRepository.WithDetailsAsync();
                query = query
                    .WhereIf(!input.DisplayValue.IsNullOrWhiteSpace(), x => x.DisplayValue.Contains(input.DisplayValue))
                    ;
                query = query.OrderBy(m => m.Sort);
                var result = await AsyncExecuter.ToListAsync(query);
                foreach (var item in result)
                {
                    DataDictionaryDto dataDictionaryDto = new DataDictionaryDto();
                    dataDictionaryDto.Id = item.Id;
                    dataDictionaryDto.DisplayValue = item.DisplayValue;
                    dataDictionaryDto.Sort = item.Sort;
                    dataDictionaryDto.Type = DataDictionaryType.DicSampleProperty;

                    dataDictionaryDto.CreationTime = item.CreationTime;
                    dataDictionaryDto.CreatorId = item.CreatorId;
                    dataDictionaryDto.LastModifierId = item.LastModifierId;
                    dataDictionaryDto.LastModificationTime = item.LastModificationTime;
                    if (item.Creator != null)
                    {
                        dataDictionaryDto.CreatorSurname = item.Creator.Surname;
                        dataDictionaryDto.CreatorName = item.Creator.Name;
                    }
                    if (item.LastModifier != null)
                    {
                        dataDictionaryDto.LastModifierSurname = item.LastModifier.Surname;
                        dataDictionaryDto.LastModifierName = item.LastModifier.Name;
                    }
                    resultList.Add(dataDictionaryDto);
                }
            }
            else if (input.Type == DataDictionaryType.DicSampleType)
            {
                var query = await _dicSampleTypeRepository.WithDetailsAsync();
                query = query
                    .WhereIf(!input.DisplayValue.IsNullOrWhiteSpace(), x => x.DisplayValue.Contains(input.DisplayValue))
                    ;
                query = query.OrderBy(m => m.Sort);
                var result = await AsyncExecuter.ToListAsync(query);
                foreach (var item in result)
                {
                    DataDictionaryDto dataDictionaryDto = new DataDictionaryDto();
                    dataDictionaryDto.Id = item.Id;
                    dataDictionaryDto.DisplayValue = item.DisplayValue;
                    dataDictionaryDto.Sort = item.Sort;
                    dataDictionaryDto.Type = DataDictionaryType.DicSampleType;

                    dataDictionaryDto.CreationTime = item.CreationTime;
                    dataDictionaryDto.CreatorId = item.CreatorId;
                    dataDictionaryDto.LastModifierId = item.LastModifierId;
                    dataDictionaryDto.LastModificationTime = item.LastModificationTime;
                    if (item.Creator != null)
                    {
                        dataDictionaryDto.CreatorSurname = item.Creator.Surname;
                        dataDictionaryDto.CreatorName = item.Creator.Name;
                    }
                    if (item.LastModifier != null)
                    {
                        dataDictionaryDto.LastModifierSurname = item.LastModifier.Surname;
                        dataDictionaryDto.LastModifierName = item.LastModifier.Name;
                    }
                    resultList.Add(dataDictionaryDto);
                }
            }
            else if (input.Type == DataDictionaryType.DicStandardType)
            {
                var query = await _dicStandardTypeRepository.WithDetailsAsync();
                query = query
                    .WhereIf(!input.DisplayValue.IsNullOrWhiteSpace(), x => x.DisplayValue.Contains(input.DisplayValue))
                    ;
                query = query.OrderBy(m => m.Sort);
                var result = await AsyncExecuter.ToListAsync(query);
                foreach (var item in result)
                {
                    DataDictionaryDto dataDictionaryDto = new DataDictionaryDto();
                    dataDictionaryDto.Id = item.Id;
                    dataDictionaryDto.DisplayValue = item.DisplayValue;
                    dataDictionaryDto.Sort = item.Sort;
                    dataDictionaryDto.Type = DataDictionaryType.DicStandardType;

                    dataDictionaryDto.CreationTime = item.CreationTime;
                    dataDictionaryDto.CreatorId = item.CreatorId;
                    dataDictionaryDto.LastModifierId = item.LastModifierId;
                    dataDictionaryDto.LastModificationTime = item.LastModificationTime;
                    if (item.Creator != null)
                    {
                        dataDictionaryDto.CreatorSurname = item.Creator.Surname;
                        dataDictionaryDto.CreatorName = item.Creator.Name;
                    }
                    if (item.LastModifier != null)
                    {
                        dataDictionaryDto.LastModifierSurname = item.LastModifier.Surname;
                        dataDictionaryDto.LastModifierName = item.LastModifier.Name;
                    }

                    resultList.Add(dataDictionaryDto);
                }
            }
            return resultList;
        }


        [Authorize(LimsPermissions.DictionaryData_Update)]
        public async Task UpdateAsync(DataDictionaryUpdateDto input)
        {
            if (input.Type == DataDictionaryType.DicEquipmentType)
            {
                var entity = await _dicEquipmentTypeRepository.FindAsync(input.Id);
                entity.DisplayValue = input.DisplayValue;
                entity.Sort = input.Sort;
                await _dicEquipmentTypeRepository.UpdateAsync(entity);
            }
            else if (input.Type == DataDictionaryType.DicProductType)
            {
                var entity = await _dicProductTypeRepository.FindAsync(input.Id);
                entity.DisplayValue = input.DisplayValue;
                entity.Sort = input.Sort;
                await _dicProductTypeRepository.UpdateAsync(entity);
            }
            else if (input.Type == DataDictionaryType.DicRatingType)
            {
                var entity = await _dicRatingTypeRepository.FindAsync(input.Id);
                entity.DisplayValue = input.DisplayValue;
                entity.Sort = input.Sort;
                await _dicRatingTypeRepository.UpdateAsync(entity);
            }
            else if (input.Type == DataDictionaryType.DicSampleProperty)
            {
                var entity = await _dicSamplePropertyRepository.FindAsync(input.Id);
                entity.DisplayValue = input.DisplayValue;
                entity.Sort = input.Sort;
                await _dicSamplePropertyRepository.UpdateAsync(entity);
            }
            else if (input.Type == DataDictionaryType.DicSampleType)
            {
                var entity = await _dicSampleTypeRepository.FindAsync(input.Id);
                entity.DisplayValue = input.DisplayValue;
                entity.Sort = input.Sort;
                await _dicSampleTypeRepository.UpdateAsync(entity);
            }
            else if (input.Type == DataDictionaryType.DicStandardType)
            {
                var entity = await _dicStandardTypeRepository.FindAsync(input.Id);
                entity.DisplayValue = input.DisplayValue;
                entity.Sort = input.Sort;
                await _dicStandardTypeRepository.UpdateAsync(entity);
            }
        }



        public async Task<List<DicEquipmentTypeLookupDto>> LookupEquipmentTypeAsync()
        {
            var query = await _dicEquipmentTypeRepository.WithDetailsAsync();
            query = query.OrderBy(m => m.Sort);
            var result = await AsyncExecuter.ToListAsync(query);
            List<DicEquipmentTypeLookupDto> resultList = new List<DicEquipmentTypeLookupDto>();
            foreach (var item in result)
            {
                DicEquipmentTypeLookupDto lookopDto = new DicEquipmentTypeLookupDto();
                lookopDto.Id = item.Id;
                lookopDto.DisplayValue = item.DisplayValue;
                resultList.Add(lookopDto);
            }
            return resultList;
        }

        /// lookup ProductType
        public async Task<List<DicProductTypeLookupDto>> LookupProductTypeAsync()
        {
            var query = await _dicProductTypeRepository.WithDetailsAsync();
            query = query.OrderBy(m => m.Sort);
            var result = await AsyncExecuter.ToListAsync(query);
            List<DicProductTypeLookupDto> resultList = new List<DicProductTypeLookupDto>();
            foreach (var item in result)
            {
                DicProductTypeLookupDto lookopDto = new DicProductTypeLookupDto();
                lookopDto.Id = item.Id;
                lookopDto.DisplayValue = item.DisplayValue;
                resultList.Add(lookopDto);
            }
            return resultList;
        }

        /// lookup RatingType
        public async Task<List<DicRatingTypeLookupDto>> LookupRatingTypeAsync()
        {
            var query = await _dicRatingTypeRepository.WithDetailsAsync();
            query = query.OrderBy(m => m.Sort);
            var result = await AsyncExecuter.ToListAsync(query);
            List<DicRatingTypeLookupDto> resultList = new List<DicRatingTypeLookupDto>();
            foreach (var item in result)
            {
                DicRatingTypeLookupDto lookopDto = new DicRatingTypeLookupDto();
                lookopDto.Id = item.Id;
                lookopDto.DisplayValue = item.DisplayValue;
                resultList.Add(lookopDto);
            }
            return resultList;
        }

        /// lookup SampleProperty
        public async Task<List<DicSamplePropertyLookupDto>> LookupSamplePropertyAsync()
        {
            var query = await _dicSamplePropertyRepository.WithDetailsAsync();
            query = query.OrderBy(m => m.Sort);
            var result = await AsyncExecuter.ToListAsync(query);
            List<DicSamplePropertyLookupDto> resultList = new List<DicSamplePropertyLookupDto>();
            foreach (var item in result)
            {
                DicSamplePropertyLookupDto lookopDto = new DicSamplePropertyLookupDto();
                lookopDto.Id = item.Id;
                lookopDto.DisplayValue = item.DisplayValue;
                resultList.Add(lookopDto);
            }
            return resultList;
        }


        public async Task<List<DicSampleTypeLookupDto>> LookupSampleTypeAsync()
        {
            var query = await _dicSampleTypeRepository.WithDetailsAsync();
            query = query.OrderBy(m => m.Sort);
            var result = await AsyncExecuter.ToListAsync(query);
            List<DicSampleTypeLookupDto> resultList = new List<DicSampleTypeLookupDto>();
            foreach (var item in result)
            {
                DicSampleTypeLookupDto lookopDto = new DicSampleTypeLookupDto();
                lookopDto.Id = item.Id;
                lookopDto.DisplayValue = item.DisplayValue;
                resultList.Add(lookopDto);
            }
            return resultList;
        }


        public async Task<List<DicStandardTypeLookupDto>> LookupStandardTypeAsync()
        {
            var query = await _dicStandardTypeRepository.WithDetailsAsync();
            query = query.OrderBy(m => m.Sort);
            var result = await AsyncExecuter.ToListAsync(query);
            List<DicStandardTypeLookupDto> resultList = new List<DicStandardTypeLookupDto>();
            foreach (var item in result)
            {
                DicStandardTypeLookupDto lookopDto = new DicStandardTypeLookupDto();
                lookopDto.Id = item.Id;
                lookopDto.DisplayValue = item.DisplayValue;
                resultList.Add(lookopDto);
            }
            return resultList;
        }

    }

}
