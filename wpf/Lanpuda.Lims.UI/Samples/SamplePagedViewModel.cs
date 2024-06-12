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
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;
using System.Collections.ObjectModel;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Lanpuda.Lims.UI.BasicInformations.Customers.Lookups;
using Lanpuda.Lims.UI.BasicInformations.Suppliers.Lookups;
using System.Windows;

namespace Lanpuda.Lims.UI.Samples
{
    public class SamplePagedViewModel : PagedViewModelBase<SampleDto>
    {

        protected IWindowService SearchWindowService { get { return this.GetService<IWindowService>("SearchWindow"); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly ISampleAppService _sampleAppService;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        public ObservableCollection<DicSampleTypeLookupDto> SampleTypeSource { get; set; }
        public ObservableCollection<DicSamplePropertyLookupDto> SamplePropertySource { get; set; }


        public SamplePagedViewModel(IServiceProvider serviceProvider, ISampleAppService sampleAppService, IDataDictionaryAppService dataDictionaryAppService)
        {
            this.PageTitle = "样品管理";
            _serviceProvider = serviceProvider;
            _sampleAppService = sampleAppService;
            _dataDictionaryAppService = dataDictionaryAppService;
            SampleTypeSource = new ObservableCollection<DicSampleTypeLookupDto>();
            SamplePropertySource = new ObservableCollection<DicSamplePropertyLookupDto>();
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

        [Command]
        public void Create()
        {
            if (this.WindowService != null)
            {
                SampleEditViewModel? viewModel = _serviceProvider.GetService<SampleEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "样品管理-新建";
                    WindowService.Show(nameof(SampleEditView), viewModel);
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
                SampleEditViewModel? viewModel = _serviceProvider.GetService<SampleEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "样品管理-编辑";
                    WindowService.Show(nameof(SampleEditView), viewModel);
                }
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
                    await _sampleAppService.DeleteAsync(this.SelectedModel.Id);
                    await this.QueryAsync();
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


        [Command]
        public void ShowProductSelectView()
        {
            if (this.SearchWindowService != null)
            {
                ProductSingleLookupViewModel? viewModel = _serviceProvider.GetService<ProductSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (product) =>
                    {
                        this.ProductId = product.Id;
                        this.ProductName = product.Name;
                    };
                    SearchWindowService.Title = "选择产品";
                    SearchWindowService.Show(nameof(ProductSingleLookupView), viewModel);
                }
            }
        }


        [Command]
        public void ShowCustomerSelectView()
        {
            if (this.SearchWindowService != null)
            {
                CustomerSingleLookupViewModel? viewModel = _serviceProvider.GetService<CustomerSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (customer) =>
                    {
                        this.CustomerId = customer.Id;
                        this.CustomerShortName = customer.ShortName;
                    };
                   SearchWindowService.Title = "选择客户";
                   SearchWindowService.Show(nameof(CustomerSingleLookupView), viewModel);
                }
            }
        }

        [Command]
        public void ShowSupplierSelectView()
        {
            if (this.SearchWindowService != null)
            {
                SupplierSingleLookupViewModel? viewModel = _serviceProvider.GetService<SupplierSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (supplier) =>
                    {
                        this.SupplierId = supplier.Id;
                        this.SupplierShortName = supplier.ShortName;
                    };
                    SearchWindowService.Title = "选择供应商";
                    SearchWindowService.Show(nameof(SupplierSingleLookupView), viewModel);
                }
            }
        }
    }
}
