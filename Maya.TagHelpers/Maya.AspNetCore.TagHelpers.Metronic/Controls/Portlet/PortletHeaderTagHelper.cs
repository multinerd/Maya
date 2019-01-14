using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Portlet
{
    [ContextClass]
    [HtmlTargetElement("portlet-header", ParentTag = "portlet")]
    [RestrictChildren("portlet-nav", new string[] { })]
    [OutputElementHint("div")]
    public class PortletHeaderTagHelper : MeconsTagHelperBase
    {
        private const string TitleAttributeName = "bc-title";
        private const string SubtitleAttributeName = "bc-subtitle";
        private const string IconAttributeName = "bc-icon";
        private const string ColorAttributeName = "bc-color";

        [HtmlAttributeName("bc-title")]
        public string Title { get; set; }

        [HtmlAttributeName("bc-subtitle")]
        public string Subtitle { get; set; }

        [HtmlAttributeName("bc-icon")]
        public Icon Icon { get; set; }

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; } = Color.Dark;

        [HtmlAttributeNotBound]
        public PortletNavTagHelper NavigationContext { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("m-portlet__head");
            this.BuildCaption(output);
            this.BuildNavigation(output);
        }

        private void BuildCaption(TagHelperOutput output)
        {
            var tagBuilder1 = new TagBuilder("div");
            tagBuilder1.AddCssClass("m-portlet__head-title");
            if (this.Icon != Icon.None)
                tagBuilder1.InnerHtml.AppendHtml("<span class=\"m-portlet__head-icon\"><i class=\"" +
                                                 this.Icon.GetIconInfo().CssClass + " " +
                                                 this.Color.GetColorInfo().TextCssClass + "\"></i></span>");
            var innerHtml = tagBuilder1.InnerHtml;
            string encoded;
            if (string.IsNullOrWhiteSpace(this.Subtitle))
                encoded = "<h3 class=\"m-portlet__head-text " + this.Color.GetColorInfo().TextCssClass + "\">" +
                          this.Title + "</h3>";
            else
                encoded = "<h3 class=\"m-portlet__head-text " + this.Color.GetColorInfo().TextCssClass + "\">" +
                          this.Title + " <small>" + this.Subtitle + "</small></h3>";
            innerHtml.AppendHtml(encoded);
            var tagBuilder2 = new TagBuilder("div");
            tagBuilder2.AddCssClass("m-portlet__head-caption");
            tagBuilder2.InnerHtml.AppendHtml(tagBuilder1);
            output.Content.SetHtmlContent(tagBuilder2);
        }

        private void BuildNavigation(TagHelperOutput output)
        {
            if (this.NavigationContext == null)
                return;
            output.Content.AppendHtml(this.NavigationContext.Build());
        }
    }
}