using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Core.Enumerations
{
    public enum VerticalAlignment
    {
        Default,

        [EnumInfo("top")]
        Top,

        [EnumInfo("middle")]
        Middle,

        [EnumInfo("bottom")]
        Bottom,
    }
}