using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Table
{
    [OutputElementHint("tr")]
    [HtmlTargetElement("tr", ParentTag = "tbody")]
    public class TrTagHelper : MeconsTagHelperBase
    {
        private const string BackgroundAttributeName = "bc-background";

        [HtmlAttributeName("bc-background")]
        public Color Background { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            if (this.Background == Color.None)
                return;
            output.AddCssClass("m-table__row--" + this.Background.GetColorInfo().Name);
        }
    }
}