using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Repairs.Dtos;
using Lanpuda.Lims.Repairs;
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

namespace Lanpuda.Lims.UI.EquipmentManagement.Repairs.Edits
{
    public  class RepairEditViewModel : EditViewModelBase<RepairEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IRepairAppService _repairAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;
        public Dictionary<string, RepairResult> RepairResultSource { get; set; }
        public RepairEditViewModel(
            IRepairAppService repairAppService, 
            IObjectMapper objectMapper, 
            IServiceProvider serviceProvider)
        {
            _repairAppService = repairAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
            RepairResultSource = EnumUtils.EnumToDictionary<RepairResult>();
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
                    var result = await _repairAppService.GetAsync(id);
                    Model = _objectMapper.Map<RepairDto, RepairEditModel>(result);
                }
                else
                {
                    this.Model.RepairDate = DateTime.Now;
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
                RepairCreateDto dto = _objectMapper.Map<RepairEditModel, RepairCreateDto>(this.Model);
                await _repairAppService.CreateAsync(dto);
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
                RepairUpdateDto dto = _objectMapper.Map<RepairEditModel, RepairUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _repairAppService.UpdateAsync((Guid)this.Model.Id, dto);
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
