using System;
using System.Text;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder Prepend(this StringBuilder builder, string content)
        {
            return builder.Insert(0, content);
        }

        public static StringBuilder PrependLine(this StringBuilder builder, string content)
        {
            builder.Prepend(content);
            return builder.Prepend(Environment.NewLine);
        }

        public static StringBuilder WrapScript(this StringBuilder builder)
        {
            builder.PrependLine("<script type='text/javascript'>$(function() {");
            builder.AppendLine("});</script>");
            return builder;
        }
    }
}