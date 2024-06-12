using System;
using System.Collections.Generic;
using System.Text;

namespace Lanpuda.Lims.Utils
{
    public static class AutoQualified
    {
        public static bool? GetQualified(double? minValue, bool hasMinValue, double? maxValue, bool hasMaxValue, double? resultValue)
        {
            if (resultValue == null)
            {
                return null;
            }

            if (minValue != null && maxValue == null)  //只有最小值
            {
                if (hasMinValue == true)
                {
                    if (resultValue >= minValue)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (resultValue > minValue)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (minValue == null && maxValue != null)  //只有最大值
            {
                if (hasMaxValue == true)
                {
                    if (resultValue <= maxValue)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (resultValue < minValue)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (minValue != null && maxValue != null)  //都有值
            {
                if (hasMinValue == true && hasMaxValue == true)   //都包含
                {
                    if (resultValue >= minValue && resultValue <= maxValue)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (hasMinValue == true && hasMaxValue == false)  //包含最小
                {
                    if (resultValue >= minValue && resultValue < maxValue)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (hasMinValue == false && hasMaxValue == true) //包含最大
                {
                    if (resultValue > minValue && resultValue <= maxValue)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (hasMinValue == false && hasMaxValue == false) //都不包含
                {
                    if (resultValue > minValue && resultValue < maxValue)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else //都没有值
            {
                return null;
            }

            return null;
        }
    }
}
