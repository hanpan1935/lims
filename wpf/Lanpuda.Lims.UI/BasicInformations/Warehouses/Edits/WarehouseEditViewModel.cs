using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Warehouses.Dtos;
using Lanpuda.Lims.Warehouses;
using Lanpuda.Lims.UI.BasicInformations.Warehouses.Edits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.Lims.UI.BasicInformations.Warehouses.Edits
{
    public class WarehouseEditViewModel : EditViewModelBase<WarehouseEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly IWarehouseAppService _warehouseAppService;
        private readonly IObjectMapper _objectMapper;

        public WarehouseEditViewModel(IWarehouseAppService warehouseAppService, IObjectMapper objectMapper)
        {
            _warehouseAppService = warehouseAppService;
            _objectMapper = objectMapper;
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
                    var result = await _warehouseAppService.GetAsync(id);
                    Model = _objectMapper.Map<WarehouseDto, WarehouseEditModel>(result);

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
                WarehouseCreateDto dto = _objectMapper.Map<WarehouseEditModel, WarehouseCreateDto>(this.Model);
                await _warehouseAppService.CreateAsync(dto);
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
                WarehouseUpdateDto dto = _objectMapper.Map<WarehouseEditModel, WarehouseUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _warehouseAppService.UpdateAsync((Guid)this.Model.Id, dto);
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
