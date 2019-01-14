using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Core.Enumerations
{
    public enum Placement
    {
        [EnumInfo("top")]
        Top,

        [EnumInfo("bottom")]
        Bottom,

        [EnumInfo("left")]
        Left,

        [EnumInfo("right")]
        Right,
    }
}