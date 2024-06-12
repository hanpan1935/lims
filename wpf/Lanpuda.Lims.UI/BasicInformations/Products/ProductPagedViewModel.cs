using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.UI.BasicInformations.Products.Edits;
using Lanpuda.Lims.UI.BasicInformations.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.Lims.Products;
using Lanpuda.Lims.Customers.Dtos;
using Lanpuda.Lims.Customers;
using Lanpuda.Lims.Products.Dtos;
using System.ComponentModel;
using HandyControl.Collections;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;
using HandyControl.Properties.Langs;
using Lanpuda.Lims.Locations;
using System.Windows;
using DevExpress.Mvvm.UI;

namespace Lanpuda.Lims.UI.BasicInformations.Products
{
    public class ProductPagedViewModel : PagedViewModelBase<ProductDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IProductAppService _productAppService;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;

        public ManualObservableCollection<DicProductTypeLookupDto> ProductTypeSource { get; set; } 

        #region 搜索
        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        public string Unit
        {
            get { return GetProperty(() => Unit); }
            set { SetProperty(() => Unit, value); }
        }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public int? DicProductTypeId
        {
            get { return GetProperty(() => DicProductTypeId); }
            set { SetProperty(() => DicProductTypeId, value); }
        }


        public string Spec
        {
            get { return GetProperty(() => Spec); }
            set { SetProperty(() => Spec, value); }
        }
    
        #endregion

        public ProductPagedViewModel(IServiceProvider serviceProvider, IProductAppService productAppService, IDataDictionaryAppService dataDictionaryAppService)
        {
            this.PageTitle = Assets.Langs.Lang.Product;
            _serviceProvider = serviceProvider;
            _productAppService = productAppService;
            _dataDictionaryAppService = dataDictionaryAppService;
            this.ProductTypeSource = new ManualObservableCollection<DicProductTypeLookupDto>();
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var productTypeList = await _dataDictionaryAppService.LookupProductTypeAsync();
                this.ProductTypeSource.Clear();
                foreach (var item in productTypeList)
                {
                    this.ProductTypeSource.Add(item);
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
                ProductGetListInput input = new ProductGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Number = this.Number;
                input.Name = this.Name;
                input.Unit = this.Unit;
                input.Spec = this.Spec;
                input.DicProductTypeId = this.DicProductTypeId;

                var result = await _productAppService.GetPagedListAsync(input);
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
                throw;
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
                ProductEditViewModel? viewModel = _serviceProvider.GetService<ProductEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = Assets.Langs.Lang.Product + Assets.Langs.Lang.Create;
                  

                    string name1 = nameof(ProductEditView);
                    string name2 = typeof(ProductEditView).FullName;
                    string name3 = typeof(ProductEditView).Name;


                    WindowService.Show(name2, viewModel);

                    // WindowService.Show(nameof(ProductEditView), viewModel);
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
                ProductEditViewModel? productCategoryEditViewModel = _serviceProvider.GetService<ProductEditViewModel>();
                if (productCategoryEditViewModel != null)
                {
                    productCategoryEditViewModel.Model.Id = this.SelectedModel.Id;
                    productCategoryEditViewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = Assets.Langs.Lang.Product + Assets.Langs.Lang.Edit;
                    WindowService.Show(nameof(ProductEditView), productCategoryEditViewModel);
                }
            }
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = string.Empty;
            this.Name = string.Empty;
            this.Unit = string.Empty;
            this.Spec = string.Empty;
            this.DicProductTypeId = null;
            await QueryAsync();
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
                    await _productAppService.DeleteAsync(this.SelectedModel.Id);
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

    }
}
