using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lanpuda.Lims.UI.Assets.Converts
{
    public class IsQualifiedToStringConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            Type type = value.GetType();
            if (type != typeof(bool))
            {
                return "绑定错误,非布尔类型";
            }
            bool boolValue = (bool)value;
            return boolValue ? "合格" : "不合格";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
