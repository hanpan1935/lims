using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Lims.UI.Assets.Attributes
{
    public class StringCanToDoubleAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                string? valueString = value.ToString();
                if (string.IsNullOrEmpty(valueString))
                {
                    return true;
                }
                else
                {
                    bool canConvert = double.TryParse(valueString, out _);
                    return canConvert;
                }
            }
        }
    }
}
