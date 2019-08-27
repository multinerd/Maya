using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Sliders
{
    [GenerateId("nouislider_", true)]
    [HtmlTargetElement("nouislider", TagStructure = TagStructure.WithoutEndTag)]
    [OutputElementHint("div")]
    public class NoUiSliderTagHelper : MeconsTagHelperBase
    {
        private const string ColorSlidersAttributeName = "bc-color-sliders";

        private const string ColorConnectorsAttributeName = "bc-color-connectors";

        private const string SlidersAttributeName = "bc-all-sliders";

        private const string SliderPrefix = "bc-slider-";

        private const string ConnectorsAttributeName = "bc-connectors";

        private const string StepAttributeName = "bc-step";

        private const string MinimumAttributeName = "bc-min";

        private const string MaximumAttributeName = "bc-max";

        private const string MarginAttributeName = "bc-margin";

        private const string LimitAttributeName = "bc-limit";

        private const string OrientationAttributeName = "bc-orientation";

        private const string DirectionAttributeName = "bc-direction";

        private const string BindingsAttributeName = "bc-all-bindings";

        private const string BindingPrefix = "asp-for-";

        private const string PipsModeAttributeName = "bc-pips-mode";

        private const string PipsDensityAttributeName = "bc-pips-density";

        private const string PipsValuesAttributeName = "bc-pips-values";

        private const string PipsSteppedAttributeName = "bc-pips-stepped";

        private List<bool> _connectors = new List<bool>();

        private List<int> _pipsValues = new List<int>();

        [HtmlAttributeName("bc-all-bindings", DictionaryAttributePrefix = "asp-for-")]
        public IDictionary<string, ModelExpression> Bindings { get; set; } = new Dictionary<string, ModelExpression>();

        [HtmlAttributeName("bc-color-connectors")]
        public Color ColorConnectors
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-color-sliders")]
        public Color ColorSliders
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-connectors")]
        public string Connectors
        {
            get
            {
                return string.Join(",", this._connectors);
            }
            set
            {
                this._connectors = (
                    from i in value.Split(new[] { ',' }, StringSplitOptions.None)
                    select Convert.ToBoolean(i)).ToList();
            }
        }

        [HtmlAttributeName("bc-direction")]
        public Direction Direction
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-limit")]
        public int Limit { get; set; } = -1;

        [HtmlAttributeName("bc-margin")]
        public int Margin { get; set; } = -1;

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

        [HtmlAttributeName("bc-pips-density")]
        public int PipsDensity { get; set; } = 1;

        [HtmlAttributeName("bc-pips-mode")]
        public PipsMode PipsMode
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-pips-stepped")]
        public bool PipsStepped
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-pips-values")]
        public string PipsValues
        {
            get
            {
                return string.Join(",", this._pipsValues);
            }
            set
            {
                this._pipsValues = value.Split(new []{ ',' }, StringSplitOptions.None).Select(s => Convert.ToInt32(s)).ToList();
            }
        }

        [HtmlAttributeName("bc-all-sliders", DictionaryAttributePrefix = "bc-slider-")]
        public IDictionary<string, double> Sliders { get; set; } = new Dictionary<string, double>();

        [HtmlAttributeName("bc-step")]
        public int Step { get; set; } = -1;

        public NoUiSliderTagHelper()
        {
        }

        private void DataBinding(TagHelperContext context, TagHelperOutput output)
        {
            if (this.Bindings.Any())
            {
                this.Sliders.Clear();
                foreach (var binding in this.Bindings)
                {
                    this.Sliders.Add(base.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(binding.Value.Name), Convert.ToDouble(binding.Value.Model));
                }
            }
        }

        private void ProcessJavascript(TagHelperOutput output)
        {
            dynamic jObjects = new JObject();
            var jArrays = jObjects;
            var sliders = this.Sliders;
            jArrays.start = new JArray(
                from s in sliders
                select s.Value);
            if (this.Margin > 0)
            {
                jObjects.margin = this.Margin;
            }
            if (this.Limit > 0)
            {
                jObjects.limit = this.Limit;
            }
            if (this.Step > 0)
            {
                jObjects.step = this.Step;
            }
            jObjects.orientation = (this.Orientation == Orientation.Vertical ? "vertical" : "horizontal");
            jObjects.direction = (this.Direction == Direction.Rtl ? "rtl" : "ltr");
            if (this._connectors.Any())
            {
                jObjects.connect = new JArray(this._connectors);
            }
            jObjects.range = new JObject();
            jObjects.range.min = this.Minimum;
            jObjects.range.max = this.Maximum;
            if (this.PipsMode != PipsMode.None)
            {
                jObjects.pips = this.ProcessPipsConfig();
            }
            var stringBuilder = new StringBuilder("<script type='text/javascript'> $(function() {");
            stringBuilder.AppendLine(string.Concat(new string[] { "var ", base.Id, " = document.getElementById('", base.Id, "');" }));
            stringBuilder.AppendLine(string.Format("noUiSlider.create({0}, {1});", base.Id, jObjects));
            stringBuilder.AppendLine(string.Concat(base.Id, ".noUiSlider.on('set', function (values, handle) {"));
            for (var i = 0; i <= this.Sliders.Count - 1; i++)
            {
                var id = new object[] { i, base.Id, null, null };
                var keyValuePair = this.Sliders.ElementAt(i);
                id[2] = keyValuePair.Key;
                id[3] = i;
                stringBuilder.AppendLine(string.Format("if (handle === {0}) {{ $('#hidden_{1}_{2}').val(values[{3}]) }}", id));
            }
            stringBuilder.AppendLine("});");
            stringBuilder.AppendLine("}); </script>");
            output.PostElement.AppendHtml(stringBuilder.ToString());
        }

        private JObject ProcessPipsConfig()
        {
            dynamic jObjects = new JObject();
            jObjects.mode = this.PipsMode.GetEnumInfo().Name;
            jObjects.density = this.PipsDensity;
            jObjects.stepped = this.PipsStepped;
            if (this._pipsValues.Any())
            {
                switch (this.PipsMode)
                {
                    case PipsMode.Positions:
                        {
                            jObjects.values = new JArray(this._pipsValues);
                            break;
                        }
                    case PipsMode.Count:
                        {
                            jObjects.values = this._pipsValues.First();
                            break;
                        }
                    case PipsMode.Values:
                        {
                            jObjects.values = new JArray(this._pipsValues);
                            break;
                        }
                }
            }
            return (JObject)jObjects;
        }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            string str;
            string str1;
            string str2;
            this.DataBinding(context, output);
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddCssClass("m-nouislider");
            var tagHelperOutput = output;
            str = (this.ColorSliders != Color.None ? string.Concat("m-nouislider--handle-", this.ColorSliders.GetColorInfo().Name) : string.Empty);
            tagHelperOutput.AddCssClass(str);
            var tagHelperOutput1 = output;
            str1 = (this.ColorConnectors != Color.None ? string.Concat("m-nouislider--connect-", this.ColorConnectors.GetColorInfo().Name) : string.Empty);
            tagHelperOutput1.AddCssClass(str1);
            foreach (var slider in this.Sliders)
            {
                var tagBuilder = new TagBuilder("input")
                {
                    TagRenderMode = TagRenderMode.SelfClosing
                };
                tagBuilder.MergeAttribute("type", "hidden");
                tagBuilder.GenerateId(string.Concat("hidden_", base.Id, "_", slider.Key), "-");
                var tagBuilder1 = tagBuilder;
                str2 = (this.Bindings.Any() ? slider.Key : string.Concat("hidden_", base.Id, "_", slider.Key));
                tagBuilder1.MergeAttribute("name", str2);
                tagBuilder.MergeAttribute("value", slider.Value.ToString());
                output.PostElement.AppendHtml(tagBuilder);
            }
            this.ProcessJavascript(output);
        }
    }
}