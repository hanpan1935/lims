using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.DataDictionaries.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lanpuda.Lims.UI.InspectionMethods.Standards.Edits
{
    public class StandardEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage ="必填")]
        public string Description
        {
            get { return GetProperty(() => Description); }
            set { SetProperty(() => Description, value); }
        }

       
        public int? DicStandardTypeId { get; set; }


        public string Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public StandardDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public ObservableCollection<StandardDetailEditModel> Details { get; set; }


        public StandardEditModel()
        {
            Details = new ObservableCollection<StandardDetailEditModel>();
        }
    }


    public class StandardDetailEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public Guid? StandardId
        {
            get { return GetProperty(() => StandardId); }
            set { SetProperty(() => StandardId, value); }
        }


        [GuidNotEmpty()]
        [Required()]
        public Guid InspectionItemId
        {
            get { return GetProperty(() => InspectionItemId); }
            set { SetProperty(() => InspectionItemId, value); }
        }

        public string? InspectionItemShortName
        {
            get { return GetProperty(() => InspectionItemShortName); }
            set { SetProperty(() => InspectionItemShortName, value); }
        }
        public string? InspectionItemFullName
        {
            get { return GetProperty(() => InspectionItemFullName); }
            set { SetProperty(() => InspectionItemFullName, value); }
        }


        public double? MinValue
        {
            get { return GetProperty(() => MinValue); }
            set { SetProperty(() => MinValue, value); }
        }


        public bool HasMinValue
        {
            get { return GetProperty(() => HasMinValue); }
            set { SetProperty(() => HasMinValue, value); }
        }

        public double? MaxValue
        {
            get { return GetProperty(() => MaxValue); }
            set { SetProperty(() => MaxValue, value); }
        }

        public bool HasMaxValue
        {
            get { return GetProperty(() => HasMaxValue); }
            set { SetProperty(() => HasMaxValue, value); }
        }


        public StandardDetailEditModel()
        {
        }
    }

}
