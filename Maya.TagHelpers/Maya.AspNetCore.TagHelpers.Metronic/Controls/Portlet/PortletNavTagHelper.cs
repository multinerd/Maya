using System.Collections.Generic;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Portlet
{
    [ContextClass]
    [HtmlTargetElement("portlet-nav", ParentTag = "portlet-header")]
    [RestrictChildren("portlet-link", new string[] { })]
    [OutputElementHint("div")]
    public class PortletNavTagHelper : MeconsTagHelperBase
    {
        [Context]
        [HtmlAttributeNotBound]
        public PortletHeaderTagHelper HeaderContext { get; set; }

        [HtmlAttributeNotBound]
        public List<PortletNavLinkTagHelper> Items { get; set; } = new List<PortletNavLinkTagHelper>();

        public override void Init(TagHelperContext context)
        {
            base.Init(context);
            this.HeaderContext.NavigationContext = this;
        }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.SuppressOutput();
        }

        public IHtmlContent Build()
        {
            var content = this.Output.GetChildContentAsync().Result.GetContent();
            var tagBuilder1 = new TagBuilder("ul");
            tagBuilder1.AddCssClass("m-portlet__nav");
            tagBuilder1.InnerHtml.AppendHtml(content);
            var tagBuilder2 = new TagBuilder("div");
            tagBuilder2.AddCssClass("m-portlet__head-tools");
            tagBuilder2.InnerHtml.AppendHtml(tagBuilder1);
            return tagBuilder2;
        }
    }
}