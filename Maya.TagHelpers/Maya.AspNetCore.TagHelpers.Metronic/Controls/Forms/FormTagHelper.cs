using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [ContextClass]
    public class FormTagHelper : MeconsTagHelperBase
    {
        private const string LabelWidthXsAttributeName = "bc-label-width-xs";
        private const string LabelWidthSmAttributeName = "bc-label-width-sm";
        private const string LabelWidthMdAttributeName = "bc-label-width-md";
        private const string LabelWidthLgAttributeName = "bc-label-width-lg";
        private const string LabelWidthXlAttributeName = "bc-label-width-xl";
        private const string LabelsSrOnlyAttributeName = "bc-labels-sr-only";
        private const string HorizontalAttributeName = "bc-horizontal";
        private const string ValidationAttributeName = "bc-validation";

        [HtmlAttributeName("bc-label-width-xs")]
        public int LabelWidthXs { get; set; }

        [HtmlAttributeName("bc-label-width-sm")]
        public int LabelWidthSm { get; set; }

        [HtmlAttributeName("bc-label-width-md")]
        public int LabelWidthMd { get; set; }

        [HtmlAttributeName("bc-label-width-lg")]
        public int LabelWidthLg { get; set; }

        [HtmlAttributeName("bc-label-width-xl")]
        public int LabelWidthXl { get; set; }

        [HtmlAttributeName("bc-horizontal")]
        public bool IsHorizontal { get; set; }

        [HtmlAttributeName("bc-labels-sr-only")]
        public bool LabelsSrOnly { get; set; }

        [HtmlAttributeName("bc-validation")]
        public bool Validation { get; set; } = true;

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.AddCssClass("m-form m-form--state");
        }

        public void WrapInDivForHorizontalForm(TagHelperOutput output)
        {
            if (!this.IsHorizontal)
                return;
            var tagBuilder = new TagBuilder("div")
            {
                TagRenderMode = TagRenderMode.StartTag
            };
            if (this.LabelWidthXs > 0 && this.LabelWidthXs < 12)
                tagBuilder.AddCssClass("col-" + (12 - this.LabelWidthXs));
            if (this.LabelWidthSm > 0 && this.LabelWidthSm < 12)
                tagBuilder.AddCssClass("col-sm-" + (12 - this.LabelWidthSm));
            if (this.LabelWidthMd != 0 && this.LabelWidthMd < 12)
                tagBuilder.AddCssClass("col-md-" + (12 - this.LabelWidthMd));
            if (this.LabelWidthLg != 0 && this.LabelWidthLg < 12)
                tagBuilder.AddCssClass("col-lg-" + (12 - this.LabelWidthLg));
            if (this.LabelWidthXl != 0 && this.LabelWidthXl < 12)
                tagBuilder.AddCssClass("col-xl-" + (12 - this.LabelWidthXl));
            output.PreElement.Prepend(tagBuilder);
            output.PostElement.AppendHtml("</div>");
        }
    }
}