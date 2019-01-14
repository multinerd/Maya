using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Core.Enumerations
{
    public enum Overflow
    {
        [EnumInfo("initial")]
        Initial,

        [EnumInfo("visible")]
        Visible,

        [EnumInfo("hidden")]
        Hidden,

        [EnumInfo("scroll")]
        Scroll,

        [EnumInfo("auto")]
        Auto,
    }
}