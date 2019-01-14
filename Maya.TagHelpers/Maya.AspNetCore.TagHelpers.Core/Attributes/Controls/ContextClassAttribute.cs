using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Reflection;

namespace Maya.AspNetCore.TagHelpers.Core.Attributes.Controls
{
    public class ContextClassAttribute : Attribute
    {
        public string Key { get; set; }

        public Type Type { get; set; }

        public ContextClassAttribute()
        {
        }

        public ContextClassAttribute(Type type)
        {
            this.Type = type;
        }

        public ContextClassAttribute(string key)
        {
            this.Key = key;
        }


        public static void SetContext(object target, TagHelperContext context)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var type = target.GetType();
            var customAttribute = type.GetTypeInfo().GetCustomAttribute<ContextClassAttribute>();
            if (customAttribute != null)
            {
                if (string.IsNullOrEmpty(customAttribute.Key))
                {
                    context.SetContextItem(customAttribute.Type ?? type, target);
                    return;
                }

                context.SetContextItem(customAttribute.Key, target);
            }
        }
    }
}