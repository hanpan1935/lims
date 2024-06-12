using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using Lanpuda.Client.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Lanpuda.Lims.UI.BasicInformations.Customers.Edits;
using Lanpuda.Lims.Customers;
using Lanpuda.Lims.Customers.Dtos;
using System.ComponentModel;
using Lanpuda.Lims.UI.Assets.Langs;
using System.Windows;

namespace Lanpuda.Lims.UI.BasicInformations.Locations
{
    public class CustomerPagedViewModel : PagedViewModelBase<CustomerDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICustomerAppService _customerAppService;

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
        [DisplayName("CustomerRemark")]
        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
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

        public CustomerPagedViewModel(IServiceProvider serviceProvider, ICustomerAppService customerAppService)
        {
            this.PageTitle = "客户信息";
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
                input.Remark = this.Remark;
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
        public void Create()
        {
            if (this.WindowService != null)
            {
                CustomerEditViewModel? viewModel = _serviceProvider.GetService<CustomerEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = Lang.Customer + Lang.Create;
                    WindowService.Show(nameof(CustomerEditView), viewModel);
                }
            }
        }


        [Command]
        public void Update()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            if (this.WindowService != null)
            {
                CustomerEditViewModel? vieModel = _serviceProvider.GetService<CustomerEditViewModel>();
                if (vieModel != null)
                {
                    vieModel.Model.Id = this.SelectedModel.Id;
                    vieModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = Lang.Customer + Lang.Edit;
                    WindowService.Show(nameof(CustomerEditView), vieModel);
                }
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

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            if (this.SelectedModel == null)
            {
                return;
            }
            try
            {
                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _customerAppService.DeleteAsync(this.SelectedModel.Id);
                    await this.QueryAsync();
                }

              
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                this.IsLoading = false;
            }
        }
    }
}
