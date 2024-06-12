using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InspectionTasks;
using Lanpuda.Lims.Inventories;
using Lanpuda.Lims.Inventories.Dtos;
using Lanpuda.Lims.Locations.Dtos;
using Lanpuda.Lims.UI.BasicInformations.Locations.Edits;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Lanpuda.Lims.Warehouses;
using Lanpuda.Lims.Warehouses.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.InventoryManagement.Inventories
{
    public class InventoryPagedViewModel : PagedViewModelBase<InventoryDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IInventoryAppService _inventoryAppService;
        private readonly IWarehouseAppService _warehouseAppService;
        #region search
        public ObservableCollection<WarehouseLookupDto> WarehouseSource { get; set; }

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

        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid? ProductId { get; set; }

        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }


        /// <summary>
        /// 
        /// </summary>
        public string? LotNumber
        {
            get { return GetProperty(() => LotNumber); }
            set { SetProperty(() => LotNumber, value); }
        }

        #endregion


        public InventoryPagedViewModel(IServiceProvider serviceProvider, IInventoryAppService inventoryAppService, IWarehouseAppService warehouseAppService)
        {
            this.PageTitle = "库存查询";
            _serviceProvider = serviceProvider;
            _inventoryAppService = inventoryAppService;
            _warehouseAppService = warehouseAppService;

            WarehouseSource = new ObservableCollection<WarehouseLookupDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var warehouseList =  await _warehouseAppService.LookupAsync();
                this.WarehouseSource.Clear();
                foreach (var item in warehouseList)
                {
                    this.WarehouseSource.Add(item);
                }
                await this.QueryAsync();
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



        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                InventoryGetListInput input = new InventoryGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                if (this.SelectedWarehouse != null)
                {
                    input.WarehouseId = this.SelectedWarehouse.Id;
                }
                if (this.SelectedLocation != null)
                {
                    input.LocationId = SelectedLocation.Id;
                }
                input.ProductId = this.ProductId;
                input.LotNumber = this.LotNumber;

                var result = await _inventoryAppService.GetPagedListAsync(input);
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
            this.SelectedWarehouse = null;
            this.SelectedLocation = null;
            this.ProductId = null;
            this.ProductName = null;
            this.LotNumber= null;
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
