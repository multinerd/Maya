using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Newtonsoft.Json;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Datatable.Models
{
    public class DatatableAjaxResponseMeta
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("sort")]
        public string Sort { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("perpage")]
        public int PerPage { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        public DatatableAjaxResponseMeta()
        {
        }

        public DatatableAjaxResponseMeta(int total, DatatableAjaxRequest request)
        {
            this.Total = total;
            this.PerPage = request.Pagination.PerPage;
            this.Page = request.Pagination.Page;
            this.Pages = total / request.Pagination.PerPage;
            this.Field = request.Sorting.Field;
            this.Sort = request.Sorting.Direction == SortDirection.Ascending ? "asc" : "desc";
        }
    }
}