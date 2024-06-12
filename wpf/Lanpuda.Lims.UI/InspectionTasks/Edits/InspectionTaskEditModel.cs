using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.Lims.Permissions.LimsPermissions;

namespace Lanpuda.Lims.UI.InspectionTasks.Edits
{
    public class InspectionTaskEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public Guid RecordDetailId { get; set; }
       

        public string? RecordNumber { get; set; }

        public Guid InspectionItemId { get; set; }
        public string? InspectionItemShortName { get; set; }
        public string? InspectionItemFullName { get; set; }


        //检验日期

        [Required(ErrorMessage ="必填")]
        public DateTime InspectionDate
        {
            get { return GetProperty(() => InspectionDate); }
            set { SetProperty(() => InspectionDate, value); }
        }

        //优先级
        public int Priority
        {
            get { return GetProperty(() => Priority); }
            set { SetProperty(() => Priority, value); }
        }

        public Guid EquipmentId
        {
            get { return GetProperty(() => EquipmentId); }
            set { SetProperty(() => EquipmentId, value); }
        }


        [Required(ErrorMessage = "必填")]
        public string EquipmentName
        {
            get { return GetProperty(() => EquipmentName); }
            set { SetProperty(() => EquipmentName, value); }
        }   

        //检验人
        public string? Inspector
        {
            get { return GetProperty(() => Inspector); }
            set { SetProperty(() => Inspector, value); }
        }

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }
    }
    
}
