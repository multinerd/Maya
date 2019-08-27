using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Editors
{
    [GenerateId("markdown_", true)]
    [HtmlTargetElement("markdown", TagStructure = TagStructure.WithoutEndTag)]
    public class MarkdownTagHelper : MeconsFormTagHelperBase
    {
        private const string HeightAttributeName = "bc-height";
        private const string AutoFocusAttributeName = "bc-auto-focus";

        [HtmlAttributeName("bc-height")]
        public int Height { get; set; } = 300;

        [HtmlAttributeName("bc-auto-focus")]
        public bool AutoFocus { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "textarea";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddCssClass("form-control");
            output.AddDataAttribute("provide", "markdown");
            output.AddDataAttribute("height", this.Height);
            this.RenderBinding(output, true, true, true, true, false, true);
            if (this.For?.Model != null)
                output.Content.AppendHtml(this.For.Model.ToString());
            this.ProcessJavascript(output);
        }

        private void ProcessJavascript(TagHelperOutput output)
        {
            dynamic jObject = new JObject();
            jObject.autofocus = this.AutoFocus;
            output.PostElement.AppendHtml(string.Format("<script type='text/javascript'>$(function() {{ $('#{0}').markdown({1}); }});</script>", base.Id, jObject));
        }
    }
}