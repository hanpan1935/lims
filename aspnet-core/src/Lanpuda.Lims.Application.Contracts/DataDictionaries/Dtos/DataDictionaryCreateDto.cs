using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lanpuda.Lims.DataDictionaries.Dtos
{
    [Serializable]
    public class DataDictionaryCreateDto
    {

        public DataDictionaryType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("DicEquipmentTypeDisplayValue")]
        public string DisplayValue { get; set; }

        public int Sort { get; set; }

    }
}
