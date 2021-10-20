using System.Globalization;
using System.Windows.Controls;

namespace Boiler.Helper
{
    public class NumericRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = (string)value;
            string str2 = str + "0";
            string str3 = "0" + str;
            string str4 = "0" + str + "0";

            double dbl;

            if (str == "环状流" || str == "柱状流" || str == "泡状流" || str == "雾状流")
            {
                return new ValidationResult(true, null);
            }

            if (!double.TryParse(str, out dbl))
            {
                if (!double.TryParse(str2, out dbl))
                {
                    if (!double.TryParse(str3, out dbl))
                    {
                        if (!double.TryParse(str4, out dbl))
                            return new ValidationResult(false, "输入的内容不是数字！");
                    }
                }
            }
            
            return new ValidationResult(true, null);
        }
    }
}
