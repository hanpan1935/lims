using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Products.Dtos;
using Lanpuda.Lims.Products;
using Lanpuda.Lims.UI.BasicInformations.Products.Edits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using System.Collections.ObjectModel;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;
using Lanpuda.Lims.UI.InspectionMethods.Standards.Lookups;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Mvvm;
using Lanpuda.Lims.Standards.Dtos;
using Lanpuda.Lims.UI.Records.Edits;

namespace Lanpuda.Lims.UI.BasicInformations.Products.Edits
{
    public class ProductEditViewModel : EditViewModelBase<ProductEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IProductAppService _productAppService;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;

        public ObservableCollection<DicProductTypeLookupDto> ProductTypeSource { get; set; }

        public ProductEditViewModel(
            IProductAppService productAppService,
            IServiceProvider serviceProvider,
            IObjectMapper objectMapper,
            IDataDictionaryAppService dataDictionaryAppService) 
        {
            _productAppService = productAppService;
            _dataDictionaryAppService = dataDictionaryAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
            ProductTypeSource = new ObservableCollection<DicProductTypeLookupDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var productTypeList = await _dataDictionaryAppService.LookupProductTypeAsync();
                foreach (var item in productTypeList)
                {
                    ProductTypeSource.Add(item);
                }
                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _productAppService.GetAsync(id);
                    Model = _objectMapper.Map<ProductDto, ProductEditModel>(result);
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
                ProductCreateDto dto = _objectMapper.Map<ProductEditModel, ProductCreateDto>(this.Model);
                await _productAppService.CreateAsync(dto);
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
                ProductUpdateDto dto = _objectMapper.Map<ProductEditModel, ProductUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _productAppService.UpdateAsync((Guid)this.Model.Id, dto);
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
            this.Model.StandardId = standard.Id;
            this.Model.StandardDescription = standard.Description;
        }
    }
}
