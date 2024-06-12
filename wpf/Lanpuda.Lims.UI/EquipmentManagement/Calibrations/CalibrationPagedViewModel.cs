using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Calibrations.Dtos;
using Lanpuda.Lims.Calibrations;
using Lanpuda.Lims.UI.EquipmentManagement.Calibrations.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Lanpuda.Lims.Locations;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Lookups;
using Lanpuda.Client.Common;
using Lanpuda.Lims.Maintenances;
using System.Windows;

namespace Lanpuda.Lims.UI.EquipmentManagement.Calibrations
{
    public class CalibrationPagedViewModel : PagedViewModelBase<CalibrationDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICalibrationAppService _calibrationAppService;
        public Dictionary<string, CalibrationResult> CalibrationResultSource { get; set; }
        public CalibrationPagedViewModel(IServiceProvider serviceProvider, ICalibrationAppService calibrationAppService)
        {
            this.PageTitle = "校准记录";
            _serviceProvider = serviceProvider;
            _calibrationAppService = calibrationAppService;
            CalibrationResultSource = EnumUtils.EnumToDictionary<CalibrationResult>();
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


        public DateTime? CalibrationDate
        {
            get { return GetProperty(() => CalibrationDate); }
            set { SetProperty(() => CalibrationDate, value); }
        }


        public DateTime? NextCalibrationDate
        {
            get { return GetProperty(() => NextCalibrationDate); }
            set { SetProperty(() => NextCalibrationDate, value); }
        }


        public CalibrationResult? CalibrationResult
        {
            get { return GetProperty(() => CalibrationResult); }
            set { SetProperty(() => CalibrationResult, value); }
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
                CalibrationGetListInput input = new CalibrationGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Number = this.Number;
                input.EquipmentId = this.EquipmentId;
                input.CalibrationDate = this.CalibrationDate;
                input.NextCalibrationDate = this.NextCalibrationDate;
                input.CalibrationResult = this.CalibrationResult;

                var result = await _calibrationAppService.GetPagedListAsync(input);
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
                CalibrationEditViewModel? viewModel = _serviceProvider.GetService<CalibrationEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "校准记录-新建";
                    WindowService.Show(nameof(CalibrationEditView), viewModel);
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
                CalibrationEditViewModel? viewModel = _serviceProvider.GetService<CalibrationEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "校准记录-编辑";
                    WindowService.Show(nameof(CalibrationEditView), viewModel);
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
                    await _calibrationAppService.DeleteAsync(this.SelectedModel.Id);
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
            this.CalibrationDate = null;
            this.NextCalibrationDate = null;
            this.CalibrationResult = null;
            await this.QueryAsync();
        }


        [Command]
        public void ShowEquipmentSelectView()
        {
            if (this.WindowService != null)
            {
                EquipmentSingleLookupViewModel? viewModel = _serviceProvider.GetService<EquipmentSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (equipment) =>
                    {
                        this.EquipmentId = equipment.Id;
                        this.EquipmentName = equipment.Name;
                    };
                    WindowService.Title = "选择设备";
                    WindowService.Show(nameof(EquipmentSingleLookupView), viewModel);
                }
            }
        }
    }
}
