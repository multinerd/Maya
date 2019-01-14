using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Portlet
{
    [ContextClass]
    [RestrictChildren("portlet-header", new string[] {"portlet-body", "portlet-footer"})]
    [OutputElementHint("div")]
    public class PortletTagHelper : MeconsTagHelperBase
    {
        private const string RoundAttributeName = "bc-round";
        private const string ShadowAttributeName = "bc-shadow";
        private const string BorderAttributeName = "bc-border";
        private const string ThemeAttributeName = "bc-theme";
        private const string BackgroundAttributeName = "bc-background";
        private const string BackgroundStyleAttributeName = "bc-background-style";

        [HtmlAttributeName("bc-round")]
        public bool IsRounded { get; set; }

        [HtmlAttributeName("bc-shadow")]
        public bool IsShadowed { get; set; } = true;

        [HtmlAttributeName("bc-border")]
        public PortletBorderStyle Border { get; set; }

        [HtmlAttributeName("bc-theme")]
        public Theme Theme { get; set; }

        [HtmlAttributeName("bc-background")]
        public Color Background { get; set; }

        [HtmlAttributeName("bc-background-style")]
        public PortletBackgroundStyle BackgroundStyle { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            output.AddCssClass("m-portlet");
            if (this.IsRounded)
                output.AddCssClass("m-portlet--rounded");
            if (!this.IsShadowed)
                output.AddCssClass("m-portlet--unair");
            switch (this.Border)
            {
                case PortletBorderStyle.Full:
                    output.AddCssClass("m-portlet--bordered");
                    break;
                case PortletBorderStyle.Semi:
                    output.AddCssClass("m-portlet--bordered m-portlet--bordered-semi");
                    break;
            }

            if (this.Theme != Theme.Default)
                output.AddCssClass("m-portlet--skin-" + this.Theme.GetEnumInfo().Name);
            if (this.Background != Color.None && this.BackgroundStyle == PortletBackgroundStyle.Full)
            {
                output.AddCssClass(this.Background.GetColorInfo().BackgroundCssClass);
            }
            else
            {
                if (this.Background == Color.None || this.BackgroundStyle != PortletBackgroundStyle.HeadOnly)
                    return;
                output.AddCssClass("m-portlet--" + this.Background.GetColorInfo().Name);
                output.AddCssClass("m-portlet--head-solid-bg");
            }
        }
    }
}