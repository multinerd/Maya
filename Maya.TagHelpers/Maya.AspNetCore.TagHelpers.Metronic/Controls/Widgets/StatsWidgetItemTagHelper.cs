using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Widgets
{
    [HtmlTargetElement("widget-stats-item", ParentTag = "widget-stats", TagStructure = TagStructure.WithoutEndTag)]
    [OutputElementHint("div")]
    public class StatsWidgetItemTagHelper : MeconsTagHelperBase
    {
        private const string TitleAttributeName = "bc-title";
        private const string DescriptionAttributeName = "bc-desc";
        private const string ValueAttributeName = "bc-value";
        private const string ColorAttributeName = "bc-color";

        [Mandatory]
        [HtmlAttributeName("bc-title")]
        public string Title { get; set; }

        [HtmlAttributeName("bc-desc")]
        public string Description { get; set; }

        [Mandatory]
        [HtmlAttributeName("bc-value")]
        public string Value { get; set; }

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; } = Color.Primary;

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("m-widget1__item");
            output.TagMode = TagMode.StartTagAndEndTag;
            var tagBuilder1 = new TagBuilder("h3");
            tagBuilder1.AddCssClass("m-widget1__title");
            tagBuilder1.InnerHtml.Append(this.Title);
            var tagBuilder2 = new TagBuilder("span");
            tagBuilder2.AddCssClass("m-widget1__desc");
            tagBuilder2.InnerHtml.AppendHtml(!string.IsNullOrEmpty(this.Description) ? this.Description : "&nbsp;");
            var tagBuilder3 = new TagBuilder("span");
            tagBuilder3.AddCssClass("m-widget1__number " + this.Color.GetColorInfo().TextCssClass);
            tagBuilder3.InnerHtml.Append(this.Value);
            var tagBuilder4 = new TagBuilder("div");
            tagBuilder4.AddCssClass("col");
            tagBuilder4.InnerHtml.AppendHtml(tagBuilder1);
            tagBuilder4.InnerHtml.AppendHtml(tagBuilder2);
            var tagBuilder5 = new TagBuilder("div");
            tagBuilder5.AddCssClass("col m--align-right");
            tagBuilder5.InnerHtml.AppendHtml(tagBuilder3);
            var tagBuilder6 = new TagBuilder("div");
            tagBuilder6.AddCssClass("row m-row--no-padding align-items-center");
            tagBuilder6.InnerHtml.AppendHtml(tagBuilder4);
            tagBuilder6.InnerHtml.AppendHtml(tagBuilder5);
            output.Content.SetHtmlContent(tagBuilder6);
        }
    }
}