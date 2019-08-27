using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Stack
{
    [OutputElementHint("div")]
    public class StackTagHelper : MeconsTagHelperBase
    {
        private const string AutoWidthAttributeName = "bc-auto-width";
        private const string OrientationAttributeName = "bc-orientation";

        [HtmlAttributeName("bc-auto-width")]
        public bool AutoWidth { get; set; }

        [HtmlAttributeName("bc-orientation")]
        public Orientation Orientation { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("m-stack m-stack--general");
            output.AddCssClass(this.Orientation == Orientation.Vertical ? "m-stack--hor" : "m-stack--ver");
            if (!this.AutoWidth)
                return;
            output.AddCssClass("m-stack--inline");
        }
    }
}