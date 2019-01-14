using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Modal
{
    [HtmlTargetElement("modal-body", ParentTag = "modal")]
    public class ModalBodyTagHelper : MeconsTagHelperBase
    {
        private const string HeightAttributeName = "bc-height";

        [HtmlAttributeName("bc-height")]
        public int Height { get; set; }

        [HtmlAttributeNotBound]
        [Context]
        public ModalTagHelper ModalContext { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("modal-body");
            if (this.Height <= 0)
                return;
            output.WrapHtmlContentOutside(
                string.Format(
                    "<div class=\"m-scrollable\" data-scrollbar-shown=\"true\" data-scrollable=\"true\" data-height=\"{0}\">",
                    this.Height), "</div>");
        }
    }
}