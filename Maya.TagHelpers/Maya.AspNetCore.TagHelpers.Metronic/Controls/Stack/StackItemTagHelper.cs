using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Stack
{
    [OutputElementHint("div")]
    public class StackItemTagHelper : MeconsTagHelperBase
    {
        private const string AlignmentAttributeName = "bc-alignment";
        private const string VerticalAlignmentAttributeName = "bc-vertical-alignment";
        private const string WidthAttributeName = "bc-width";
        private const string HeightAttributeName = "bc-height";

        [HtmlAttributeName("bc-alignment")]
        public HorizontalAlignment Alignment { get; set; } = HorizontalAlignment.Left;

        [HtmlAttributeName("bc-vertical-alignment")]
        public VerticalAlignment VerticalAlignment { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("m-stack__item");
            if (this.Alignment != HorizontalAlignment.Default)
                output.AddCssClass("m-stack__item--" + this.Alignment.GetEnumInfo().Name);
            if (this.VerticalAlignment == VerticalAlignment.Default)
                return;
            output.AddCssClass("m-stack__item--" + this.VerticalAlignment.GetEnumInfo().Name);
        }
    }
}