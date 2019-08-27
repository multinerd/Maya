using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Content
{
    [OutputElementHint("span")]
    public class BadgeTagHelper : MeconsTagHelperBase
    {
        private const string ColorAttributeName = "bc-color";
        private const string WideAttributeName = "bc-wide";
        private const string StyleAttributeName = "bc-style";

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; } = Color.Primary;

        [HtmlAttributeName("bc-wide")]
        public bool IsWide { get; set; }

        [HtmlAttributeName("bc-style")]
        public BadgeStyle Style { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "span";
            output.AddCssClass("m-badge");
            if (this.Color != Color.None)
                output.AddCssClass("m-badge--" + this.Color.GetColorInfo().Name);
            if (this.IsWide)
                output.AddCssClass("m-badge--wide");
            switch (this.Style)
            {
                case BadgeStyle.Dot:
                    output.AddCssClass("m-badge--dot");
                    output.Content.SetContent(string.Empty);
                    break;
                case BadgeStyle.Round:
                    output.AddCssClass("m-badge--rounded");
                    break;
            }
        }
    }
}