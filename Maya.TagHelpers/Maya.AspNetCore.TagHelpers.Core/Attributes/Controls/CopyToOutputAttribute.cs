using System;
using System.Linq;
using System.Reflection;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Attributes.Controls
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CopyToOutputAttribute : Attribute
    {
        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public string OutputAttributeName { get; set; }

        public bool CopyIfValueIsNull { get; set; }


        public CopyToOutputAttribute()
        {
        }

        public CopyToOutputAttribute(string outputAttributeName)
        {
            this.OutputAttributeName = outputAttributeName;
        }

        public CopyToOutputAttribute(string prefix, string suffix)
        {
            this.Prefix = prefix;
            this.Suffix = suffix;
        }

        public CopyToOutputAttribute(bool copyIfValueIsNull)
        {
            this.CopyIfValueIsNull = copyIfValueIsNull;
        }

        public CopyToOutputAttribute(bool copyIfValueIsNull, string outputAttributeName)
        {
            this.OutputAttributeName = outputAttributeName;
            this.CopyIfValueIsNull = copyIfValueIsNull;
        }

        public CopyToOutputAttribute(bool copyIfValueIsNull, string prefix, string suffix)
        {
            this.Prefix = prefix;
            this.Suffix = suffix;
            this.CopyIfValueIsNull = copyIfValueIsNull;
        }

        public CopyToOutputAttribute(bool copyIfValueIsNull, string outputAttributeName, string prefix, string suffix)
        {
            this.Prefix = prefix;
            this.Suffix = suffix;
            this.OutputAttributeName = outputAttributeName;
            this.CopyIfValueIsNull = copyIfValueIsNull;
        }

        public CopyToOutputAttribute(string outputAttributeName, string prefix, string suffix)
        {
            this.Prefix = prefix;
            this.Suffix = suffix;
            this.OutputAttributeName = outputAttributeName;
        }


        public static void CopyPropertiesToOutput(object target, TagHelperOutput output)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var ppropertyInfos = target.GetType().GetProperties().Where(pI => pI.HasCustomAttribute<CopyToOutputAttribute>());
            foreach (var propertyInfo in ppropertyInfos)
            {
                var lower = propertyInfo.GetValue(target);
                if (propertyInfo.PropertyType.IsAssignableFrom(typeof(bool)))
                    lower = lower?.ToString().ToLower();

                foreach (var customAttribute in propertyInfo.GetCustomAttributes<CopyToOutputAttribute>())
                {
                    if (lower != null || customAttribute.CopyIfValueIsNull)
                        output.Attributes.Add(
                            customAttribute.Prefix +
                            (customAttribute.OutputAttributeName ?? propertyInfo.GetHtmlAttributeName()) +
                            customAttribute.Suffix, lower);
                }
            }
        }
    }
}