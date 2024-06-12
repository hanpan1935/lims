using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.BasicInformations.Suppliers.Edits
{
    public class SupplierEditModel : ModelBase
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 供应商编码*
        /// </summary>
        public string? Number
        {
            get { return GetValue<string>(nameof(Number)); }
            set { SetValue(value, nameof(Number)); }
        }

        /// <summary>
        /// 供应商全称称*
        /// </summary>
        [Required(ErrorMessage ="必填")]
        [MaxLength(128)]
        public string FullName
        {
            get { return GetValue<string>(nameof(FullName)); }
            set { SetValue(value, nameof(FullName)); }
        }

        /// <summary>
        /// 供应商简称*
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [MaxLength(128)]
        public string ShortName
        {
            get { return GetValue<string>(nameof(ShortName)); }
            set { SetValue(value, nameof(ShortName)); }
        }

        [MaxLength(128)]
        public string? Manager
        {
            get { return GetValue<string>(nameof(Manager)); }
            set { SetValue(value, nameof(Manager)); }
        }

        [MaxLength(128)]
        public string? ManagerTel
        {
            get { return GetValue<string>(nameof(ManagerTel)); }
            set { SetValue(value, nameof(ManagerTel)); }
        }

        /// 联系电话
        /// </summary>
        [MaxLength(128)]
        public string? Remark
        {
            get { return GetValue<string>(nameof(Remark)); }
            set { SetValue(value, nameof(Remark)); }
        }

      
    }
}
