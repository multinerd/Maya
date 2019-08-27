using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Timeline
{
    [OutputElementHint("span")]
    [HtmlTargetElement("timeline-item", ParentTag = "timeline")]
    public class TimelineItemTagHelper : MeconsTagHelperBase
    {
        private const string TimeAttributeName = "bc-time";
        private const string ColorAttributeName = "bc-color";
        private const string IconAttributeName = "bc-icon";

        [Mandatory]
        [HtmlAttributeName("bc-time")]
        public string Time { get; set; }

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; }

        [HtmlAttributeName("bc-icon")]
        public Icon Icon { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "span";
            output.AddCssClass("m-list-timeline__text");
            output.PreElement.AppendHtml(this.BuildBadge());
            if (this.Icon != Icon.None)
                output.PreElement.AppendHtml(this.BuildIcon());
            output.PostElement.AppendHtml(this.BuildTime());
            output.WrapHtmlOutside("<div class=\"m-list-timeline__item\">", "</div>");
        }

        private IHtmlContent BuildBadge()
        {
            var tagBuilder = new TagBuilder("span");
            tagBuilder.AddCssClass("m-list-timeline__badge");
            if (this.Color != Color.None)
                tagBuilder.AddCssClass("m-list-timeline__badge--" + this.Color.GetEnumInfo().Name);
            return tagBuilder;
        }

        private IHtmlContent BuildIcon()
        {
            var tagBuilder = new TagBuilder("span");
            tagBuilder.AddCssClass("m-list-timeline__icon");
            tagBuilder.AddCssClass(this.Icon.GetIconInfo().CssClass);
            return tagBuilder;
        }

        private IHtmlContent BuildTime()
        {
            var tagBuilder = new TagBuilder("span");
            tagBuilder.AddCssClass("m-list-timeline__time");
            tagBuilder.InnerHtml.Append(this.Time);
            return tagBuilder;
        }
    }
}