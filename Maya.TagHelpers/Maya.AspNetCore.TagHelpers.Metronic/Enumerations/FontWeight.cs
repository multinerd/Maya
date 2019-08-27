using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Metronic.Enumerations
{
    public enum FontWeight
    {
        Normal,

        [EnumInfo("bold")]
        Bold,

        [EnumInfo("bolder")]
        Bolder,

        [EnumInfo("boldest")]
        Boldest,
    }
}