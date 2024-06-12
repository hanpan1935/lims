using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Suppliers.Dtos;
using Lanpuda.Lims.Suppliers;
using Lanpuda.Lims.UI.BasicInformations.Suppliers.Edits;
using Lanpuda.Lims.UI.BasicInformations.Products.Edits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.Lims.UI.BasicInformations.Suppliers.Edits
{
    public class SupplierEditViewModel : EditViewModelBase<SupplierEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly ISupplierAppService _supplierAppService;
        private readonly IObjectMapper _objectMapper;

        public SupplierEditViewModel(ISupplierAppService supplierAppService, IObjectMapper objectMapper)
        {
            _supplierAppService = supplierAppService;
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
                    var result = await _supplierAppService.GetAsync(id);
                    Model = _objectMapper.Map<SupplierDto, SupplierEditModel>(result);

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
                SupplierCreateDto dto = _objectMapper.Map<SupplierEditModel, SupplierCreateDto>(this.Model);
                await _supplierAppService.CreateAsync(dto);
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
                SupplierUpdateDto dto = _objectMapper.Map<SupplierEditModel, SupplierUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _supplierAppService.UpdateAsync((Guid)this.Model.Id, dto);
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
