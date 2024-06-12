using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InspectionTasks.Dtos;
using Lanpuda.Lims.UI.InspectionTasks.Dashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.Lims.Permissions.LimsPermissions;

namespace Lanpuda.Lims.UI.InspectionTasks.Details
{
    public class InspectionTaskDetailViewModel : RootViewModelBase
    {
        public InspectionTaskDetailModel SelectedModel
        {
            get { return GetProperty(() => SelectedModel); }
            set { SetProperty(() => SelectedModel, value); }
        }

        public InspectionTaskDetailViewModel()
        {

        }
    }
}
