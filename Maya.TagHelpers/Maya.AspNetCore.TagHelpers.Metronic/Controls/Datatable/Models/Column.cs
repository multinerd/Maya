namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Datatable.Models
{
    public class Column
    {
        public int Index { get; set; }

        public string Field { get; set; }

        public string Title { get; set; }

        public bool IsSortable { get; set; }

        public bool IsSearchable { get; set; }

        public override string ToString()
        {
            return string.Format("Column (Title: '{0}', Field: '{1}', Index: {2})", this.Title,
                this.Field, this.Index);
        }
    }
}