using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Core.Enumerations
{
    public enum Theme
    {
        Default,

        [EnumInfo("dark")]
        Dark,

        [EnumInfo("light")]
        Light,
    }
}