using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Buttons
{
    [OutputElementHint("button")]
    [RestrictChildren("a", new string[] {"header", "divider"})]
    [HtmlTargetElement("button-dropdown")]
    [GenerateId("dropdown_", false)]
    public class ButtonDropdownTagHelper : MeconsTagHelperBase
    {
        private const string TitleAttributeName = "bc-title";
        private const string DropupAttributeName = "bc-dropup";
        private const string ColorAttributeName = "bc-color";
        private const string RightAlignmentAttributeName = "bc-alignment-right";
        private const string SizeAttributeName = "bc-size";
        private const string SplitAttributeName = "bc-split";

        [HtmlAttributeName("bc-dropup")]
        public bool IsDropup { get; set; }

        [Mandatory]
        [HtmlAttributeName("bc-title")]
        public string Title { get; set; }

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; } = Color.Primary;

        [HtmlAttributeName("bc-alignment-right")]
        public bool RightAlignment { get; set; }

        [HtmlAttributeName("bc-size")]
        public Size Size { get; set; }

        [HtmlAttributeName("bc-split")]
        public bool IsSplit { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContentAsync = await output.GetChildContentAsync();
            output.TagName = "button";
            output.AddCssClass("btn");
            if (this.Color != Color.None)
                output.AddCssClass("btn-" + this.Color.GetColorInfo().Name);
            if (this.Size != Size.Default)
                output.AddCssClass("btn-" + this.Size.GetEnumInfo().Name);
            output.Content.Append(this.Title);
            if (this.IsSplit)
            {
                output.PostElement.AppendHtml(this.DropdownButtonBuilder());
            }
            else
            {
                output.AddCssClass("dropdown-toggle");
                output.MergeAttribute("type", "button");
                output.MergeAttribute("id", this.Id);
                output.MergeAttribute("data-toggle", "dropdown");
                output.MergeAttribute("aria-haspopup", "true");
                output.MergeAttribute("aria-expanded", "false");
            }

            output.PostElement.AppendHtml("<div class=\"" +
                                          (this.RightAlignment
                                              ? "dropdown-menu dropdown-menu-right"
                                              : "dropdown-menu") + "\" aria-labelledby=\"" + this.Id +
                                          "\">");
            output.PostElement.AppendHtml(childContentAsync);
            output.PostElement.AppendHtml("</div>");
            output.WrapHtmlOutside(
                "<div class=\"" + (this.IsDropup ? "btn-group dropup" : "btn-group") + "\">", "</div>");
        }

        private TagBuilder DropdownButtonBuilder()
        {
            var tagBuilder = new TagBuilder("button");
            tagBuilder.AddCssClass("btn dropdown-toggle");
            tagBuilder.MergeAttribute("type", "button");
            tagBuilder.MergeAttribute("id", this.Id);
            tagBuilder.MergeAttribute("data-toggle", "dropdown");
            tagBuilder.MergeAttribute("aria-haspopup", "true");
            tagBuilder.MergeAttribute("aria-expanded", "false");
            if (this.Color != Color.None)
                tagBuilder.AddCssClass("btn-" + this.Color.GetColorInfo().Name);
            if (this.Size != Size.Default)
                tagBuilder.AddCssClass("btn-" + this.Size.GetEnumInfo().Name);
            tagBuilder.InnerHtml.AppendHtml("<span class=\"sr-only\">" + this.Title + "</span>");
            return tagBuilder;
        }
    }
}