using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [ContextClass]
    [GenerateId("touchspin_", true)]
    [HtmlTargetElement("touchspin")]
    [OutputElementHint("input")]
    public class TouchspinTagHelper : MeconsFormTagHelperBase
    {
        private const string MinimumAttributeName = "bc-min";

        private const string MaximumAttributeName = "bc-max";

        private const string StepAttributeName = "bc-step";

        private const string DecimalsAttributeName = "bc-decimals";

        private const string PrefixAttributeName = "bc-prefix";

        private const string PostfixAttributeName = "bc-postfix";

        private const string DownColorAttributeName = "bc-color-down";

        private const string UpColorAttributeName = "bc-color-up";

        private const string OrientationAttributeName = "bc-orientation";

        [HtmlAttributeName("bc-decimals")]
        public int Decimals
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-color-down")]
        public Color DownColor { get; set; } = Color.Secondary;

        [HtmlAttributeName("bc-max")]
        public int Maximum { get; set; } = 100;

        [HtmlAttributeName("bc-min")]
        public int Minimum
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-orientation")]
        public Orientation Orientation
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-postfix")]
        public string Postfix
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-prefix")]
        public string Prefix
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-step")]
        public double Step { get; set; } = 1;

        [HtmlAttributeName("bc-color-up")]
        public Color UpColor { get; set; } = Color.Secondary;

        [CopyToOutput]
        public double? Value
        {
            get;
            set;
        }

        public TouchspinTagHelper()
        {
        }

        private void ProcessJavascript(TagHelperOutput output)
        {
            dynamic jObjects = new JObject();
            jObjects.buttondown_class = string.Concat("btn btn-", this.DownColor.GetColorInfo().Name);
            jObjects.buttonup_class = string.Concat("btn btn-", this.UpColor.GetColorInfo().Name);
            jObjects.min = this.Minimum;
            jObjects.max = this.Maximum;
            jObjects.step = this.Step;
            jObjects.decimals = this.Decimals;
            if (!string.IsNullOrEmpty(this.Prefix))
            {
                jObjects.prefix = this.Prefix;
            }
            if (!string.IsNullOrEmpty(this.Postfix))
            {
                jObjects.postfix = this.Postfix;
            }
            if (this.Orientation == Orientation.Vertical)
            {
                jObjects.verticalbuttons = true;
                jObjects.verticalupclass = "la la-plus";
                jObjects.verticaldownclass = "la la-minus";
            }
            string str = string.Format("<script type='text/javascript'>$(function() {{ $('#{0}').TouchSpin({1}); }});</script>", base.Id, jObjects);
            output.PostElement.AppendHtml(str);
        }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            this.BindProperty(context);
            output.TagName = "input";
            output.Attributes.Add("type", "text");
            output.AddCssClass("form-control");
            this.RenderBinding(output, true, true, true, true, true, true);
            this.ProcessJavascript(output);
        }
    }
}