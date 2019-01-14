using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class StringExtensions
    {
        public static List<char> IllegalCharacters { get; } = new List<char>()
        {
            '.',
            '-',
            ' ',
            '@',
            '+'
        };

        public static string FirstLetterToUpper(this string value)
        {
            if (value == null)
                return string.Empty;

            if (value.Length <= 1)
                return value.ToUpper();

            var upper = char.ToUpper(value[0]);
            return string.Concat(upper.ToString(), value.Substring(1));
        }

        public static string TransformClassname(this string value)
        {
            if (value == null)
                return string.Empty;

            value = IllegalCharacters.Aggregate(value, (current, c) => current.Replace(c, '\u005F'));
            value = string.Join(string.Empty, value.Split('\u005F').Select(s => s.FirstLetterToUpper()));

            if (int.TryParse(value.Substring(0, 1), out _))
                value = string.Concat("_", value);

            return value;
        }

        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(IEnumerable<TKey> keys, IEnumerable<TValue> values)
        {
            var dic = new Dictionary<TKey, TValue>();
            keys.Each<TKey>((x, i) => dic.Add(x, values.ElementAt(i)));
            return dic;
        }

        public static void Each<T>(this IEnumerable collection, Action<T, int> a)
        {
            var num = 0;
            foreach (T obj in collection)
                a(obj, num++);
        }
    }
}