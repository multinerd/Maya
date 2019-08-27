using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Components
{
    [HtmlTargetElement("progress", TagStructure = TagStructure.WithoutEndTag)]
    [OutputElementHint("div")]
    public class ProgressTagHelper : MeconsTagHelperBase
    {
        private const string ValueAttributeName = "bc-value";
        private const string LabelAttributeName = "bc-label";
        private const string SizeAttributeName = "bc-size";
        private const string HeightAttributeName = "bc-height";
        private const string ColorAttributeName = "bc-color";
        private const string StripedAttributeName = "bc-striped";
        private const string AnimatedAttributeName = "bc-animated";

        [HtmlAttributeName("bc-value")]
        public int Value { get; set; }

        [HtmlAttributeName("bc-label")]
        public bool HasLabel { get; set; }

        [HtmlAttributeName("bc-size")]
        public Size Size { get; set; }

        [HtmlAttributeName("bc-height")]
        public int Height { get; set; } = 16;

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; } = Color.Primary;

        [HtmlAttributeName("bc-striped")]
        public bool IsStriped { get; set; }

        [HtmlAttributeName("bc-animated")]
        public bool IsAnimated { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddCssClass("progress");
            if (this.Size != Size.Default)
                output.AddCssClass("m-progress--" + this.Size.GetEnumInfo().Name);
            else
                output.AddCssStyle("height", string.Format("{0}px", this.Height));
            output.Content.SetHtmlContent(ProgressTagHelper.Build(this.Value, this.Size, this.HasLabel, this.Color,
                this.IsAnimated, this.IsStriped));
        }

        public static IHtmlContent Build(
            int value,
            Size size = Size.Default,
            bool hasLabel = false,
            Color color = Color.Primary,
            bool isAnimated = false,
            bool isStriped = false)
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("progress-bar");
            tagBuilder.AddCssClass(color.GetColorInfo().BackgroundCssClass);
            tagBuilder.MergeAttribute("aria-valuemin", "0");
            tagBuilder.MergeAttribute("aria-valuemax", "100");
            tagBuilder.MergeAttribute("aria-valuenow", value.ToString());
            tagBuilder.MergeAttribute("role", "progressbar");
            tagBuilder.MergeAttribute("style", string.Format("width: {0}%;", value));
            if (hasLabel)
                tagBuilder.InnerHtml.Append(string.Format("{0}%", value));
            if (isAnimated)
                tagBuilder.AddCssClass("progress-bar-striped progress-bar-animated");
            else if (isStriped)
                tagBuilder.AddCssClass("progress-bar-striped");
            return tagBuilder;
        }
    }
}