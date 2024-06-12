using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Customers.Dtos;
using Lanpuda.Lims.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.Lims.UI.BasicInformations.Customers.Edits
{
    public class CustomerEditViewModel : EditViewModelBase<CustomerEditModel>
    {
        public Func<Task>? RefreshPagedViewFunc { get; set; }
        private readonly ICustomerAppService _customerAppService;
        private readonly IObjectMapper _objectMapper;

        public CustomerEditViewModel(ICustomerAppService customerAppService, IObjectMapper objectMapper)
        {
            _customerAppService = customerAppService;
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
                    var result = await _customerAppService.GetAsync(id);
                    Model = _objectMapper.Map<CustomerDto, CustomerEditModel>(result);

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
                CustomerCreateDto dto = _objectMapper.Map<CustomerEditModel, CustomerCreateDto>(this.Model);
                await _customerAppService.CreateAsync(dto);
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
                CustomerUpdateDto dto = _objectMapper.Map<CustomerEditModel, CustomerUpdateDto>(this.Model);
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _customerAppService.UpdateAsync((Guid)this.Model.Id, dto);
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
