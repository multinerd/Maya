using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Metronic.Enumerations
{
    public enum PipsMode
    {
        None,

        [EnumInfo("range")]
        Range,

        [EnumInfo("steps")]
        Steps,

        [EnumInfo("positions")]
        Positions,

        [EnumInfo("count")]
        Count,

        [EnumInfo("values")]
        Values,
    }
}