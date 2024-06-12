using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InventoryStores.Dtos;
using Lanpuda.Lims.InventoryStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Mvvm;
using Lanpuda.Lims.UI.InventoryManagement.InventoryStores.Edits;
using Lanpuda.Lims.InspectionTasks;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Lanpuda.Client.Theme.Utils;
using Lanpuda.Lims.UsageHistories;
using System.Windows;

namespace Lanpuda.Lims.UI.InventoryManagement.InventoryStores
{
    public class InventoryStorePagedViewModel : PagedViewModelBase<InventoryStoreDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IInventoryStoreAppService _inventoryStoreAppService;
        public Dictionary<string,bool> IsSuccessfulSource { get; set; }

        #region search
        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string? Reason
        {
            get { return GetProperty(() => Reason); }
            set { SetProperty(() => Reason, value); }
        }


        public bool? IsSuccessful
        {
            get { return GetProperty(() => IsSuccessful); }
            set { SetProperty(() => IsSuccessful, value); }
        }


        public Guid? ProductId { get; set; }
        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        [AsyncCommand]
        public async void ResetAsync()
        {
            this.Number = null;
            this.ProductName = null;
            this.ProductId = null;
            this.IsSuccessful = null;
            this.Reason = null;
            await this.QueryAsync();
        }

        #endregion

        public InventoryStorePagedViewModel(IServiceProvider serviceProvider, IInventoryStoreAppService inventoryStoreAppService)
        {
            this.PageTitle = "入库操作";
            _serviceProvider = serviceProvider;
            _inventoryStoreAppService = inventoryStoreAppService;

            IsSuccessfulSource = ItemsSoureUtils.GetBoolDictionary();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                InventoryStoreGetListInput input = new InventoryStoreGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Number = this.Number;
                input.Reason = this.Reason;
                input.IsSuccessful = this.IsSuccessful;
                input.ProductId = this.ProductId;

                var result = await _inventoryStoreAppService.GetPagedListAsync(input);
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
                InventoryStoreEditViewModel? viewModel = _serviceProvider.GetService<InventoryStoreEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "入库操作-新建";
                    WindowService.Show(nameof(InventoryStoreEditView), viewModel);
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
                InventoryStoreEditViewModel? viewModel = _serviceProvider.GetService<InventoryStoreEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "入库操作-编辑";
                    WindowService.Show(nameof(InventoryStoreEditView), viewModel);
                }
            }
        }

        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsSuccessful == true)
            {
                return false;
            }
            return true;
        }


        [AsyncCommand]
        public async Task StoreAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }

                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要入库吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _inventoryStoreAppService.StoragedAsync(this.SelectedModel.Id);
                    await this.QueryAsync();
                }

               
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                this.IsLoading = false;
            }
        }
        public bool CanStoreAsync()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }
            if (this.SelectedModel.IsSuccessful == true)
            {
                return false;
            }
            return true;
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
                    await _inventoryStoreAppService.DeleteAsync(this.SelectedModel.Id);
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


        public bool CanDeleteAsync()
        {
            if (this.SelectedModel == null) { return false; }
            if (this.SelectedModel.IsSuccessful == true)
            {
                return false;
            }
            return true;
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


        

    }
}
