using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Localization;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Datatable
{
    [GenerateId("datatable_", true)]
    [OutputElementHint("table")]
    [ContextClass]
    public class DatatableTagHelper : MeconsTagHelperBase
    {
        private Dictionary<string, string> _jsPlaceholders = new Dictionary<string, string>();
        private const string AjaxUrlAttributeName = "bc-ajax-url";
        private const string AjaxMethodAttributeName = "bc-ajax-method";
        private const string AjaxParamsAttributeName = "bc-ajax-all-params";
        private const string AjaxParamPrefix = "bc-ajax-param-";
        private const string AjaxHeadersAttributeName = "bc-ajax-all-headers";
        private const string AjaxHeaderPrefix = "bc-ajax-header-";
        private const string AjaxFilteringAttributeName = "bc-ajax-filtering";
        private const string AjaxPagingAttributeName = "bc-ajax-paging";
        private const string AjaxSortingAttributeName = "bc-ajax-sorting";
        private const string AjaxTimeoutAttributeName = "bc-ajax-timeout";
        private const string SortingAttributeName = "bc-sorting";
        private const string PagingAttributeName = "bc-paging";
        private const string PageSizeAttributeName = "bc-page-size";
        private const string HeightAttributeName = "bc-height";
        private const string HeaderAttributeName = "bc-header";
        private const string FooterAttributeName = "bc-footer";
        private const string SearchInputAttributeName = "bc-search-input";
        private const string SearchDelayAttributeName = "bc-search-delay";
        private IDictionary<string, string> _ajaxParams;
        private IDictionary<string, string> _ajaxHeaders;
        private bool _ajaxSorting;
        private bool _ajaxPaging;

        [HtmlAttributeName("bc-sorting")]
        public bool IsSortable { get; set; }

        [HtmlAttributeName("bc-paging")]
        public bool IsPagable { get; set; }

        [HtmlAttributeName("bc-page-size")]
        public int PageSize { get; set; } = 10;

        [HtmlAttributeName("bc-height")]
        public int Height { get; set; }

        [HtmlAttributeName("bc-header")]
        public bool HasHeader { get; set; } = true;

        [HtmlAttributeName("bc-footer")]
        public bool HasFooter { get; set; }

        [HtmlAttributeName("bc-search-input")]
        public string SearchInput { get; set; }

        [HtmlAttributeName("bc-search-delay")]
        public int SearchDelay { get; set; } = 400;

        [CopyToOutput]
        public string Class { get; set; }

        [HtmlAttributeNotBound]
        public List<ColumnTagHelper> Columns { get; set; } = new List<ColumnTagHelper>();

        [HtmlAttributeName("bc-ajax-url")]
        public string AjaxUrl { get; set; }

        [HtmlAttributeName("bc-ajax-method")]
        public HttpVerb AjaxMethod { get; set; } = HttpVerb.Post;

        [HtmlAttributeName("bc-ajax-all-params", DictionaryAttributePrefix = "bc-ajax-param-")]
        public IDictionary<string, string> AjaxParams
        {
            get
            {
                if (this._ajaxParams == null)
                    this._ajaxParams = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                return this._ajaxParams;
            }
            set { this._ajaxParams = value; }
        }

        [HtmlAttributeName("bc-ajax-all-headers", DictionaryAttributePrefix = "bc-ajax-header-")]
        public IDictionary<string, string> AjaxHeaders
        {
            get
            {
                if (this._ajaxHeaders == null)
                    this._ajaxHeaders = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                return this._ajaxHeaders;
            }
            set { this._ajaxHeaders = value; }
        }

        [HtmlAttributeName("bc-ajax-paging")]
        public bool AjaxPaging
        {
            get { return this._ajaxPaging; }
            set
            {
                this._ajaxPaging = value;
                this.IsPagable = true;
            }
        }

        [HtmlAttributeName("bc-ajax-sorting")]
        public bool AjaxSorting
        {
            get { return this._ajaxSorting; }
            set
            {
                this._ajaxSorting = value;
                this.IsSortable = true;
            }
        }

        [HtmlAttributeName("bc-ajax-filtering")]
        public bool AjaxFiltering { get; set; }

        [HtmlAttributeName("bc-ajax-timeout")]
        public int AjaxTimeout { get; set; } = 30000;

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            output.TagName = !string.IsNullOrEmpty(this.AjaxUrl) ? "div" : "table";
            output.AddCssClass("m-datatable");
            this.ProcessJavascript(output);
        }

        private void ProcessJavascript(TagHelperOutput output)
        {
            dynamic jObject = new JObject();
            jObject.data = new JObject();
            jObject.data.pageSize = this.PageSize;
            jObject.data.saveState = new JObject();
            jObject.data.saveState.cookie = true;
            jObject.data.saveState.webstorage = true;
            if (!string.IsNullOrEmpty(this.AjaxUrl))
            {
                jObject.data.type = "remote";
                jObject.data.serverPaging = this.AjaxPaging;
                jObject.data.serverFiltering = this.AjaxFiltering;
                jObject.data.serverSorting = this.AjaxSorting;
                jObject.data.source = new JObject();
                jObject.data.source.read = new JObject();
                jObject.data.source.read.url = this.AjaxUrl;
                jObject.data.source.read.method = this.AjaxMethod.GetEnumInfo().Name;
                jObject.data.source.read.timeout = this.AjaxTimeout;
                jObject.data.source.read.@params = new JObject();
                jObject.data.source.read.@params.headers = this.AjaxHeaders.ToJObject();
                jObject.data.source.read.@params.query = this.AjaxParams.ToJObject();
                if (this.Columns.Count > 0)
                {
                    jObject.data.source.read.@params.query.columns = this.GetColumnInformation();
                }
            }

            jObject.layout = new JObject();
            jObject.layout.theme = "default";
            jObject.layout.@class = this.Class;
            jObject.layout.scroll = true;
            jObject.layout.header = this.HasHeader;
            jObject.layout.footer = this.HasFooter;
            if (this.Height > 0)
            {
                jObject.layout.height = this.Height;
            }

            jObject.sortable = this.IsSortable;
            jObject.pagination = this.IsPagable;
            if (!string.IsNullOrEmpty(this.SearchInput))
            {
                this._jsPlaceholders.Add("[[JSP_SEARCH_INPUT]]", string.Concat("$('#", this.SearchInput, "')"));
                jObject.search = new JObject();
                jObject.search.input = "[[JSP_SEARCH_INPUT]]";
                jObject.search.delay = this.SearchDelay;
            }

            if (this.Columns.Count > 0)
            {
                jObject.columns = this.GetColumnConfiguration();
            }

            jObject.translate = new JObject();
            jObject.translate.records = new JObject();
            jObject.translate.records.processing = Resources.Datatable_Records_Processing;
            jObject.translate.records.noRecords = Resources.Datatable_Records_NoRecords;
            jObject.translate.toolbar = new JObject();
            jObject.translate.toolbar.pagination = new JObject();
            jObject.translate.toolbar.pagination.items = new JObject();
            jObject.translate.toolbar.pagination.items.@default = new JObject();
            jObject.translate.toolbar.pagination.items.@default.first = Resources.Datatable_Toolbar_Pagination_First;
            jObject.translate.toolbar.pagination.items.@default.prev = Resources.Datatable_Toolbar_Pagination_Previous;
            jObject.translate.toolbar.pagination.items.@default.next = Resources.Datatable_Toolbar_Pagination_Next;
            jObject.translate.toolbar.pagination.items.@default.last = Resources.Datatable_Toolbar_Pagination_Last;
            jObject.translate.toolbar.pagination.items.@default.more = Resources.Datatable_Toolbar_Pagination_More;
            jObject.translate.toolbar.pagination.items.@default.input = Resources.Datatable_Toolbar_Pagination_Input;
            jObject.translate.toolbar.pagination.items.@default.select = Resources.Datatable_Toolbar_Pagination_Select;
            jObject.translate.toolbar.pagination.items.info = Resources.Datatable_Toolbar_Pagination_Info;
            var str = this._jsPlaceholders.ReplaceJsPlaceholders(jObject as JObject);
            var str1 = string.Concat(new string[]
            {
                "<script type='text/javascript'>$(function() { $('#", base.Id, "').mDatatable(", str, "); });</script>"
            });
            output.PostElement.AppendHtml(str1);
        }

        public DatatableTagHelper()
        {
        }

        private JArray GetColumnConfiguration()
        {
            var jArray = new JArray();
            foreach (var column in this.Columns)
            {
                dynamic jObject = new JObject();
                var obj = jObject;
                var field = column.Field ?? string.Empty;
                obj.field = field;
                jObject.title = (!string.IsNullOrEmpty(column.Title) ? column.Title : "");
                jObject.sortable = column.IsSortable;
                jObject.textAlign = (column.Alignment != HorizontalAlignment.Default
                    ? column.Alignment.GetEnumInfo().Name
                    : "left");
                jObject.overflow = column.Overflow.GetEnumInfo().Name;
                if (column.Width > 0)
                {
                    jObject.width = column.Width;
                }

                if (!string.IsNullOrEmpty(column.Template))
                {
                    if (!column.Template.StartsWith("function"))
                    {
                        jObject.template = column.Template;
                    }
                    else
                    {
                        var str = string.Concat("[[JSP_COLUMN_", column.Field, "]]");
                        jObject.template = str;
                        this._jsPlaceholders.Add(str, column.Template);
                    }
                }

                jArray.Add(jObject);
            }

            return jArray;
        }

        private JArray GetColumnInformation()
        {
            var jArray = new JArray();
            foreach (var column in this.Columns)
            {
                dynamic jObject = new JObject();
                jObject.field = column.Field;
                jObject.title = column.Title;
                jObject.sortable = column.IsSortable;
                jObject.searchable = column.IsSearchable;
                jArray.Add(jObject);
            }

            return jArray;
        }
    }
}