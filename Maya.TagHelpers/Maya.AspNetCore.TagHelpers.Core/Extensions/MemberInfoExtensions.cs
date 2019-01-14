using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class MemberInfoExtensions
    {
        public static string GetHtmlAttributeName(this MemberInfo property)
        {
            var customAttribute = property.GetCustomAttribute<HtmlAttributeNameAttribute>();
            if (customAttribute != null)
                return string.Concat(customAttribute.DictionaryAttributePrefix, customAttribute.Name);

            return Regex.Replace(property.Name, "([A-Z])", "-$1").ToLower().Trim(new[] { '-' });
        }

        public static bool HasCustomAttribute<T>(this MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(typeof(T), true).Any();
        }

        public static bool HasCustomAttribute(this MemberInfo memberInfo, Type attributeType)
        {
            return memberInfo.GetCustomAttributes(attributeType, true).Any();
        }
    }
}