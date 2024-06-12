using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InspectionItems.Dtos;
using Lanpuda.Lims.InspectionItems;
using Lanpuda.Lims.UI.InspectionMethods.InspectionItems;
using Lanpuda.Lims.UI.InspectionMethods.InspectionItems.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.UsageHistories;
using System.ComponentModel.DataAnnotations;
using Lanpuda.Lims.InspectionTasks;
using System.Windows;

namespace Lanpuda.Lims.UI.InspectionMethods.InspectionItems
{
    public class InspectionItemPagedViewModel : PagedViewModelBase<InspectionItemDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IInspectionItemAppService _inspectionItemAppService;
        public InspectionItemPagedViewModel(IServiceProvider serviceProvider, IInspectionItemAppService inspectionItemAppService)
        {
            this.PageTitle = "检验项目";
            _serviceProvider = serviceProvider;
            _inspectionItemAppService = inspectionItemAppService;
        }

        #region search

        public string? ShortName
        {
            get { return GetProperty(() => ShortName); }
            set { SetProperty(() => ShortName, value); }
        }


        public string? FullName
        {
            get { return GetProperty(() => FullName); }
            set { SetProperty(() => FullName, value); }
        }


        public string? Basis
        {
            get { return GetProperty(() => Basis); }
            set { SetProperty(() => Basis, value); }
        }


        public string? Unit
        {
            get { return GetProperty(() => Unit); }
            set { SetProperty(() => Unit, value); }
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
                InspectionItemGetListInput input = new InspectionItemGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.FullName = this.FullName;
                input.ShortName = this.ShortName;
                input.Basis = this.Basis;
                input.Unit = this.Unit;

                var result = await _inspectionItemAppService.GetPagedListAsync(input);
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
                InspectionItemEditViewModel? viewModel = _serviceProvider.GetService<InspectionItemEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "检验项目-新建";
                    WindowService.Show(nameof(InspectionItemEditView), viewModel);
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
                InspectionItemEditViewModel? viewModel = _serviceProvider.GetService<InspectionItemEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "检验项目-编辑";
                    WindowService.Show(nameof(InspectionItemEditView), viewModel);
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
                    await _inspectionItemAppService.DeleteAsync(this.SelectedModel.Id);
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

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.FullName = null;
            this.ShortName = null;
            this.Unit = null;
            this.Basis = null;
            await QueryAsync();
        }
    }
}
