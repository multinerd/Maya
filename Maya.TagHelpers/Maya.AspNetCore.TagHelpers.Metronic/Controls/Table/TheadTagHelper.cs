using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Table
{
    [OutputElementHint("thead")]
    [HtmlTargetElement("thead", ParentTag = "table")]
    public class TheadTagHelper : MeconsTagHelperBase
    {
        private const string StyleAttributeName = "bc-theme";

        [HtmlAttributeName("bc-theme")]
        public Theme Theme { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.AddCssClass(this.Theme != Theme.Default ? "thead-" + this.Theme.GetEnumInfo().Name : string.Empty);
        }
    }
}