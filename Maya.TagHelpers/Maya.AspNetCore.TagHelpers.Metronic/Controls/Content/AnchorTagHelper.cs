using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Content
{
    [OutputElementHint("span")]
    [HtmlTargetElement("a", Attributes = "bc-link")]
    public class AnchorTagHelper : MeconsTagHelperBase
    {
        private const string LinkAttributeName = "bc-link";
        private const string UppercaseAttributeName = "bc-uppercase";
        private const string WeightAttributeName = "bc-weight";
        private const string ColorAttributeName = "bc-color";

        [HtmlAttributeName("bc-link")]
        public bool IsAnimated { get; set; }

        [HtmlAttributeName("bc-uppercase")]
        public bool IsUppercase { get; set; }

        [HtmlAttributeName("bc-weight")]
        public FontWeight Weight { get; set; }

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "a";
            if (this.IsAnimated)
                output.AddCssClass("m-link");
            if (this.IsUppercase)
                output.AddCssClass("m--font-transform-u");
            if (this.Weight != FontWeight.Normal)
                output.AddCssClass("m--font-" + this.Weight.GetEnumInfo().Name);
            if (this.Color == Color.None)
                return;
            output.AddCssClass("m-link--" + this.Color.GetEnumInfo().Name);
        }
    }
}