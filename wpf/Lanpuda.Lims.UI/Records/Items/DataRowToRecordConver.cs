using Lanpuda.Lims.Records.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lanpuda.Lims.UI.Records.Items
{
    public class DataRowToRecordConver : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            DataRowView dataRowView = (DataRowView)value;
            DataRow dataRow = dataRowView.Row;
            RecordDto recordDto = new RecordDto();
            recordDto.Id = (Guid)dataRow["Id"];
            recordDto.Number = (string)dataRow["Number"];
            recordDto.SampleId = (Guid)dataRow["SampleId"];
            recordDto.SampleNumber = (string)dataRow["SampleNumber"];
            recordDto.ProductName = (string)dataRow["ProductName"];
            return recordDto;
        }

        public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            ;
            return null;
        }
    }
}
