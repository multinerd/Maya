using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [HtmlTargetElement("addon", ParentTag = "input-group")]
    [OutputElementHint("span")]
    public class AddonTagHelper : BreconsTagHelperBase
    {
        private const string TypeAttributeName = "bc-type";

        [HtmlAttributeName("bc-type")]
        public AddonType Type { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "span";
            output.AddCssClass("input-group-" + this.Type.GetEnumInfo().Name);
        }

        public static IHtmlContent Build(string text, AddonType type)
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("input-group-" + type.GetEnumInfo().Name);
            tagBuilder.InnerHtml.AppendHtml("<span class=\"input-group-text\">" + text + "</span>");
            return tagBuilder;
        }

        public static IHtmlContent Build(Icon icon, AddonType type)
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("input-group-" + type.GetEnumInfo().Name);
            tagBuilder.InnerHtml.AppendHtml("<span class=\"input-group-text\"><i class=\"" +
                                            icon.GetIconInfo().CssClass + "\"></i></span>");
            return tagBuilder;
        }
    }
}