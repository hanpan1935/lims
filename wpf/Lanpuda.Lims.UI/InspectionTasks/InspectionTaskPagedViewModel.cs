using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InspectionTasks.Dtos;
using Lanpuda.Lims.InspectionTasks;
using Lanpuda.Lims.UI.InspectionTasks.Edits;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.Lims.UI.InspectionTasks.Create;
using Lanpuda.Lims.Records.Dtos;
using System.Windows.Forms;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using Lanpuda.Lims.UI.InspectionTasks.Dialogs;
using Lanpuda.Lims.Records;
using Lanpuda.Lims.UI.Utils;
using Lanpuda.Lims.Standards;
using Lanpuda.Lims.InspectionItems.Dtos;
using System.Collections.ObjectModel;
using Lanpuda.Lims.InspectionItems;
using Lanpuda.Lims.UI.BasicInformations.Products.Lookups;
using Lanpuda.Lims.UI.EquipmentManagement.Equipments.Lookups;
using Lanpuda.Lims.UI.InspectionTasks.Dashboards;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Volo.Abp.ObjectMapping;
using System.Windows;

namespace Lanpuda.Lims.UI.InspectionTasks
{
    public class InspectionTaskPagedViewModel : PagedViewModelBase<InspectionTaskDto>
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly IObjectMapper _objectMapper;
        private readonly IInspectionTaskAppService _inspectionTaskAppService;
        private readonly IRecordAppService _recordAppService;
        private readonly IInspectionItemAppService _inspectionItemAppService;
        protected IWindowService ResultValueEditWindowService { get { return this.GetService<IWindowService>("ResultValueEditWindow"); } }
        protected IWindowService EditWindowService { get { return this.GetService<IWindowService>("EditWindow"); } }
        
        public ObservableCollection<InspectionItemDto> InspectionItemsSource { get; set; }

        #region search

        public string? RecordNumber
        {
            get { return GetProperty(() => RecordNumber); }
            set { SetProperty(() => RecordNumber, value); }
        }

        public string? SampleNumber
        {
            get { return GetProperty(() => SampleNumber); }
            set { SetProperty(() => SampleNumber, value); }
        }

