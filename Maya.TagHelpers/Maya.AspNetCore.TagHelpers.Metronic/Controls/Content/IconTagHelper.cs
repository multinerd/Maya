using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Exceptions;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Content
{
    [HtmlTargetElement("icon", TagStructure = TagStructure.WithoutEndTag)]
    public class IconTagHelper : MeconsTagHelperBase
    {
        private const string TypeAttributeName = "bc-type";
        private const string ColorAttributeName = "bc-color";

        [HtmlAttributeName("bc-type")]
        public Icon Type { get; set; }

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "i";
            output.TagMode = TagMode.StartTagAndEndTag;
            if (this.Type == Icon.None)
                throw new MandatoryAttributeException("Type", this.GetType());
            output.AddCssClass(this.Type.GetIconInfo().CssClass);
            if (this.Color == Color.None)
                return;
            output.AddCssClass(this.Color.GetColorInfo().TextCssClass);
        }

        public static IHtmlContent Build(Icon type, Color color)
        {
            var tagBuilder = new TagBuilder("i");
            tagBuilder.AddCssClass(type.GetIconInfo().CssClass);
            if (color != Color.None)
                tagBuilder.AddCssClass(color.GetColorInfo().TextCssClass);
            return tagBuilder;
        }
    }
}