using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using System.Reflection;

namespace Maya.AspNetCore.TagHelpers.Core.Attributes.Controls
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ContextAttribute : Attribute
    {
        public bool UseInherited { get; set; } = true;

        public bool RemoveContext { get; set; }

        public string Key { get; set; }

        public ContextAttribute()
        {
        }

        public ContextAttribute(string key)
        {
            this.Key = key;
        }

        public static void SetContexts(object target, TagHelperContext context)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var elements = target.GetType().GetProperties(flags).Where(pi => pi.HasCustomAttribute<ContextAttribute>());
            foreach (var element in elements)
            {
                var customAttribute = element.GetCustomAttribute<ContextAttribute>();
                if (string.IsNullOrEmpty(customAttribute.Key))
                {
                    var contextItem = context.GetContextItem(element.PropertyType, customAttribute.UseInherited);
                    if (contextItem != null)
                        element.SetValue(target, contextItem);

                    if (customAttribute.RemoveContext)
                        context.RemoveContextItem(element.PropertyType, customAttribute.UseInherited);
                }
                else
                {
                    element.SetValue(target, context.GetContextItem(element.PropertyType, customAttribute.Key));
                    if (customAttribute.RemoveContext)
                        context.RemoveContextItem(customAttribute.Key);
                }
            }
        }
    }
}