using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Tabs
{
    [HtmlTargetElement("tabs-pane", ParentTag = "tabs")]
    [GenerateId("pane_", true)]
    public class TabsPaneTagHelper : MeconsTagHelperBase
    {
        [Mandatory]
        [HtmlAttributeName("bc-header")]
        public string Header { get; set; }

        [HtmlAttributeName("bc-active")]
        public bool IsActive { get; set; }

        [Context]
        [HtmlAttributeNotBound]
        public TabsTagHelper TabsContext { get; set; }

        [HtmlAttributeNotBound]
        public TagHelperContent Content { get; set; }

        public override void Init(TagHelperContext context)
        {
            base.Init(context);
            this.TabsContext.Panes.Add(this);
        }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            this.Content = await output.GetChildContentAsync();
            output.SuppressOutput();
        }
    }
}