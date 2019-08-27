using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Datatable.Models
{
    public class DatatableAjaxResponse<T> where T : class
    {
        [JsonProperty("meta")]
        public DatatableAjaxResponseMeta Meta { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }

        public DatatableAjaxResponse()
        {
        }

        public DatatableAjaxResponse(int total, DatatableAjaxRequest request)
        {
            this.Meta = new DatatableAjaxResponseMeta(total, request);
        }
    }
}