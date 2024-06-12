using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InspectionTasks;
using Lanpuda.Lims.InspectionTasks.Dtos;
using Lanpuda.Lims.Records;
using Lanpuda.Lims.Records.Dtos;
using Lanpuda.Lims.UI.InspectionTasks.Dashboards;
using Lanpuda.Lims.UI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.InspectionTasks.Dialogs
{
    public class ResultValueDialogViewModel : RootViewModelBase
    {
        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

        public InspectionTaskDetailModel SelectedModel
        {
            get { return GetProperty(() => SelectedModel); }
            set { SetProperty(() => SelectedModel, value); }
        }


        public double? ResultValue
        {
            get { return GetProperty(() => ResultValue); }
            set { SetProperty(() => ResultValue, value); }
        }

        /// <summary>
        /// 关闭后执行列表页的刷新
        /// </summary>
        public Func<Task>? SubmitActionAsync { get; set; }

        private readonly IInspectionTaskAppService _inspectionTaskAppService;
        private readonly IRecordAppService _recordAppService;



        public ResultValueDialogViewModel(IInspectionTaskAppService inspectionTaskAppService, IRecordAppService recordAppService)
        {
            this._inspectionTaskAppService = inspectionTaskAppService;
            _recordAppService = recordAppService;
        }


        [AsyncCommand]
        public async Task Submit()
        {

            try
            {
                this.IsLoading = true;
                await _inspectionTaskAppService.UpdateResultValueAsync(SelectedModel.Id, ResultValue);
                if (SubmitActionAsync != null)
                {
                    await SubmitActionAsync();
                }
                this.CurrentWindowService.Close();
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
