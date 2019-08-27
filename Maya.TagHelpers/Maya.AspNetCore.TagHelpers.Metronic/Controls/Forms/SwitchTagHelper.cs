using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [HtmlTargetElement("switch")]
    [ContextClass]
    [GenerateId("switch_", true)]
    [OutputElementHint("input")]
    public class SwitchTagHelper : MeconsFormTagHelperBase
    {
        private const string ColorAttributeName = "bc-color";
        private const string IconAttributeName = "bc-icon";
        private const string OutlineAttributeName = "bc-outline";

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; }

        [HtmlAttributeName("bc-icon")]
        public bool Icon { get; set; }

        [HtmlAttributeName("bc-outline")]
        public bool Outline { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            this.BindProperty(context);
            output.TagName = "input";
            output.Attributes.Add("type", "checkbox");
            output.PostElement.AppendHtml("<span></span>");
            output.WrapHtmlOutside("<label>", "</label>");
            output.WrapOutside(this.BuildWrapper());
            output.AddCssStyle("display", "block");
            this.RenderBinding(output, false, true, true, true, false, true);
            var modelExpression = this.GetModelExpression(context);
            if (!(modelExpression?.Model is bool) || !(bool) modelExpression.Model)
                return;
            output.Attributes.Add("checked", "checked)");
        }

        private TagBuilder BuildWrapper()
        {
            var tagBuilder = new TagBuilder("span");
            tagBuilder.AddCssClass("m-switch");
            tagBuilder.Attributes.Add("style", "display: block;");
            if (this.Icon)
                tagBuilder.AddCssClass("m-switch--icon");
            if (this.Size != Size.Default)
                tagBuilder.AddCssClass("m-switch--" + this.Size.GetEnumInfo().Name);
            if (this.Color != Color.None)
                tagBuilder.AddCssClass("m-switch--" + this.Color.GetColorInfo().Name);
            if (this.Outline)
                tagBuilder.AddCssClass("m-switch--outline");
            return tagBuilder;
        }
    }
}