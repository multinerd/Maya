using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Components
{
    [HtmlTargetElement("*", Attributes = "bc-tooltip")]
    public class TooltipTagHelper : MeconsTagHelperBase
    {
        private const string TooltipAttributeName = "bc-tooltip";
        private const string HtmlAttributeName = "bc-tooltip-html";
        private const string PlacementAttributeName = "bc-tooltip-placement";
        private const string AnimationAttributeName = "bc-tooltip-animation";
        private const string DelayAttributeName = "bc-tooltip-delay";

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("bc-tooltip")]
        [CopyToOutput("title")]
        public string Tooltip { get; set; }

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("bc-tooltip-html")]
        public bool IsHtml { get; set; }

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("bc-tooltip-placement")]
        public Placement Placement { get; set; }

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("bc-tooltip-animation")]
        public bool Animation { get; set; } = true;

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("bc-tooltip-delay")]
        public int Delay { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.AddDataAttribute("toggle", "m-tooltip");
            output.AddDataAttribute("container", "body");
            output.AddDataAttribute("placement", this.Placement.GetEnumInfo().Name);
            output.AddDataAttribute("animation", this.Animation ? "true" : "false");
            output.AddDataAttribute("delay", this.Delay.ToString());
            output.AddDataAttribute("html", this.IsHtml ? "true" : "false");
        }
    }
}