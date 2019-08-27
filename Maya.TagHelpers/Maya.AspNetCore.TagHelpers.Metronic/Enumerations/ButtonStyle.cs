using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Metronic.Enumerations
{
    public enum ButtonStyle
    {
        Default,

        [EnumInfo("square")]
        Square,

        [EnumInfo("pill")]
        Pill,
    }
}