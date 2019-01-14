using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Controls.Buttons;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Modal
{
    [ContextClass]
    [GenerateId("modal_", true)]
    [RestrictChildren("modal-header", new string[] {"modal-body", "modal-footer"})]
    public class ModalTagHelper : MeconsTagHelperBase
    {
        private const string ToggleButtonTextAttributeName = "bc-toggle-text";
        private const string ToggleButtonColorAttributeName = "bc-toggle-color";
        private const string TitleAttributeName = "bc-title";
        private const string SizeAttributeName = "bc-size";
        private const string PositionAttributeName = "bc-position";

        [Mandatory]
        [HtmlAttributeName("bc-toggle-text")]
        public string ToggleButtonText { get; set; }

        [HtmlAttributeName("bc-toggle-color")]
        public Color ToggleButtonColor { get; set; }

        [HtmlAttributeName("bc-title")]
        public string Title { get; set; }

        [HtmlAttributeNotBound]
        public string HeaderHtml { get; set; }

        [HtmlAttributeName("bc-size")]
        public Size Size { get; set; }

        [HtmlAttributeName("bc-position")]
        public ModalPosition Position { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(this.HeaderHtml) && !string.IsNullOrEmpty(this.Title))
                await new ModalHeaderTagHelper().RunTagHelperAsync(new TagHelperExtensions.Options()
                {
                    Context = context,
                    Content = new DefaultTagHelperContent().AppendHtml(this.Title)
                });
            if (!string.IsNullOrEmpty(this.ToggleButtonText))
            {
                var tagHelperContent = output.PreElement;
                var tagHelperArray = new ITagHelper[2]
                {
                    new ButtonTagHelper()
                    {
                        Color = this.ToggleButtonColor
                    },
                    new ModalToggleTagHelper()
                    {
                        ModalTarget = this.Id
                    }
                };
                tagHelperContent.AppendHtml(
                    await tagHelperArray.ToTagHelperContentAsync(
                        new TagHelperExtensions.Options()
                        {
                            Content = new DefaultTagHelperContent().AppendHtml(this.ToggleButtonText),
                            TagName = "button"
                        }));
                tagHelperContent = null;
            }

            output.TagName = "div";
            output.AddCssClass("modal fade");
            output.Attributes.Add("tabindex", "-1");
            output.Attributes.Add("role", "dialog");
            output.Attributes.Add("aria-hidden", "true");
            output.PreContent.AppendHtmlLine(this.HeaderHtml);
            output.WrapContentOutside(this.BuildContent());
            output.WrapContentOutside(this.BuildDialog());
        }

        private TagBuilder BuildDialog()
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttribute("role", "document");
            tagBuilder.AddCssClass("modal-dialog");
            if (this.Size != Size.Default)
                tagBuilder.AddCssClass("modal-" + this.Size.GetEnumInfo().Name);
            if (this.Position != ModalPosition.Top)
                tagBuilder.AddCssClass("modal-dialog-centered");
            return tagBuilder;
        }

        private TagBuilder BuildContent()
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("modal-content");
            return tagBuilder;
        }
    }
}