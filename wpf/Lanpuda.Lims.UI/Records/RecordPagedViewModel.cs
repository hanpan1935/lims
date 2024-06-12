using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Records.Dtos;
using Lanpuda.Lims.Records;
using Lanpuda.Lims.UI.Records.BatchCreates;
using Lanpuda.Lims.UI.Records.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.UI.BasicInformations.Customers.Lookups;
using Lanpuda.Lims.UI.InspectionTasks.Create;
using Lanpuda.Lims.UI.InspectionTasks.Edits;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using Lanpuda.Lims.InspectionTasks;
using Lanpuda.Lims.DataDictionaries;
using System.Collections.ObjectModel;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Lanpuda.Lims.UI.BasicInformations.Suppliers.Lookups;
using Lanpuda.Lims.Samples;
using System.Windows;

namespace Lanpuda.Lims.UI.Records
{
    public class RecordPagedViewModel : PagedViewModelBase<RecordDto>
    {
        protected IWindowService TaskWindowService { get { return this.GetService<IWindowService>("TaskWindow"); } }

        private readonly IServiceProvider _serviceProvider;
        private readonly IRecordAppService _recordAppService;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;

        public ObservableCollection<DicSampleTypeLookupDto> SampleTypeSource { get; set; }
        public ObservableCollection<DicSamplePropertyLookupDto> SamplePropertySource { get; set; }
        public ObservableCollection<DicRatingTypeLookupDto> RatingTypeSource { get; set; }



        #region search
        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }


        public string? SampleNumber
        {
            get { return GetProperty(() => SampleNumber); }
            set { SetProperty(() => SampleNumber, value); }
        }


        public Guid? ProductId { get; set; }
        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public DicSampleTypeLookupDto? SelectSampleType
        {
            get { return GetProperty(() => SelectSampleType); }
            set { SetProperty(() => SelectSampleType, value); }
        }
       

        public DicSamplePropertyLookupDto? SelectSampleProperty
        {
            get { return GetProperty(() => SelectSampleProperty); }
            set { SetProperty(() => SelectSampleProperty, value); }
        }

        public DicRatingTypeLookupDto? SelectRatingType
        {
            get { return GetProperty(() => SelectRatingType); }
            set { SetProperty(() => SelectRatingType, value); }
        }


        public DateTime? SampleTimeStart
        {
            get { return GetProperty(() => SampleTimeStart); }
            set { SetProperty(() => SampleTimeStart, value); }
        }


        public DateTime? SampleTimeEnd
        {
            get { return GetProperty(() => SampleTimeEnd); }
            set { SetProperty(() => SampleTimeEnd, value); }
        }


        public string? Sender
        {
            get { return GetProperty(() => Sender); }
            set { SetProperty(() => Sender, value); }
        }
     

        public Guid? CustomerId { get; set; }
        public string? CustomerName
        {
            get { return GetProperty(() => CustomerName); }
            set { SetProperty(() => CustomerName, value); }
        }

        public Guid? SupplierId { get; set; }
        public string? SupplierName
        {
            get { return GetProperty(() => SupplierName); }
            set { SetProperty(() => SupplierName, value); }
        }


       
        #endregion


        public RecordPagedViewModel(IServiceProvider serviceProvider, IRecordAppService recordAppService, IDataDictionaryAppService dataDictionaryAppService)
        {
            this.PageTitle = "检验数据";
            _serviceProvider = serviceProvider;
            _recordAppService = recordAppService;
             _dataDictionaryAppService = dataDictionaryAppService;
            SampleTypeSource = new ObservableCollection<DicSampleTypeLookupDto>();
            SamplePropertySource = new ObservableCollection<DicSamplePropertyLookupDto>();
            RatingTypeSource = new ObservableCollection<DicRatingTypeLookupDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var sampleTypes =  await _dataDictionaryAppService.LookupSampleTypeAsync();
                SampleTypeSource.Clear();
                foreach (var item in sampleTypes)
                {
                    this.SampleTypeSource.Add(item);
                }
                var sampleProperties = await _dataDictionaryAppService.LookupSamplePropertyAsync();
                SamplePropertySource.Clear();
                foreach (var item in sampleProperties)
                {
                    this.SamplePropertySource.Add(item);
                }

                var ratingTypes = await _dataDictionaryAppService.LookupRatingTypeAsync();
                RatingTypeSource.Clear();
                foreach (var item in ratingTypes)
                {
                    this.RatingTypeSource.Add(item);
                }
                await this.QueryAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
            
        }



        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                RecordGetListInput input = new RecordGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Number = this.Number;
                input.SampleNumber = this.SampleNumber;
                input.ProductId = this.ProductId;
                input.DicSampleTypeId = this.SelectSampleType?.Id;
                input.DicSamplePropertyId = this.SelectSampleProperty?.Id;
                input.DicRatingTypeId = this.SelectRatingType?.Id;
                input.SampleTimeStart = this.SampleTimeStart;
                input.SampleTimeEnd = this.SampleTimeEnd;
                input.Sender = this.Sender;
                input.CustomerId = this.CustomerId;
                input.SupplierId = this.SupplierId;

