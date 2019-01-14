using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Buttons
{
    [HtmlTargetElement("a", Attributes = "bc-button")]
    [HtmlTargetElement("button")]
    public class ButtonTagHelper : MeconsTagHelperBase
    {
        private const string ButtonAttributeName = "bc-button";
        private const string StyleAttributeName = "bc-style";
        private const string ColorAttributeName = "bc-color";
        private const string WideAttributeName = "bc-wide";
        private const string OutlineAttributeName = "bc-outline";
        private const string SizeAttributeName = "bc-size";
        private const string BlockAttributeName = "bc-block";
        private const string ActiveAttributeName = "bc-active";
        private const string DisableAttributeName = "bc-disable";
        private const string IconAttributeName = "bc-icon";

        [HtmlAttributeName("bc-button")]
        public bool Button { get; set; }

        [HtmlAttributeName("bc-style")]
        public ButtonStyle Style { get; set; }

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; } = Color.Primary;

        [HtmlAttributeName("bc-wide")]
        public bool IsWide { get; set; }

        [HtmlAttributeName("bc-outline")]
        public bool IsOutline { get; set; }

        [HtmlAttributeName("bc-size")]
        public Size Size { get; set; }

        [HtmlAttributeName("bc-block")]
        public bool IsBlock { get; set; }

        [HtmlAttributeName("bc-active")]
        public bool IsActive { get; set; }

        [HtmlAttributeName("bc-disable")]
        public bool IsDisabled { get; set; }

        [HtmlAttributeName("bc-icon")]
        public Icon Icon { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.AddCssClass("btn");
            if (output.TagName == "a")
                output.MergeAttribute("role", "button");
            
            if (this.Style != ButtonStyle.Default)
                output.AddCssClass("m-btn--" + this.Style.GetEnumInfo().Name);
            
            if (this.Color != Color.None)
                output.AddCssClass(this.IsOutline
                    ? "btn-outline-" + this.Color.GetColorInfo().Name
                    : "btn-" + this.Color.GetColorInfo().Name);
            
            if (this.IsWide)
                output.AddCssClass("m-btn--wide");
            
            if (this.Size != Size.Default)
                output.AddCssClass("btn-" + this.Size.GetEnumInfo().Name);
            
            if (this.IsBlock)
                output.AddCssClass("btn-block");
            
            if (this.IsActive)
                output.AddCssClass("active");
            
            if (this.IsDisabled)
            {
                if (output.TagName == "a")
                {
                    output.AddCssClass("disabled");
                    output.AddAriaAttribute("disabled", "true");
                }
                else
                    output.MergeAttribute("disabled", "disabled");
            }

            if (this.Icon != Icon.None)
            {
                output.AddCssClass("m-btn--icon");
                var childContentAsync = await output.GetChildContentAsync();
                if (!string.IsNullOrEmpty(childContentAsync.GetContent()))
                {
                    var tagBuilder = new TagBuilder("span");
                    tagBuilder.InnerHtml.AppendHtml("<i class=\"" + this.Icon.GetIconInfo().CssClass + "\"></i>");
                    tagBuilder.InnerHtml.AppendHtml("<span>" + childContentAsync.GetContent() + "</span>");
                    output.Content.SetHtmlContent(tagBuilder);
                }
                else
                {
                    output.AddCssClass("m-btn--icon-only");
                    output.Content.SetHtmlContent("<i class=\"" + this.Icon.GetIconInfo().CssClass + "\"></i>");
                }
            }
        }
    }
}