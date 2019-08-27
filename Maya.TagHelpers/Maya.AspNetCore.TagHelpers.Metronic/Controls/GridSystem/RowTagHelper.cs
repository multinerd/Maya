using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.GridSystem
{
    [OutputElementHint("div")]
    [RestrictChildren("column", new string[] { })]
    public class RowTagHelper : MeconsTagHelperBase
    {
        private const string VerticalAlignmentAttributeName = "vertical-alignment";
        private const string HorizontalAlignmentAttributeName = "alignment";

        [HtmlAttributeName("vertical-alignment")]
        public VerticalAlignment VerticalAlignment { get; set; }

        [HtmlAttributeName("alignment")]
        public GridHorizontalAlignment HorizontalAlignment { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("row");
            switch (this.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    output.AddCssClass("align-items-start");
                    break;
                case VerticalAlignment.Middle:
                    output.AddCssClass("align-items-center");
                    break;
                case VerticalAlignment.Bottom:
                    output.AddCssClass("align-items-end");
                    break;
            }

            switch (this.HorizontalAlignment)
            {
                case GridHorizontalAlignment.Left:
                    output.AddCssClass("justify-content-start");
                    break;
                case GridHorizontalAlignment.Center:
                    output.AddCssClass("justify-content-center");
                    break;
                case GridHorizontalAlignment.Right:
                    output.AddCssClass("justify-content-end");
                    break;
                case GridHorizontalAlignment.Around:
                    output.AddCssClass("justify-content-around");
                    break;
                case GridHorizontalAlignment.Between:
                    output.AddCssClass("justify-content-between");
                    break;
            }
        }
    }
}