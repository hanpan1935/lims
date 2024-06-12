using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.InspectionTasks.Create
{
    public class InspectionTaskCreateModel : ModelBase
    {
        [Required(ErrorMessage = "必填")]
        public string RecordNumber
        {
            get { return GetProperty(() => RecordNumber); }
            set { SetProperty(() => RecordNumber, value); }
        }


        public InspectionTaskCreateDetailModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public ObservableCollection<InspectionTaskCreateDetailModel> Details { get; set; }  

        public InspectionTaskCreateModel()
        {
            Details = new ObservableCollection<InspectionTaskCreateDetailModel>();
        }
    }


    public class InspectionTaskCreateDetailModel : ModelBase
    {
        public Guid InspectionItemId { get; set; }

        public string? InspectionItemShortName
        {
            get { return GetProperty(() => InspectionItemShortName); }
            set { SetProperty(() => InspectionItemShortName, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("InspectionTaskRecordDetailId")]
        public Guid RecordDetailId { get; set; }



        [DisplayName("InspectionTaskInspectionDate")]
        public DateTime InspectionDate
        {
            get { return GetProperty(() => InspectionDate); }
            set { SetProperty(() => InspectionDate, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("InspectionTaskPriority")]
        public int Priority
        {
            get { return GetProperty(() => Priority); }
            set { SetProperty(() => Priority, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("InspectionTaskEquipmentId")]
        public Guid EquipmentId { get; set; }


        [Required(ErrorMessage = "必填")]
        public string EquipmentName
        {
            get { return GetProperty(() => EquipmentName); }
            set { SetProperty(() => EquipmentName, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("InspectionTaskInspector")]
        public string? Inspector
        {
            get { return GetProperty(() => Inspector); }
            set { SetProperty(() => Inspector, value); }
        }


        /// <summary>
        /// 
        /// </summary>
        [DisplayName("InspectionTaskRemark")]
        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }
    }
}
