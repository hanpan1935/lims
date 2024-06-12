using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Maintenances.Dtos;
using Lanpuda.Lims.Maintenances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Lanpuda.Lims.UI.BasicInformations.Customers.Lookups;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Lookups;
using DevExpress.Mvvm;
using static Lanpuda.Lims.Permissions.LimsPermissions;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Client.Common;

namespace Lanpuda.Lims.UI.EquipmentManagement.Maintenances.Edits
{
    public class MaintenanceEditViewModel : EditViewModelBase<MaintenanceEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IMaintenanceAppService _maintenanceAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;
        public Dictionary<string, MaintenanceType> MaintenanceTypeSource { get; set; }
        public Dictionary<string, MaintenanceResult> MaintenanceResultSource { get; set; }

        public MaintenanceEditViewModel(
            IMaintenanceAppService maintenanceAppService,
            IObjectMapper objectMapper, 
            IServiceProvider serviceProvider)
        {
            _maintenanceAppService = maintenanceAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
            MaintenanceTypeSource = EnumUtils.EnumToDictionary<MaintenanceType>();
            MaintenanceResultSource = EnumUtils.EnumToDictionary<MaintenanceResult>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _maintenanceAppService.GetAsync(id);
                    Model = _objectMapper.Map<MaintenanceDto, MaintenanceEditModel>(result);
                }
                else
                {
                    this.Model.Date = DateTime.Now;
                    this.Model.MaintenanceType = MaintenanceType.Scheduled;
                    this.Model.Result = MaintenanceResult.Completed;
                }

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



        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null)
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
                MaintenanceCreateDto dto = _objectMapper.Map<MaintenanceEditModel, MaintenanceCreateDto>(this.Model);
                await _maintenanceAppService.CreateAsync(dto);
                if (RefreshPagedViewFunc != null)
                {
                    await RefreshPagedViewFunc();
                    this.Close();
                }
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


        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                MaintenanceUpdateDto dto = _objectMapper.Map<MaintenanceEditModel, MaintenanceUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _maintenanceAppService.UpdateAsync((Guid)this.Model.Id, dto);
                if (RefreshPagedViewFunc != null)
                {
                    await RefreshPagedViewFunc();
                    this.Close();
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
            this.Model.EquipmentName = equipment.Name;
            this.Model.EquipmentId = equipment.Id;
        }
    }
}
