using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Buttons
{
    [HtmlTargetElement("divider", ParentTag = "button-dropdown", TagStructure = TagStructure.WithoutEndTag)]
    public class ButtonDropdownDividerTagHelper : MeconsTagHelperBase
    {
        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddCssClass("dropdown-divider");
        }
    }
}