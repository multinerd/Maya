using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Core.Enumerations
{
    public enum HorizontalAlignment
    {
        Default,

        [EnumInfo("left")]
        Left,

        [EnumInfo("center")]
        Center,

        [EnumInfo("right")]
        Right,
    }
}