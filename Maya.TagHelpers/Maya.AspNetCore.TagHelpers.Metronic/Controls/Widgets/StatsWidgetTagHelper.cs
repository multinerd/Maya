using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Widgets
{
    [HtmlTargetElement("widget-stats")]
    [OutputElementHint("div")]
    [RestrictChildren("widget-stats-item", new string[] { })]
    public class StatsWidgetTagHelper : MeconsTagHelperBase
    {
        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("m-widget1");
        }
    }
}