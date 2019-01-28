using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    public class LabelTagHelper : MeconsTagHelperBase
    {
        private const string SrOnlyAttributeName = "bc-sronly";

        [HtmlAttributeName("bc-sronly")]
        public bool SrOnly { get; set; }

        protected override void RenderProcess(TagHelperContext context, TagHelperOutput output)
        {
            if (!this.SrOnly)
                return;
            output.AddCssClass("sr-only");
        }

        public static IHtmlContent Build(
            string content,
            string controlId,
            bool isRequired,
            FormTagHelper formContext)
        {
            var tagBuilder = new TagBuilder("label");
            if (!string.IsNullOrEmpty(controlId))
                tagBuilder.Attributes.Add("for", controlId);
            if (formContext != null && formContext.LabelsSrOnly)
                tagBuilder.AddCssClass("sr-only");
            tagBuilder.InnerHtml.Append(content);
            if (isRequired)
                tagBuilder.InnerHtml.AppendHtml("<span class=\"required\" aria-required=\"true\" style=\"color: red;\"> * </span>");
            if (formContext != null && formContext.IsHorizontal)
            {
                tagBuilder.AddCssClass("col-form-label");
                if (formContext.LabelWidthXs > 0 && formContext.LabelWidthXs < 12)
                    tagBuilder.AddCssClass(string.Format("col-{0}", formContext.LabelWidthXs));
                if (formContext.LabelWidthSm > 0 && formContext.LabelWidthSm < 12)
                    tagBuilder.AddCssClass(string.Format("col-sm-{0}", formContext.LabelWidthSm));
                if (formContext.LabelWidthMd > 0 && formContext.LabelWidthMd < 12)
                    tagBuilder.AddCssClass(string.Format("col-md-{0}", formContext.LabelWidthMd));
                if (formContext.LabelWidthLg > 0 && formContext.LabelWidthLg < 12)
                    tagBuilder.AddCssClass(string.Format("col-lg-{0}", formContext.LabelWidthLg));
                if (formContext.LabelWidthXl > 0 && formContext.LabelWidthXl < 12)
                    tagBuilder.AddCssClass(string.Format("col-xl-{0}", formContext.LabelWidthXl));
            }

            return tagBuilder;
        }
    }
}