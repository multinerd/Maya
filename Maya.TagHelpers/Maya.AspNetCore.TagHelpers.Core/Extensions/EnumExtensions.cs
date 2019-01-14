using System;
using System.Linq;
using System.Reflection;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class EnumExtensions
    {
        public static EnumInfoAttribute GetEnumInfo(this Enum e)
        {
            var memberInfo = e.GetType().GetMember(e.ToString()).FirstOrDefault();
            if (memberInfo == null)
                throw new InvalidOperationException("It is not possible to read EnumInfoAttribute from enumeration.");

            return memberInfo.GetCustomAttribute<EnumInfoAttribute>();
        }
    }
}