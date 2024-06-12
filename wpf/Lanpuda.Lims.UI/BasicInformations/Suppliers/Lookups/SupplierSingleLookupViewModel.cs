using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Suppliers;
using Lanpuda.Lims.Suppliers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.BasicInformations.Suppliers.Lookups
{
    public class SupplierSingleLookupViewModel : PagedViewModelBase<SupplierDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly ISupplierAppService _supplierAppService;
        public Action<SupplierDto>? OnSelectedCallback;
        public SupplierSingleLookupViewModel(IServiceProvider serviceProvider, ISupplierAppService supplierAppService)
        {
            this.PageTitle = "供应商";
            _serviceProvider = serviceProvider;
            _supplierAppService = supplierAppService;
        }

        #region 搜索
        public string? FullName
        {
            get { return GetProperty(() => FullName); }
            set { SetProperty(() => FullName, value); }
        }

        public string? ShortName
        {
            get { return GetProperty(() => ShortName); }
            set { SetProperty(() => ShortName, value); }
        }

        public string? Manager
        {
            get { return GetProperty(() => Manager); }
            set { SetProperty(() => Manager, value); }
        }

        public string? ManagerTel
        {
            get { return GetProperty(() => ManagerTel); }
            set { SetProperty(() => ManagerTel, value); }
        }

        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }
        #endregion


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                SupplierGetListInput input = new SupplierGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.FullName = this.FullName;
                input.ShortName = this.ShortName;
                input.Manager = this.Manager;
                input.ManagerTel = this.ManagerTel;
                input.Number = this.Number;
                input.Remark = this.Remark;

                var result = await _supplierAppService.GetPagedListAsync(input);
                this.TotalCount = result.TotalCount;
                this.PagedDatas.CanNotify = false;
                this.PagedDatas.Clear();
                foreach (var item in result.Items)
                {
                    this.PagedDatas.Add(item);
                }
                this.PagedDatas.CanNotify = true;
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
        public void OnSelected()
        {
            if (this.OnSelectedCallback != null && this.SelectedModel != null)
            {
                OnSelectedCallback(this.SelectedModel);
                CurrentWindowService.Close();
            }
        }

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.FullName = null;
            this.ShortName = null;
            this.Number = null;
            this.Manager = null;
            this.ManagerTel = null;
            await QueryAsync();
        }


    }
}
