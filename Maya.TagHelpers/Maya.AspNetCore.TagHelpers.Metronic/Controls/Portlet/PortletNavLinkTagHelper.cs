using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Portlet
{
    [HtmlTargetElement("portlet-link", ParentTag = "portlet-nav", TagStructure = TagStructure.WithoutEndTag)]
    [OutputElementHint("a")]
    public class PortletNavLinkTagHelper : MeconsTagHelperBase
    {
        private const string IconAttributeName = "bc-icon";
        private const string TextAttributeName = "bc-text";
        private const string ColorAttributeName = "bc-color";

        [CopyToOutput]
        public string Href { get; set; } = "#";

        [HtmlAttributeName("bc-icon")]
        public Icon Icon { get; set; }

        [HtmlAttributeName("bc-text")]
        public string Text { get; set; }

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; }

        [Context]
        public PortletNavTagHelper NavigationContext { get; set; }

        public override void Init(TagHelperContext context)
        {
            base.Init(context);
            this.NavigationContext.Items.Add(this);
        }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            if (this.Icon == Icon.None && string.IsNullOrEmpty(this.Text))
                return;
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddCssClass("m-portlet__nav-link");
            output.AddCssClass(this.Color != Color.None
                ? "btn btn-" + this.Color.GetColorInfo().Name + " m-btn m-btn--pill"
                : "btn btn-secondary m-btn m-btn--pill");
            if (!string.IsNullOrEmpty(this.Text))
            {
                if (this.Icon != Icon.None)
                {
                    output.AddCssClass("m-btn--icon");
                    var tagBuilder = new TagBuilder("span");
                    tagBuilder.InnerHtml.AppendHtml("<i class=\"" + this.Icon.GetIconInfo().CssClass + "\"></i>");
                    tagBuilder.InnerHtml.AppendHtml("<span>" + this.Text + "</span>");
                    output.Content.AppendHtml(tagBuilder);
                }
                else
                    output.Content.Append(this.Text);
            }
            else if (this.Icon != Icon.None)
            {
                output.AddCssClass("m-btn--icon m-btn--icon-only");
                output.Content.AppendHtml("<i class=\"" + this.Icon.GetIconInfo().CssClass + "\"></i>");
            }

            output.WrapHtmlOutside("<li class=\"m-portlet__nav-item\">", "</li>");
        }
    }
}