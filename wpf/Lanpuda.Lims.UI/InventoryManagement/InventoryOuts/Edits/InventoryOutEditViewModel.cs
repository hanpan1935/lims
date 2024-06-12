using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Inventories.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.InventoryOuts;
using Lanpuda.Lims.UI.InventoryManagement.Inventories.Lookups;
using Lanpuda.Lims.InventoryOuts.Dtos;

namespace Lanpuda.Lims.UI.InventoryManagement.InventoryOuts.Edits
{
    public class InventoryOutEditViewModel : EditViewModelBase<InventoryOutEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IInventoryOutAppService _inventoryOutAppService;
        private readonly IServiceProvider _serviceProvider;


        public InventoryOutEditViewModel(
            IInventoryOutAppService inventoryOutAppService,
            IServiceProvider serviceProvider)
        {
            _inventoryOutAppService = inventoryOutAppService;
            _serviceProvider = serviceProvider;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "其他出库-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _inventoryOutAppService.GetAsync(id);
                    Model.Reason = result.Reason;
                    Model.Remark = result.Remark;
                    Model.Number = result.Number;
                    foreach (var detail in result.Details)
                    {
                        InventoryOutDetailEditModel detailModel = new InventoryOutDetailEditModel();
                        detailModel.Id = detail.Id;
                        detailModel.ProductId = detail.ProductId;
                        detailModel.LocationId = detail.LocationId;
                        detailModel.LotNumber = detail.LotNumber;
                        detailModel.Quantity = detail.Quantity;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductUnitName = detail.ProductUnit;
                        detailModel.WarehouseName = detail.WarehouseName;
                        detailModel.LocationName = detail.LocationName;
                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "其他出库-新建";
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
        public void ShowSelectInventoryWindow()
        {
            InventoryMultipleLookupViewModel? viewModel = _serviceProvider.GetService<InventoryMultipleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSaveCallback = this.OnInventorySelected;
                this.WindowService.Title = "请选择产品";
                this.WindowService.Show("InventoryMultipleLookupView", viewModel);
            }
        }

        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedRow != null)
            {
                this.Model.Details.Remove(Model.SelectedRow);
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
                InventoryOutCreateDto dto = new InventoryOutCreateDto();
                dto.Remark = Model.Remark;
                dto.Reason = Model.Reason;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    InventoryOutDetailCreateDto detailDto = new InventoryOutDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.LocationId = detail.LocationId;
                    detailDto.LotNumber = detail.LotNumber;
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                await _inventoryOutAppService.CreateAsync(dto);
                CurrentWindowService.Close();
                if (RefreshPagedViewFunc != null)
                {
                    await RefreshPagedViewFunc();
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

        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                InventoryOutUpdateDto dto = new InventoryOutUpdateDto();
                dto.Reason = Model.Reason;
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    InventoryOutDetailUpdateDto detailDto = new InventoryOutDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.Id = detail.Id;
                    detailDto.LocationId = detail.LocationId;
                    detailDto.LotNumber = detail.LotNumber;
                    detailDto.ProductId = detail.ProductId;
                    detailDto.Quantity = detail.Quantity;
                    dto.Details.Add(detailDto);
                }
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _inventoryOutAppService.UpdateAsync((Guid)Model.Id, dto);
                CurrentWindowService.Close();
                if (RefreshPagedViewFunc != null)
                {
                    await RefreshPagedViewFunc();
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

        private void OnInventorySelected(ICollection<InventoryDto> inventoryList)
        {
            foreach (var inventory in inventoryList)
            {

                var has = this.Model.Details.Any(m=>m.InventoryId ==  inventory.Id);
                if (has)
                {
                    continue;
                }

                InventoryOutDetailEditModel detailEditModel = new InventoryOutDetailEditModel();
                detailEditModel.InventoryId = inventory.Id;
                detailEditModel.LocationId = inventory.LocationId;
                detailEditModel.LotNumber = inventory.LotNumber;
                detailEditModel.ProductId = inventory.ProductId;
                detailEditModel.ProductName = inventory.ProductName;
                detailEditModel.WarehouseName = inventory.WarehouseName;
                detailEditModel.LocationName = inventory.LocationName;
                this.Model.Details.Add(detailEditModel);
            }
        }
    }
}
