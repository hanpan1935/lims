using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;

namespace Lanpuda.Lims.UI.BasicInformations.DataDictionaries.Edits
{
    public class DataDictionaryEditViewModel : EditViewModelBase<DataDictionaryEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IDataDictionaryAppService _dataDictionaryAppService;

        public Dictionary<string, DataDictionaryType> DataDictionaryTypeSource { get; set; }
        

        public DataDictionaryEditViewModel(IDataDictionaryAppService dataDictionaryAppService)
        {
            DataDictionaryTypeSource = EnumUtils.EnumToDictionary<DataDictionaryType>();
            _dataDictionaryAppService = dataDictionaryAppService;
        }


        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null)
            {
                await CreateAsync();
            }
            else
            {
                await UpdateAsync();
            }
        }


        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }


        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                DataDictionaryCreateDto dto = new DataDictionaryCreateDto();
                dto.Type = Model.Type;
                dto.DisplayValue = Model.DisplayValue;
                dto.Sort = Model.Sort;
                await _dataDictionaryAppService.CreateAsync(dto);
                if (RefreshPagedViewFunc != null)
                {
                    await RefreshPagedViewFunc();
                    this.Close();
                }
            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        private async Task UpdateAsync()
        {
            
            try
            {
                this.IsLoading = true;
                DataDictionaryUpdateDto dto = new DataDictionaryUpdateDto();
                if (Model.Id != null)
                {
                    dto.Id = (int)this.Model.Id;

                }
                dto.Type = Model.Type;
                dto.DisplayValue = Model.DisplayValue;
                dto.Sort = Model.Sort;
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _dataDictionaryAppService.UpdateAsync( dto);
                if (RefreshPagedViewFunc != null)
                {
                    await RefreshPagedViewFunc();
                    this.Close();
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally
            {
                this.IsLoading = false;
            }
        }
    }
}
