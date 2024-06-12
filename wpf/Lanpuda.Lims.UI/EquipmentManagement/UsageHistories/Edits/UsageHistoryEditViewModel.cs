using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Equipments.Dtos;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Lookups;
using Lanpuda.Lims.UsageHistories;
using Lanpuda.Lims.UsageHistories.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.Lims.UI.EquipmentManagement.UsageHistories.Edits
{
    public class UsageHistoryEditViewModel : EditViewModelBase<UsageHistoryEditModel>   
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IUsageHistoryAppService _usageHistoryAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;

        public UsageHistoryEditViewModel(IUsageHistoryAppService usageHistoryAppService, IObjectMapper objectMapper, IServiceProvider serviceProvider)
        {
            _usageHistoryAppService = usageHistoryAppService;
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
                    var result = await _usageHistoryAppService.GetAsync(id);
                    Model = _objectMapper.Map<UsageHistoryDto, UsageHistoryEditModel>(result);

                }
                else
                {
                    this.Model.StartTime = DateTime.Now;
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
                UsageHistoryCreateDto dto = _objectMapper.Map<UsageHistoryEditModel, UsageHistoryCreateDto>(this.Model);
                await _usageHistoryAppService.CreateAsync(dto);
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
                UsageHistoryUpdateDto dto = _objectMapper.Map<UsageHistoryEditModel, UsageHistoryUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _usageHistoryAppService.UpdateAsync((Guid)this.Model.Id, dto);
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
