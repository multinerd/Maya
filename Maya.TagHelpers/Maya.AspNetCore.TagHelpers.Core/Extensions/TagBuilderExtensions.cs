using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class TagBuilderExtensions
    {
        public static string GetString(this TagBuilder content)
        {
            var stringWriter = new StringWriter();
            content.WriteTo(stringWriter, HtmlEncoder.Default);
            return stringWriter.ToString();
        }
    }
}