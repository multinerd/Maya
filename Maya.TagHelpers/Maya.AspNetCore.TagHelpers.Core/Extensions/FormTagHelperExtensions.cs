using Maya.AspNetCore.TagHelpers.Core.Controls;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class FormTagHelperExtensions
    {
        public static bool IsValid(this FormTagHelperBase tagHelper)
        {
            if (tagHelper.For == null)
            {
                return true;
            }
            return tagHelper.ViewContext.ViewData.ModelState.GetFieldValidationState(tagHelper.For.Metadata.PropertyName) == ModelValidationState.Valid;
        }

        public static bool IsPostValid(this FormTagHelperBase tagHelper)
        {
            if (tagHelper.For == null || tagHelper.ViewContext.HttpContext.Request.Method != "POST")
                return true;

            return tagHelper.ViewContext.ViewData.ModelState.GetFieldValidationState(tagHelper.For.Metadata.PropertyName) == ModelValidationState.Valid;
        }
    }
}