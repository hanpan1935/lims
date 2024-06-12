using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lanpuda.Lims.Permissions.LimsPermissions;
using System.Collections.ObjectModel;
using Lanpuda.Lims.DataDictionaries.Dtos;
using Lanpuda.Lims.Utils;

namespace Lanpuda.Lims.UI.Records.Edits
{
    public class RecordEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string? Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public Guid SampleId { get; set; }
        [Required(ErrorMessage = "样品编号不能为空")]
        public string? SampleNumber
        {
            get { return GetProperty(() => SampleNumber); }
            set { SetProperty(() => SampleNumber, value); }
        }
        public string? ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }


        public DicRatingTypeLookupDto? SelectRatingType
        {
            get { return GetProperty(() => SelectRatingType); }
            set { SetProperty(() => SelectRatingType, value); }
        }

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public RecordDetailEditModel? SelectedRow
        {
            get { return GetProperty(() => SelectedRow); }
            set { SetProperty(() => SelectedRow, value); }
        }

        public ObservableCollection<RecordDetailEditModel> Details { get; set; }


        public RecordEditModel()
        {
            Details = new ObservableCollection<RecordDetailEditModel>();
        }
    }


    public class RecordDetailEditModel : ModelBase
    {
        public Guid? Id
        {
            get { return GetProperty(() => Id); }
            set { SetProperty(() => Id, value); }
        }

        public Guid InspectionItemId { get; set; }

        public string? InspectionItemFullName
        {
            get { return GetProperty(() => InspectionItemFullName); }
            set { SetProperty(() => InspectionItemFullName, value); }
        }

        public string? InspectionItemShortName
        {
            get { return GetProperty(() => InspectionItemShortName); }
            set { SetProperty(() => InspectionItemShortName, value); }
        }

        public double? MinValue
        {
            get { return GetProperty(() => MinValue); }
            set { SetProperty(() => MinValue, value, NotifyIsQualifiedChanged); }
        }

        public bool HasMinValue
        {
            get { return GetProperty(() => HasMinValue); }
            set { SetProperty(() => HasMinValue, value, NotifyIsQualifiedChanged); }
        }

        public double? MaxValue
        {
            get { return GetProperty(() => MaxValue); }
            set { SetProperty(() => MaxValue, value, NotifyIsQualifiedChanged); }
        }

        public bool HasMaxValue
        {
            get { return GetProperty(() => HasMaxValue); }
            set { SetProperty(() => HasMaxValue, value, NotifyIsQualifiedChanged); }
        }


        public double? ResultValue
        {
            get { return GetProperty(() => ResultValue); }
            set { SetProperty(() => ResultValue, value, NotifyIsQualifiedChanged); }
        }


   

        public bool? IsQualified
        {
            get { return GetProperty(() => IsQualified); }
            set { SetProperty(() => IsQualified, value); }
        }

        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public Dictionary<string, bool?> IsQualifiedSource { get; set; }

        public RecordDetailEditModel()
        {
            IsQualifiedSource = new Dictionary<string, bool?>()
            {
                { "未检", null },
                { "合格", true },
                { "不合格", false }
            };
        }

        public void NotifyIsQualifiedChanged()
        {
            bool? res = AutoQualified.GetQualified(MinValue, HasMinValue, MaxValue, HasMaxValue, ResultValue);
            this.IsQualified = res;
            RaisePropertyChanged(nameof(IsQualified));
        }
    }
}
