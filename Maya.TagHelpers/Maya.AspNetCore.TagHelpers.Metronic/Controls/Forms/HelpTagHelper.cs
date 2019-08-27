using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [OutputElementHint("span")]
    [HtmlTargetElement("help", ParentTag = "form-group")]
    public class HelpTagHelper : MeconsTagHelperBase
    {
        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "span";
            output.AddCssClass("m-form__help");
        }

        public static IHtmlContent Build(string content, string helpId)
        {
            var tagBuilder = new TagBuilder("span");
            tagBuilder.GenerateId(helpId, "-");
            tagBuilder.AddCssClass("m-form__help");
            tagBuilder.InnerHtml.Append(content);
            return tagBuilder;
        }
    }
}