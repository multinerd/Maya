using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Extensions
{
    internal static class RenderingExtensions
    {
        internal static void RenderBinding(
            this MeconsFormTagHelperBase tagHelper,
            TagHelperOutput output,
            bool name = false,
            bool validation = false,
            bool help = false,
            bool formLayout = false,
            bool size = false,
            bool label = false)
        {
            if (name)
                tagHelper.RenderName(output);
            if (validation)
                tagHelper.RenderValidation(output);
            if (help)
                tagHelper.RenderHelp(output);
            if (formLayout)
                tagHelper.RenderFormLayout(output);
            if (size)
                tagHelper.RenderSize(output);
            if (!label)
                return;
            tagHelper.RenderLabel(output);
        }

        private static void RenderName(this MeconsFormTagHelperBase tagHelper, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(tagHelper.Name))
                return;
            output.MergeAttribute("name", tagHelper.Name);
        }

        private static void RenderValidation(
            this MeconsFormTagHelperBase tagHelper,
            TagHelperOutput output)
        {
            if (!tagHelper.IsPostValid() && tagHelper.Validation)
            {
                output.AddCssClass("form-control-danger");
                if (tagHelper.FormGroupContext != null)
                    tagHelper.FormGroupContext.State = ValidationState.Danger;
            }

            if (tagHelper.For == null || !tagHelper.Validation)
                return;
            output.PostElement.AppendHtml(ValidationTagHelper.Generate(tagHelper.For, tagHelper.ViewContext));
        }

        private static void RenderLabel(this MeconsFormTagHelperBase tagHelper, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(tagHelper.Label))
                return;
            output.PreElement.Prepend(LabelTagHelper.Build(tagHelper.Label, tagHelper.Id, tagHelper.IsRequired,
                tagHelper.FormContext));
        }

        private static void RenderHelp(this MeconsFormTagHelperBase tagHelper, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(tagHelper.Help))
                return;
            output.MergeAttribute("aria-describedby", tagHelper.Id + "-help");
            output.PostElement.AppendHtml(HelpTagHelper.Build(tagHelper.Help, tagHelper.Id + "-help"));
        }

        private static void RenderSize(this MeconsFormTagHelperBase tagHelper, TagHelperOutput output)
        {
            if (tagHelper.Size == Size.Default)
                return;
            output.AddCssClass("form-control-" + tagHelper.Size.GetEnumInfo().Name);
        }

        private static void RenderFormLayout(
            this MeconsFormTagHelperBase tagHelper,
            TagHelperOutput output)
        {
            if (tagHelper.FormContext == null)
                return;
            tagHelper.FormContext.WrapInDivForHorizontalForm(output);
        }
    }
}