using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InspectionItems.Dtos;
using Lanpuda.Lims.InspectionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Mvvm;
using Lanpuda.Lims.Products.Dtos;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Lookups;
using Lanpuda.Lims.Equipments.Dtos;
using static Lanpuda.Lims.Permissions.LimsPermissions;

namespace Lanpuda.Lims.UI.InspectionMethods.InspectionItems.Edits
{
    public class InspectionItemEditViewModel : EditViewModelBase<InspectionItemEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IInspectionItemAppService _inspectionItemAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;

        public InspectionItemEditViewModel(
            IInspectionItemAppService inspectionItemAppService, 
            IObjectMapper objectMapper, 
            IServiceProvider serviceProvider)
        {
            _inspectionItemAppService = inspectionItemAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
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
                    var result = await _inspectionItemAppService.GetAsync(id);
                    Model = _objectMapper.Map<InspectionItemDto, InspectionItemEditModel>(result);

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
                InspectionItemCreateDto dto = _objectMapper.Map<InspectionItemEditModel, InspectionItemCreateDto>(this.Model);
                await _inspectionItemAppService.CreateAsync(dto);
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
                InspectionItemUpdateDto dto = _objectMapper.Map<InspectionItemEditModel, InspectionItemUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _inspectionItemAppService.UpdateAsync((Guid)this.Model.Id, dto);
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
        public void ShowEquipmentSingleSelectView()  
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
            this.Model.DefaultEquipmentId = equipment.Id;
            this.Model.DefaultEquipmentName = equipment.Name;
        }



    }
}
