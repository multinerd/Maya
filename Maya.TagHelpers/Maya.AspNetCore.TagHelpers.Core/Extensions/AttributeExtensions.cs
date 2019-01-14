using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Maya.AspNetCore.TagHelpers.Core.Attributes;
using Maya.AspNetCore.TagHelpers.Core.Models;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class AttributeExtensions
    {
        public static LocalizedPropertyInfo GetLocalization(this PropertyInfo property)
        {
            if (property != null)
            {
                if (property.IsDefined(typeof(BreconsDisplayAttribute)))
                {
                    var customAttribute = property.GetCustomAttribute<BreconsDisplayAttribute>();
                    return new LocalizedPropertyInfo()
                    {
                        DisplayName = customAttribute.DisplayName,
                        Description = customAttribute.Description
                    };
                }

                if (property.IsDefined(typeof(DisplayAttribute)))
                {
                    var customAttribute = property.GetCustomAttribute<DisplayAttribute>();
                    return new LocalizedPropertyInfo()
                    {
                        DisplayName = customAttribute.Name,
                        Description = customAttribute.Description
                    };
                }

                if (property.IsDefined(typeof(DisplayNameAttribute)))
                {
                    var customAttribute = property.GetCustomAttribute<DisplayNameAttribute>();
                    return new LocalizedPropertyInfo()
                    {
                        DisplayName = customAttribute.DisplayName
                    };
                }
            }

            return null;
        }
    }
}