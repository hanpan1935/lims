using Lanpuda.Client.Common.Attributes;
using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.BasicInformations.Locations.Edits
{
    public class LocationEditModel : ModelBase
    {
        public Guid? Id
        {
            get { return GetValue<Guid?>(nameof(Id)); }
            set { SetValue(value, nameof(Id)); }
        }

        /// <summary>
        /// 仓库ID
        /// </summary>
        [GuidNotEmpty(ErrorMessage = "必填")]
        public Guid WarehouseId
        {
            get { return GetValue<Guid>(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// 库位名称
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

    }
}
