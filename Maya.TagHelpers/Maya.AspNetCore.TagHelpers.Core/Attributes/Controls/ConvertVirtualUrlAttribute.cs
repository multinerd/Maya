using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Linq;

namespace Maya.AspNetCore.TagHelpers.Core.Attributes.Controls
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ConvertVirtualUrlAttribute : Attribute
    {
        public static void ConvertUrls(object target, IActionContextAccessor accessor)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));

            var list = target.GetType().GetProperties().Where(pI => pI.HasCustomAttribute<ConvertVirtualUrlAttribute>()).ToList();
            if (!list.Any()) return;

            var urlHelper = new UrlHelper(accessor.ActionContext);
            foreach (var propertyInfo in list)
            {
                if (propertyInfo.PropertyType != typeof(string))
                    throw new Exception("Decorated property must be a string");

                propertyInfo.SetValue(target, urlHelper.Content((string) propertyInfo.GetValue(target)));
            }
        }
    }
}