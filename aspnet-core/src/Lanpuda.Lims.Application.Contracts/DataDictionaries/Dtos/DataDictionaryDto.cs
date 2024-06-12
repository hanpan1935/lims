using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Lanpuda.Lims.DataDictionaries.Dtos
{
    public class DataDictionaryDto : LimsAuditedEntityDto<int>
    {
        public DataDictionaryType Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayValue { get; set; }

        public int Sort { get; set; }
    }
}