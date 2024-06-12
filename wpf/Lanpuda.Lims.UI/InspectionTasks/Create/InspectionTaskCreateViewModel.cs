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
using System.Runtime.InteropServices;

namespace Lanpuda.Lims.UI.InspectionTasks.Create
{
    public class InspectionTaskCreateViewModel : EditViewModelBase<InspectionTaskCreateModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IInspectionTaskAppService _inspectionTaskAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;

        public InspectionTaskCreateViewModel(IInspectionTaskAppService inspectionTaskAppService, IObjectMapper objectMapper, IServiceProvider serviceProvider)
        {
            _inspectionTaskAppService = inspectionTaskAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                await Task.CompletedTask;
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
            await CreateAsync();
        }


        public bool CanSaveAsync()
        {

            if (Model.HasErrors())
            {
                return false;
            }

            foreach (var item in Model.Details)
            {
                if (item.HasErrors())
                {
                    return false;
                }
            }

            return true;
        }


        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                List<InspectionTaskCreateDto> inputs = new List<InspectionTaskCreateDto>();
                foreach (var item in Model.Details)
                {
                    InspectionTaskCreateDto input = new InspectionTaskCreateDto();
                    input.RecordDetailId = item.RecordDetailId;
                    input.InspectionDate = item.InspectionDate;
                    input.Priority = item.Priority;
                    input.EquipmentId = item.EquipmentId;
                    input.Inspector = item.Inspector;
                    input.Remark = item.Remark;
                    inputs.Add(input);
                }
                await _inspectionTaskAppService.MultipleCreateAsync(inputs);
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

        [Command]
        public void ShowSelectRecordView()
        {
            RecordSingleLookupViewModel? viewModel = _serviceProvider.GetService<RecordSingleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnRecordSelected;
                WindowService.Title = "选择检验记录";
                WindowService.Show(nameof(RecordSingleLookupView), viewModel);
            }
        }


        private void OnRecordSelected(RecordDto record)
        {
            this.Model.Details.Clear();
            this.Model.RecordNumber = record.Number;
            foreach (var item in record.Details)
            {
                InspectionTaskCreateDetailModel detailModel = new InspectionTaskCreateDetailModel();
                detailModel.InspectionItemId = item.InspectionItemId;
                detailModel.InspectionItemShortName = item.InspectionItemShortName;
                detailModel.RecordDetailId = item.Id;
                detailModel.InspectionDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
                detailModel.Priority = 1;
                if (item.DefaultEquipmentId != null && !string.IsNullOrEmpty(item.DefaultEquipmentName))
                {
                    detailModel.EquipmentId = (Guid)item.DefaultEquipmentId;
                    detailModel.EquipmentName = item.DefaultEquipmentName;
                }
                this.Model.Details.Add(detailModel);
            }

        }

        [Command]
        public void ShowSelectEquipmentView()
        {
            EquipmentSingleLookupViewModel? viewModel = _serviceProvider.GetService<EquipmentSingleLookupViewModel>();
            if (viewModel != null)
            {
                viewModel.OnSelectedCallback = OnEquipmentSelected;
                WindowService.Title = "选择检验记录";
                WindowService.Show(nameof(EquipmentSingleLookupView), viewModel);
            }
        }

        private void OnEquipmentSelected(EquipmentDto equipment)
        {
            if (this.Model.SelectedRow != null)
            {
                Model.SelectedRow.EquipmentId = equipment.Id;
                Model.SelectedRow.EquipmentName = equipment.Name;
            }
        }
    }
}
