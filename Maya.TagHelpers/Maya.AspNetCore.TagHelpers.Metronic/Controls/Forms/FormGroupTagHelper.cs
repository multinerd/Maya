using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [OutputElementHint("div")]
    [HtmlTargetElement("form-group")]
    [ContextClass]
    public class FormGroupTagHelper : MeconsTagHelperBase
    {
        private const string StateAttributeName = "bc-state";

        [HtmlAttributeName("bc-state")]
        public ValidationState State { get; set; }

        [Context]
        [HtmlAttributeNotBound]
        public FormTagHelper FormContext { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("form-group m-form__group");
            if (this.State != ValidationState.None)
                output.AddCssClass("has-" + this.State.GetEnumInfo().Name);
            if (this.FormContext == null || !this.FormContext.IsHorizontal)
                return;
            output.AddCssClass("row");
        }
    }
}