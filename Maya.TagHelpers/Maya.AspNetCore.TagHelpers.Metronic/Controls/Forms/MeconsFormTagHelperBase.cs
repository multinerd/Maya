using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Controls;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    public abstract class MeconsFormTagHelperBase : FormTagHelperBase
    {
        [Context]
        [HtmlAttributeNotBound]
        public FormTagHelper FormContext { get; set; }

        [Context]
        [HtmlAttributeNotBound]
        public FormGroupTagHelper FormGroupContext { get; set; }

        public override void Init(TagHelperContext context)
        {
            base.Init(context);
            if (this.FormContext == null || !this.FormContext.Validation)
                return;
            this.Validation = this.FormContext.Validation;
        }
    }
}