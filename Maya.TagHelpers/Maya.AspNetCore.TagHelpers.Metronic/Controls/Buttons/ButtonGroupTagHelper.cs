using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Buttons
{
    [RestrictChildren("button", new string[] {"a", "button-dropdown"})]
    [OutputElementHint("div")]
    public class ButtonGroupTagHelper : MeconsTagHelperBase
    {
        private const string VerticalAttributeName = "bc-vertical";
        private const string SizeAttributeName = "bc-size";

        [HtmlAttributeName("bc-size")]
        public Size Size { get; set; }

        [HtmlAttributeName("bc-vertical")]
        public bool IsVertical { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass(this.IsVertical ? "btn-group-vertical m-btn-group" : "btn-group m-btn-group");
            output.MergeAttribute("role", "group");
            if (this.Size == Size.Default)
                return;
            output.AddCssClass("btn-group-" + this.Size.GetEnumInfo().Name);
        }
    }
}