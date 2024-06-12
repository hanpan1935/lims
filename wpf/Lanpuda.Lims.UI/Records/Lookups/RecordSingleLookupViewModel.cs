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
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;
using System.Collections.ObjectModel;
using Lanpuda.Lims.UI.BasicInformations.Customers.Lookups;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Lanpuda.Lims.UI.BasicInformations.Suppliers.Lookups;

namespace Lanpuda.Lims.UI.Records.Lookups
{
    public class RecordSingleLookupViewModel : PagedViewModelBase<RecordDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly IRecordAppService _recordAppService;
        public Action<RecordDto>? OnSelectedCallback;

        private readonly IDataDictionaryAppService _dataDictionaryAppService;

        public ObservableCollection<DicSampleTypeLookupDto> SampleTypeSource { get; set; }
        public ObservableCollection<DicSamplePropertyLookupDto> SamplePropertySource { get; set; }
        public ObservableCollection<DicRatingTypeLookupDto> RatingTypeSource { get; set; }

        public RecordSingleLookupViewModel(IServiceProvider serviceProvider, IRecordAppService recordAppService, IDataDictionaryAppService dataDictionaryAppService)
        {
            _serviceProvider = serviceProvider;
            _recordAppService = recordAppService;
            _dataDictionaryAppService = dataDictionaryAppService;
            SampleTypeSource = new ObservableCollection<DicSampleTypeLookupDto>();
            SamplePropertySource = new ObservableCollection<DicSamplePropertyLookupDto>();
            RatingTypeSource = new ObservableCollection<DicRatingTypeLookupDto>();
        }


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



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            this.IsLoading = true;
            var sampleTypes = await _dataDictionaryAppService.LookupSampleTypeAsync();
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



        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                RecordGetListInput input = new RecordGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;

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
        public void Select()
        {
            if (this.SelectedModel != null && this.OnSelectedCallback != null)
            {
                OnSelectedCallback(this.SelectedModel);
                this.CurrentWindowService.Close();
            }
        }

        public bool CanSelect()
        {
            return this.SelectedModel != null;
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

    }
}
