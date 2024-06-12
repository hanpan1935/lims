using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.UI.EquipmentManagement.UsageHistories.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.UsageHistories.Dtos;
using Lanpuda.Lims.UsageHistories;
using Lanpuda.Lims.Repairs;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Lookups;
using Lanpuda.Client.Common;
using System.Windows;

namespace Lanpuda.Lims.UI.EquipmentManagement.UsageHistories
{
    public  class UsageHistoryPagedViewModel : PagedViewModelBase<UsageHistoryDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUsageHistoryAppService _usageHistoryAppService;
        public Dictionary<string, UsageHistoryType> UsageHistoryTypeSource { get; set; }
        public UsageHistoryPagedViewModel(IServiceProvider serviceProvider, IUsageHistoryAppService usageHistoryAppService)
        {
            this.PageTitle = "使用记录";
            _serviceProvider = serviceProvider;
            _usageHistoryAppService = usageHistoryAppService;
            UsageHistoryTypeSource = EnumUtils.EnumToDictionary<UsageHistoryType>();
        }

        #region search
        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public Guid? EquipmentId
        {
            get { return GetProperty(() => EquipmentId); }
            set { SetProperty(() => EquipmentId, value); }
        }

        public string? EquipmentName
        {
            get { return GetProperty(() => EquipmentName); }
            set { SetProperty(() => EquipmentName, value); }
        }
    

        public UsageHistoryType? UsageHistoryType
        {
            get { return GetProperty(() => UsageHistoryType); }
            set { SetProperty(() => UsageHistoryType, value); }
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
                UsageHistoryGetListInput input = new UsageHistoryGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Number = this.Number;
                input.EquipmentId = this.EquipmentId;
                input.UsageHistoryType = this.UsageHistoryType;

                var result = await _usageHistoryAppService.GetPagedListAsync(input);
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
                UsageHistoryEditViewModel? viewModel = _serviceProvider.GetService<UsageHistoryEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "使用记录-新建";
                    WindowService.Show(nameof(UsageHistoryEditView), viewModel);
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
                UsageHistoryEditViewModel? viewModel = _serviceProvider.GetService<UsageHistoryEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "使用记录-编辑";
                    WindowService.Show(nameof(UsageHistoryEditView), viewModel);
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
                    await _usageHistoryAppService.DeleteAsync(this.SelectedModel.Id);
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
            this.Number = null;
            this.EquipmentId = null;
            this.EquipmentName = null;
            this.UsageHistoryType = null;
            await QueryAsync();
        }


        [Command]
        public void ShowSelectEquipmentView()
        {
            EquipmentSingleLookupViewModel? viewModel = _serviceProvider.GetService<EquipmentSingleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnEquipmentSelected;
                WindowService.Title = "选择设备";
                WindowService.Show(nameof(EquipmentSingleLookupView), viewModel);
            }
        }

        private void OnEquipmentSelected(EquipmentDto equipment)
        {
            this.EquipmentName = equipment.Name;
            this.EquipmentId = equipment.Id;
        }
    }
    
}
