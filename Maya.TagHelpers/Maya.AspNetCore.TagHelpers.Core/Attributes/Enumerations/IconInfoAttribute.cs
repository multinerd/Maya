using System;

namespace Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations
{
    [AttributeUsage(AttributeTargets.Field)]
    public class IconInfoAttribute : EnumInfoAttribute
    {
        public string CssClass { get; set; }

        public string Collection { get; set; }

        public IconInfoAttribute(string collection, string name, string cssClass)
            : base(name)
        {
            this.Collection = collection;
            this.CssClass = cssClass;
        }
    }
}