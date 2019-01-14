using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class TagHelperContentExtensions
    {
        public static void Prepend(this TagHelperContent content, string value)
        {
            if (content.IsEmptyOrWhiteSpace)
                content.SetContent(value);
            else
                content.SetContent(value + content.GetContent());
        }

        public static void Prepend(this TagHelperContent content, IHtmlContent value)
        {
            if (content.IsEmptyOrWhiteSpace)
            {
                content.SetHtmlContent(value);
                content.AppendLine();
            }
            else
            {
                var content1 = content.GetContent();
                content.SetHtmlContent(value);
                content.AppendHtml(content1);
            }
        }

        public static void Prepend(this TagHelperContent content, TagHelperOutput output)
        {
            content.Prepend(output.ToTagHelperContent());
        }

        public static void PrependHtml(this TagHelperContent content, string value)
        {
            if (content.IsEmptyOrWhiteSpace)
                content.SetHtmlContent(value);
            else
                content.SetHtmlContent(value + content.GetContent());
        }

        public static void Wrap(this TagHelperContent content, TagBuilder builder)
        {
            builder.TagRenderMode = TagRenderMode.StartTag;
            Wrap(content, builder, new TagBuilder(builder.TagName)
            {
                TagRenderMode = TagRenderMode.EndTag
            });
        }

        public static void Wrap(TagHelperContent content, string contentStart, string contentEnd)
        {
            content.Prepend(contentStart);
            content.Append(contentEnd);
        }

        public static void Wrap(TagHelperContent content, IHtmlContent contentStart, IHtmlContent contentEnd)
        {
            content.Prepend(contentStart);
            content.AppendHtml(contentEnd);
        }

        public static void WrapHtml(TagHelperContent content, string contentStart, string contentEnd)
        {
            content.PrependHtml(contentStart);
            content.AppendHtml(contentEnd);
        }
    }
}