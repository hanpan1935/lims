using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lanpuda.Lims.Records.Dtos
{
    public class RecordDetailResultValueUpdateDto
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("RecordDetailResultValue")]
        public double? ResultValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("RecordDetailIsQualified")]
        public bool? IsQualified { get; set; }

    }
}
