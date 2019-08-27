using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Buttons
{
    [HtmlTargetElement("a", ParentTag = "button-dropdown")]
    public class ButtonDropdownItemTagHelper : MeconsTagHelperBase
    {
        private const string DisableAttributeName = "bc-disable";

        [HtmlAttributeName("bc-disable")]
        public bool IsDisabled { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.AddCssClass("dropdown-item");
            if (this.IsDisabled)
            {
                output.AddCssClass("disabled");
            }
        }
    }
}