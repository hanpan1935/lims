using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using Lanpuda.Lims.Products.Dtos;
using Lanpuda.Lims.Warehouses.Dtos;
using Lanpuda.Lims.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InventoryStores;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using DevExpress.Mvvm;
using Lanpuda.Lims.InventoryStores.Dtos;
using DevExpress.Mvvm.Native;
using System.Collections.ObjectModel;

namespace Lanpuda.Lims.UI.InventoryManagement.InventoryStores.Edits
{
    public  class InventoryStoreEditViewModel : EditViewModelBase<InventoryStoreEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IInventoryStoreAppService _inventoryStoreAppService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWarehouseAppService _warehouseLookupAppService;
        private List<WarehouseLookupDto> _warehouseList;



        public InventoryStoreEditViewModel(
            IInventoryStoreAppService inventoryStoreAppService,
            IWarehouseAppService warehouseLookupAppService,
            IServiceProvider serviceProvider)
        {
            _inventoryStoreAppService = inventoryStoreAppService;
            _warehouseLookupAppService = warehouseLookupAppService;
            _serviceProvider = serviceProvider;
            _warehouseList = new List<WarehouseLookupDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                _warehouseList = await _warehouseLookupAppService.LookupAsync();

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _inventoryStoreAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.Remark = result.Remark;
                    foreach (var detail in result.Details)
                    {
                        InventoryStoreDetailEditModel detailModel = new InventoryStoreDetailEditModel();
                        detailModel.Id = detail.Id;
                        foreach (var item in _warehouseList)
                        {
                            detailModel.WarehouseSource.Add(item);
                        }
                        detailModel.ProductId = detail.ProductId;
                        detailModel.LocationId = detail.LocationId;
                        detailModel.LotNumber = detail.LotNumber;
                        detailModel.Quantity = detail.Quantity;
                        detailModel.SelectedWarehouse = detailModel.WarehouseSource.Where(m => m.Id == detail.WarehouseId).FirstOrDefault();
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductUnit = detail.ProductUnit;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "销售订单-新建";
                    this.Model.Number = "系统自动生成";
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
        public void ShowSelectProductWindow()
        {
            ProductMultipleLookupViewModel? viewModel = _serviceProvider.GetService<ProductMultipleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = this.OnProductSelected;
                this.WindowService.Title = "请选择产品";
                this.WindowService.Show(nameof(ProductMultipleLookupView), viewModel);
            }
        }

        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedDetailRow != null)
            {
                this.Model.Details.Remove(Model.SelectedDetailRow);
            }
        }

        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null || Model.Id == Guid.Empty)
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
                InventoryStoreCreateDto dto = new InventoryStoreCreateDto();
                dto.Reason = Model.Reason;
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    InventoryStoreDetailCreateDto detailDto = new InventoryStoreDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.ProductId = detail.ProductId;
                    detailDto.LocationId = detail.LocationId;
                    detailDto.LotNumber = detail.LotNumber;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                await _inventoryStoreAppService.CreateAsync(dto);
                
                if (RefreshPagedViewFunc != null)
                {
                    await RefreshPagedViewFunc();
                }
                CurrentWindowService.Close();

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
                InventoryStoreUpdateDto dto = new InventoryStoreUpdateDto();
                dto.Remark = Model.Remark;
                dto.Reason = Model.Reason;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    InventoryStoreDetailUpdateDto detailDto = new InventoryStoreDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.Id = detail.Id;
                    detailDto.ProductId = detail.ProductId;
                    detailDto.LocationId = detail.LocationId;
                    detailDto.LotNumber = detail.LotNumber;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _inventoryStoreAppService.UpdateAsync((Guid)Model.Id, dto);
               
                if (RefreshPagedViewFunc != null)
                {
                    await RefreshPagedViewFunc();
                }
                CurrentWindowService.Close();
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

        private void OnProductSelected(ICollection<ProductDto> products)
        {
            foreach (var item in products)
            {
                InventoryStoreDetailEditModel detailEditModel = new InventoryStoreDetailEditModel();
                detailEditModel.ProductId = item.Id;
                detailEditModel.ProductName = item.Name;
                detailEditModel.ProductUnit = item.Unit;
                detailEditModel.WarehouseSource = new ObservableCollection<WarehouseLookupDto>(_warehouseList);
                this.Model.Details.Add(detailEditModel);
            }
        }
    }
}
