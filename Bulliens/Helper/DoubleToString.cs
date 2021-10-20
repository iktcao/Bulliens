using System;
using System.Windows.Data;

namespace Boiler.Helper
{
    public class DoubleToString : IValueConverter
    {
        string lastInput;
        bool hasPoint;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double dbl = (double)value;
            if (hasPoint)
            {
                hasPoint = false;
                return lastInput;
            }
            else
            {
                if (dbl == 0)
                    return string.Empty;
                else
                    return dbl;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string str = (string)value;
            string str2 = str + "0";

            double dbl;
            lastInput = str;

            if (str == "0")
            {
                dbl = 0;
                hasPoint = true;
                return dbl;
            }
            //else if (str == "e" || str == "E")
            //{
            //    dbl = 0;
            //    hasPoint = true;
            //    return dbl;
            //}
            else if (double.TryParse(str, out dbl))
            {
                if (double.Parse(str) == double.Parse(str2))
                {
                    hasPoint = true;
                    return dbl;
                }
                else
                {
                    hasPoint = false;
                    lastInput = str;
                    return dbl;
                }
            }
            else if (double.TryParse(str2, out dbl))
            {
                hasPoint = true;
                return dbl;
            }
            else
            {
                hasPoint = true;
                return str;
            }

            //if (!double.TryParse(str, out dbl))
            //{
            //    if (!double.TryParse(str2, out dbl))
            //    {
            //        if (!double.TryParse(str3, out dbl))
            //        {
            //            if (!double.TryParse(str4, out dbl))
            //                throw new FormatException("输入的内容不是数字！");
            //            else
            //                hasPoint = true;
            //        }
            //        else
            //            hasPoint = true;
            //    }
            //    else
            //        hasPoint = true;
            //}
            //else
            //{
            //    if (double.Parse(str2) == double.Parse(str4))
            //        hasPoint = true;
            //}            
            //lastInput = str;
            //return dbl.ToString();
        }

        //bool addDecimalPoint;
        //public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    double d = (double)value;
        //    if (d == 0)
        //        return string.Empty;
        //    //string s = d.ToString("#.##", System.Globalization.CultureInfo.InvariantCulture);
        //    string s = d.ToString(System.Globalization.CultureInfo.InvariantCulture);
        //    if (addDecimalPoint)
        //    {
        //        s += ".";
        //        addDecimalPoint = false;
        //        return s;
        //    }

        //    return d;
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    string s = value.ToString();
        //    if (string.IsNullOrEmpty(s))
        //        return 0;
        //    if (s.EndsWith("."))
        //    {
        //        s += "0";
        //        addDecimalPoint = true;
        //    }
        //    double d;
        //    if (double.TryParse(s, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out d))
        //        return d;
        //    return value;            
        //}
    }
}