        public Guid? ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
        }
        
        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }



        public InspectionItemDto? SelectedInspectionItem
        {
            get { return GetProperty(() => SelectedInspectionItem); }
            set { SetProperty(() => SelectedInspectionItem, value); }
        }


        public DateTime? InspectionDateStart
        {
            get { return GetProperty(() => InspectionDateStart); }
            set { SetProperty(() => InspectionDateStart, value); }
        }
        public DateTime? InspectionDateEnd
        {
            get { return GetProperty(() => InspectionDateEnd); }
            set { SetProperty(() => InspectionDateEnd, value); }
        }

        public Guid? EquipmentId
        {
            get { return GetProperty(() => EquipmentId); }
            set { SetProperty(() => EquipmentId, value); }
        }

        public string? EquipmentName
        {
            get { return GetProperty(() => EquipmentName); }
            set { SetProperty(() => EquipmentName, value); }
        }

        public string? Inspector
        {
            get { return GetProperty(() => Inspector); }
            set { SetProperty(() => Inspector, value); }
        }

        #endregion


        public InspectionTaskPagedViewModel(
            IServiceProvider serviceProvider,
            IObjectMapper _objectMapper,
            IInspectionTaskAppService inspectionTaskAppService, 
            IRecordAppService recordAppService,
            IInspectionItemAppService inspectionItemAppService)
        {
            _serviceProvider = serviceProvider;
            this._objectMapper = _objectMapper;
            this.PageTitle = "检验任务-任务视图";
            _inspectionTaskAppService = inspectionTaskAppService;
            _recordAppService = recordAppService;
            _inspectionItemAppService = inspectionItemAppService;
            InspectionItemsSource = new ObservableCollection<InspectionItemDto>();
        }




        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var itemList = await _inspectionItemAppService.GetAllAsync();
                InspectionItemsSource.Clear();
                foreach (var item in itemList)
                {
                    InspectionItemsSource.Add(item);
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
            await this.QueryAsync();
        }

        protected override async Task GetPagedDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                InspectionTaskGetListInput input = new InspectionTaskGetListInput();
                input.MaxResultCount = this.DataCountPerPage;
                input.SkipCount = this.SkipCount;
                input.RecordNumber = this.RecordNumber;
                input.SampleNumber = this.SampleNumber;
                input.ProductId = this.ProductId;
                if (this.SelectedInspectionItem != null)
                {
                    input.InspectionItemId = this.SelectedInspectionItem.Id;
                }
                input.InspectionDateStart = this.InspectionDateStart;
                input.InspectionDateEnd = this.InspectionDateEnd;
                input.EquipmentId = this.EquipmentId;
                input.Inspector = this.Inspector;

                var result = await _inspectionTaskAppService.GetPagedListAsync(input);
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

        [AsyncCommand]
        public async Task ResetAsync()
        {
            this.RecordNumber = null;
            this.SampleNumber = null;
            this.ProductId = null;
            this.ProductName = null;
            this.SelectedInspectionItem = null;
            this.InspectionDateStart = null;
            this.InspectionDateEnd = null;
            this.EquipmentId = null;
            this.EquipmentName = null;
            this.Inspector = null;
            await QueryAsync();
        }


        [Command]
        public void Create()
        {
            if (this.WindowService != null)
            {
                InspectionTaskCreateViewModel? viewModel = _serviceProvider.GetService<InspectionTaskCreateViewModel>();
                if (viewModel != null)
                {
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    WindowService.Title = "检验任务-根据检验数据新建";
                    WindowService.Show(nameof(InspectionTaskCreateView), viewModel);
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
            if (this.EditWindowService != null)
            {
                InspectionTaskEditViewModel? viewModel = _serviceProvider.GetService<InspectionTaskEditViewModel>();
                if (viewModel != null)
                {
                    viewModel.Model.Id = this.SelectedModel.Id;

                    viewModel.Model.RecordNumber = SelectedModel.RecordNumber;
                    viewModel.Model.InspectionItemId = SelectedModel.InspectionItemId;
                    viewModel.Model.RecordDetailId = SelectedModel.RecordDetailId;
                    viewModel.Model.InspectionItemFullName = SelectedModel.InspectionItemFullName;
                    viewModel.Model.InspectionItemShortName = SelectedModel.InspectionItemShortName;
                    viewModel.RefreshPagedViewFunc = this.QueryAsync;
                    EditWindowService.Title = "检验任务-编辑";
                    EditWindowService.Show(nameof(InspectionTaskEditView), viewModel);
                }
            }
        }

        [Command]
        public void ShowInspectionTaskView()
        {
            ModuleManager.DefaultManager.InjectOrNavigate(RegionNames.MainContentRegion, LimsUIViewKeys.Lims_InspectionTaskDashboard, null);
        }


        [Command]
        public void ShowResultValueEditView()
        {
            if (this.ResultValueEditWindowService != null && this.SelectedModel != null)
            {
                ResultValueDialogViewModel? viewModel = _serviceProvider.GetService<ResultValueDialogViewModel>();
                if (viewModel != null)
                {
                    viewModel.ResultValue = this.SelectedModel.ResultValue;
                    var detailModel = _objectMapper.Map<InspectionTaskDto, InspectionTaskDetailModel>(this.SelectedModel);
                    viewModel.SelectedModel = detailModel;
                    viewModel.SubmitActionAsync = this.QueryAsync;
                    ResultValueEditWindowService.Title = "检测结果";
                    ResultValueEditWindowService.Show(nameof(ResultValueDialogView), viewModel);
                }
            }
        }






        [Command]
        public void ShowProductSelectView()
        {
            if (this.WindowService != null)
            {
                ProductSingleLookupViewModel? viewModel = _serviceProvider.GetService<ProductSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (product) =>
                    {
                        this.ProductId = product.Id;
                        this.ProductName = product.Name;
                    };
                    WindowService.Title = "选择产品";
                    WindowService.Show(nameof(ProductSingleLookupView), viewModel);
                }
            }
        }

        [Command]
        public void ShowEquipmentSelectView()
        {
            if (this.WindowService != null)
            {
                EquipmentSingleLookupViewModel? viewModel = _serviceProvider.GetService<EquipmentSingleLookupViewModel>();
                if (viewModel != null)
                {
                    viewModel.OnSelectedCallback = (equipment) =>
                    {
                        this.EquipmentId = equipment.Id;
                        this.EquipmentName = equipment.Name;
                    };
                    WindowService.Title = "选择设备";
                    WindowService.Show(nameof(EquipmentSingleLookupView), viewModel);
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
                    await _inspectionTaskAppService.DeleteAsync(this.SelectedModel.Id);
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

       

    }
}
