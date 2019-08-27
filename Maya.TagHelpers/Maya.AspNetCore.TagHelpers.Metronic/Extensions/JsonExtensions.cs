using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Maya.AspNetCore.TagHelpers.Metronic.Extensions
{
    internal static class JsonExtensions
    {
        internal static JObject ToJObject(this IDictionary<string, string> dictionary)
        {
            var jobject = new JObject();
            foreach (var keyValuePair in dictionary
            )
                jobject.Add(new JProperty(keyValuePair.Key, keyValuePair.Value));
            return jobject;
        }

        internal static string ReplaceJsPlaceholders(
            this IDictionary<string, string> placeholders,
            JObject json)
        {
            var str = JsonConvert.SerializeObject(json, Formatting.None);
            foreach (var placeholder in
                placeholders)
                str = str.Replace("\"" + placeholder.Key + "\"", placeholder.Value);
            return str;
        }
    }
}