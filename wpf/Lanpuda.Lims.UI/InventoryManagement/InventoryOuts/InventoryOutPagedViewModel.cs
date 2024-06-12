using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.Client.Theme.Utils;
using Lanpuda.Lims.InspectionTasks;
using Lanpuda.Lims.InventoryOuts;
using Lanpuda.Lims.InventoryOuts.Dtos;
using Lanpuda.Lims.InventoryStores;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Lanpuda.Lims.UI.InventoryManagement.InventoryOuts.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.Lims.UI.InventoryManagement.InventoryOuts
{
    public class InventoryOutPagedViewModel : PagedViewModelBase<InventoryOutDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IInventoryOutAppService _inventoryOutAppService;
        private readonly IObjectMapper _objectMapper;

        public string Number
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


        public Dictionary<string, bool> IsSuccessfulSource { get; set; }


        public InventoryOutPagedViewModel(
            IServiceProvider serviceProvider,
            IInventoryOutAppService inventoryOutAppService,
            IObjectMapper objectMapper
            )
        {
            _serviceProvider = serviceProvider;
            _inventoryOutAppService = inventoryOutAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "其他出库";
            IsSuccessfulSource = ItemsSoureUtils.GetBoolDictionary();
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [Command]
        public void Create()
        {
            InventoryOutEditViewModel? inventoryOutEditViewModel = _serviceProvider.GetService<InventoryOutEditViewModel>();
            if (inventoryOutEditViewModel != null)
            {
                WindowService.Title = "入库操作-新建";
                inventoryOutEditViewModel.RefreshPagedViewFunc = QueryAsync;
                WindowService.Show("InventoryOutEditView", inventoryOutEditViewModel);
            }
        }



        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            InventoryOutEditViewModel? viewModel = _serviceProvider.GetService<InventoryOutEditViewModel>();
            if (viewModel != null)
            {
                WindowService.Title = "入库操作-编辑";
                viewModel.RefreshPagedViewFunc = QueryAsync;
                viewModel.Model.Id = SelectedModel.Id;
                WindowService.Show("InventoryOutEditView", viewModel);
            }
        }


        public bool CanUpdate()
        {
            if (this.SelectedModel == null)
            {
                return false;
            }

            if (this.SelectedModel.IsSuccessful != false)
            {
                return false;
            }
            return true;
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = String.Empty;
            this.IsSuccessful = null;
            this.ProductId = null;
            this.ProductName = string.Empty;
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
                    await _inventoryOutAppService.DeleteAsync(this.SelectedModel.Id);
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



        [AsyncCommand]
        public async Task OutedAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }

                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要出库吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _inventoryOutAppService.OutedAsync(this.SelectedModel.Id);
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

        public bool CanOutedAsync()
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
                    WindowService.Title = "选择库存";
                    WindowService.Show(nameof(ProductSingleLookupView), viewModel);
                }
            }
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                InventoryOutGetListInput input = new InventoryOutGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.IsSuccessful = this.IsSuccessful;
                input.Number = this.Number;
                input.Reason = this.Reason;
                input.ProductId = this.ProductId;
                var result = await _inventoryOutAppService.GetPagedListAsync(input);
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
    }
}
