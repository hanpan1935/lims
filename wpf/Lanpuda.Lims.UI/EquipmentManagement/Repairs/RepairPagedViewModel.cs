using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Repairs.Dtos;
using Lanpuda.Lims.Repairs;
using Lanpuda.Lims.UI.EquipmentManagement.Repairs.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.Maintenances;
using System.ComponentModel;
using Lanpuda.Client.Common;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Lookups;
using Lanpuda.Lims.Calibrations;
using System.Windows;

namespace Lanpuda.Lims.UI.EquipmentManagement.Repairs
{
    public class RepairPagedViewModel : PagedViewModelBase<RepairDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepairAppService _repairAppService;
        public Dictionary<string, RepairResult> RepairResultSource { get; }
        public RepairPagedViewModel(IServiceProvider serviceProvider, IRepairAppService repairAppService)
        {
            this.PageTitle = "维修记录";
            _serviceProvider = serviceProvider;
            _repairAppService = repairAppService;
            RepairResultSource = EnumUtils.EnumToDictionary<RepairResult>();
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


        public DateTime? RepairDate
        {
            get { return GetProperty(() => RepairDate); }
            set { SetProperty(() => RepairDate, value); }
        }

        public RepairResult? RepairResult
        {
            get { return GetProperty(() => RepairResult); }
            set { SetProperty(() => RepairResult, value); }
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
                RepairGetListInput input = new RepairGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Number = this.Number;
                input.EquipmentId = this.EquipmentId;
                input.RepairDate = this.RepairDate;
                input.RepairResult = this.RepairResult;

                var result = await _repairAppService.GetPagedListAsync(input);
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
                RepairEditViewModel? viewModel = _serviceProvider.GetService<RepairEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "维修记录-新建";
                    WindowService.Show(nameof(RepairEditView), viewModel);
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
                RepairEditViewModel? viewModel = _serviceProvider.GetService<RepairEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "维修记录-编辑";
                    WindowService.Show(nameof(RepairEditView), viewModel);
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
                    await _repairAppService.DeleteAsync(this.SelectedModel.Id);
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
            this.RepairDate = null;
            this.RepairResult = null;
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
