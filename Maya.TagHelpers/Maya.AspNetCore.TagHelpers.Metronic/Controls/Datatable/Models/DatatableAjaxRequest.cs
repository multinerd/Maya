using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Datatable.Models
{
    public class DatatableAjaxRequest
    {
        public DatatableAjaxRequest(IFormCollection data)
        {
            this.Pagination = new Pagination()
            {
                Page = data.ContainsKey("datatable[pagination][page]")
                    ? Convert.ToInt32(data["datatable[pagination][page]"])
                    : -1,
                Pages = data.ContainsKey("datatable[pagination][pages]")
                    ? Convert.ToInt32(data["datatable[pagination][pages]"])
                    : -1,
                PerPage = data.ContainsKey("datatable[pagination][perpage]")
                    ? Convert.ToInt32(data["datatable[pagination][perpage]"])
                    : -1,
                Total = data.ContainsKey("datatable[pagination][total]")
                    ? Convert.ToInt32(data["datatable[pagination][total]"])
                    : -1,
                Direction = !data.ContainsKey("datatable[pagination][sort]") ||
                            !(data["datatable[pagination][sort]"] == "asc")
                    ? SortDirection.Descending
                    : SortDirection.Ascending,
                Field = data.ContainsKey("datatable[pagination][total]")
                    ? data["datatable[pagination][total]"].ToString()
                    : string.Empty
            };
            this.Sorting = new Sorting()
            {
                Direction = !data.ContainsKey("datatable[sort][sort]") || !(data["datatable[sort][sort]"] == "asc")
                    ? SortDirection.Descending
                    : SortDirection.Ascending,
                Field = data.ContainsKey("datatable[sort][field]")
                    ? data["datatable[sort][field]"].ToString()
                    : string.Empty
            };
            this.Params = data
                .Where(d =>
                    Regex.IsMatch(d.Key, "(datatable\\[query\\]\\[[a-zA-Z0-9-]*\\]$)"))
                .ToDictionary(
                    k =>
                        Regex.Match(k.Key, "\\[[a-zA-Z0-9-]*\\]$").Value.Replace("[", "").Replace("]", ""),
                    v => v.Value.ToString());
            var regexHeaders = new Regex("(?!datatable)(?!\\[headers\\])\\[[a-zA-Z0-9-]*]");
            this.Headers = data
                .Where(d => d.Key.StartsWith("datatable[headers]"))
                .ToDictionary(
                    k =>
                        regexHeaders.Match(k.Key).Value.Replace("[", "").Replace("]", ""),
                    v => v.Value.ToString());
            this.GeneralSearch = data.ContainsKey("datatable[query][generalSearch]")
                ? data["datatable[query][generalSearch]"].ToString()
                : string.Empty;
            for (var i = 0;
                i <= data.Where(
                    d =>
                    {
                        if (d.Key.StartsWith("datatable[query][columns]"))
                            return d.Key.EndsWith("[field]");
                        return false;
                    }).Count() - 1;
                i++)
            {
                var columns = this.Columns;
                var column = new Column();
                column.Index = i;
                var keyValuePair =
                    data.First(
                        d =>
                            d.Key == string.Format("datatable[query][columns][{0}][field]", i));
                column.Field = keyValuePair.Value;
                keyValuePair = data.First(
                    d =>
                        d.Key == string.Format("datatable[query][columns][{0}][title]", i));
                column.Title = keyValuePair.Value;
                keyValuePair = data.First(
                    d =>
                        d.Key == string.Format("datatable[query][columns][{0}][sortable]", i));
                column.IsSortable = Convert.ToBoolean(keyValuePair.Value);
                keyValuePair = data.First(
                    d =>
                        d.Key == string.Format("datatable[query][columns][{0}][searchable]", i));
                column.IsSearchable = Convert.ToBoolean(keyValuePair.Value);
                columns.Add(column);
            }
        }

        public Pagination Pagination { get; set; }

        public Sorting Sorting { get; set; }

        public string GeneralSearch { get; set; }

        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>();

        public List<Column> Columns { get; set; } = new List<Column>();
    }
}