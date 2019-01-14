using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Controls
{
    public abstract class FormTagHelperBase : BreconsTagHelperBase
    {
        private const string ValidationAttributeName = "bc-validation";
        private const string AspForAttributeName = "asp-for";
        private const string HelpAttributeName = "bc-help";
        private const string LabelAttributeName = "bc-label";
        private const string SizeAttributeName = "bc-size";
        private const string RequiredAttributeName = "bc-required";

        [HtmlAttributeName("bc-validation")]
        public bool Validation { get; set; }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("bc-help")]
        public string Help { get; set; }

        [HtmlAttributeName("bc-label")]
        public string Label { get; set; }

        [HtmlAttributeName("bc-size")]
        public Size Size { get; set; }

        [HtmlAttributeName("bc-required")]
        public bool IsRequired { get; set; }
    }
}