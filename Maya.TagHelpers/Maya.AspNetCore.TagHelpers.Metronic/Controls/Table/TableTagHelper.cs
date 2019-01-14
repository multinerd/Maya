using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Table
{
    public class TableTagHelper : MeconsTagHelperBase
    {
        private const string StripedAttributeName = "bc-striped";
        private const string SmallAttributeName = "bc-small";
        private const string BorderAttributeName = "bc-border";
        private const string ResponsiveAttributeName = "bc-responsive";
        private const string HoverAttributeName = "bc-hover";
        private const string ThemeAttributeName = "bc-theme";

        [HtmlAttributeName("bc-striped")]
        public bool IsStriped { get; set; }

        [HtmlAttributeName("bc-small")]
        public bool IsSmall { get; set; }

        [HtmlAttributeName("bc-border")]
        public TableBorder Border { get; set; }

        [HtmlAttributeName("bc-responsive")]
        public bool IsResponsive { get; set; }

        [HtmlAttributeName("bc-hover")]
        public bool IsHover { get; set; }

        [HtmlAttributeName("bc-theme")]
        public Theme Theme { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.AddCssClass("table m-table");
            output.AddCssClass(this.Theme != Theme.Default ? "table-" + this.Theme.GetEnumInfo().Name : string.Empty);
            if (this.IsStriped)
                output.AddCssClass("table-striped");
            if (this.IsSmall)
                output.AddCssClass("table-sm");
            switch (this.Border)
            {
                case TableBorder.Bordered:
                    output.AddCssClass("table-bordered");
                    break;
                case TableBorder.Borderless:
                    output.AddCssClass("table-borderless");
                    break;
            }

            if (this.IsHover)
                output.AddCssClass("table-hover");
            if (!this.IsResponsive)
                return;
            output.AddCssClass("table-responsive");
        }
    }
}