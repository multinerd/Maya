using System;
using System.Globalization;
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
    [GenerateId("timepicker_", true)]
    [HtmlTargetElement("timepicker", TagStructure = TagStructure.WithoutEndTag)]
    [OutputElementHint("input")]
    public class TimePickerTagHelper : MeconsFormTagHelperBase
    {
        private const string MinuteStepAttributeName = "bc-minute-step";

        private const string SecondStepAttributeName = "bc-second-step";

        private const string SnapToStepAttributeName = "bc-snap-to-step";

        private const string SecondsAttributeName = "bc-seconds";

        private const string MeridianAttributeName = "bc-meridian";

        private const string InputsAttributeName = "bc-inputs";

        private const string IconAttributeName = "bc-icon";

        private const string DisableFocusAttributeName = "bc-disable-focus";

        [HtmlAttributeName("bc-disable-focus")]
        public bool DisableFocus
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-icon")]
        public Icon Icon
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-inputs")]
        public bool Inputs { get; set; } = true;

        [HtmlAttributeName("bc-meridian")]
        public bool Meridian { get; set; } = true;

        [HtmlAttributeName("bc-minute-step")]
        public int MinuteStep { get; set; } = 15;

        [CopyToOutput]
        public string Placeholder
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-seconds")]
        public bool Seconds
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-second-step")]
        public int SecondStep { get; set; } = 15;

        [HtmlAttributeName("bc-snap-to-step")]
        public bool SnapToStep
        {
            get;
            set;
        }

        [CopyToOutput]
        public object Value
        {
            get;
            set;
        }

        public TimePickerTagHelper()
        {
        }

        private void ProcessJavascript(TagHelperOutput output)
        {
            dynamic jObjects = new JObject();
            jObjects.minuteStep = this.MinuteStep;
            jObjects.secondStep = this.SecondStep;
            jObjects.snapToStep = this.SnapToStep;
            jObjects.showSeconds = this.Seconds;
            jObjects.showMeridian = this.Meridian;
            jObjects.showInputs = this.Inputs;
            jObjects.disableFocus = this.DisableFocus;
            string str = string.Format("<script type='text/javascript'>$(function() {{ var tp = $('#{0}'); tp.timepicker({1}); }});</script>", base.Id, jObjects);
            output.PostElement.AppendHtml(str);
        }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            object str;
            string str1;
            this.BindProperty(context);
            var timePickerTagHelper = this;
            var modelValue = this.GetModelValue(context);
            if (modelValue == null)
            {
                modelValue = this.Value;
            }
            timePickerTagHelper.Value = modelValue;
            output.TagName = "input";
            output.Attributes.Add("type", "text");
            output.Attributes.Add("readonly", "readonly");
            output.AddCssClass("form-control m-input");
            if (this.Icon != Icon.None)
            {
                var preElement = output.PreElement;
                str1 = (base.Size != Size.Default ? string.Concat("<div class=\"input-group m-input-group input-group-", base.Size.GetEnumInfo().Name, " timepicker\">") : "<div class=\"input-group m-input-group timepicker\">");
                preElement.PrependHtml(str1);
                output.PostElement.AppendHtml(AddonTagHelper.Build(this.Icon, AddonType.Append));
                output.PostElement.AppendHtml("</div>");
            }
            this.RenderBinding(output, true, false, true, true, true, true);
            if (this.Value != null)
            {
                var tagHelperOutput = output;
                if (this.Value.GetType() == typeof(DateTime))
                {
                    var value = (DateTime)this.Value;
                    str = value.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);
                }
                else
                {
                    str = this.Value;
                }
                tagHelperOutput.MergeAttribute("value", str);
            }
            this.ProcessJavascript(output);
        }
    }
}