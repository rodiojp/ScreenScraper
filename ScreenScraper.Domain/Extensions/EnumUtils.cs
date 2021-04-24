using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScreenScraper.Domain.Extensions
{
    public class EnumUtils
    {
        public static IEnumerable<T> GetEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
        public static TEnum FromDescription<TEnum>(string descripton) where TEnum : struct
        {
            if (string.IsNullOrEmpty(descripton))
            {
                throw new ArgumentException($"'{nameof(descripton)}' cannot be null or empty.", nameof(descripton));
            }
            Type typeFromHandle = typeof(TEnum);
            FieldInfo[] fields = typeFromHandle.GetFields();
            FieldInfo fieldInfo = fields.FirstOrDefault((FieldInfo f) => Enumerable.Any<DescriptionAttribute>(Enumerable.Cast<DescriptionAttribute>((IEnumerable)f.GetCustomAttributes(typeof(DescriptionAttribute), false)), (Func<DescriptionAttribute, bool>)((DescriptionAttribute a) => descripton.Equals(a.Description, StringComparison.OrdinalIgnoreCase))));
            if (fieldInfo != (FieldInfo)null)
            {
                return (TEnum)fieldInfo.GetValue(null);
            }
            return (TEnum)Enum.Parse(typeFromHandle, descripton);
        }
        public static TEnum FromString<TEnum>(string name) where TEnum : struct
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }
            Type typeFromHandle = typeof(TEnum);
            IDictionary<string, TEnum> dictionary = Enum.GetValues(typeFromHandle).Cast<TEnum>().ToDictionary((TEnum x) => x.ToString().ToUpper(), (TEnum x) => x);
            if (!dictionary.ContainsKey(name.ToUpper()))
            {
                throw new ArgumentOutOfRangeException($"Enumerated type {typeFromHandle} does not contain {name}");
            }
            return dictionary[name.ToUpper()];
        }
    }
}
