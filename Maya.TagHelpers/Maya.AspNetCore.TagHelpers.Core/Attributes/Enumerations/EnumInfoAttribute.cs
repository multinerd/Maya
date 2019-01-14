using System;

namespace Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumInfoAttribute : Attribute
    {
        public string Name { get; set; }

        public EnumInfoAttribute(string name)
        {
            this.Name = name;
        }
    }
}