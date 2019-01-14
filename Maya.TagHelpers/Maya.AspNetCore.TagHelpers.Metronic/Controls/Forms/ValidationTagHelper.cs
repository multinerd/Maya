using System.Linq;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [OutputElementHint("div")]
    [HtmlTargetElement("validation", ParentTag = "form-group")]
    public class ValidationTagHelper : BreconsTagHelperBase
    {
        private const string AspForAttributeName = "asp-for";

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [Context]
        [HtmlAttributeNotBound]
        public FormTagHelper FormContext { get; set; }

        protected override void RenderProcess(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddCssClass("form-control-feedback");
            output.MergeAttribute("data-valmsg-for", this.For.Metadata.PropertyName);
            output.MergeAttribute("data-valmsg-replace", "true");
            var modelStateEntry = this.ViewContext.ViewData.ModelState
                .FirstOrDefault(
                    k => k.Key == this.For.Metadata.PropertyName)
                .Value;
            if (modelStateEntry != null && modelStateEntry.ValidationState == ModelValidationState.Invalid)
            {
                output.AddCssClass("field-validation-error");
                output.Content.SetContent(modelStateEntry.Errors.FirstOrDefault()?.ErrorMessage);
            }
            else
                output.AddCssClass("field-validation-valid");
        }

        public static IHtmlContent Generate(ModelExpression model, ViewContext viewContext)
        {
            var tagBuilder = new TagBuilder("div")
            {
                TagRenderMode = TagRenderMode.Normal
            };
            tagBuilder.AddCssClass("form-control-feedback");
            tagBuilder.MergeAttribute("data-valmsg-for", model.Metadata.PropertyName);
            tagBuilder.MergeAttribute("data-valmsg-replace", "true");
            var modelStateEntry = viewContext.ViewData.ModelState
                .FirstOrDefault(
                    k => k.Key == model.Metadata.PropertyName)
                .Value;
            if (modelStateEntry != null && modelStateEntry.ValidationState == ModelValidationState.Invalid)
            {
                tagBuilder.AddCssClass("field-validation-error");
                tagBuilder.InnerHtml.Append(modelStateEntry.Errors.FirstOrDefault()?.ErrorMessage);
            }
            else
                tagBuilder.AddCssClass("field-validation-valid");

            return tagBuilder;
        }
    }
}