                var result = await _recordAppService.GetPagedListAsync(input);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                this.PagedDatas.CanNotify = true;
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
        public void Create()
        {
            if (this.WindowService != null)
            {
                RecordEditViewModel? viewModel = _serviceProvider.GetService<RecordEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "检验数据-新建";
                    WindowService.Show(nameof(RecordEditView), viewModel);
                }
            }
        }

        [Command]
        public void BathcCreate()
        {
            if (this.WindowService != null)
            {
                RecordBatchCreateViewModel? viewModel = _serviceProvider.GetService<RecordBatchCreateViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "检验数据-批量新建";
                    WindowService.Show(nameof(RecordBatchCreateView), viewModel);
                }
            }
        }

        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            if (this.WindowService != null)
            {
                RecordEditViewModel? viewModel = _serviceProvider.GetService<RecordEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "检验数据-编辑";
                    WindowService.Show(nameof(RecordEditView), viewModel);
                }
            }
        }


        [Command]
        public void ShowItemView()
        {
            ModuleManager.DefaultManager.InjectOrNavigate(RegionNames.MainContentRegion, LimsUIViewKeys.Lims_RecordRecordItems, null);
        }


        [Command]
        public void ShowCreateTaskView(Guid id)
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            RecordDetailDto recordDetailDto = this.SelectedModel.Details.Where(m => m.Id == id).First();

            InspectionTaskEditViewModel? viewModel = _serviceProvider.GetService<InspectionTaskEditViewModel>();
            if (viewModel != null)
            {
                viewModel.Model.RecordNumber = SelectedModel.Number;
                viewModel.Model.InspectionItemId = recordDetailDto.InspectionItemId;
                viewModel.Model.RecordDetailId = recordDetailDto.Id;
                viewModel.Model.InspectionItemFullName = recordDetailDto.InspectionItemFullName;
                viewModel.Model.InspectionItemShortName = recordDetailDto.InspectionItemShortName;
                TaskWindowService.Title = "检验任务-新建";
                TaskWindowService.Show(nameof(InspectionTaskEditView), viewModel);
            }
        }


        [AsyncCommand]
        public async Task DeleteAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }

                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _recordAppService.DeleteAsync(this.SelectedModel.Id);
                    await QueryAsync();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = null;
            this.SampleNumber = null;
            this.ProductId = null;
            this.ProductName = null;
            this.SelectSampleType = null;
            this.SelectSampleProperty = null;
            this.SelectRatingType = null;
            this.SampleTimeStart = null;
            this.SampleTimeEnd = null;
            this.Sender = null;
            this.CustomerId = null;
            this.CustomerName = null;
            this.SupplierId = null;
            this.CustomerId = null;
            await QueryAsync();
        }


        [Command]
        public void ShowProductSelectView()
        {
            if (this.WindowService != null)
            {
                ProductSingleLookupViewModel? viewModel = _serviceProvider.GetService<ProductSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (product) =>
                    {
                        this.ProductId = product.Id;
                        this.ProductName = product.Name;
                    };
                    WindowService.Title = "选择产品";
                    WindowService.Show(nameof(ProductSingleLookupView), viewModel);
                }
            }
        }

        [Command]
        public void ShowCustomerSelectView()
        {
            if (this.WindowService != null)
            {
                CustomerSingleLookupViewModel? viewModel = _serviceProvider.GetService<CustomerSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (customer) =>
                    {
                        this.CustomerId = customer.Id;
                        this.CustomerName = customer.ShortName;
                    };
                    WindowService.Title = "选择客户";
                    WindowService.Show(nameof(CustomerSingleLookupView), viewModel);
                }
            }
        }

        [Command]
        public void ShowSupplierSelectView()
        {
            if (this.WindowService != null)
            {
                SupplierSingleLookupViewModel? viewModel = _serviceProvider.GetService<SupplierSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (supplier) =>
                    {
                        this.SupplierId = supplier.Id;
                        this.SupplierName = supplier.ShortName;
                    };
                    WindowService.Title = "选择供应商";
                    WindowService.Show(nameof(SupplierSingleLookupView), viewModel);
                }
            }
        }


        [Command]
        public void Print()
        {

        }

    }
}
