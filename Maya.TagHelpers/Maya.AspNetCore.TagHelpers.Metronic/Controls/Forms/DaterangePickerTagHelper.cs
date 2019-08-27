using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [GenerateId("daterangepicker_", true)]
    [HtmlTargetElement("daterangepicker", TagStructure = TagStructure.WithoutEndTag)]
    [OutputElementHint("input")]
    public class DaterangePickerTagHelper : MeconsFormTagHelperBase
    {
        private const string ApplyColorAttributeName = "bc-apply-color";
        private const string CancelColorAttributeName = "bc-cancel-color";
        private const string IconAttributeName = "bc-icon";
        private const string TimepickerAttributeName = "bc-timepicker";
        private const string TimepickerStepAttributeName = "bc-timepicker-step";
        private const string FormatAttributeName = "bc-format";
        private const string StartDateAttributeName = "bc-start-date";
        private const string EndDateAttributeName = "bc-end-date";
        private const string MinDateAttributeName = "bc-min-date";
        private const string MaxDateAttributeName = "bc-max-date";
        private const string AspFor2AttributeName = "asp-for2";

        [HtmlAttributeName("bc-apply-color")]
        public Color ApplyColor { get; set; } = Color.Primary;

        [HtmlAttributeName("bc-cancel-color")]
        public Color CancelColor { get; set; } = Color.Secondary;

        [HtmlAttributeName("bc-icon")]
        public Icon Icon { get; set; }

        [HtmlAttributeName("bc-timepicker")]
        public bool IsTimepicker { get; set; }

        [HtmlAttributeName("bc-timepicker-step")]
        public int TimepickerStep { get; set; } = 30;

        [HtmlAttributeName("bc-format")]
        public string Format { get; set; }

        [HtmlAttributeName("bc-start-date")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [HtmlAttributeName("bc-end-date")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [HtmlAttributeName("bc-min-date")]
        public DateTime? MinDate { get; set; }

        [HtmlAttributeName("bc-max-date")]
        public DateTime? MaxDate { get; set; }

        [HtmlAttributeName("asp-for2")]
        public ModelExpression For2 { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "input";
            output.Attributes.Add("type", "text");
            output.Attributes.Add("readonly", "readonly");
            output.AddCssClass("form-control m-input");
            this.DataBinding(context, output);
            if (this.Icon != Icon.None)
            {
                output.PreElement.PrependHtml(this.Size != Size.Default
                    ? "<div class=\"input-group m-input-group input-group-" + this.Size.GetEnumInfo().Name +
                      " date\">"
                    : "<div class=\"input-group m-input-group date\">");
                output.PostElement.AppendHtml(AddonTagHelper.Build(this.Icon, AddonType.Append));
                output.PostElement.AppendHtml("</div>");
            }

            this.RenderBinding(output, false, false, true, true, true, true);
            this.ProcessJavascript(context, output);
        }

        private void ProcessJavascript(TagHelperContext context, TagHelperOutput output)
        {
            DateTime value;
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            dynamic jObject = new JObject();
            jObject.buttonClasses = "m-btn btn";
            jObject.applyClass = string.Concat("btn-", this.ApplyColor.GetColorInfo().Name);
            jObject.cancelClass = string.Concat("btn-", this.CancelColor.GetColorInfo().Name);
            if (this.IsTimepicker)
            {
                jObject.timePicker = true;
                jObject.timePickerIncrement = this.TimepickerStep;
            }

            if (this.MinDate.HasValue)
            {
                value = this.MinDate.Value;
                jObject.minDate = value.ToString(currentCulture.DateTimeFormat.ShortDatePattern,
                    CultureInfo.InvariantCulture);
            }

            if (this.MaxDate.HasValue)
            {
                value = this.MaxDate.Value;
                jObject.maxDate = value.ToString(currentCulture.DateTimeFormat.ShortDatePattern,
                    CultureInfo.InvariantCulture);
            }

            jObject.locale = this.DaterangePickerLocalization();
            var stringBuilder = new StringBuilder("<script type='text/javascript'> $(function() {");
            stringBuilder.AppendLine(string.Format("$('#{0}').daterangepicker({1});", base.Id, jObject));
            var id = new string[] {"$('#", base.Id, "').data('daterangepicker').setStartDate('", null, null};
            value = this.StartDate;
            id[3] = value.ToString(this.Format);
            id[4] = "');";
            stringBuilder.AppendLine(string.Concat(id));
            var str = new string[] {"$('#", base.Id, "').data('daterangepicker').setEndDate('", null, null};
            value = this.EndDate;
            str[3] = value.ToString(this.Format);
            str[4] = "');";
            stringBuilder.AppendLine(string.Concat(str));
            var modelExpression = this.GetModelExpression(context);
            if (modelExpression != null)
            {
                stringBuilder.AppendLine(string.Concat(new string[]
                {
                    "$('#", base.Id, "').on('apply.daterangepicker', function(ev, picker) { $('#start_", base.Id,
                    "').val(picker.startDate.format('", this.Format.ConvertNetFormatToMomentJs(), "')); $('#end_",
                    base.Id, "').val(picker.endDate.format('", this.Format.ConvertNetFormatToMomentJs(), "')); });"
                }));
                var dateTime = Convert.ToDateTime(modelExpression.Item1.Model);
                var dateTime1 = Convert.ToDateTime(modelExpression.Item2.Model);
                stringBuilder.AppendLine(string.Concat(new string[]
                {
                    "$('#", base.Id, "').data('daterangepicker').setStartDate('", dateTime.ToString(this.Format), "');"
                }));
                stringBuilder.AppendLine(string.Concat(new string[]
                {
                    "$('#", base.Id, "').data('daterangepicker').setEndDate('", dateTime1.ToString(this.Format), "');"
                }));
            }

            stringBuilder.AppendLine("}); </script>");
            output.PostElement.AppendHtml(stringBuilder.ToString());
        }

        private JObject DaterangePickerLocalization()
        {
            string str;
            dynamic jObject = new JObject();
            jObject.applyLabel = Resources.DaterangePicker_Button_Apply;
            jObject.cancelLabel = Resources.DaterangePicker_Button_Cancel;
            if (string.IsNullOrEmpty(this.Format))
            {
                var obj = jObject;
                str = (this.IsTimepicker
                    ? string.Concat(
                        CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ConvertNetFormatToMomentJs(), " ",
                        CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern.ConvertNetFormatToMomentJs())
                    : CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ConvertNetFormatToMomentJs());
                obj.format = str;
            }
            else
            {
                jObject.format = this.Format.ConvertNetFormatToMomentJs();
            }

            var jArray = new JArray();
            jArray.Add(Resources.Days_Su);
            jArray.Add(Resources.Days_Mo);
            jArray.Add(Resources.Days_Tu);
            jArray.Add(Resources.Days_We);
            jArray.Add(Resources.Days_Th);
            jArray.Add(Resources.Days_Fr);
            jArray.Add(Resources.Days_Sa);
            jObject.daysOfWeek = jArray;
            var jArray1 = new JArray();
            jArray1.Add(Resources.Months_January);
            jArray1.Add(Resources.Months_February);
            jArray1.Add(Resources.Months_March);
            jArray1.Add(Resources.Months_April);
            jArray1.Add(Resources.Months_May);
            jArray1.Add(Resources.Months_June);
            jArray1.Add(Resources.Months_July);
            jArray1.Add(Resources.Months_August);
            jArray1.Add(Resources.Months_September);
            jArray1.Add(Resources.Months_October);
            jArray1.Add(Resources.Months_November);
            jArray1.Add(Resources.Months_December);
            jObject.monthNames = jArray1;
            jObject.firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            return (JObject) jObject;
        }

        public DaterangePickerTagHelper()
        {
        }

        private void DataBinding(TagHelperContext context, TagHelperOutput output)
        {
            var modelExpression = this.GetModelExpression(context);
            if (modelExpression != null)
            {
                var tagBuilder = new TagBuilder("input");
                tagBuilder.TagRenderMode = TagRenderMode.SelfClosing;
                var tagBuilder1 = tagBuilder;
                tagBuilder1.Attributes.Add("type", "hidden");
                tagBuilder1.Attributes.Add("id", string.Concat("start_", base.Id));
                tagBuilder1.Attributes.Add("name",
                    base.ViewContext.ViewData.TemplateInfo
                        .GetFullHtmlFieldName(modelExpression.Item1.Name));
                output.PostElement.AppendHtml(tagBuilder1);
                var tagBuilder2 = new TagBuilder("input");
                tagBuilder2.TagRenderMode = TagRenderMode.SelfClosing;
                var tagBuilder3 = tagBuilder2;
                tagBuilder3.Attributes.Add("type", "hidden");
                tagBuilder3.Attributes.Add("id", string.Concat("end_", base.Id));
                tagBuilder3.Attributes.Add("name",
                    base.ViewContext.ViewData.TemplateInfo
                        .GetFullHtmlFieldName(modelExpression.Item2.Name));
                output.PostElement.AppendHtml(tagBuilder3);
                this.HandleRequired(modelExpression.Item1);
                this.HandleInformation(modelExpression.Item1);
            }
        }

        private Tuple<ModelExpression, ModelExpression> GetModelExpression(TagHelperContext context)
        {
            object value;
            object obj;
            var tagHelperAttribute = context.AllAttributes
                .FirstOrDefault((TagHelperAttribute a) => a.Name == "asp-for");
            if (tagHelperAttribute != null)
            {
                value = tagHelperAttribute.Value;
            }
            else
            {
                value = null;
            }

            var modelExpression = value as ModelExpression;
            var tagHelperAttribute1 = context.AllAttributes
                .FirstOrDefault((TagHelperAttribute a) => a.Name == "asp-for2");
            if (tagHelperAttribute1 != null)
            {
                obj = tagHelperAttribute1.Value;
            }
            else
            {
                obj = null;
            }

            var modelExpression1 = obj as ModelExpression;
            if (modelExpression == null || modelExpression1 == null)
            {
                return null;
            }

            return new Tuple<ModelExpression, ModelExpression>(modelExpression, modelExpression1);
        }
    }
}