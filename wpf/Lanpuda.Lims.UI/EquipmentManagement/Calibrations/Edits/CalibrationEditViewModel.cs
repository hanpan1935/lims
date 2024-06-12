using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Calibrations.Dtos;
using Lanpuda.Lims.Calibrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Lookups;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Mvvm;
using Lanpuda.Client.Common;

namespace Lanpuda.Lims.UI.EquipmentManagement.Calibrations.Edits
{
    public class CalibrationEditViewModel : EditViewModelBase<CalibrationEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly ICalibrationAppService _calibrationAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;
        public Dictionary<string, CalibrationResult> CalibrationResultSource { get; set; }

        public CalibrationEditViewModel(
            ICalibrationAppService calibrationAppService, 
            IObjectMapper objectMapper, 
            IServiceProvider serviceProvider)
        {
            _calibrationAppService = calibrationAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
            CalibrationResultSource = EnumUtils.EnumToDictionary<CalibrationResult>();
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
                    var result = await _calibrationAppService.GetAsync(id);
                    Model = _objectMapper.Map<CalibrationDto, CalibrationEditModel>(result);

                }
                else
                {
                    this.Model.CalibrationDate = DateTime.Now;
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
                CalibrationCreateDto dto = _objectMapper.Map<CalibrationEditModel, CalibrationCreateDto>(this.Model);
                await _calibrationAppService.CreateAsync(dto);
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
                CalibrationUpdateDto dto = _objectMapper.Map<CalibrationEditModel, CalibrationUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _calibrationAppService.UpdateAsync((Guid)this.Model.Id, dto);
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
