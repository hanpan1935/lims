using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InspectionTasks;
using Lanpuda.Lims.Records.Dtos;
using Lanpuda.Lims.Records;
using Lanpuda.Lims.UI.InspectionTasks.Dialogs;
using Lanpuda.Lims.UI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using Lanpuda.Lims.UI.InspectionTasks.Create;
using Microsoft.Extensions.DependencyInjection;
using Lanpuda.Lims.UI.InspectionTasks.Details;
using Volo.Abp.ObjectMapping;
using Lanpuda.Lims.InspectionTasks.Dtos;
using Lanpuda.Lims.UI.InspectionTasks.Edits;
using Lanpuda.Lims.UI.Records.Edits;
using static Lanpuda.Lims.Permissions.LimsPermissions;

namespace Lanpuda.Lims.UI.InspectionTasks.Dashboards
{
    public class InspectionTaskViewModel : RootViewModelBase
    {
        protected IWindowService ResultValueEditWindowService { get { return this.GetService<IWindowService>("ResultValueEditWindow"); } }
        protected IWindowService EditWindowService { get { return this.GetService<IWindowService>("EditWindow"); } }
        private readonly IServiceProvider _serviceProvider;
        private readonly IObjectMapper _objectMapper;
        private readonly IInspectionTaskAppService _inspectionTaskAppService;
        private readonly IRecordAppService _recordAppService;
        public ObservableCollection<InspectionTaskModel> DataList { get; set; }
        public DateTime InspectionDate
        {
            get { return GetProperty(() => InspectionDate); }
            set { SetProperty(() => InspectionDate, value, async () => { await GetDatasAsync(); }); }
        }


        public InspectionTaskViewModel(
            IServiceProvider serviceProvider,
            IInspectionTaskAppService inspectionTaskAppService,
            IObjectMapper objectMapper,
            IRecordAppService recordAppService
            )
        {
            this.PageTitle = "检验任务-设备视图";
            _serviceProvider = serviceProvider;
            _objectMapper = objectMapper;
            _inspectionTaskAppService = inspectionTaskAppService;
            _recordAppService = recordAppService;
            DataList = new ObservableCollection<InspectionTaskModel>();
            InspectionDate = DateTime.Now.Date;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            //await GetDatasAsync();
            await Task.CompletedTask;
        }

        [Command]
        public void ShowInspectionTaskPagedView()
        {
            ModuleManager.DefaultManager.InjectOrNavigate(RegionNames.MainContentRegion, LimsUIViewKeys.Lims_InspectionTask, null);
        }


        [AsyncCommand]
        public async Task GetDatasAsync()
        {
            try
            {
                this.IsLoading = true;
                this.DataList.Clear();
                var result = await _inspectionTaskAppService.GetListByDateAsync(this.InspectionDate);
                foreach (var item in result)
                {
                    InspectionTaskModel model = new InspectionTaskModel();
                    model.EquipmentName = item.EquipmentName;
                    model.EquipmentId = item.EquipmentId;
                    foreach (var detail in item.Details)
                    {
                        var detailModel = _objectMapper.Map<InspectionTaskDto, InspectionTaskDetailModel>(detail);
                        detailModel.ShowEditResultValueViewAction = ShowEditResultValueView;
                        detailModel.ShowEditViewAction = ShowEditView;
                        detailModel.ShowDetailViewAction = ShowDetailView;
                        model.Details.Add(detailModel);
                    }
                    DataList.Add(model);
                }
                ;
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



        private void ShowDetailView(InspectionTaskDetailModel detail)
        {
            InspectionTaskDetailViewModel? viewModel = _serviceProvider.GetService<InspectionTaskDetailViewModel>();
            if (viewModel != null)
            {
                viewModel.SelectedModel = detail;
                WindowService.Title = "检验任务-明细";
                WindowService.Show(nameof(InspectionTaskDetailView), viewModel);
            }
        }


        private void ShowEditView(InspectionTaskDetailModel detailModel)
        {
            InspectionTaskEditViewModel? viewModel = _serviceProvider.GetService<InspectionTaskEditViewModel>();
            if (viewModel != null)
            {
                viewModel.Model.Id = detailModel.Id;
                viewModel.Model.RecordNumber = detailModel.RecordNumber;
                viewModel.Model.InspectionItemId = detailModel.InspectionItemId;
                viewModel.Model.RecordDetailId = detailModel.RecordDetailId;
                viewModel.Model.InspectionItemFullName = detailModel.InspectionItemFullName;
                viewModel.Model.InspectionItemShortName = detailModel.InspectionItemShortName;
                EditWindowService.Title = "检验任务-编辑";
                EditWindowService.Show(nameof(InspectionTaskEditView), viewModel);
            }
        }
      

        private void ShowEditResultValueView(InspectionTaskDetailModel detail)
        {
            ResultValueDialogViewModel? viewModel = _serviceProvider.GetService<ResultValueDialogViewModel>();
            if (viewModel != null)
            {
                viewModel.SelectedModel = detail;
                viewModel.ResultValue = detail.ResultValue;
                viewModel.SubmitActionAsync = GetDatasAsync;
                ResultValueEditWindowService.Title = "检测值-编辑";
                ResultValueEditWindowService.Show(nameof(ResultValueDialogView), viewModel);
            }
        }
    }
}
