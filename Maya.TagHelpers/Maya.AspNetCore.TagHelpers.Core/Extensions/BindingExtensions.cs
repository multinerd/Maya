using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Controls;
using Maya.AspNetCore.TagHelpers.Core.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class BindingExtensions
    {
        public static void BindProperty(this FormTagHelperBase tagHelper, TagHelperContext context)
        {
            var modelExpression = tagHelper.GetModelExpression(context);
            if (modelExpression != null)
            {
                tagHelper.HandleId(modelExpression);
                tagHelper.HandleName(modelExpression);
                tagHelper.HandleRequired(modelExpression);
                tagHelper.HandleInformation(modelExpression);
            }
        }

        public static ModelExpression GetModelExpression(this FormTagHelperBase tagHelper, TagHelperContext context)
        {
            return context.AllAttributes.FirstOrDefault(a => a.Name == "asp-for")?.Value as ModelExpression;
        }

        public static object GetModelValue(this FormTagHelperBase tagHelper, TagHelperContext context)
        {
            return tagHelper.GetModelExpression(context)?.Model;
        }

        public static void HandleId(this FormTagHelperBase tagHelper, ModelExpression modelExpression)
        {
            var generateIdAttribute = tagHelper.GetType().GetTypeInfo().GetCustomAttributes<GenerateIdAttribute>(true).FirstOrDefault();
            if (string.IsNullOrEmpty(tagHelper.Id) || generateIdAttribute != null && tagHelper.GeneratedId == tagHelper.Id)
            {
                string name = modelExpression.Name;
                tagHelper.Id = tagHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            }
        }

        public static void HandleName(this FormTagHelperBase tagHelper, ModelExpression modelExpression)
        {
            var name = modelExpression.Name;
            tagHelper.Name = tagHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
        }

        public static void HandleRequired(this FormTagHelperBase tagHelper, ModelExpression modelExpression)
        {
            if (modelExpression.Metadata != null && modelExpression.Metadata.ContainerType.GetProperty(modelExpression.Metadata.PropertyName).IsDefined(typeof(RequiredAttribute)))
            {
                tagHelper.IsRequired = true;
            }
        }

        public static void HandleInformation(this FormTagHelperBase tagHelper, ModelExpression modelExpression)
        {
            if (modelExpression.Metadata != null)
            {
                var localization = modelExpression.Metadata.ContainerType.GetProperty(modelExpression.Metadata.PropertyName).GetLocalization();
                if (localization != null)
                {
                    if (string.IsNullOrEmpty(tagHelper.Label) && !string.IsNullOrEmpty(localization.DisplayName))
                    {
                        tagHelper.Label = localization.DisplayName;
                    }
                    if (string.IsNullOrEmpty(tagHelper.Help) && !string.IsNullOrEmpty(localization.Description))
                    {
                        tagHelper.Help = localization.Description;
                    }
                }
            }
        }
    }
}