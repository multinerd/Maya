using System;
using System.Linq;
using System.Reflection;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Attributes.Controls
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HtmlAttributeMinimizableAttribute : Attribute
    {
        public static void FillMinimizableAttributes(object target, TagHelperContext context)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (context == null)
                throw new ArgumentNullException(nameof(context));

            foreach (var property in target.GetType().GetProperties().Where(pI => pI.GetCustomAttribute<HtmlAttributeMinimizableAttribute>() != null))
            {
                var htmlAttributeName = property.GetHtmlAttributeName();
                if (!context.AllAttributes.ContainsName(htmlAttributeName))
                    continue;

                var item = context.AllAttributes[htmlAttributeName];
                if (item.Value is bool)
                    property.SetValue(target, item.Value);
                else
                    property.SetValue(target, !(item.Value ?? "").ToString().Equals("false"));
            }
        }
    }
}