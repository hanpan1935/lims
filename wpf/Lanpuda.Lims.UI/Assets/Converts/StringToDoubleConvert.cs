using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lanpuda.Lims.UI.Assets.Converts
{
    public class StringToDoubleConvert : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            Type type = value.GetType();
            if (type != typeof(double))
            {
                return null;
            }
            return value;
            //Type type = value.GetType();
            //if (type != typeof(string))
            //{
            //    return "绑定错误,非string类型";
            //}
            //string? str = value.ToString();
            //if (string.IsNullOrEmpty(str))
            //{
            //    return null;
            //}
            //else
            //{
            //    return double.Parse(str);
            //}
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }


            Type type = value.GetType();
            if (type == typeof(string))
            {
                var str = value.ToString();
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                else
                {
                    //return double.Parse(str);
                    bool canParse = double.TryParse(str, out double result);
                    if (canParse)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return value;
        }
    }
}
