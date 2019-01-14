using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Metronic.Enumerations
{
    public enum DatepickerOrientation
    {
        [EnumInfo("auto")]
        Auto,

        [EnumInfo("top auto")]
        TopAuto,

        [EnumInfo("top left")]
        TopLeft,

        [EnumInfo("top right")]
        TopRight,

        [EnumInfo("auto left")]
        AutoLeft,

        [EnumInfo("auto right")]
        AutoRight,

        [EnumInfo("bottom auto")]
        BottomAuto,

        [EnumInfo("bottom left")]
        BottomLeft,

        [EnumInfo("bottom right")]
        BottomRight,
    }
}