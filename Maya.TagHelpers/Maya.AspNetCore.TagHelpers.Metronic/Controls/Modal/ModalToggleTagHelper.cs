using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Modal
{
    [HtmlTargetElement("button", Attributes = "bc-modal-target")]
    public class ModalToggleTagHelper : MeconsTagHelperBase
    {
        private const string ModalTargetAttributeName = "bc-modal-target";

        [HtmlAttributeName("bc-modal-target")]
        public string ModalTarget { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.Attributes.Add("data-target", "#" + this.ModalTarget);
            output.Attributes.Add("data-toggle", "modal");
        }
    }
}