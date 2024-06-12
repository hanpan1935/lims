using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Inventories.Dtos;
using Lanpuda.Lims.Inventories;
using Lanpuda.Lims.Locations.Dtos;
using Lanpuda.Lims.Warehouses.Dtos;
using Lanpuda.Lims.Warehouses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.InventoryManagement.Inventories.Lookups
{
    public class InventoryMultipleLookupViewModel : PagedViewModelBase<InventoryDto>
    {
        private readonly IInventoryAppService _inventoryAppService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWarehouseAppService _warehouseAppService;
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        public Action<ICollection<InventoryDto>>? OnSaveCallback;


        public InventoryDto? SelectedListSelectedRow
        {
            get { return GetProperty(() => SelectedListSelectedRow); }
            set { SetProperty(() => SelectedListSelectedRow, value); }
        }
        public ObservableCollection<InventoryDto> SelectedList { get; set; }

        #region SearchItems

        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public string LotNumber
        {
            get { return GetProperty(() => LotNumber); }
            set { SetProperty(() => LotNumber, value); }
        }

        public WarehouseLookupDto? SelectedWarehouse
        {
            get { return GetProperty(() => SelectedWarehouse); }
            set { SetProperty(() => SelectedWarehouse, value); }
        }

        public ObservableCollection<WarehouseLookupDto> WarehouseSource { get; set; }

        public LocationDto? SelectedLocation
        {
            get { return GetProperty(() => SelectedLocation); }
            set { SetProperty(() => SelectedLocation, value); }
        }

        #endregion


        public InventoryMultipleLookupViewModel(
            IInventoryAppService inventoryAppService,
            IWarehouseAppService warehouseAppService,
            IServiceProvider serviceProvider)
        {
            _inventoryAppService = inventoryAppService;
            _serviceProvider = serviceProvider;
            _warehouseAppService = warehouseAppService;
            WarehouseSource = new ObservableCollection<WarehouseLookupDto>();
            SelectedList = new ObservableCollection<InventoryDto>();
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var warehouses = await _warehouseAppService.LookupAsync();
                this.WarehouseSource.Clear();
                foreach (var item in warehouses)
                {
                    this.WarehouseSource.Add(item);
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


        [Command]
        public void Selected()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            this.SelectedList.Add(SelectedModel);

        }

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.LotNumber = string.Empty;
            this.SelectedLocation = null;
            this.SelectedWarehouse = null;
            this.ProductName = string.Empty;
            await this.QueryAsync();
        }


        [Command]
        public void DeleteSelectedListRow()
        {
            if (this.SelectedListSelectedRow != null)
            {
                SelectedList.Remove(SelectedListSelectedRow);
            }
        }

        [Command]
        public void Save()
        {
            if (OnSaveCallback != null)
            {
                OnSaveCallback(this.SelectedList);
                this.Close();
            }
        }

        [Command]
        public void Close()
        {
            this.CurrentWindowService.Close();
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                InventoryGetListInput requestDto = new InventoryGetListInput();
                requestDto.MaxResultCount = this.DataCountPerPage;
                requestDto.SkipCount = this.SkipCount;
                requestDto.ProductName = this.ProductName;
                if (this.SelectedWarehouse !=null)
                {
                    requestDto.WarehouseId = this.SelectedWarehouse.Id;
                }
                if (this.SelectedLocation != null)
                {
                    requestDto.LocationId = this.SelectedLocation.Id;
                }
                requestDto.LotNumber = this.LotNumber;

                var result = await _inventoryAppService.GetPagedListAsync(requestDto);
                this.TotalCount = result.TotalCount;
                PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                PagedDatas.CanNotify = true;
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
