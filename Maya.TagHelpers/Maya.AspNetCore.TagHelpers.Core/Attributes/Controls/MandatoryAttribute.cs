using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Maya.AspNetCore.TagHelpers.Core.Exceptions;
using Maya.AspNetCore.TagHelpers.Core.Extensions;

namespace Maya.AspNetCore.TagHelpers.Core.Attributes.Controls
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MandatoryAttribute : Attribute
    {
        public static void CheckProperties(object tagHelper)
        {
            foreach (var property in tagHelper.GetType().GetProperties()
                .Where(pi => pi.HasCustomAttribute<MandatoryAttribute>()))
            {
                var obj = property.GetValue(tagHelper);
                var htmlAttributeName = property.GetHtmlAttributeName();

                if (property.PropertyType == typeof(string) && string.IsNullOrEmpty((string) obj))
                    throw new MandatoryAttributeException(htmlAttributeName, tagHelper.GetType());

                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && !((IEnumerable) obj).GetEnumerator().MoveNext())
                    throw new MandatoryAttributeException(htmlAttributeName, tagHelper.GetType());

                if (obj == null)
                    throw new MandatoryAttributeException(htmlAttributeName, tagHelper.GetType());
            }
        }
    }
}