using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Controls.Components;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Widgets
{
    [HtmlTargetElement("widget-progress", TagStructure = TagStructure.WithoutEndTag)]
    [OutputElementHint("div")]
    public class ProgressWidgetTagHelper : MeconsTagHelperBase
    {
        private const string TitleAttributeName = "bc-title";
        private const string DescriptionAttributeName = "bc-desc";
        private const string ValueAttributeName = "bc-value";
        private const string ProgressAttributeName = "bc-progress";
        private const string ProgressDescriptionAttributeName = "bc-progress-desc";
        private const string ColorAttributeName = "bc-color";

        [Mandatory]
        [HtmlAttributeName("bc-title")]
        public string Title { get; set; }

        [HtmlAttributeName("bc-desc")]
        public string Description { get; set; }

        [Mandatory]
        [HtmlAttributeName("bc-value")]
        public string Value { get; set; }

        [HtmlAttributeName("bc-progress")]
        public int Progress { get; set; }

        [HtmlAttributeName("bc-progress-desc")]
        public string ProgressDescription { get; set; }

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; } = Color.Primary;

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddCssClass("m-widget24");
            var tagBuilder1 = new TagBuilder("div");
            tagBuilder1.AddCssClass("m-widget24__item");
            var tagBuilder2 = new TagBuilder("h4");
            tagBuilder2.AddCssClass("m-widget24__title");
            tagBuilder2.InnerHtml.Append(this.Title);
            tagBuilder1.InnerHtml.AppendHtml(tagBuilder2);
            tagBuilder1.InnerHtml.AppendHtml("<br />");
            var tagBuilder3 = new TagBuilder("span");
            tagBuilder3.AddCssClass("m-widget24__desc");
            tagBuilder3.InnerHtml.AppendHtml(!string.IsNullOrEmpty(this.Description) ? this.Description : "&nbsp;");
            tagBuilder1.InnerHtml.AppendHtml(tagBuilder3);
            var tagBuilder4 = new TagBuilder("span");
            tagBuilder4.AddCssClass("m-widget24__stats " + this.Color.GetColorInfo().TextCssClass);
            tagBuilder4.InnerHtml.Append(this.Value);
            tagBuilder1.InnerHtml.AppendHtml(tagBuilder4);
            tagBuilder1.InnerHtml.AppendHtml("<div class=\"m--space-10\"></div>");
            var tagBuilder5 = new TagBuilder("div");
            tagBuilder5.AddCssClass("progress m-progress--sm");
            tagBuilder5.InnerHtml.AppendHtml(ProgressTagHelper.Build(this.Progress, Size.Small, false, this.Color,
                false, false));
            tagBuilder1.InnerHtml.AppendHtml(tagBuilder5);
            tagBuilder1.InnerHtml.AppendHtml("<span class=\"m-widget24__change\">" + this.ProgressDescription +
                                             "</span>");
            tagBuilder1.InnerHtml.AppendHtml(string.Format("<span class=\"m-widget24__number\">{0}%</span>",
                this.Progress));
            output.Content.AppendHtml(tagBuilder1);
        }
    }
}