using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Lims.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Lanpuda.Client.Common;
using System.Collections.ObjectModel;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.DataDictionaries;

namespace Lanpuda.Lims.UI.EquipmentManagement.Equipments.Edits
{
    public class EquipmentEditViewModel : EditViewModelBase<EquipmentEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IEquipmentAppService _equipmentAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        public Dictionary<string,EquipmentStatus> EquipmentStatusSource { get; set; }
        public Dictionary<string, MaintenancePeriodType> MaintenancePeriodSource { get; set; }
        public ObservableCollection<DicEquipmentTypeLookupDto> EquipmentTypeSource { get; set; } 


        public EquipmentEditViewModel(
            IEquipmentAppService equipmentAppService,
            IDataDictionaryAppService dataDictionaryAppService,
            IObjectMapper objectMapper)
        {
            _equipmentAppService = equipmentAppService;
            _dataDictionaryAppService = dataDictionaryAppService;
            _objectMapper = objectMapper;
            this.PageTitle = "设备信息";
            EquipmentStatusSource = EnumUtils.EnumToDictionary<EquipmentStatus>();
            MaintenancePeriodSource = EnumUtils.EnumToDictionary<MaintenancePeriodType>();
            EquipmentTypeSource = new ObservableCollection<DicEquipmentTypeLookupDto>();

        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var equipmentTypeResult = await _dataDictionaryAppService.LookupEquipmentTypeAsync();
                foreach (var item in equipmentTypeResult)
                {
                    EquipmentTypeSource.Add(item);
                }


                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _equipmentAppService.GetAsync(id);
                    Model = _objectMapper.Map<EquipmentDto, EquipmentEditModel>(result);

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
                EquipmentCreateDto dto = _objectMapper.Map<EquipmentEditModel, EquipmentCreateDto>(this.Model);
                await _equipmentAppService.CreateAsync(dto);
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


        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                EquipmentUpdateDto dto = _objectMapper.Map<EquipmentEditModel, EquipmentUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _equipmentAppService.UpdateAsync((Guid)this.Model.Id, dto);
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
    }
}
