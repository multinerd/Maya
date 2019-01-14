using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class TagHelperOutputExtensions
    {
        public static void AddAriaAttribute(this TagHelperOutput output, string name, object value)
        {
            output.MergeAttribute("aria-" + name, value);
        }

        public static void AddDataAttribute(this TagHelperOutput output, string name, object value)
        {
            output.MergeAttribute("data-" + name, value);
        }


        public static void AddCssClass(this TagHelperOutput output, IEnumerable<string> cssClasses)
        {
            if (output.Attributes.ContainsName("class") && output.Attributes["class"] != null)
            {
                var classes = output.Attributes["class"].Value.ToString().Split(' ').ToList();
                foreach (var str in cssClasses.Where(cssClass => !classes.Contains(cssClass)))
                    classes.Add(str);

                output.Attributes.SetAttribute("class", classes.Aggregate((s, s1) => s + " " + s1));
            }
            else if (output.Attributes.ContainsName("class"))
            {
                output.Attributes.SetAttribute("class", cssClasses.Aggregate((s, s1) => s + " " + s1));
            }
            else
            {
                output.Attributes.Add("class", cssClasses.Aggregate((s, s1) => s + " " + s1));
            }
        }

        public static void AddCssClass(this TagHelperOutput output, string cssClass)
        {
            output.AddCssClass(new string[1]
            {
                cssClass
            });
        }

        public static void RemoveCssClass(this TagHelperOutput output, string cssClass)
        {
            if (!output.Attributes.ContainsName("class"))
                return;

            var list = output.Attributes["class"].Value.ToString().Split(' ').ToList();
            list.Remove(cssClass);

            if (list.Count == 0)
                output.Attributes.RemoveAll("class");
            else
                output.Attributes.SetAttribute("class", list.Aggregate((s, s1) => s + " " + s1));
        }


        public static void AddCssStyle(this TagHelperOutput output, string name, string value)
        {
            if (output.Attributes.ContainsName("style"))
            {
                if (string.IsNullOrEmpty(output.Attributes["style"].Value.ToString()))
                {
                    output.Attributes.SetAttribute("style", name + ": " + value + ";");
                }
                else
                {
                    output.Attributes.SetAttribute("style",
                        (output.Attributes["style"].Value.ToString().EndsWith(";") ? " " : "; ") + name + ": " + value + ";");
                }
            }
            else
            {
                output.Attributes.Add("style", name + ": " + value + ";");
            }
        }


        public static async Task LoadChildContentAsync(this TagHelperOutput output)
        {
            var tagHelperContent = output.Content;
            tagHelperContent.SetHtmlContent(await output.GetChildContentAsync() ?? new DefaultTagHelperContent());
            tagHelperContent = null;
        }

        public static TagHelperContent ToTagHelperContent(this TagHelperOutput output)
        {
            var tagHelperContent = new DefaultTagHelperContent();
            tagHelperContent.AppendHtml(output.PreElement);
            
            var tagBuilder = new TagBuilder(output.TagName);
            foreach (var attribute in output.Attributes)
                tagBuilder.Attributes.Add(attribute.Name, attribute.Value?.ToString());
            
            if (output.TagMode == TagMode.SelfClosing)
            {
                tagBuilder.TagRenderMode = TagRenderMode.SelfClosing;
                tagHelperContent.AppendHtml(tagBuilder);
            }
            else
            {
                tagBuilder.TagRenderMode = TagRenderMode.StartTag;
                tagHelperContent.AppendHtml(tagBuilder);
                tagHelperContent.AppendHtml(output.PreContent);
                tagHelperContent.AppendHtml(output.Content);
                tagHelperContent.AppendHtml(output.PostContent);
                if (output.TagMode == TagMode.StartTagAndEndTag)
                    tagHelperContent.AppendHtml("</" + output.TagName + ">");
            }

            tagHelperContent.AppendHtml(output.PostElement);
            return tagHelperContent;
        }


        public static void WrapInside(this TagHelperOutput output, TagBuilder builder)
        {
            builder.TagRenderMode = TagRenderMode.StartTag;
            output.WrapInside(builder, new TagBuilder(builder.TagName)
            {
                TagRenderMode = TagRenderMode.EndTag
            });
        }

        public static void WrapInside(this TagHelperOutput output, IHtmlContent startTag, IHtmlContent endTag)
        {
            output.PreElement.AppendHtml(startTag);
            output.PostElement.Prepend(endTag);
        }

        public static void WrapInside(this TagHelperOutput output, string startTag, string endTag)
        {
            output.PreElement.Append(startTag);
            output.PostElement.Prepend(endTag);
        }


        public static void WrapOutside(this TagHelperOutput output, TagBuilder builder)
        {
            builder.TagRenderMode = TagRenderMode.StartTag;
            output.WrapOutside(builder, new TagBuilder(builder.TagName)
            {
                TagRenderMode = TagRenderMode.EndTag
            });
        }

        public static void WrapOutside(this TagHelperOutput output, IHtmlContent startTag, IHtmlContent endTag)
        {
            output.PreElement.Prepend(startTag);
            output.PostElement.AppendHtml(endTag);
        }

        public static void WrapOutside(this TagHelperOutput output, string startTag, string endTag)
        {
            output.PreElement.Prepend(startTag);
            output.PostElement.Append(endTag);
        }


        public static void WrapContentInside(this TagHelperOutput output, TagBuilder builder)
        {
            builder.TagRenderMode = TagRenderMode.StartTag;
            output.WrapContentInside(builder, new TagBuilder(builder.TagName)
            {
                TagRenderMode = TagRenderMode.EndTag
            });
        }

        public static void WrapContentInside(this TagHelperOutput output, IHtmlContent startTag, IHtmlContent endTag)
        {
            output.PreContent.AppendHtml(startTag);
            output.PostContent.Prepend(endTag);
        }

        public static void WrapContentInside(this TagHelperOutput output, string startTag, string endTag)
        {
            output.PreContent.Append(startTag);
            output.PostContent.Prepend(endTag);
        }


        public static void WrapContentOutside(this TagHelperOutput output, TagBuilder builder)
        {
            builder.TagRenderMode = TagRenderMode.StartTag;
            output.WrapContentOutside(builder, new TagBuilder(builder.TagName)
            {
                TagRenderMode = TagRenderMode.EndTag
            });
        }

        public static void WrapContentOutside(this TagHelperOutput output, IHtmlContent startTag, IHtmlContent endTag)
        {
            output.PreContent.Prepend(startTag);
            output.PostContent.AppendHtml(endTag);
        }

        public static void WrapContentOutside(this TagHelperOutput output, string startTag, string endTag)
        {
            output.PreContent.Prepend(startTag);
            output.PostContent.Append(endTag);
        }


        public static void WrapHtmlInside(this TagHelperOutput output, string startTag, string endTag)
        {
            output.PreElement.AppendHtml(startTag);
            output.PostElement.PrependHtml(endTag);
        }


        public static void WrapHtmlOutside(this TagHelperOutput output, string startTag, string endTag)
        {
            output.PreElement.PrependHtml(startTag);
            output.PostElement.AppendHtml(endTag);
        }


        public static void WrapHtmlContentInside(this TagHelperOutput output, string startTag, string endTag)
        {
            output.PreContent.AppendHtml(startTag);
            output.PostContent.PrependHtml(endTag);
        }


        public static void WrapHtmlContentOutside(this TagHelperOutput output, string startTag, string endTag)
        {
            output.PreContent.PrependHtml(startTag);
            output.PostContent.AppendHtml(endTag);
        }


        public static void MergeAttribute(this TagHelperOutput output, string key, object value)
        {
            output.Attributes.SetAttribute(key, value);
        }
    }
}