using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Maintenances.Dtos;
using Lanpuda.Lims.Maintenances;
using Lanpuda.Lims.UI.EquipmentManagement.Maintenances.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.Equipments;
using System.ComponentModel;
using Lanpuda.Client.Common;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Lookups;
using System.Windows;

namespace Lanpuda.Lims.UI.EquipmentManagement.Maintenances
{
    public class MaintenancePagedViewModel : PagedViewModelBase<MaintenanceDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMaintenanceAppService _maintenanceAppService;
        public Dictionary<string, MaintenanceType> MaintenanceTypeSource { get; set; }
        public Dictionary<string, MaintenanceResult> MaintenanceResultSource { get; set; }
        public MaintenancePagedViewModel(IServiceProvider serviceProvider, IMaintenanceAppService maintenanceAppService)
        {
            this.PageTitle = "设备维护";
            _serviceProvider = serviceProvider;
            _maintenanceAppService = maintenanceAppService;
            MaintenanceTypeSource = EnumUtils.EnumToDictionary<MaintenanceType>();
            MaintenanceResultSource = EnumUtils.EnumToDictionary<MaintenanceResult>();
        }

        #region search

        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public Guid? EquipmentId { get; set; }

        public string? EquipmentName
        {
            get { return GetProperty(() => EquipmentName); }
            set { SetProperty(() => EquipmentName, value); }
        }

        public DateTime? DateStart
        {
            get { return GetProperty(() => DateStart); }
            set { SetProperty(() => DateStart, value); }
        }


        public DateTime? DateEnd
        {
            get { return GetProperty(() => DateEnd); }
            set { SetProperty(() => DateEnd, value); }
        }


        public MaintenanceType? MaintenanceType
        {
            get { return GetProperty(() => MaintenanceType); }
            set { SetProperty(() => MaintenanceType, value); }
        }

        public MaintenanceResult? Result
        {
            get { return GetProperty(() => Result); }
            set { SetProperty(() => Result, value); }
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
                MaintenanceGetListInput input = new MaintenanceGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Number = this.Number;
                input.EquipmentId = this.EquipmentId;
                input.DateStart = this.DateStart;
                input.DateEnd = this.DateEnd;
                input.MaintenanceType = this.MaintenanceType;
                input.Result = this.Result;

                var result = await _maintenanceAppService.GetPagedListAsync(input);
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
                MaintenanceEditViewModel? viewModel = _serviceProvider.GetService<MaintenanceEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "设备维护-新建";
                    WindowService.Show(nameof(MaintenanceEditView), viewModel);
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
                MaintenanceEditViewModel? viewModel = _serviceProvider.GetService<MaintenanceEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "设备维护-编辑";
                    WindowService.Show(nameof(MaintenanceEditView), viewModel);
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
                    await _maintenanceAppService.DeleteAsync(this.SelectedModel.Id);
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
            this.DateStart = null;
            this.DateEnd = null;
            this.MaintenanceType = null;
            this.Result = null;
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
