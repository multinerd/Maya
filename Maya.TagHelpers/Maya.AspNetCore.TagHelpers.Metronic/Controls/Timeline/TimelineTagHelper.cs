using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Timeline
{
    [OutputElementHint("div")]
    [RestrictChildren("timeline-item", new string[] { })]
    public class TimelineTagHelper : MeconsTagHelperBase
    {
        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("m-list-timeline");
            output.WrapHtmlContentOutside("<div class=\"m-list-timeline__items\">", "</div>");
        }
    }
}