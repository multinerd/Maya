using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [HtmlTargetElement("radio-list")]
    [ContextClass]
    public class RadioListTagHelper : CheckboxListTagHelper
    {
        protected override string ListName
        {
            get { return "radio"; }
        }
    }
}