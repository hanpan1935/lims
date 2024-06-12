using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Collections;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Customers;
using Lanpuda.Lims.Locations;
using Lanpuda.Lims.Locations.Dtos;
using Lanpuda.Lims.UI.BasicInformations.Locations.Edits;
using Lanpuda.Lims.Warehouses;
using Lanpuda.Lims.Warehouses.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.Lims.UI.BasicInformations.Locations
{
    public class LocationPagedViewModel : PagedViewModelBase<LocationDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILocationAppService _locationAppService;
        private readonly IWarehouseAppService _warehouseAppService;
        public ManualObservableCollection<WarehouseLookupDto> WarehouseSource
        {
            get { return GetProperty(() => WarehouseSource); }
            set { SetProperty(() => WarehouseSource, value); }
        }


     

        public LocationPagedViewModel(IServiceProvider serviceProvider, ILocationAppService locationAppService, IWarehouseAppService warehouseAppService)
        {
            this.PageTitle = "库位设置";
            _serviceProvider = serviceProvider; 
            _locationAppService = locationAppService;
            _warehouseAppService = warehouseAppService;
            WarehouseSource = new ManualObservableCollection<WarehouseLookupDto>();
        }


        #region 搜索
        public WarehouseLookupDto? SelectedWarehouse
        {
            get { return GetProperty(() => SelectedWarehouse); }
            set { SetProperty(() => SelectedWarehouse, value); }
        }

        public string? Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }
        #endregion


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var warehouseList = await _warehouseAppService.LookupAsync();
                WarehouseSource.CanNotify = false;
                WarehouseSource.Clear();
                foreach (var item in warehouseList)
                {
                    WarehouseSource.Add(item);
                }
                WarehouseSource.CanNotify = true;
                await this.QueryAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            
            }
            finally { this.IsLoading = false; }
           
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                LocationGetListInput input = new LocationGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                if (this.SelectedWarehouse != null)
                {
                    input.WarehouseId = SelectedWarehouse.Id;
                }
                input.Name = this.Name;
                input.Remark = this.Remark;
                var result = await _locationAppService.GetPagedListAsync(input);
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
                LocationEditViewModel? viewModel = _serviceProvider.GetService<LocationEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "库位设置-新建";
                    WindowService.Show(nameof(LocationEditView), viewModel);
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
                LocationEditViewModel? viewModel = _serviceProvider.GetService<LocationEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "库位设置-编辑";
                    WindowService.Show(nameof(LocationEditView), viewModel);
                }
            }
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
                    await _locationAppService.DeleteAsync(this.SelectedModel.Id);
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

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.SelectedWarehouse = null;
            this.Name = null;
            await this.QueryAsync();
        }

    }
}
