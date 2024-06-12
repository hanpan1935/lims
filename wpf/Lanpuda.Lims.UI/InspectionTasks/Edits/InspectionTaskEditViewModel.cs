using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InspectionTasks.Dtos;
using Lanpuda.Lims.InspectionTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Lanpuda.Lims.Suppliers.Dtos;
using Lanpuda.Lims.UI.BasicInformations.Suppliers.Lookups;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.Lims.UI.Records.Lookups;
using DevExpress.Mvvm;
using Lanpuda.Lims.Records.Dtos;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Lookups;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Lims.InspectionItems;
using System.Collections.ObjectModel;
using Lanpuda.Lims.InspectionItems.Dtos;

namespace Lanpuda.Lims.UI.InspectionTasks.Edits
{
    public class InspectionTaskEditViewModel : EditViewModelBase<InspectionTaskEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IInspectionTaskAppService _inspectionTaskAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly IInspectionItemAppService _inspectionItemAppService;
        

        public InspectionTaskEditViewModel(
            IInspectionTaskAppService inspectionTaskAppService, 
            IObjectMapper objectMapper, 
            IServiceProvider serviceProvider,
            IInspectionItemAppService inspectionItemAppService)
        {
            _inspectionTaskAppService = inspectionTaskAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
            _inspectionItemAppService = inspectionItemAppService;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var item =  await _inspectionItemAppService.GetAsync(Model.InspectionItemId);

                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _inspectionTaskAppService.GetAsync(id);
                    Model = _objectMapper.Map<InspectionTaskDto, InspectionTaskEditModel>(result);
                }
                else
                {
                    this.Model.InspectionDate = DateTime.Now;
                    if (item.DefaultEquipmentId !=  null && !string.IsNullOrEmpty(item.DefaultEquipmentName))
                    {
                        this.Model.EquipmentId = (Guid)item.DefaultEquipmentId;
                        this.Model.EquipmentName = item.DefaultEquipmentName;
                    }
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
                InspectionTaskCreateDto dto = _objectMapper.Map<InspectionTaskEditModel, InspectionTaskCreateDto>(this.Model);
                await _inspectionTaskAppService.CreateAsync(dto);
                if (RefreshPagedViewFunc != null)
                {
                    await RefreshPagedViewFunc();
                }
                this.Close();
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
                InspectionTaskUpdateDto dto = _objectMapper.Map<InspectionTaskEditModel, InspectionTaskUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _inspectionTaskAppService.UpdateAsync((Guid)this.Model.Id, dto);
                if (RefreshPagedViewFunc != null)
                {
                    await RefreshPagedViewFunc();
                }
                this.Close();
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
        public void ShowSelectRecordView()
        {
            RecordSingleLookupViewModel? viewModel = _serviceProvider.GetService<RecordSingleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnRecordSelected;
                WindowService.Title = "样品管理-编辑";
                WindowService.Show(nameof(RecordSingleLookupView), viewModel);
            }
        }


        private void OnRecordSelected(RecordDto record)
        {
           
        }


        [Command]
        public void ShowSelectEquipmentView()
        {
            EquipmentSingleLookupViewModel? viewModel = _serviceProvider.GetService<EquipmentSingleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnEquipmentSelected;
                WindowService.Title = "样品管理-编辑";
                WindowService.Show(nameof(EquipmentSingleLookupView), viewModel);
            }
        }

        public void OnEquipmentSelected(EquipmentDto equipment)
        {
            this.Model.EquipmentId = equipment.Id;
            this.Model.EquipmentName = equipment.Name;
        }
    }
}
