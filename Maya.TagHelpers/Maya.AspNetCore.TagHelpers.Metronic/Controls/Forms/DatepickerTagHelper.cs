using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Localization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [GenerateId("datepicker_", true)]
    [HtmlTargetElement("datepicker", TagStructure = TagStructure.WithoutEndTag)]
    [OutputElementHint("input")]
    public class DatepickerTagHelper : MeconsFormTagHelperBase
    {
        private const string OrientationAttributeName = "bc-orientation";

        private const string IconAttributeName = "bc-icon";

        private const string CloseAttributeName = "bc-close";

        private const string WeeksAttributeName = "bc-weeks";

        private const string FormatAttributeName = "bc-format";

        private const string StartDateAttributeName = "bc-start-date";

        private const string EndDateAttributeName = "bc-end-date";

        [HtmlAttributeName("bc-end-date")]
        public string EndDate
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-format")]
        public string Format
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

        [HtmlAttributeName("bc-close")]
        public bool IsCloseEnabled
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-weeks")]
        public bool IsWeeksEnabled
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-orientation")]
        public DatepickerOrientation Orientation
        {
            get;
            set;
        }

        [CopyToOutput]
        public string Placeholder
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-start-date")]
        public string StartDate
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

        public DatepickerTagHelper()
        {
        }

        private JObject DatePickerLocalization()
        {
            string str;
            dynamic jObjects = new JObject();
            jObjects.today = Resources.DatePicker_Today;
            jObjects.clear = Resources.DatePicker_Clear;
            var jArrays = new JArray()
            {
                Resources.Days_Sunday,
                Resources.Days_Monday,
                Resources.Days_Tuesday,
                Resources.Days_Wednesday,
                Resources.Days_Thursday,
                Resources.Days_Friday,
                Resources.Days_Saturday
            };
            jObjects.days = jArrays;
            var jArrays1 = new JArray()
            {
                Resources.Days_Sun,
                Resources.Days_Mon,
                Resources.Days_Tue,
                Resources.Days_Wed,
                Resources.Days_Thu,
                Resources.Days_Fri,
                Resources.Days_Sat
            };
            jObjects.daysShort = jArrays1;
            var jArrays2 = new JArray()
            {
                Resources.Days_Su,
                Resources.Days_Mo,
                Resources.Days_Tu,
                Resources.Days_We,
                Resources.Days_Th,
                Resources.Days_Fr,
                Resources.Days_Sa
            };
            jObjects.daysMin = jArrays2;
            var jArrays3 = new JArray()
            {
                Resources.Months_January,
                Resources.Months_February,
                Resources.Months_March,
                Resources.Months_April,
                Resources.Months_May,
                Resources.Months_June,
                Resources.Months_July,
                Resources.Months_August,
                Resources.Months_September,
                Resources.Months_October,
                Resources.Months_November,
                Resources.Months_December
            };
            jObjects.months = jArrays3;
            var jArrays4 = new JArray()
            {
                Resources.Months_Jan,
                Resources.Months_Feb,
                Resources.Months_Mar,
                Resources.Months_Apr,
                Resources.Months_May,
                Resources.Months_Jun,
                Resources.Months_Jul,
                Resources.Months_Aug,
                Resources.Months_Sep,
                Resources.Months_Oct,
                Resources.Months_Nov,
                Resources.Months_Dec
            };
            jObjects.monthsShort = jArrays4;
            var obj = jObjects;
            str = (!string.IsNullOrEmpty(this.Format) ? this.Format.ConvertNetFormatTojQuery() : CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ConvertNetFormatTojQuery());
            obj.format = str;
            jObjects.weekStart = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            return (JObject)jObjects;
        }

        public override void Init(TagHelperContext context)
        {
            object model;
            base.Init(context);
            var @for = base.For;
            if (@for != null)
            {
                model = @for.Model;
            }
            else
            {
                model = null;
            }
            this.Value = model;
        }

        private void ProcessJavascript(TagHelperOutput output)
        {
            dynamic jObjects = new JObject();
            jObjects.orientation = this.Orientation.GetEnumInfo().Name;
            jObjects.autoclose = this.IsCloseEnabled;
            jObjects.calendarWeeks = this.IsWeeksEnabled;
            jObjects.language = CultureInfo.CurrentCulture.Name;
            if (!string.IsNullOrEmpty(this.StartDate))
            {
                jObjects.startDate = this.StartDate;
            }
            if (!string.IsNullOrEmpty(this.EndDate))
            {
                jObjects.endDate = this.EndDate;
            }
            var stringBuilder = new StringBuilder("<script type='text/javascript'> $(function() {");
            stringBuilder.AppendLine(string.Format("$.fn.datepicker.dates['{0}'] = {1};", CultureInfo.CurrentCulture.Name, this.DatePickerLocalization()));
            stringBuilder.AppendLine(string.Format("var dp_{0} = $('#{1}'); dp_{2}.datepicker({3}); dp_{4}.on('changeDate', function(e) {{ dp_{5}.val(e.target.value); }});", new object[] { base.Id, base.Id, base.Id, jObjects, base.Id, base.Id }));
            stringBuilder.AppendLine("}); </script>");
            output.PostElement.AppendHtml(stringBuilder.ToString());
        }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            object str;
            string str1;
            this.BindProperty(context);
            output.TagName = "input";
            output.Attributes.Add("type", "text");
            output.Attributes.Add("readonly", "readonly");
            output.AddCssClass("form-control m-input");
            if (this.Icon != Icon.None)
            {
                var preElement = output.PreElement;
                str1 = (base.Size != Size.Default ? string.Concat("<div class=\"input-group m-input-group input-group-", base.Size.GetEnumInfo().Name, " date\">") : "<div class=\"input-group m-input-group date\">");
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
                    str = value.ToString(this.Format, CultureInfo.InvariantCulture);
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