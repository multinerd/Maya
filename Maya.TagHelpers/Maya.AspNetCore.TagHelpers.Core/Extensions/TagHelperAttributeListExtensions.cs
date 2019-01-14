using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class TagHelperAttributeListExtensions
    {
        public static void AddAriaAttribute(this TagHelperAttributeList attributeList, string attributeName, object value)
        {
            attributeList.Add("aria-" + attributeName, value);
        }

        public static void AddDataAttribute(this TagHelperAttributeList attributeList, string attributeName, object value)
        {
            attributeList.Add("data-" + attributeName, value);
        }

        public static bool RemoveAll(this TagHelperAttributeList attributeList, params string[] attributeNames)
        {
            return attributeNames.Aggregate(false, (current, name) => attributeList.RemoveAll(name) | current);
        }
    }
}