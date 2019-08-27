using System.Text;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Editors
{
    [GenerateId("summernote_", true)]
    [HtmlTargetElement("summernote", TagStructure = TagStructure.WithoutEndTag)]
    public class SummernoteTagHelper : MeconsFormTagHelperBase
    {
        private const string HeightAttributeName = "bc-height";

        [HtmlAttributeName("bc-height")]
        public int Height { get; set; } = 300;

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            if (For != null)
            {
                this.BindProperty(context);
                output.PostElement.AppendHtml("<input type=\"hidden\" id=\"" + Name + "\" name=\"" + Name + "\" />");
            }

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddCssClass("summernote");
            if (For?.Model != null)
                output.Content.AppendHtml(For.Model.ToString());
            this.RenderBinding(output, false, true, true, true, false, true);
            ProcessJavascript(output);
        }

        private void ProcessJavascript(TagHelperOutput output)
        {
            dynamic jObject = new JObject();
            jObject.height = this.Height;
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("$('#{0}').summernote({1});", base.Id, jObject));
            if (base.For != null)
            {
                var fullHtmlFieldName = base.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(base.For.Name);
                stringBuilder.AppendLine(string.Concat(new string[] { "$('#", base.Id, "').on('summernote.change', function(e) { var markup = $('#", base.Id, "').summernote('code'); $('#", fullHtmlFieldName, "').val(markup); });" }));
            }
            output.PostElement.AppendHtml(string.Concat("<script type='text/javascript'>$(function() { ", stringBuilder.ToString(), " });</script>"));
        }
    }
}