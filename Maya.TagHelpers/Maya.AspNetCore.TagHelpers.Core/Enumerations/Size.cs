using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Core.Enumerations
{
    public enum Size
    {
        Default,

        [EnumInfo("lg")]
        Large,

        [EnumInfo("sm")]
        Small,
    }
}