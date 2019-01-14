using System;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Datatable
{
    [HtmlTargetElement("datatable-column", ParentTag = "datatable")]
    public class ColumnTagHelper : MeconsTagHelperBase
    {
        private const string FieldAttributeName = "bc-field";
        private const string TitleAttributeName = "bc-title";
        private const string SortingAttributeName = "bc-sorting";
        private const string SearchingAttributeName = "bc-searching";
        private const string WidthAttributeName = "bc-width";
        private const string AlignmentAttributeName = "bc-alignment";
        private const string OverflowAttributeName = "bc-overflow";
        private const string TemplateAttributeName = "bc-template";

        [Mandatory]
        [HtmlAttributeName("bc-field")]
        public string Field { get; set; }

        [HtmlAttributeName("bc-title")]
        public string Title { get; set; }

        [HtmlAttributeName("bc-sorting")]
        public bool IsSortable { get; set; } = true;

        [HtmlAttributeName("bc-searching")]
        public bool IsSearchable { get; set; } = true;

        [HtmlAttributeName("bc-width")]
        public int Width { get; set; }

        [HtmlAttributeName("bc-alignment")]
        public HorizontalAlignment Alignment { get; set; }

        [HtmlAttributeName("bc-overflow")]
        public Overflow Overflow { get; set; }

        [HtmlAttributeName("bc-template")]
        public string Template { get; set; }

        [Context]
        public DatatableTagHelper DatatableContext { get; set; }

        public override void Init(TagHelperContext context)
        {
            base.Init(context);
            this.DatatableContext.Columns.Add(this);
        }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(this.Template))
                this.Template = (await output.GetChildContentAsync()).GetContent()
                    .Replace(Environment.NewLine, string.Empty).Replace("<!--", string.Empty)
                    .Replace("-->", string.Empty).TrimStart(' ').TrimEnd(' ');
            output.SuppressOutput();
        }
    }
}