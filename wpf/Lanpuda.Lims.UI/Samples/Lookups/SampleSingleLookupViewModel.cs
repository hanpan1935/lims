using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Samples.Dtos;
using Lanpuda.Lims.Samples;
using Lanpuda.Lims.UI.Samples.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.UI.BasicInformations.Customers.Lookups;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Lanpuda.Lims.UI.BasicInformations.Suppliers.Lookups;
using System.Collections.ObjectModel;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;

namespace Lanpuda.Lims.UI.Samples.Lookups
{
    public class SampleSingleLookupViewModel : PagedViewModelBase<SampleDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly ISampleAppService _sampleAppService;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;

        public ObservableCollection<DicSampleTypeLookupDto> SampleTypeSource { get; set; }
        public ObservableCollection<DicSamplePropertyLookupDto> SamplePropertySource { get; set; }


        public Action<SampleDto>? OnSelectedCallback;


        public SampleSingleLookupViewModel(IServiceProvider serviceProvider, ISampleAppService sampleAppService, IDataDictionaryAppService dataDictionaryAppService)
        {
            _serviceProvider = serviceProvider;
            _sampleAppService = sampleAppService;
            this.SampleTypeSource = new ObservableCollection<DicSampleTypeLookupDto>();
            this.SamplePropertySource = new ObservableCollection<DicSamplePropertyLookupDto>();
            _dataDictionaryAppService = dataDictionaryAppService;
        }

        #region 搜索
        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public Guid? ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
        }

        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public int? DicSampleTypeId
        {
            get { return GetProperty(() => DicSampleTypeId); }
            set { SetProperty(() => DicSampleTypeId, value); }
        }

        public int? DicSamplePropertyId
        {
            get { return GetProperty(() => DicSamplePropertyId); }
            set { SetProperty(() => DicSamplePropertyId, value); }
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


        public DateTime? ExpireTime
        {
            get { return GetProperty(() => ExpireTime); }
            set { SetProperty(() => ExpireTime, value); }
        }


        public string? Sender
        {
            get { return GetProperty(() => Sender); }
            set { SetProperty(() => Sender, value); }
        }

        public Guid? CustomerId { get; set; }
        public string? CustomerShortName
        {
            get { return GetProperty(() => CustomerShortName); }
            set { SetProperty(() => CustomerShortName, value); }
        }



        public Guid? SupplierId { get; set; }
        public string? SupplierShortName
        {
            get { return GetProperty(() => SupplierShortName); }
            set { SetProperty(() => SupplierShortName, value); }
        }

        #endregion


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var samplePropertyList = await _dataDictionaryAppService.LookupSamplePropertyAsync();
                foreach (var item in samplePropertyList)
                {
                    this.SamplePropertySource.Add(item);
                }

                var sampleTypeList = await _dataDictionaryAppService.LookupSampleTypeAsync();
                foreach (var item in sampleTypeList)
                {
                    this.SampleTypeSource.Add(item);
                }

                await this.QueryAsync();
            }
            catch (Exception ex)
            {

                HandleException(ex);
            }
            finally
            {
                IsLoading = false;
            }

        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                SampleGetListInput input = new SampleGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;

                var result = await _sampleAppService.GetPagedListAsync(input);
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
        public  void Selected()
        {
            if (this.OnSelectedCallback != null && this.SelectedModel != null)
            {
                
               OnSelectedCallback(this.SelectedModel);
                this.CurrentWindowService.Close();
               
            }
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
                        this.CustomerShortName = customer.ShortName;
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
                        this.SupplierShortName = supplier.ShortName;
                    };
                    WindowService.Title = "选择供应商";
                    WindowService.Show(nameof(SupplierSingleLookupView), viewModel);
                }
            }
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.CustomerId = null;
            this.CustomerShortName = null;
            this.SupplierId = null;
            this.SupplierShortName = null;
            this.Number = null;
            this.ProductId = null;
            this.ProductName = null;
            this.DicSampleTypeId = null;
            this.DicSamplePropertyId = null;
            this.SampleTimeStart = null;
            this.SampleTimeEnd = null;
            this.ExpireTime = null;
            this.Sender = null;
            await QueryAsync();

        }
    }
}
