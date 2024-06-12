using Lanpuda.Client.Mvvm;
using Lanpuda.Lims.UI.Assets.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.Samples.Edits
{
    public class SampleEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        public string Number
        {
            get { return GetProperty(() => Number); }
            set { SetProperty(() => Number, value); }
        }

        public Guid ProductId
        {
            get { return GetProperty(() => ProductId); }
            set { SetProperty(() => ProductId, value); }
        }

        [Required(ErrorMessage = "必填")]
        public string ProductName
        {
            get { return GetProperty(() => ProductName); }
            set { SetProperty(() => ProductName, value); }
        }

        public int? DicSampleTypeId
        {
            get { return GetProperty(() => DicSampleTypeId); }
            set { SetProperty(() => DicSampleTypeId, value); }
        }

        public int? DicSamplePropertyId
        {
            get { return GetProperty(() => DicSamplePropertyId); }
            set { SetProperty(() => DicSamplePropertyId, value); }
        }

        [DisplayName("SampleTime")]
        public DateTime SampleTime
        {
            get { return GetProperty(() => SampleTime); }
            set { SetProperty(() => SampleTime, value); }
        }

        [StringCanToDateTimeAttribute(ErrorMessage = "格式错误")]
        public string SampleTimeStr
        {
            get { return GetProperty(() => SampleTimeStr); }
            set { SetProperty(() => SampleTimeStr, value); }
        }

        public double? SampleCount
        {
            get { return GetProperty(() => SampleCount); }
            set { SetProperty(() => SampleCount, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SampleExpireTime")]
        public DateTime? ExpireTime
        {
            get { return GetProperty(() => ExpireTime); }
            set { SetProperty(() => ExpireTime, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SampleSender")]
        public string? Sender
        {
            get { return GetProperty(() => Sender); }
            set { SetProperty(() => Sender, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("SampleRemark")]
        public string? Remark
        {
            get { return GetProperty(() => Remark); }
            set { SetProperty(() => Remark, value); }
        }

        public Guid? CustomerId
        {
            get { return GetProperty(() => CustomerId); }
            set { SetProperty(() => CustomerId, value); }
        }


        public string? CustomerShortName
        {
            get { return GetProperty(() => CustomerShortName); }
            set { SetProperty(() => CustomerShortName, value); }
        }

        public Guid? SupplierId
        {
            get { return GetProperty(() => SupplierId); }
            set { SetProperty(() => SupplierId, value); }
        }


        public string? SupplierShortName
        {
            get { return GetProperty(() => SupplierShortName); }
            set { SetProperty(() => SupplierShortName, value); }
        }


        public void NotifySampleTimeChanged()
        {
            RaisePropertyChanged(nameof(SampleTime));
        }
    }
}
