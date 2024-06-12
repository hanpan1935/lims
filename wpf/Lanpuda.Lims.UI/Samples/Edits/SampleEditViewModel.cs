using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Samples.Dtos;
using Lanpuda.Lims.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using DevExpress.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Lanpuda.Lims.Products.Dtos;
using System.Collections.ObjectModel;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;
using Lanpuda.Lims.UI.BasicInformations.Customers.Lookups;
using Lanpuda.Lims.Customers.Dtos;
using static Lanpuda.Lims.Permissions.LimsPermissions;
using Lanpuda.Lims.Suppliers.Dtos;
using Lanpuda.Lims.UI.BasicInformations.Suppliers.Lookups;
using NUglify.Helpers;
using Lanpuda.Lims.Standards.Dtos;
using Lanpuda.Lims.UI.InspectionMethods.Standards.Edits;
using Lanpuda.Lims.UI.InspectionMethods.Standards.Lookups;
using Lanpuda.Lims.Standards;

namespace Lanpuda.Lims.UI.Samples.Edits
{
    public class SampleEditViewModel : EditViewModelBase<SampleEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly ISampleAppService _sampleAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        private readonly IStandardAppService   _standardAppService;
        public ObservableCollection<DicSampleTypeLookupDto> SampleTypeSource { get; set; }
        public ObservableCollection<DicSamplePropertyLookupDto> SamplePropertySource { get; set; }

        #region search
        public string? Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }
        #endregion


        public SampleEditViewModel(
            ISampleAppService sampleAppService,
            IObjectMapper objectMapper,
            IServiceProvider serviceProvider,
            IDataDictionaryAppService dataDictionaryAppService,
            IStandardAppService standardAppService
            )
        {
            _sampleAppService = sampleAppService;
            _dataDictionaryAppService = dataDictionaryAppService;
            _objectMapper = objectMapper;
            this._serviceProvider = serviceProvider;
            this._standardAppService = standardAppService;
            SampleTypeSource = new ObservableCollection<DicSampleTypeLookupDto>();
            SamplePropertySource = new ObservableCollection<DicSamplePropertyLookupDto>();
        }


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


                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _sampleAppService.GetAsync(id);
                    Model = _objectMapper.Map<SampleDto, SampleEditModel>(result);
                    DateTime dateTime = (DateTime)result.SampleTime;
                    Model.SampleTimeStr = dateTime.ToString();
                }
                else
                {
                    this.Model.Number = "系统自动生成";
                    this.Model.SampleTime = DateTime.Now;
                    this.Model.NotifySampleTimeChanged();
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
            if (Model.SampleTimeStr != null)
            {
                DateTime samppleTime = new DateTime();
                bool canParse = DateTime.TryParse(Model.SampleTimeStr, out samppleTime);
                if (canParse == true)
                {
                    this.Model.SampleTime = samppleTime;
                }
            }

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


            if (hasError == true)
            {
                ;
            }
            return !hasError;
        }


        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                SampleCreateDto dto = _objectMapper.Map<SampleEditModel, SampleCreateDto>(this.Model);
                await _sampleAppService.CreateAsync(dto);
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
                SampleUpdateDto dto = _objectMapper.Map<SampleEditModel, SampleUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _sampleAppService.UpdateAsync((Guid)this.Model.Id, dto);
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
        public void ShowSelectProductView()
        {
            ProductSingleLookupViewModel? viewModel = _serviceProvider.GetService<ProductSingleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnProductSelected;

                WindowService.Title = "选择产品";
                WindowService.Show(nameof(ProductSingleLookupView), viewModel);
            }
        }


        private  void OnProductSelected(ProductDto product)
        {
            this.Model.ProductId = product.Id;
            this.Model.ProductName = product.Name;

            
        }


        [Command]
        public void ShowSelectCustomerView()
        {
            CustomerSingleLookupViewModel? viewModel = _serviceProvider.GetService<CustomerSingleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnCustomerSelected;

                WindowService.Title = "选择客户";
                WindowService.Show(nameof(CustomerSingleLookupView), viewModel);
            }
        }


        private void OnCustomerSelected(CustomerDto customer)
        {
            this.Model.CustomerId = customer.Id;
            this.Model.CustomerShortName = customer.FullName;
        }



        [Command]
        public void ShowSelectSupplierView()
        {
            SupplierSingleLookupViewModel? viewModel = _serviceProvider.GetService<SupplierSingleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnSupplierSelected;
                WindowService.Title = "选择供应商";
                WindowService.Show(nameof(SupplierSingleLookupView), viewModel);
            }
        }


        private void OnSupplierSelected(SupplierDto supplier)
        {
            this.Model.SupplierId = supplier.Id;
            this.Model.SupplierShortName = supplier.FullName;
        }

    }
}
