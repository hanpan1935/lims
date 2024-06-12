using Lanpuda.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.BasicInformations.DataDictionaries.Edits
{
    public class DataDictionaryEditModel : ModelBase
    {

        public int? Id { get; set; }

        [Required(ErrorMessage ="必填")]
        public DataDictionaryType Type
        {
            get { return GetProperty(() => Type); }
            set { SetProperty(() => Type, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [Required(ErrorMessage ="必填")]
        [DisplayName("DicEquipmentTypeDisplayValue")]
        public string DisplayValue
        {
            get { return GetProperty(() => DisplayValue); }
            set { SetProperty(() => DisplayValue, value); }
        }

        public int Sort
        {
            get { return GetProperty(() => Sort); }
            set { SetProperty(() => Sort, value); }
        }
    }
}
