using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Modal
{
    [HtmlTargetElement("modal-footer", ParentTag = "modal")]
    [OutputElementHint("div")]
    public class ModalFooterTagHelper : MeconsTagHelperBase
    {
        [HtmlAttributeNotBound]
        [Context]
        public ModalTagHelper ModalContext { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("modal-footer");
        }
    }
}