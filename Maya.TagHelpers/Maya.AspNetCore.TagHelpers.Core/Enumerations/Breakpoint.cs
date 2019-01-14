using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Core.Enumerations
{
    public enum Breakpoint
    {
        [EnumInfo("xs")]
        XSmall,

        [EnumInfo("sm")]
        Small,

        [EnumInfo("md")]
        Medium,

        [EnumInfo("lg")]
        Large,

        [EnumInfo("xl")]
        XLarge,
    }
}