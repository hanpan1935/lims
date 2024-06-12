using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.Customers;
using Lanpuda.Lims.DataDictionaries;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.UI.BasicInformations.Customers.Edits;
using Lanpuda.Lims.UI.BasicInformations.DataDictionaries.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.Lims.UI.BasicInformations.DataDictionaries
{
    public class DataDictionaryPagedViewModel : RootViewModelBase
    {
        private readonly IDataDictionaryAppService _dataDictionaryAppService;
        public ObservableCollection<DataDictionaryDto> PagedDatas { get; set; }
        public Dictionary<string, DataDictionaryType> DataDictionaryTypeSource { get; set; }
        public DataDictionaryType SelectedDataDictionaryType { get; set; }
        public DataDictionaryDto? SelectedModel
        {
            get { return GetProperty(() => SelectedModel); }
            set { SetProperty(() => SelectedModel, value); }
        }
        private readonly IServiceProvider _serviceProvider;

        public DataDictionaryPagedViewModel(IDataDictionaryAppService dataDictionaryAppService, IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
            _dataDictionaryAppService = dataDictionaryAppService;
            PagedDatas = new ObservableCollection<DataDictionaryDto>();

            DataDictionaryTypeSource = EnumUtils.EnumToDictionary<DataDictionaryType>();
            SelectedDataDictionaryType = DataDictionaryTypeSource.First().Value;

            ;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await this.QueryAsync();
        }


        [AsyncCommand]
        public async Task QueryAsync()
        {
            await GetPagedDatasAsync();

            if (this.SelectedModel == null)
            {
                this.SelectedModel = PagedDatas.FirstOrDefault();
            }
           
        }

        private  async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                DataDictionaryGetListInput input = new DataDictionaryGetListInput();
                input.Type = SelectedDataDictionaryType;
                var result = await _dataDictionaryAppService.GetListAsync(input);
                this.PagedDatas.Clear();
                foreach (var item in result)
                {
                    this.PagedDatas.Add(item);  
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


        [Command]
        public void Create()
        {
            if (this.WindowService != null)
            {
                DataDictionaryEditViewModel? viewModel = _serviceProvider.GetService<DataDictionaryEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                   
                    WindowService.Title = "数据字典-新建";
                    WindowService.Show(nameof(DataDictionaryEditView), viewModel);
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
                DataDictionaryEditViewModel? viewModel = _serviceProvider.GetService<DataDictionaryEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    viewModel.Model.Id = this.SelectedModel.Id;
                    viewModel.Model.DisplayValue = this.SelectedModel.DisplayValue;
                    viewModel.Model.Type = this.SelectedModel.Type; 
                    viewModel.Model.Sort = this.SelectedModel.Sort;
                    WindowService.Title = "数据字典-编辑";
                    WindowService.Show(nameof(DataDictionaryEditView), viewModel);
                }
            }
        }


        [AsyncCommand]
        public async Task ResetAsync()
        {
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

                    DataDictionaryDeleteDto input = new DataDictionaryDeleteDto();
                    input.Type = this.SelectedDataDictionaryType;
                    input.Id = SelectedModel.Id;

                    await _dataDictionaryAppService.DeleteAsync(input);
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
