using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Localization;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Components
{
    [OutputElementHint("div")]
    public class AlertTagHelper : MeconsTagHelperBase
    {
        private const string ColorAttributeName = "bc-color";
        private const string DismissibleAttributeName = "bc-dismissible";
        private const string DisableLinkStylingAttributeName = "bc-disable-link-styling";
        private const string TitleAttributeName = "bc-title";

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; } = Color.Primary;

        [HtmlAttributeName("bc-dismissible")]
        public bool Dismissible { get; set; }

        [HtmlAttributeName("bc-disable-link-styling")]
        public bool DisableLinkStyling { get; set; }

        [HtmlAttributeName("bc-title")]
        public string Title { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddCssClass("alert");
            output.MergeAttribute("role", "alert");
            if (this.Color != Color.None)
                output.AddCssClass("alert-" + this.Color.GetColorInfo().Name);
            if (!string.IsNullOrEmpty(this.Title))
                output.PreContent.AppendHtml("<strong>" + this.Title + "</strong> ");
            if (this.Dismissible)
            {
                output.AddCssClass("alert-dismissible");
                output.PreContent.SetHtmlContent(
                    "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"" +
                    Resources.Alert_CloseIconText + "\"><span aria-hidden=\"true\">&times;</span></button>");
            }

            if (this.DisableLinkStyling)
                return;
            output.Content.SetHtmlContent(Regex.Replace((await output.GetChildContentAsync(true)).GetContent(),
                "<a( [^>]+)?>", new MatchEvaluator(this.AddLinkStyle)));
        }

        private string AddLinkStyle(Match match)
        {
            if (match.ToString().Contains("class=\""))
                return match.ToString().Replace("class=\"", "class=\"alert-link ");
            return "<a class=\"alert-link\"" + match.ToString().Substring(2);
        }
    }
}