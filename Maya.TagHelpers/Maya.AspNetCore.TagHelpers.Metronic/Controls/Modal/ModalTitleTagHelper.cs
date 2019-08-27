using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Modal
{
    [HtmlTargetElement("h1", ParentTag = "modal-header")]
    [HtmlTargetElement("h2", ParentTag = "modal-header")]
    [HtmlTargetElement("h3", ParentTag = "modal-header")]
    [HtmlTargetElement("h4", ParentTag = "modal-header")]
    [HtmlTargetElement("h5", ParentTag = "modal-header")]
    [HtmlTargetElement("h6", ParentTag = "modal-header")]
    public class ModalTitleTagHelper : MeconsTagHelperBase
    {
        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.AddCssClass("modal-title");
        }
    }
}