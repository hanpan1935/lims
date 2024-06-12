using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InventoryLogs.Dtos;
using Lanpuda.Lims.InventoryLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.InspectionTasks;
using System.ComponentModel;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using Lanpuda.Lims.Warehouses.Dtos;
using Lanpuda.Lims.Warehouses;
using Lanpuda.Lims.Locations.Dtos;

namespace Lanpuda.Lims.UI.InventoryManagement.InventoryLogs
{
    public class InventoryLogPagedViewModel : PagedViewModelBase<InventoryLogDto>
    {
        private readonly IInventoryLogAppService _inventoryLogAppService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWarehouseAppService _warehouseAppService;
        public ObservableCollection<WarehouseLookupDto> WarehouseSource { get; set; }
        #region search
        /// <summary>
        /// 发生单号
        /// </summary>
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

        

        public string? Reason
        {
            get { return GetProperty(() => Reason); }
            set { SetProperty(() => Reason, value); }
        }
        /// <summary>
        /// 批次号
        /// </summary>
        public string? LotNumber
        {
            get { return GetProperty(() => LotNumber); }
            set { SetProperty(() => LotNumber, value); }
        }

        public WarehouseLookupDto? SelectedWarehouse
        {
            get { return GetProperty(() => SelectedWarehouse); }
            set { SetProperty(() => SelectedWarehouse, value); }
        }


        public LocationDto? SelectedLocation
        {
            get { return GetProperty(() => SelectedLocation); }
            set { SetProperty(() => SelectedLocation, value); }
        }


        #endregion


        public InventoryLogPagedViewModel(
            IInventoryLogAppService inventoryLogAppService, 
            IServiceProvider serviceProvider, 
            IWarehouseAppService warehouseAppService)
        {
            _inventoryLogAppService = inventoryLogAppService;
            _serviceProvider = serviceProvider;
            _warehouseAppService = warehouseAppService;
            WarehouseSource = new ObservableCollection<WarehouseLookupDto>();
        }

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true; 
                var warehouseList = await _warehouseAppService.LookupAsync();
                foreach (var item in warehouseList)
                {
                    this.WarehouseSource.Add(item);
                }
                await this.QueryAsync();
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


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                InventoryLogGetListInput input = new InventoryLogGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Number = this.Number;
                input.ProductId = this.ProductId;
                input.Reason = this.Reason;
                input.LotNumber = this.LotNumber;

                if (this.SelectedWarehouse != null)
                {
                    input.WarehouseId = SelectedWarehouse.Id;
                }
                if (this.SelectedLocation != null)
                {
                    input.LocationId = SelectedLocation.Id;
                }

                var result = await _inventoryLogAppService.GetPagedListAsync(input);
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
            this.Number = string.Empty;
            this.ProductId = null;
            this.ProductName = string.Empty;
            this.Reason = null;
            this.LotNumber = string.Empty;
            this.SelectedLocation = null;
            this.SelectedWarehouse = null;
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

    }
}
