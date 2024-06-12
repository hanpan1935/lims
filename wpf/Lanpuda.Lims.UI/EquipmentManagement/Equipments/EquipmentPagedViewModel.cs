using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Lims.Equipments;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.Lims.Permissions.LimsPermissions;
using Lanpuda.Client.Common;
using System.Collections.ObjectModel;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;
using Lanpuda.Lims.Calibrations;
using Lanpuda.Lims.Standards;
using System.Windows;

namespace Lanpuda.Lims.UI.EquipmentManagement.Equipments
{
    public class EquipmentPagedViewModel : PagedViewModelBase<EquipmentDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEquipmentAppService _equipmentAppService;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        public Dictionary<string, EquipmentStatus> EquipmentStatusSource { get; set; } 
        public Dictionary<string, MaintenancePeriodType> MaintenancePeriodTypeSource { get; set; } 
        public ObservableCollection<DicEquipmentTypeLookupDto> DicEquipmentTypeSource { get; set; }
        public EquipmentPagedViewModel(IServiceProvider serviceProvider, IEquipmentAppService equipmentAppService, IDataDictionaryAppService dataDictionaryAppService)
        {
            this.PageTitle = "设备信息";
            _serviceProvider = serviceProvider;
            _equipmentAppService = equipmentAppService;
            this.EquipmentStatusSource = EnumUtils.EnumToDictionary<EquipmentStatus>();
            this.MaintenancePeriodTypeSource = EnumUtils.EnumToDictionary<MaintenancePeriodType>();
            DicEquipmentTypeSource = new ObservableCollection<DicEquipmentTypeLookupDto>();
            _dataDictionaryAppService = dataDictionaryAppService;
        }

        #region 搜索
        // 设备名称
        public string? Name 
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }
        // 设备状态
        public EquipmentStatus? Status
        {
            get { return GetProperty(() => Status); }
            set { SetProperty(() => Status, value); }
        }

        //维护周期：指示预防性维护的周期，例如每月、每季度、每年等。
        public MaintenancePeriodType? MaintenancePeriod
        {
            get { return GetProperty(() => MaintenancePeriod); }
            set { SetProperty(() => MaintenancePeriod, value); }
        }

        //设备编号
        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        //设备类型 例如仪器、工具、仪表等。  字典表
        public DicEquipmentTypeLookupDto? EquipmentType
        {
            get { return GetProperty(() => EquipmentType); }
            set { SetProperty(() => EquipmentType, value); }
        }

        // 设备规格
        public string? Spec
        {
            get { return GetProperty(() => Spec); }
            set { SetProperty(() => Spec, value); }
        }  
        // 制造商
        public string? Manufacturer
        {
            get { return GetProperty(() => Manufacturer); }
            set { SetProperty(() => Manufacturer, value); }
        }  


        // 安装位置
        public string? InstallationLocation
        {
            get { return GetProperty(() => InstallationLocation); }
            set { SetProperty(() => InstallationLocation, value); }
        } 

        #endregion


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var result = await _dataDictionaryAppService.LookupEquipmentTypeAsync();
                this.DicEquipmentTypeSource.Clear();
                foreach (var item in result)
                {
                    this.DicEquipmentTypeSource.Add(item);
                }

                await this.QueryAsync();
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




        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                EquipmentGetListInput input = new EquipmentGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                //
                input.Name = this.Name;
                input.Status = this.Status;
                input.MaintenancePeriod = this.MaintenancePeriod;
                input.Number = this.Number;
                if (this.EquipmentType != null)
                {
                    input.DicEquipmentTypeId = this.EquipmentType.Id;
                }
               
                input.Spec = this.Spec;
                input.Manufacturer = this.Manufacturer;
                input.InstallationLocation = this.InstallationLocation;

                var result = await _equipmentAppService.GetPagedListAsync(input);
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
                EquipmentEditViewModel? viewModel = _serviceProvider.GetService<EquipmentEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "设备信息-新建";
                    WindowService.Show(nameof(EquipmentEditView), viewModel);
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
                EquipmentEditViewModel? viewModel = _serviceProvider.GetService<EquipmentEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "设备信息-编辑";
                    WindowService.Show(nameof(EquipmentEditView), viewModel);
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
                    await _equipmentAppService.DeleteAsync(this.SelectedModel.Id);
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
            this.Name = null;
            this.Status = null;
            this.MaintenancePeriod = null;
            this.Number = null;
            this.EquipmentType = null;
            this.Spec = null;
            this.Manufacturer = null;
            this.InstallationLocation = null;
            await QueryAsync();
        }
    }
}
