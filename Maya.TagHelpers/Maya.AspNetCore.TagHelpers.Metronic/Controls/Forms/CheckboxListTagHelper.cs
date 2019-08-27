using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [HtmlTargetElement("checkbox-list")]
    [ContextClass]
    [GenerateId("inputlist_", true)]
    public class CheckboxListTagHelper : MeconsFormTagHelperBase
    {
        private const string InlineAttributeName = "bc-inline";
        private const string LabelAttributeName = "bc-label";
        private const string HelpAttributeName = "bc-help";
        private const string ItemsAttributeName = "bc-items";

        [HtmlAttributeName("bc-inline")]
        public bool IsInline { get; set; }

        [HtmlAttributeName("bc-items")]
        public IEnumerable<SelectListItem> Items { get; set; }

        protected virtual string ListName
        {
            get { return "checkbox"; }
        }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            this.BindProperty(context);
            output.TagName = "div";
            output.AddCssClass(this.IsInline
                ? "m-" + this.ListName + "-inline"
                : "m-" + this.ListName + "-list");
            if (this.FormContext != null)
                this.FormContext.WrapInDivForHorizontalForm(output);
            this.RenderBinding(output, false, false, true, false, false, true);
            if (this.Items == null)
                return;
            foreach (var selectListItem in this.Items)
                output.Content.AppendHtml(this.BuildCheckInput(selectListItem, context));
        }

        private TagBuilder BuildCheckInput(SelectListItem item, TagHelperContext listContext)
        {
            var input = new TagBuilder("input")
            {
                TagRenderMode = TagRenderMode.SelfClosing
            };
            input.MergeAttribute("type", this.ListName);
            input.MergeAttribute("value", item.Value);
            var modelExpression = this.GetModelExpression(listContext);
            if (modelExpression != null)
            {
                input.MergeAttribute("name", this.Name);
                this.RenderSelected(modelExpression, item, input);
            }

            var tagBuilder = new TagBuilder("label");
            tagBuilder.AddCssClass("m-" + this.ListName);
            tagBuilder.AddCssClass(item.Disabled ? "m-" + this.ListName + "--disabled" : string.Empty);
            tagBuilder.InnerHtml.AppendHtml(input);
            tagBuilder.InnerHtml.Append(item.Text);
            tagBuilder.InnerHtml.AppendHtml("<span></span>");
            return tagBuilder;
        }

        private void RenderSelected(
            ModelExpression modelExpression,
            SelectListItem item,
            TagBuilder input)
        {
            var model = modelExpression?.Model;
            if (model == null)
                return;
            if (this.ListName == "radio")
            {
                if (model.ToString() == item.Value)
                    input.MergeAttribute("checked", "checked");
                if (model.GetType().BaseType == typeof(Enum) && ((int) model).ToString() == item.Value)
                    input.MergeAttribute("checked", "checked");
            }

            if (this.ListName == "checkbox")
            {
                var stringList = model as List<string>;
                if (stringList != null && stringList.Contains(item.Value))
                    input.MergeAttribute("checked", "checked");
            }

            if (!item.Selected)
                return;
            input.MergeAttribute("checked", "checked");
        }
    }
}