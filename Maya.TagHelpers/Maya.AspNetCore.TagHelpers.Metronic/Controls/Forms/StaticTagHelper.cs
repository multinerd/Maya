using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [GenerateId("static_", true)]
    public class StaticTagHelper : MeconsFormTagHelperBase
    {
        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            var modelExpression = this.GetModelExpression(context);
            if (modelExpression != null)
            {
                this.HandleInformation(modelExpression);
                output.Content.SetContent(modelExpression.Model.ToString());
            }

            output.TagName = "p";
            output.AddCssClass("form-control-static mb-0");
            this.RenderBinding(output, false, false, true, false, false, true);
        }
    }
}