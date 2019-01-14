using System;

namespace Maya.AspNetCore.TagHelpers.Core.Attributes
{
    public class BreconsDisplayAttribute : Attribute, IBreconsDisplayAttribute
    {
        public virtual string DisplayName { get; }

        public virtual string Description { get; }

        public BreconsDisplayAttribute(string displayName)
            : this(displayName, string.Empty)
        {
        }

        public BreconsDisplayAttribute(string displayName, string description)
        {
            this.DisplayName = displayName;
            this.Description = description;
        }
    }
}