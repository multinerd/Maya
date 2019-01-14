using System;
using System.Linq;
using System.Reflection;
using Maya.AspNetCore.TagHelpers.Core.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Attributes.Controls
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GenerateIdAttribute : Attribute
    {
        public string Prefix { get; set; }

        public bool RenderIdAttribute { get; set; }

        public string Id { get; set; }

        public GenerateIdAttribute(string prefix, bool renderIdAttribute = true)
        {
            this.Prefix = prefix;
            this.RenderIdAttribute = renderIdAttribute;
            this.Id = this.Prefix + Guid.NewGuid().ToString("N");
        }

        public static void CopyIdentifier(BreconsTagHelperBase tagHelper)
        {
            var idAttribute = tagHelper.GetType().GetTypeInfo().GetCustomAttributes<GenerateIdAttribute>(true).FirstOrDefault();
            if (string.IsNullOrEmpty(tagHelper.Id) && idAttribute != null && idAttribute.RenderIdAttribute)
            {
                tagHelper.GeneratedId = idAttribute.Id;
                tagHelper.Id = tagHelper.GeneratedId;
            }
        }

        public static void RenderIdentifier(BreconsTagHelperBase tagHelper, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(tagHelper.Id))
                output.MergeAttribute("id", tagHelper.Id);
        }
    }
}