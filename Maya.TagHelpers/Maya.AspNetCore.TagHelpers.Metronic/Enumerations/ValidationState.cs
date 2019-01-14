using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Metronic.Enumerations
{
    public enum ValidationState
    {
        None,

        [EnumInfo("success")]
        Success,

        [EnumInfo("warning")]
        Warning,

        [EnumInfo("danger")]
        Danger,
    }
}