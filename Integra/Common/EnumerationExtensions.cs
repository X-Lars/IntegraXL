using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Integra.Common
{
    public interface IEnumeration
    {
        List<string> Values { get; }
    }
    public abstract class Enumeration : IEnumeration
    {
        public int Value { get; set; }
        public List<string> Values { get; }
        public override string ToString()
        {
            return Values[Value];
        }
    }

    public static class EnumerationExtensions
    {
        public static string Description<T>(this T value) where T: struct
        {
            Type type = value.GetType();

            var member = type.GetMember(value.ToString()).FirstOrDefault();

            if(member != null)
            {
                object attribute = member.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

                if (attribute != null)
                    return ((DescriptionAttribute)attribute).Description;

            }

            return value.ToString();
        }
    }

    public class DescriptionConverter : EnumConverter
    {
        public DescriptionConverter(Type type) : base(type) { }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

                    if (fieldInfo != null)
                    {
                        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                        return ((attributes.Length > 0) && (!string.IsNullOrEmpty(attributes[0].Description))) ? attributes[0].Description : value.ToString();
                    }
                }

                return string.Empty;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

       
    }
}
