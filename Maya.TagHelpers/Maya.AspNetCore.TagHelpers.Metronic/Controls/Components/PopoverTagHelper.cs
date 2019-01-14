using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Components
{
    [HtmlTargetElement("*", Attributes = "bc-popover")]
    public class PopoverTagHelper : MeconsTagHelperBase
    {
        private const string PopoverAttributeName = "bc-popover";
        private const string TitleAttributeName = "bc-popover-title";
        private const string DismissibleAttributeName = "bc-popover-dismissible";
        private const string DelayAttributeName = "bc-popover-delay";
        private const string PlacementAttributeName = "bc-popover-placement";
        private const string HtmlAttributeName = "bc-popover-html";

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("bc-popover-title")]
        public string Title { get; set; }

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("bc-popover")]
        [CopyToOutput("data-content")]
        public string Content { get; set; }

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("bc-popover-placement")]
        public Placement Placement { get; set; }

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("bc-popover-dismissible")]
        public bool Dismissible { get; set; }

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("bc-popover-delay")]
        [CopyToOutput("data-delay")]
        public int Delay { get; set; }

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("bc-popover-html")]
        public bool IsHtml { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.AddDataAttribute("toggle", "m-popover");
            output.AddDataAttribute("container", "body");
            output.AddDataAttribute("placement", this.Placement.GetEnumInfo().Name);
            output.AddDataAttribute("html", this.IsHtml ? "true" : "false");
            if (!string.IsNullOrEmpty(this.Title))
                output.MergeAttribute("title", this.Title);
            if (!this.Dismissible)
                return;
            output.AddDataAttribute("trigger", "focus");
        }
    }
}