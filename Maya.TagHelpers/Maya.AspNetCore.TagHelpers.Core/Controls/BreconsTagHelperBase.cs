using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Controls
{
    public abstract class BreconsTagHelperBase : TagHelper
    {
        protected const string AttributePrefix = "bc-";
        private const string DisableBreconsAttributeName = "disable-brecons";

        [HtmlAttributeName("disable-brecons")]
        [HtmlAttributeNotBound]
        [HtmlAttributeMinimizable]
        public bool DisableBootstrap { get; set; }

        [HtmlAttributeNotBound]
        public string GeneratedId { get; set; }

        [CopyToOutput]
        public string Id { get; set; }

        [CopyToOutput]
        public string Name { get; set; }

        [HtmlAttributeNotBound]
        public TagHelperOutput Output { get; set; }

        [HtmlAttributeNotBound]
        public IActionContextAccessor ActionContextAccessor { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }


        public override void Init(TagHelperContext context)
        {
            ContextAttribute.SetContexts(this, context);
            ContextClassAttribute.SetContext(this, context);
            HtmlAttributeMinimizableAttribute.FillMinimizableAttributes(this, context);
            ConvertVirtualUrlAttribute.ConvertUrls(this, this.ActionContextAccessor);
        }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.Output = output;
            if (!this.DisableBootstrap)
            {
                CopyToOutputAttribute.CopyPropertiesToOutput(this, output);
                MandatoryAttribute.CheckProperties(this);
                GenerateIdAttribute.CopyIdentifier(this);
                this.RenderProcess(context, output);
                GenerateIdAttribute.RenderIdentifier(this, output);
                this.RemoveMinimizableAttributes(output);
            }
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            this.Output = output;
            if (!this.DisableBootstrap)
            {
                CopyToOutputAttribute.CopyPropertiesToOutput(this, output);
                MandatoryAttribute.CheckProperties(this);
                GenerateIdAttribute.CopyIdentifier(this);
                await this.RenderProcessAsync(context, output);
                GenerateIdAttribute.RenderIdentifier(this, output);
                this.RemoveMinimizableAttributes(output);
            }
        }


        protected virtual void RenderProcess(TagHelperContext context, TagHelperOutput output)
        {
        }

        protected virtual async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            this.RenderProcess(context, output);
        }


        private void RemoveMinimizableAttributes(TagHelperOutput output)
        {
            output.Attributes.RemoveAll(this.GetType().GetProperties()
                .Where(pI => pI.GetCustomAttribute<HtmlAttributeMinimizableAttribute>() != null)
                .Select(pI => pI.GetHtmlAttributeName())
                .ToArray());
        }
    }
}