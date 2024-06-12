using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Standards.Dtos;
using Lanpuda.Lims.Standards;
using Lanpuda.Lims.UI.InspectionMethods.Standards;
using Lanpuda.Lims.UI.InspectionMethods.Standards.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.DataDictionaries;
using System.Collections.ObjectModel;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.InspectionItems;
using static Lanpuda.Lims.Permissions.LimsPermissions;
using System.Windows;

namespace Lanpuda.Lims.UI.InspectionMethods.Standards
{
    public class StandardPagedViewModel : PagedViewModelBase<StandardDto>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IStandardAppService _standardAppService;
        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        public ObservableCollection<DicStandardTypeLookupDto> StandardTypeSource { get; set; }
        public StandardPagedViewModel(IServiceProvider serviceProvider, IStandardAppService standardAppService, IDataDictionaryAppService dataDictionaryAppService)
        {
            this.PageTitle = "检验标准";
            this._serviceProvider = serviceProvider;
            _standardAppService = standardAppService;
            _dataDictionaryAppService = dataDictionaryAppService;
            StandardTypeSource = new ObservableCollection<DicStandardTypeLookupDto>();

        }


        #region search
        public string? Description
        {
            get { return GetProperty(() => Description); }
            set { SetProperty(() => Description, value); }
        }
       
        public DicStandardTypeLookupDto? StandardType
        {
            get { return GetProperty(() => StandardType); }
            set { SetProperty(() => StandardType, value); }
        }

        #endregion

        [AsyncCommand]
        public async Task InitializeAsync()
        {

            try
            {
                var standardTypeList = await _dataDictionaryAppService.LookupStandardTypeAsync();
                StandardTypeSource.Clear();
                foreach (var item in standardTypeList)
                {
                    StandardTypeSource.Add(item);
                }

                await this.QueryAsync();
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



        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                StandardGetListInput input = new StandardGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.Description = this.Description;
                if (this.StandardType != null)
                {
                    input.DicStandardTypeId = StandardType.Id;
                }

                var result = await _standardAppService.GetPagedListAsync(input);
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
                StandardEditViewModel? viewModel = _serviceProvider.GetService<StandardEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "检验标准-新建";
                    WindowService.Show(nameof(StandardEditView), viewModel);
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
                StandardEditViewModel? viewModel = _serviceProvider.GetService<StandardEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "检验标准-编辑";
                    WindowService.Show(nameof(StandardEditView), viewModel);
                }
            }
        }

        [AsyncCommand]
        public async Task DeleteAsync()
        {
            try
            {
                if (this.SelectedModel == null)
                {
                    return;
                }

                var result = HandyControl.Controls.MessageBox.Show(messageBoxText: "确定要删除吗?", caption: "警告!", button: MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.IsLoading = true;
                    await _standardAppService.DeleteAsync(this.SelectedModel.Id);
                    await QueryAsync();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.Description = null;
            this.StandardType = null;
            await QueryAsync();
        }
    }
}
