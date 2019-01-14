using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Tabs
{
    [OutputElementHint("nav")]
    [GenerateId("tabs_", true)]
    [ContextClass]
    [RestrictChildren("tabs-pane", new string[] { })]
    public class TabsTagHelper : MeconsTagHelperBase
    {
        private const string PillsAttributeName = "bc-pills";

        [HtmlAttributeName("bc-pills")]
        public bool Pills { get; set; }

        [HtmlAttributeNotBound]
        public List<TabsPaneTagHelper> Panes { get; set; } = new List<TabsPaneTagHelper>();

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "nav";
            output.AddCssClass(this.Pills ? "nav nav-pills mb-3" : "nav nav-tabs");
            output.MergeAttribute("role", "tablist");
            if (!this.Panes.Any(p => p.IsActive))
                this.Panes.First().IsActive = true;
            foreach (var pane in this.Panes)
                output.PreContent.AppendHtml(this.BuildTabItem(pane));
            output.PostElement.AppendHtml("<div class=\"tab-content\">");
            foreach (var pane in this.Panes)
                output.PostElement.AppendHtml(this.BuildTabPane(pane));
            output.PostElement.AppendHtml("</div>");
        }

        private IHtmlContent BuildTabItem(TabsPaneTagHelper pane)
        {
            var tagBuilder = new TagBuilder("a");
            tagBuilder.AddCssClass(pane.IsActive ? "nav-item nav-link active" : "nav-item nav-link");
            tagBuilder.GenerateId(pane.Id + "-tab", "-");
            tagBuilder.MergeAttribute("data-toggle", this.Pills ? "pill" : "tab");
            tagBuilder.MergeAttribute("role", "tab");
            tagBuilder.MergeAttribute("href", "#" + pane.Id);
            tagBuilder.MergeAttribute("aria-controls", pane.Id);
            tagBuilder.MergeAttribute("aria-expanded", pane.IsActive ? "true" : "false");
            tagBuilder.InnerHtml.Append(pane.Header);
            return tagBuilder;
        }

        private IHtmlContent BuildTabPane(TabsPaneTagHelper pane)
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass(pane.IsActive ? "tab-pane fade show active" : "tab-pane fade");
            tagBuilder.GenerateId(pane.Id ?? "", "-");
            tagBuilder.MergeAttribute("role", "tabpanel");
            tagBuilder.MergeAttribute("aria-labelledby", pane.Id + "-tab");
            tagBuilder.InnerHtml.AppendHtml(pane.Content.GetContent());
            return tagBuilder;
        }
    }
}