using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [GenerateId("select_", true)]
    public class SelectTagHelper : MeconsFormTagHelperBase
    {
        protected override void RenderProcess(TagHelperContext context, TagHelperOutput output)
        {
            this.BindProperty(context);
            output.AddCssClass("form-control");
            this.RenderBinding(output, false, true, true, true, true, true);
        }
    }
}