using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Modal
{
    [HtmlTargetElement("modal-header", ParentTag = "modal")]
    [OutputElementHint("div")]
    public class ModalHeaderTagHelper : MeconsTagHelperBase
    {
        private const string DisableCloseIconAttributeName = "bc-disable-close-icon";

        [HtmlAttributeName("bc-disable-close-icon")]
        public bool DisableCloseIcon { get; set; }

        [HtmlAttributeNotBound]
        [Context]
        public ModalTagHelper ModalContext { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddCssClass("modal-header");
            if (!this.DisableCloseIcon)
                output.PostContent.AppendHtml(
                    "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>");
            var childContentAsync = await output.GetChildContentAsync();
            if (this.ModalContext != null && !string.IsNullOrEmpty(this.ModalContext.Title))
                output.Content.AppendHtml("<h4 class=\"modal-title\">" + childContentAsync.GetContent() + "</h4>");
            else
                output.Content.AppendHtml(childContentAsync.GetContent());
            this.ModalContext.HeaderHtml = output.ToTagHelperContent().GetContent();
            output.SuppressOutput();
        }
    }
}