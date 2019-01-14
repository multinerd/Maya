using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Core.Enumerations
{
    public enum HttpVerb
    {
        [EnumInfo("GET")]
        Get,

        [EnumInfo("POST")]
        Post,

        [EnumInfo("PUT")]
        Put,

        [EnumInfo("DELETE")]
        Delete,
    }
}