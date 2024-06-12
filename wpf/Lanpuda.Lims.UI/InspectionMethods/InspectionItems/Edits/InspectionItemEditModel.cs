using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.InspectionItems.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.InspectionMethods.InspectionItems.Edits
{
    public class InspectionItemEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "必填")]
        public string FullName
        {
            get { return GetProperty(() => FullName); }
            set { SetProperty(() => FullName, value); }
        }

        [Required(ErrorMessage = "必填")]
        public string ShortName
        {
            get { return GetProperty(() => ShortName); }
            set { SetProperty(() => ShortName, value); }
        }

        [Required(ErrorMessage = "必填")]
        public string Basis
        {
            get { return GetProperty(() => Basis); }
            set { SetProperty(() => Basis, value); }
        }

        [Required(ErrorMessage = "必填")]
        public string Unit
        {
            get { return GetProperty(() => Unit); }
            set { SetProperty(() => Unit, value); }
        }

        public Guid? DefaultEquipmentId
        {
            get { return GetProperty(() => DefaultEquipmentId); }
            set { SetProperty(() => DefaultEquipmentId, value); }
        }

        public string? DefaultEquipmentName
        {
            get { return GetProperty(() => DefaultEquipmentName); }
            set { SetProperty(() => DefaultEquipmentName, value); }
        }


        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public InspectionItemEditModel()
        {
            this.FullName = string.Empty;
            this.ShortName = string.Empty;
            this.Basis = string.Empty;
            this.Unit = string.Empty;
        }
    }


}
