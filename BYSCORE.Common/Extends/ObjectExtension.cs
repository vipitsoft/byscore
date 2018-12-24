using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;

namespace BYSCORE.Common
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 把对象转换为JSON字符串
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJSON(this object o)
        {
            if (o == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(o);
        }
        /// <summary>
        /// 把Json文本转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T FromJSON<T>(this string input)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(input);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        /// <summary>
        /// 把具有同结构的实体赋值到另一个实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this object from) where T : new()
        {
            if (from == null)
            {
                return default(T);
            }
            var to = Activator.CreateInstance<T>();
            return from.ConvertTo<T>(to);
        }
        public static T ConvertTo<T>(this object from, T to) where T : new()
        {
            if (from == null)
            {
                return to;
            }
            if (to == null)
            {
                to = Activator.CreateInstance<T>();
            }
            PropertyDescriptorCollection fromProperties = TypeDescriptor.GetProperties(from);
            PropertyDescriptorCollection toProperties = TypeDescriptor.GetProperties(to);
            foreach (PropertyDescriptor fromProperty in fromProperties)
            {

                PropertyDescriptor toProperty = toProperties.Find(fromProperty.Name, true);
                if (toProperty != null && !toProperty.IsReadOnly)
                {
                    bool isDirectlyAssignable = toProperty.PropertyType.IsAssignableFrom(fromProperty.PropertyType);
                    bool liftedValueType = !isDirectlyAssignable && Nullable.GetUnderlyingType(fromProperty.PropertyType) == toProperty.PropertyType;
                    if (isDirectlyAssignable || liftedValueType)
                    {
                        object fromValue = fromProperty.GetValue(from);
                        if (isDirectlyAssignable || (fromValue != null && liftedValueType))
                        {
                            toProperty.SetValue(to, fromValue);
                        }
                    }
                }
            }
            return to;
        }

        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }

        /// <summary>
        /// 检查模型验证
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool VerifyModel(this object instance, ref string message)
        {
            var context = new ValidationContext(instance, null, null);
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(instance, context, results, true);
            if (!valid)
            {
                message = results.Aggregate(message, (current, item) => current + (string.Join(",", item.MemberNames) + ":" + item.ErrorMessage + ";"));
            }
            return valid;
        }
    }
}
