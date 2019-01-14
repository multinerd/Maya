using Maya.AspNetCore.TagHelpers.Core.Attributes.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Metronic.Enumerations
{
    public enum Color
    {
        None,

        [ColorInfo("primary", BackgroundCssClass = "m--bg-primary", BorderCssClass = "border-primary",
            TextCssClass = "m--font-primary")]
        Primary,

        [ColorInfo("secondary", BackgroundCssClass = "m--bg-secondary", BorderCssClass = "border-secondary",
            TextCssClass = "m--font-secondary")]
        Secondary,

        [ColorInfo("success", BackgroundCssClass = "m--bg-success", BorderCssClass = "border-success",
            TextCssClass = "m--font-success")]
        Success,

        [ColorInfo("danger", BackgroundCssClass = "m--bg-danger", BorderCssClass = "border-danger",
            TextCssClass = "m--font-danger")]
        Danger,

        [ColorInfo("warning", BackgroundCssClass = "m--bg-warning", BorderCssClass = "border-warning",
            TextCssClass = "m--font-warning")]
        Warning,

        [ColorInfo("info", BackgroundCssClass = "m--bg-info", BorderCssClass = "border-info",
            TextCssClass = "m--font-info")]
        Info,

        [ColorInfo("light", BackgroundCssClass = "m--bg-light", BorderCssClass = "border-light",
            TextCssClass = "m--font-light")]
        Light,

        [ColorInfo("dark", BackgroundCssClass = "m--bg-dark", BorderCssClass = "border-dark",
            TextCssClass = "m--font-dark")]
        Dark,

        [ColorInfo("white", BackgroundCssClass = "m--bg-white", BorderCssClass = "border-white",
            TextCssClass = "m--font-white")]
        White,

        [ColorInfo("brand", BackgroundCssClass = "m--bg-brand", BorderCssClass = "border-brand",
            TextCssClass = "m--font-brand")]
        Brand,

        [ColorInfo("accent", BackgroundCssClass = "m--bg-accent", BorderCssClass = "border-accent",
            TextCssClass = "m--font-accent")]
        Accent,

        [ColorInfo("focus", BackgroundCssClass = "m--bg-focus", BorderCssClass = "border-focus",
            TextCssClass = "m--font-focus")]
        Focus,

        [ColorInfo("metal", BackgroundCssClass = "m--bg-metal", BorderCssClass = "border-metal",
            TextCssClass = "m--font-metal")]
        Metal,
    }
}