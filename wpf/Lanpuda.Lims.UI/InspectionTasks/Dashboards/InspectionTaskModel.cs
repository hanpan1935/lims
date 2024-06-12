using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InspectionTasks;
using Lanpuda.Lims.InspectionTasks.Dtos;
using Lanpuda.Lims.UI.InspectionTasks.Create;
using Lanpuda.Lims.UI.InspectionTasks.Details;
using Lanpuda.Lims.UI.InspectionTasks.Dialogs;
using Lanpuda.Lims.UI.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.InspectionTasks.Dashboards
{
    public class InspectionTaskModel : ModelBase
    {


        public Guid EquipmentId { get; set; }
        public string EquipmentName
        {
            get { return GetProperty(() => EquipmentName); }
            set { SetProperty(() => EquipmentName, value); }
        }


        public InspectionTaskDto? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public ObservableCollection<InspectionTaskDetailModel> Details { get; set; }


        public InspectionTaskModel()
        {
            Details = new ObservableCollection<InspectionTaskDetailModel>();
            EquipmentName = "";
        }


    }


    public class InspectionTaskDetailModel : ModelBase
    {

        public Action<InspectionTaskDetailModel>? ShowDetailViewAction { get; set; }
        public Action<InspectionTaskDetailModel>? ShowEditResultValueViewAction { get; set; }
        public Action<InspectionTaskDetailModel>? ShowEditViewAction { get; set; }


        public InspectionTaskDetailModel()
        {
            this.RecordNumber = "";
        }

        public Guid Id { get; set; }
        public Guid RecordId { get; set; }
        public string RecordNumber { get; set; }
        public string? SampleId { get; set; }
        public string? SampleNumber { get; set; }
        public Guid InspectionItemId { get; set; }
        public string? InspectionItemFullName { get; set; }
        public string? InspectionItemShortName { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public double? ResultValue { get; set; }
        public bool? IsQualified { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid RecordDetailId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime InspectionDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid EquipmentId { get; set; }

        public string? EquipmentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Inspector { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Remark { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public double? MinValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasMinValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double? MaxValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasMaxValue { get; set; }



        public string? Standard { get; set; }


        [Command]
        public void ShowDetailView()
        {
            if (this.ShowDetailViewAction != null)
            {
                ShowDetailViewAction(this);
            }
        }


        [Command]
        public void ShowEditResultValueView()
        {
            if (this.ShowEditResultValueViewAction != null)
            {
                ShowEditResultValueViewAction(this);
            }
        }


        [Command]
        public void ShowEditView()
        {
            if (ShowEditViewAction != null)
            {
                ShowEditViewAction(this);
            }
        }
    }
}
