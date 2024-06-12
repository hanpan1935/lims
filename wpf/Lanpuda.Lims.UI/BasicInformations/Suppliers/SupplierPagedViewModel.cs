using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.UI.BasicInformations.Suppliers.Edits;
using Lanpuda.Lims.UI.BasicInformations.Suppliers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.Suppliers.Dtos;
using Lanpuda.Lims.Suppliers;
using System.ComponentModel;
using Lanpuda.Lims.Products;
using System.Windows;

namespace Lanpuda.Lims.UI.BasicInformations.Suppliers
{
    public class SupplierPagedViewModel : PagedViewModelBase<SupplierDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISupplierAppService _supplierAppService;
        public SupplierPagedViewModel(IServiceProvider serviceProvider,ISupplierAppService supplierAppService)
        {
            this.PageTitle = "供应商";
            _serviceProvider = serviceProvider;
            _supplierAppService = supplierAppService;
        }

        #region 搜索
        public string? FullName
        {
            get { return GetProperty(() => FullName); }
            set { SetProperty(() => FullName, value); }
        }

        public string? ShortName
        {
            get { return GetProperty(() => ShortName); }
            set { SetProperty(() => ShortName, value); }
        }

        public string? Manager
        {
            get { return GetProperty(() => Manager); }
            set { SetProperty(() => Manager, value); }
        }

        public string? ManagerTel
        {
            get { return GetProperty(() => ManagerTel); }
            set { SetProperty(() => ManagerTel, value); }
        }

        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }
     
        #endregion


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
                SupplierGetListInput input = new SupplierGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.FullName = this.FullName;
                input.ShortName = this.ShortName;
                input.Manager = this.Manager;
                input.ManagerTel = this.ManagerTel;
                input.Number = this.Number;

                var result = await _supplierAppService.GetPagedListAsync(input);
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
                SupplierEditViewModel? viewModel = _serviceProvider.GetService<SupplierEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "供应商-新建";
                    WindowService.Show(nameof(SupplierEditView), viewModel);
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
                SupplierEditViewModel? viewModel = _serviceProvider.GetService<SupplierEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "供应商-编辑";
                    WindowService.Show(nameof(SupplierEditView), viewModel);
                }
            }
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.FullName = null;
            this.ShortName = null;
            this.Number = null;
            this.Manager = null;
            this.ManagerTel = null;
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
                    await _supplierAppService.DeleteAsync(this.SelectedModel.Id);
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
