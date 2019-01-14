using System;

namespace Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ColorInfoAttribute : EnumInfoAttribute
    {
        public string TextCssClass { get; set; }

        public string BackgroundCssClass { get; set; }

        public string BorderCssClass { get; set; }

        public ColorInfoAttribute(string name)
            : base(name)
        {
        }
    }
}