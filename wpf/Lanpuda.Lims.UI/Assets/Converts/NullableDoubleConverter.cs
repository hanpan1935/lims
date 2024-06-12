using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Lanpuda.Lims.UI.Assets.Converts
{
    public class NullableDoubleConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            var type = value.GetType();
            double result = 0; 
            bool canParse = double.TryParse(value.ToString(), out  result);

            if (canParse == true)
            {
                return result;
            }
            else
            {
                return string.Empty;
            }
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return null;
            }

            if (double.TryParse((string)value, out double result))
            {
                return result;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
