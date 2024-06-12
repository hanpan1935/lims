using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Records.Dtos;
using Lanpuda.Lims.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Lanpuda.Lims.Standards.Dtos;
using Lanpuda.Lims.UI.InspectionMethods.Standards.Edits;
using Lanpuda.Lims.UI.InspectionMethods.Standards.Lookups;
using Microsoft.Extensions.DependencyInjection;
using static Lanpuda.Lims.Permissions.LimsPermissions;
using Lanpuda.Lims.Samples.Dtos;
using DevExpress.Mvvm;
using Lanpuda.Lims.UI.Samples.Lookups;
using Lanpuda.Lims.InspectionItems.Dtos;
using Lanpuda.Lims.UI.InspectionMethods.InspectionItems.Lookups;
using Lanpuda.Lims.DataDictionaries;
using Lanpuda.Lims.DataDictionaries.Dtos;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Lanpuda.Lims.Standards;

namespace Lanpuda.Lims.UI.Records.Edits
{
    public class RecordEditViewModel : EditViewModelBase<RecordEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IRecordAppService _recordAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        private readonly IStandardAppService _standardAppService;

        public ObservableCollection<DicRatingTypeLookupDto> RatingTypeSource { get; set; }


     

        public RecordEditViewModel(
            IRecordAppService recordAppService,
            IObjectMapper objectMapper,
            IServiceProvider serviceProvider,
            IDataDictionaryAppService dataDictionaryAppService,
            IStandardAppService standardAppService
            )
        {
            _recordAppService = recordAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
            _dataDictionaryAppService = dataDictionaryAppService;
            _standardAppService = standardAppService;
            RatingTypeSource = new ObservableCollection<DicRatingTypeLookupDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;

                var ratingTypes = await _dataDictionaryAppService.LookupRatingTypeAsync();
                RatingTypeSource.Clear();
                foreach (var item in ratingTypes)
                {
                    this.RatingTypeSource.Add(item);
                }

                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _recordAppService.GetAsync(id);
                    Model = _objectMapper.Map<RecordDto, RecordEditModel>(result);
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
                RecordCreateDto dto = _objectMapper.Map<RecordEditModel, RecordCreateDto>(this.Model);
                if (this.Model.SelectRatingType != null)
                {
                    dto.DicRatingTypeId = this.Model.SelectRatingType.Id;
                }
                await _recordAppService.CreateAsync(dto);
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
                RecordUpdateDto dto = _objectMapper.Map<RecordEditModel, RecordUpdateDto>(this.Model);
                if (this.Model.SelectRatingType != null)
                {
                    dto.DicRatingTypeId = this.Model.SelectRatingType.Id;
                }
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _recordAppService.UpdateAsync((Guid)this.Model.Id, dto);
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
        public void ShowSelectSampleView()
        {
            SampleSingleLookupViewModel? viewModel = _serviceProvider.GetService<SampleSingleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnSampleSelected;
                WindowService.Title = "选择样品";
                WindowService.Show(nameof(SampleSingleLookupView), viewModel);
            }
        }


        private async void OnSampleSelected(SampleDto sample)
        {
            this.Model.SampleId = sample.Id;
            this.Model.SampleNumber = sample.Number;
            this.Model.ProductName = sample.ProductName;
            if (sample.ProductStandardId !=null)
            {
                Guid standardId = (Guid)sample.ProductStandardId;
                var standard = await _standardAppService.GetAsync(standardId);


                this.Model.Details.Clear();
                foreach (var item in standard.Details)
                {
                    RecordDetailEditModel detail = new RecordDetailEditModel();
                    detail.InspectionItemId = item.InspectionItemId;
                    detail.InspectionItemFullName = item.InspectionItemFullName;
                    detail.InspectionItemShortName = item.InspectionItemShortName;
                    detail.MinValue = item.MinValue;
                    detail.MaxValue = item.MaxValue;
                    detail.HasMinValue = item.HasMinValue;
                    detail.HasMaxValue = item.HasMaxValue;
                    this.Model.Details.Add(detail);
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
                WindowService.Title = "选择标准";
                WindowService.Show(nameof(StandardSingleLookupView), viewModel);
            }
        }


        private void OnStandardSelected(StandardDto standard)
        {
            foreach (var item in standard.Details)
            {
                RecordDetailEditModel detail = new RecordDetailEditModel();
                detail.InspectionItemId = item.InspectionItemId;
                detail.InspectionItemFullName = item.InspectionItemFullName;
                detail.InspectionItemShortName = item.InspectionItemShortName;
                detail.MinValue = item.MinValue;
                detail.MaxValue = item.MaxValue;
                detail.HasMinValue = item.HasMinValue;
                detail.HasMaxValue  = item.HasMaxValue;
                this.Model.Details.Add(detail);
            }
        }



        [Command]
        public void ShowSelectInspectionItemView()
        {
            InspectionItemMultipleLookupViewModel? viewModel = _serviceProvider.GetService<InspectionItemMultipleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnInspectionItemSelected;
                WindowService.Title = "选择标准";
                WindowService.Show(nameof(InspectionItemMultipleLookupView), viewModel);
            }
        }


        private void OnInspectionItemSelected(ICollection<InspectionItemDto> inspectionItemList)
        {
            foreach (var item in inspectionItemList)
            {
                RecordDetailEditModel detail = new RecordDetailEditModel();
                detail.InspectionItemId = item.Id;
                detail.InspectionItemFullName = item.FullName;
                detail.InspectionItemShortName = item.ShortName;
                this.Model.Details.Add(detail);
            }
        }

        [Command]
        public void DeleteSelectedRow()
        {
            if (this.Model.SelectedRow != null)
            {
                Model.Details.Remove(this.Model.SelectedRow);   
            }
        }

        public bool CanDeleteSelectedRow()
        {
            if (this.Model.SelectedRow == null)
            {
                return false;
            }
            return true;
        }
    }
}
