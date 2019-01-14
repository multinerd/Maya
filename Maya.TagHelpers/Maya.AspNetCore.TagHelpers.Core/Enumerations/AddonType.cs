using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Core.Enumerations
{
    public enum AddonType
    {
        [EnumInfo("prepend")]
        Prepend,

        [EnumInfo("append")]
        Append,
    }
}