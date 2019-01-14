using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class TagHelperExtensions
    {
        public static async Task RunTagHelperAsync(this ITagHelper tagHelper, TagHelperExtensions.Options options)
        {
            if (options.Context == null)
                options.Context = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("N"));

            if (options.Attributes == null)
                options.Attributes = new List<TagHelperAttribute>();

            if (options.TagName == null)
                options.TagName = tagHelper.GetTagName();

            var output = new TagHelperOutput(options.TagName, new TagHelperAttributeList(options.Attributes), (b, e) => Task.Factory.StartNew(() => options.Content))
            {
                TagMode = options.TagMode
            };

            if (options.InitTagHelper)
                tagHelper.Init(options.Context);

            await tagHelper.ProcessAsync(options.Context, output);
        }

        public static async Task<TagHelperContent> ToTagHelperContentAsync(this IEnumerable<ITagHelper> tagHelpers, Options options)
        {
            var tagHelperList = tagHelpers.ToList();
            if (options.Context == null)
                options.Context = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("N"));

            if (options.Attributes == null)
                options.Attributes = new List<TagHelperAttribute>();

            if (options.TagName == null)
                options.TagName = tagHelperList.GetTagName();

            var output = new TagHelperOutput(options.TagName, new TagHelperAttributeList(options.Attributes), (b, e) => Task.Factory.StartNew(() => options.Content))
            {
                TagMode = options.TagMode
            };

            if (options.InitTagHelper)
                tagHelperList.ForEach(tH => tH.Init(options.Context));

            foreach (var tagHelperComponent in tagHelperList)
                await tagHelperComponent.ProcessAsync(options.Context, output);

            if (options.Content != null && !output.IsContentModified)
                output.Content.SetHtmlContent(options.Content);

            return output.ToTagHelperContent();
        }

        public static string GetTagName(this ITagHelper tagHelper)
        {
            var tag = tagHelper.GetType().GetTypeInfo().GetCustomAttributes<HtmlTargetElementAttribute>().FirstOrDefault(a => a.Tag != "*")?.Tag;
            if (tag == null)
                tag = Regex.Replace(tagHelper.GetType().Name.Replace("TagHelper", ""), "([A-Z])", "-$1").Trim('-').ToLower();

            return tag;
        }

        public static string GetTagName(this IEnumerable<ITagHelper> tagHelpers)
        {
            var source = tagHelpers.ToList();
            var tagHelper = source.OrderBy(tH => tH.Order).FirstOrDefault(tH => tH.GetType().GetTypeInfo().HasCustomAttribute<HtmlTargetElementAttribute>());

            var tag = tagHelper?.GetType().GetTypeInfo().GetCustomAttributes<HtmlTargetElementAttribute>().FirstOrDefault(a => a.Tag != "*")?.Tag;
            if (tag == null)
                tag = Regex.Replace(source.Min(tH => tH.Order).GetType().Name.Replace("TagHelper", ""), "([A-Z])", "-$1").Trim('-').ToLower();

            return tag;
        }


        public class Options
        {
            public string TagName { get; set; }

            public TagMode TagMode { get; set; }

            public TagHelperContent Content { get; set; }

            public TagHelperContext Context { get; set; }

            public IEnumerable<TagHelperAttribute> Attributes { get; set; }

            public bool InitTagHelper { get; set; } = true;
        }
    }
}