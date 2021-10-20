using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Boiler.Models
{
    public class ValidateModelBase : ObservableObject, IDataErrorInfo
    {
        // 构造函数
        public ValidateModelBase()
        {

        }

        // 字典字段
        private Dictionary<string, string> dataErrors = new Dictionary<string, string>();

        // 属性：判断是否通过验证
        public bool IsValiad
        {
            get
            {
                if (dataErrors != null && dataErrors.Count > 0)
                    return false;
                return true;
            }
        }

        // Error属性：用于指示整个对象的错误
        public string Error => null;

        // 索引器：用于指示单个属性级别的错误
        public string this[string columnName]
        {
            get
            {
                ValidationContext validationContext = new ValidationContext(this, null, null);
                validationContext.MemberName = columnName;
                List<ValidationResult> validationResults = new List<ValidationResult>();
                bool result = Validator.TryValidateProperty(this.GetType().GetProperty(columnName).GetValue(this, null),
                    validationContext, validationResults);

                if (validationResults.Count > 0)
                {
                    AddDic(dataErrors, validationContext.MemberName);
                    return string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage).ToArray());
                }
                RemoveDic(dataErrors, validationContext.MemberName);
                return null;
            }
        }

        // 字典添加键值对的方法
        private void AddDic(Dictionary<string, string> dic, string key)
        {
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, "");
            }
        }

        // 字典移除键值对的方法
        private void RemoveDic(Dictionary<string, string> dic, string key)
        {
            dic.Remove(key);
        }
    }
}
