using System;
using System.ComponentModel;
using System.Linq;

namespace Integra.Common
{
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
}
