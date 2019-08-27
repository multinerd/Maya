using System.Linq;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.GridSystem
{
    [OutputElementHint("div")]
    [HtmlTargetElement("column", ParentTag = "row")]
    public class ColumnTagHelper : MeconsTagHelperBase
    {
        private const string XsSizeAttributeName = "xs-size";
        private const string SmSizeAttributeName = "sm-size";
        private const string MdSizeAttributeName = "md-size";
        private const string LgSizeAttributeName = "lg-size";
        private const string XlSizeAttributeName = "xl-size";
        private const string XsOffsetAttributeName = "xs-offset";
        private const string SmOffsetAttributeName = "sm-offset";
        private const string MdOffsetAttributeName = "md-offset";
        private const string LgOffsetAttributeName = "lg-offset";
        private const string XlOffsetAttributeName = "xl-offset";
        private const string XsRenderAttributeName = "xs-render";
        private const string SmRenderAttributeName = "sm-render";
        private const string MdRenderAttributeName = "md-render";
        private const string LgRenderAttributeName = "lg-render";
        private const string XlRenderAttributeName = "xl-render";
        private const string XsOrderAttributeName = "xs-order";
        private const string SmOrderAttributeName = "sm-order";
        private const string MdOrderAttributeName = "md-order";
        private const string LgOrderAttributeName = "lg-order";
        private const string XlOrderAttributeName = "xl-order";
        private const string VerticalAlignmentAttributeName = "vertical-alignment";

        [HtmlAttributeName("xs-size")]
        public int XsSize { get; set; }

        [HtmlAttributeName("sm-size")]
        public int SmSize { get; set; }

        [HtmlAttributeName("md-size")]
        public int MdSize { get; set; }

        [HtmlAttributeName("lg-size")]
        public int LgSize { get; set; }

        [HtmlAttributeName("xl-size")]
        public int XlSize { get; set; }

        [HtmlAttributeName("xs-offset")]
        public int XsOffset { get; set; }

        [HtmlAttributeName("sm-offset")]
        public int SmOffset { get; set; }

        [HtmlAttributeName("md-offset")]
        public int MdOffset { get; set; }

        [HtmlAttributeName("lg-offset")]
        public int LgOffset { get; set; }

        [HtmlAttributeName("xl-offset")]
        public int XlOffset { get; set; }

        [HtmlAttributeName("xs-render")]
        public GridColumnRenderType XsRender { get; set; } = GridColumnRenderType.Fixed;

        [HtmlAttributeName("sm-render")]
        public GridColumnRenderType SmRender { get; set; } = GridColumnRenderType.Fixed;

        [HtmlAttributeName("md-render")]
        public GridColumnRenderType MdRender { get; set; } = GridColumnRenderType.Fixed;

        [HtmlAttributeName("lg-render")]
        public GridColumnRenderType LgRender { get; set; } = GridColumnRenderType.Fixed;

        [HtmlAttributeName("xl-render")]
        public GridColumnRenderType XlRender { get; set; } = GridColumnRenderType.Fixed;

        [HtmlAttributeName("xs-order")]
        public int XsOrder { get; set; }

        [HtmlAttributeName("sm-order")]
        public int SmOrder { get; set; }

        [HtmlAttributeName("md-order")]
        public int MdOrder { get; set; }

        [HtmlAttributeName("lg-order")]
        public int LgOrder { get; set; }

        [HtmlAttributeName("xl-order")]
        public int XlOrder { get; set; }

        [HtmlAttributeName("vertical-alignment")]
        public VerticalAlignment VerticalAlignment { get; set; }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = "div";
            this.ProceedSize(output);
            this.ProcessOffset(output);
            this.ProceedOrder(output);
            switch (this.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    output.AddCssClass("align-self-start");
                    break;
                case VerticalAlignment.Middle:
                    output.AddCssClass("align-self-center");
                    break;
                case VerticalAlignment.Bottom:
                    output.AddCssClass("align-self-end");
                    break;
            }
        }

        private void ProceedSize(TagHelperOutput output)
        {
            switch (this.XsRender)
            {
                case GridColumnRenderType.Dynamic:
                    output.AddCssClass("col-auto");
                    break;
                case GridColumnRenderType.Auto:
                    output.AddCssClass("col");
                    break;
                case GridColumnRenderType.Fixed:
                    if (this.XsSize > 0 && this.XsSize <= 12)
                    {
                        output.AddCssClass(string.Format("col-{0}", this.XsSize));
                        break;
                    }

                    break;
            }

            switch (this.SmRender)
            {
                case GridColumnRenderType.Dynamic:
                    output.AddCssClass("col-sm-auto");
                    break;
                case GridColumnRenderType.Auto:
                    output.AddCssClass("col-sm");
                    break;
                case GridColumnRenderType.Fixed:
                    if (this.SmSize > 0 && this.SmSize <= 12)
                    {
                        output.AddCssClass(string.Format("col-sm-{0}", this.SmSize));
                        break;
                    }

                    break;
            }

            switch (this.MdRender)
            {
                case GridColumnRenderType.Dynamic:
                    output.AddCssClass("col-md-auto");
                    break;
                case GridColumnRenderType.Auto:
                    output.AddCssClass("col-md");
                    break;
                case GridColumnRenderType.Fixed:
                    if (this.MdSize > 0 && this.MdSize <= 12)
                    {
                        output.AddCssClass(string.Format("col-md-{0}", this.MdSize));
                        break;
                    }

                    break;
            }

            switch (this.LgRender)
            {
                case GridColumnRenderType.Dynamic:
                    output.AddCssClass("col-lg-auto");
                    break;
                case GridColumnRenderType.Auto:
                    output.AddCssClass("col-lg");
                    break;
                case GridColumnRenderType.Fixed:
                    if (this.LgSize > 0 && this.LgSize <= 12)
                    {
                        output.AddCssClass(string.Format("col-lg-{0}", this.LgSize));
                        break;
                    }

                    break;
            }

            switch (this.XlRender)
            {
                case GridColumnRenderType.Dynamic:
                    output.AddCssClass("col-xl-auto");
                    break;
                case GridColumnRenderType.Auto:
                    output.AddCssClass("col-xl");
                    break;
                case GridColumnRenderType.Fixed:
                    if (this.XlSize > 0 && this.XlSize <= 12)
                    {
                        output.AddCssClass(string.Format("col-xl-{0}", this.XlSize));
                        break;
                    }

                    break;
            }

            var tagHelperAttribute =
                output.Attributes.FirstOrDefault(
                    a => a.Name == "class");
            if (tagHelperAttribute != null && tagHelperAttribute.Value.ToString().Contains("col"))
                return;
            output.AddCssClass("col");
        }

        private void ProcessOffset(TagHelperOutput output)
        {
            if (this.XsOffset > 0 && this.XsOffset <= 12)
                output.AddCssClass(string.Format("offset-{0}", this.XsOffset));
            if (this.SmOffset > 0 && this.SmOffset <= 12)
                output.AddCssClass(string.Format("offset-sm-{0}", this.SmOffset));
            if (this.MdOffset > 0 && this.MdOffset <= 12)
                output.AddCssClass(string.Format("offset-md-{0}", this.MdOffset));
            if (this.LgOffset > 0 && this.LgOffset <= 12)
                output.AddCssClass(string.Format("offset-lg-{0}", this.LgOffset));
            if (this.XlOffset <= 0 || this.XlOffset > 12)
                return;
            output.AddCssClass(string.Format("offset-xl-{0}", this.XlOffset));
        }

        private void ProceedOrder(TagHelperOutput output)
        {
            if (this.XsOrder > 0 && this.XsOrder <= 12)
                output.AddCssClass(string.Format("order-{0}", this.XsOrder));
            if (this.SmOrder > 0 && this.SmOrder <= 12)
                output.AddCssClass(string.Format("order-sm-{0}", this.SmOrder));
            if (this.MdOrder > 0 && this.MdOrder <= 12)
                output.AddCssClass(string.Format("order-md-{0}", this.MdOrder));
            if (this.LgOrder > 0 && this.LgOrder <= 12)
                output.AddCssClass(string.Format("order-lg-{0}", this.LgOrder));
            if (this.XlOrder <= 0 || this.XlOrder > 12)
                return;
            output.AddCssClass(string.Format("order-xl-{0}", this.XlOrder));
        }
    }
}