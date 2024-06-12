using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.DataDictionaries.Dtos
{
    public class DataDictionaryGetListInput
    {

        public DataDictionaryType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("DicEquipmentTypeDisplayValue")]
        public string? DisplayValue { get; set; }


        public DataDictionaryGetListInput()
        {
            this.Type = DataDictionaryType.DicEquipmentType;
        }

    }
}