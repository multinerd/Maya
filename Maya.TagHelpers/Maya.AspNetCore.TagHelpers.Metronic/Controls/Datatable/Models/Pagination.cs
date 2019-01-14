using Maya.AspNetCore.TagHelpers.Core.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Datatable.Models
{
    public class Pagination
    {
        public int Page { get; set; }

        public int Pages { get; set; }

        public int PerPage { get; set; }

        public int Total { get; set; }

        public SortDirection Direction { get; set; }

        public string Field { get; set; }
    }
}