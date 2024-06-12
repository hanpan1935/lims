using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Standards.Dtos;
using Lanpuda.Lims.Standards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Lanpuda.Lims.UI.BasicInformations.Suppliers.Lookups;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.Lims.UI.InspectionMethods.InspectionItems.Lookups;
using DevExpress.Mvvm;
using Lanpuda.Lims.InspectionItems.Dtos;
using Lanpuda.Lims.DataDictionaries.Dtos;
using System.Collections.ObjectModel;
using Lanpuda.Lims.DataDictionaries;
using Lanpuda.Lims.UI.InspectionMethods.Standards.Lookups;

namespace Lanpuda.Lims.UI.InspectionMethods.Standards.Edits
{
    public class StandardEditViewModel : EditViewModelBase<StandardEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IStandardAppService _standardAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        public ObservableCollection<DicStandardTypeLookupDto> StandardTypeSource { get; set; }


        public StandardEditViewModel(
            IStandardAppService standardAppService,
            IObjectMapper objectMapper, 
            IServiceProvider serviceProvider,
            IDataDictionaryAppService dataDictionaryAppService
            )
        {
            _standardAppService = standardAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
            _dataDictionaryAppService = dataDictionaryAppService;
            StandardTypeSource = new ObservableCollection<DicStandardTypeLookupDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;

                var standardTypeList = await _dataDictionaryAppService.LookupStandardTypeAsync();
                StandardTypeSource.Clear();
                foreach (var item in standardTypeList)
                {
                    StandardTypeSource.Add(item);
                }


                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _standardAppService.GetAsync(id);
                    Model = _objectMapper.Map<StandardDto, StandardEditModel>(result);

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
                StandardCreateDto dto = _objectMapper.Map<StandardEditModel, StandardCreateDto>(this.Model);
                await _standardAppService.CreateAsync(dto);
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
                StandardUpdateDto dto = _objectMapper.Map<StandardEditModel, StandardUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _standardAppService.UpdateAsync((Guid)this.Model.Id, dto);
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


        [Command]
        public void ShowSelectItemView()
        {
            InspectionItemMultipleLookupViewModel? viewModel = _serviceProvider.GetService<InspectionItemMultipleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnInspectionItemSelected;
                WindowService.Title = "选择检验项目-编辑";
                WindowService.Show(nameof(InspectionItemMultipleLookupView), viewModel);
            }
        }


        private void OnInspectionItemSelected(ICollection<InspectionItemDto> selectedItems)
        {
            if (selectedItems != null && selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {

                    StandardDetailEditModel detail = new StandardDetailEditModel();
                    detail.InspectionItemId = item.Id;
                    detail.InspectionItemShortName   = item.ShortName;
                    detail.InspectionItemFullName = item.FullName;
                    Model.Details.Add(detail);
                }
            }
        }

        [Command]
        public void ShowSelectStandardView()
        {
            StandardSingleLookupViewModel? viewModel = _serviceProvider.GetService<StandardSingleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnStandardSelected;
                WindowService.Title = "选择相似检验标准-编辑";
                WindowService.Show(nameof(StandardSingleLookupView), viewModel);
            }
        }


        private void OnStandardSelected(StandardDto standard)
        {
            foreach (var item in standard.Details)
            {
                StandardDetailEditModel detail = new StandardDetailEditModel();
                detail.InspectionItemId = item.InspectionItemId;
                detail.InspectionItemShortName = item.InspectionItemShortName;
                detail.InspectionItemFullName = item.InspectionItemFullName;
                Model.Details.Add(detail);
            }
        }

        [Command]
        public void DeleteSelectedRow()
        {
            if (this.Model.SelectedRow != null)
            {
                this.Model.Details.Remove(this.Model.SelectedRow);
            }
        }

        public bool CanDeleteSelectedRow()
        {
            return this.Model.SelectedRow != null;
        }

    }
}
