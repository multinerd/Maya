using Maya.AspNetCore.TagHelpers.Core.Enumerations;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Datatable.Models
{
    public class Sorting
    {
        public SortDirection Direction { get; set; }

        public string Field { get; set; }
    }
}