using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.BasicInformations.Products.Edits
{
    public class ProductEditModel : ModelBase
    {
        public Guid? Id
        {
            get { return GetValue<Guid?>(nameof(Id)); }
            set { SetValue(value, nameof(Id)); }
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Name
        {
            get { return GetValue<string>(nameof(Name)); }
            set { SetValue(value, nameof(Name)); }
        }


        [Required(ErrorMessage = "必填")]
        public string Unit
        {
            get { return GetProperty(() => Unit); }
            set { SetProperty(() => Unit, value); }
        }


        public int? DicProductTypeId
        {
            get { return GetProperty(() => DicProductTypeId); }
            set { SetProperty(() => DicProductTypeId, value); }
        }

        /// <summary>
        /// 产品编码
        /// </summary>
        public string Number
        {
            get { return GetValue<string>(nameof(Number)); }
            set { SetValue(value, nameof(Number)); }
        }


        /// <summary>
        /// 产品规格
        /// </summary>
        public string? Spec
        {
            get { return GetValue<string>(nameof(Spec)); }
            set { SetValue(value, nameof(Spec)); }
        }


        public Guid? StandardId { get; set; }

        public string? StandardDescription
        {
            get { return GetValue<string>(nameof(StandardDescription)); }
            set { SetValue(value, nameof(StandardDescription)); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark
        {
            get { return GetValue<string>(nameof(Remark)); }
            set { SetValue(value, nameof(Remark)); }
        }

    }
}
