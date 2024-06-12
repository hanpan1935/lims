using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Locations.Dtos;
using Lanpuda.Lims.Locations;
using Lanpuda.Lims.UI.BasicInformations.Locations.Edits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Lanpuda.Lims.Warehouses;
using System.Collections.ObjectModel;
using Lanpuda.Lims.Warehouses.Dtos;

namespace Lanpuda.Lims.UI.BasicInformations.Locations.Edits
{
    public class LocationEditViewModel : EditViewModelBase<LocationEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly ILocationAppService _locationAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IWarehouseAppService _warehouseAppService;

        public ObservableCollection<WarehouseLookupDto> WarehouseSource { get; set; } 

        public LocationEditViewModel(ILocationAppService locationAppService, IObjectMapper objectMapper, IWarehouseAppService warehouseAppService)
        {
            _locationAppService = locationAppService;
            _objectMapper = objectMapper;
            _warehouseAppService = warehouseAppService;
            WarehouseSource = new ObservableCollection<WarehouseLookupDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var warehouseList = await _warehouseAppService.LookupAsync();
                WarehouseSource.Clear();
                foreach (var item in warehouseList)
                {
                    this.WarehouseSource.Add(item);
                }

                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _locationAppService.GetAsync(id);
                    Model = _objectMapper.Map<LocationDto, LocationEditModel>(result);

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
                LocationCreateDto dto = _objectMapper.Map<LocationEditModel, LocationCreateDto>(this.Model);
                await _locationAppService.CreateAsync(dto);
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
                LocationUpdateDto dto = _objectMapper.Map<LocationEditModel, LocationUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _locationAppService.UpdateAsync((Guid)this.Model.Id, dto);
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

