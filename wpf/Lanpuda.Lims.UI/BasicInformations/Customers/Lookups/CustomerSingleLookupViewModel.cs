using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Customers;
using Lanpuda.Lims.Customers.Dtos;
using Lanpuda.Lims.UI.Assets.Langs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.BasicInformations.Customers.Lookups
{
    public class CustomerSingleLookupViewModel : PagedViewModelBase<CustomerDto>
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly ICustomerAppService _customerAppService;

        public Action<CustomerDto>? OnSelectedCallback;

        #region 搜索

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerNumber")]
        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerFullName")]
        public string? FullName
        {
            get { return GetProperty(() => FullName); }
            set { SetProperty(() => FullName, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerShortName")]
        public string? ShortName
        {
            get { return GetProperty(() => ShortName); }
            set { SetProperty(() => ShortName, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerManager")]
        public string? Manager
        {
            get { return GetProperty(() => Manager); }
            set { SetProperty(() => Manager, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerManagerTel")]
        public string? ManagerTel
        {
            get { return GetProperty(() => ManagerTel); }
            set { SetProperty(() => ManagerTel, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("CustomerConsignee")]
        public string? Consignee
        {
            get { return GetProperty(() => Consignee); }
            set { SetProperty(() => Consignee, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string? ConsigneeTel
        {
            get { return GetProperty(() => ConsigneeTel); }
            set { SetProperty(() => ConsigneeTel, value); }
        }
        #endregion

        public CustomerSingleLookupViewModel(IServiceProvider serviceProvider, ICustomerAppService customerAppService)
        {
            this.PageTitle = Lang.Customer;
            _serviceProvider = serviceProvider;
            _customerAppService = customerAppService;
        }

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
                CustomerGetListInput input = new CustomerGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.FullName = this.FullName;
                input.ShortName = this.ShortName;
                input.Manager = this.Manager;
                input.ManagerTel = this.ManagerTel;
                input.Consignee = this.Consignee;
                input.ConsigneeTel = this.ConsigneeTel;
                input.Number = this.Number;

                var result = await _customerAppService.GetPagedListAsync(input);
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
        public void Reset()
        {
            this.Number = null;
            this.FullName = null;
            this.ShortName = null;
            this.Manager = null;
            this.ManagerTel = null;
            this.Consignee = null;
            this.ConsigneeTel = null;
        }


        [Command]
        public void OnSelected()
        {
            if (this.SelectedModel != null && this.OnSelectedCallback != null)
            {
                OnSelectedCallback(SelectedModel);
                CurrentWindowService.Close();
            }
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Number = string.Empty;
            this.FullName = string.Empty;
            this.ShortName = string.Empty;
            this.Manager = string.Empty;
            this.ManagerTel = string.Empty;
            this.ConsigneeTel = string.Empty;
            this.Consignee = string.Empty;
            await QueryAsync();
        }
    }
}
