using System.Linq;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [GenerateId("input_", true)]
    [HtmlTargetElement("input", TagStructure = TagStructure.WithoutEndTag)]
    public class InputTagHelper : MeconsFormTagHelperBase
    {
        private const string PreAddonTextAttributeName = "bc-pre-text";

        private const string PostAddonTextAttributeName = "bc-post-text";

        private const string PreAddonIconAttributeName = "bc-pre-icon";

        private const string PostAddonIconAttributeName = "bc-post-icon";

        [Context]
        [HtmlAttributeNotBound]
        public CheckboxListTagHelper CheckboxListContext
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-post-icon")]
        public Icon PostAddonIcon
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-post-text")]
        public string PostAddonText
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-pre-icon")]
        public Icon PreAddonIcon
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-pre-text")]
        public string PreAddonText
        {
            get;
            set;
        }

        [Context]
        [HtmlAttributeNotBound]
        public RadioListTagHelper RadioListContext
        {
            get;
            set;
        }

        [CopyToOutput]
        public string Type { get; set; } = "text";

        public InputTagHelper()
        {
        }

        private void RenderButtonControl(TagHelperOutput output)
        {
            output.AddCssClass("btn");
            if (base.Size != Size.Default)
            {
                output.AddCssClass(string.Concat("btn-", base.Size.GetEnumInfo().Name));
            }
        }

        private void RenderCheckControl(TagHelperOutput output)
        {
            output.PostElement.Append(string.Concat(" ", base.Label));
            output.PostElement.AppendHtml("<span></span>");
            this.RenderBinding(output, false, true, false, false, false, false);
            var tagBuilder = new TagBuilder("label");
            tagBuilder.AddCssClass(string.Concat("m-", this.Type.ToLower()));
            tagBuilder.AddCssClass((output.Attributes.ContainsName("disabled") ? string.Concat("m-", this.Type.ToLower(), "--disabled") : string.Empty));
            output.WrapOutside(tagBuilder);
        }

        private void RenderFileControl(TagHelperOutput output)
        {
            output.AddCssClass("custom-file-input");
            output.PostElement.AppendHtml(string.Concat(new string[] { "<label class=\"custom-file-label\" for=\"", base.Id, "\">", Resources.Input_File_ChooseFile, "</label>" }));
            output.WrapHtmlOutside("<label class=\"custom-file\">", "</label>");
            output.PreElement.PrependHtml("<div></div>");
            this.RenderBinding(output, false, true, true, true, false, true);
        }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            this.BindProperty(context);
            if (BreconsConsts.CheckTypes.Any(t => t == this.Type.ToLower()))
            {
                this.RenderCheckControl(output);
            }
            else if (BreconsConsts.ButtonTypes.Any(t => t == this.Type.ToLower()))
            {
                this.RenderButtonControl(output);
            }
            else if (BreconsConsts.FileTypes.Any(t => t == this.Type.ToLower()))
            {
                this.RenderFileControl(output);
            }
            else if (!BreconsConsts.InputTypes.Any(t => t == this.Type.ToLower()))
            {
                this.Type = "text";
                this.RenderTextControl(output);
            }
            else
            {
                this.RenderTextControl(output);
            }
        }

        private void RenderTextControl(TagHelperOutput output)
        {
            output.AddCssClass("form-control m-input");
            if (!string.IsNullOrEmpty(this.PostAddonText) || !string.IsNullOrEmpty(this.PreAddonText) || this.PostAddonIcon != Icon.None || this.PreAddonIcon != Icon.None)
            {
                output.PreElement.PrependHtml((base.Size != Size.Default ? string.Concat("<div class=\"input-group m-input-group input-group-", base.Size.GetEnumInfo().Name, "\">") : "<div class=\"input-group m-input-group\">"));
                if (!string.IsNullOrEmpty(this.PreAddonText))
                {
                    output.PreElement.AppendHtml(AddonTagHelper.Build(this.PreAddonText, AddonType.Prepend));
                }
                if (!string.IsNullOrEmpty(this.PostAddonText))
                {
                    output.PostElement.AppendHtml(AddonTagHelper.Build(this.PostAddonText, AddonType.Append));
                }
                if (this.PreAddonIcon != Icon.None)
                {
                    output.PreElement.AppendHtml(AddonTagHelper.Build(this.PreAddonIcon, AddonType.Prepend));
                }
                if (this.PostAddonIcon != Icon.None)
                {
                    output.PostElement.AppendHtml(AddonTagHelper.Build(this.PostAddonIcon, AddonType.Append));
                }
                output.PostElement.AppendHtml("</div>");
            }
            this.RenderBinding(output, false, true, true, true, true, true);
        }
    }
}