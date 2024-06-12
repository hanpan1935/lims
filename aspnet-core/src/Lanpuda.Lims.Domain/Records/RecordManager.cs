using Lanpuda.Lims.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Lanpuda.Lims.Records
{
    public class RecordManager : DomainService
    {
        public void SetResultValue(RecordDetail recordDetail , double? resultValue)
        {
            recordDetail.ResultValue = resultValue;
            if (resultValue == null)
            {
                recordDetail.IsQualified = null;
            }
            else
            {
                var result = AutoQualified.GetQualified(recordDetail.MinValue,recordDetail.HasMinValue,recordDetail.MaxValue,recordDetail.HasMaxValue,resultValue);
                recordDetail.IsQualified = result;
            }
        }
    }
}
