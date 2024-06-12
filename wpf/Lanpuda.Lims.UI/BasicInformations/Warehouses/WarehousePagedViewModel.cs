using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Warehouses.Dtos;
using Lanpuda.Lims.Warehouses;
using Lanpuda.Lims.UI.BasicInformations.Warehouses.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.Suppliers;
using System.Windows;

namespace Lanpuda.Lims.UI.BasicInformations.Warehouses
{
    public class WarehousePagedViewModel : PagedViewModelBase<WarehouseDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWarehouseAppService _warehouseAppService;

        public string? Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }


        public WarehousePagedViewModel(IServiceProvider serviceProvider, IWarehouseAppService warehouseAppService)
        {
            this.PageTitle = "仓库设置";
            _serviceProvider = serviceProvider;
            this._warehouseAppService = warehouseAppService;
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
                WarehouseGetListInput input = new WarehouseGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Name = this.Name;

                var result = await _warehouseAppService.GetPagedListAsync(input);
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
                WarehouseEditViewModel? viewModel = _serviceProvider.GetService<WarehouseEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "仓库设置-新建";
                    WindowService.Show(nameof(WarehouseEditView), viewModel);
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
                WarehouseEditViewModel? viewModel = _serviceProvider.GetService<WarehouseEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "仓库设置-编辑";
                    WindowService.Show(nameof(WarehouseEditView), viewModel);
                }
            }
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Name = null;
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
                    await _warehouseAppService.DeleteAsync(this.SelectedModel.Id);
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
    }   
}
