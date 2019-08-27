using System;
using System.Linq;
using System.Reflection;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Metronic.Extensions
{
    internal static class EnumExtensions
    {
        internal static ColorInfoAttribute GetColorInfo(this Color color)
        {
            var element = color.GetType().GetMember(color.ToString())
                .FirstOrDefault();
            if (element != null)
                return element.GetCustomAttribute<ColorInfoAttribute>();
            throw new InvalidOperationException("It is not possible to read ColorInfoAttribute from enumeration.");
        }

        internal static IconInfoAttribute GetIconInfo(this Icon icon)
        {
            var element = icon.GetType().GetMember(icon.ToString())
                .FirstOrDefault();
            if (element != null)
                return element.GetCustomAttribute<IconInfoAttribute>();
            throw new InvalidOperationException("It is not possible to read IconInfoAttribute from enumeration.");
        }
    }
}