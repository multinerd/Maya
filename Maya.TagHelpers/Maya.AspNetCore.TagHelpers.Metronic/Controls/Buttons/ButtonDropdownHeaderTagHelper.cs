using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Buttons
{
    [OutputElementHint("h6")]
    [HtmlTargetElement("header", ParentTag = "button-dropdown")]
    public class ButtonDropdownHeaderTagHelper : MeconsTagHelperBase
    {
        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "h6";
            output.AddCssClass("dropdown-header");
        }
    }
}