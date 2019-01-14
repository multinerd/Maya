using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Portlet
{
    [HtmlTargetElement("portlet-body", ParentTag = "portlet")]
    [OutputElementHint("div")]
    public class PortletBodyTagHelper : MeconsTagHelperBase
    {
        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("m-portlet__body");
        }
    }
}