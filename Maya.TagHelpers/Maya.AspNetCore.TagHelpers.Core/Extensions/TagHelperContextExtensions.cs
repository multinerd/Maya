using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class TagHelperContextExtensions
    {
        public static bool HasContextItem<T>(this TagHelperContext context)
        {
            return context.HasContextItem<T>(true);
        }

        public static bool HasContextItem<T>(this TagHelperContext context, bool useInherited)
        {
            return context.HasContextItem(typeof(T), useInherited);
        }

        public static bool HasContextItem<T>(this TagHelperContext context, string key)
        {
            return context.HasContextItem(typeof(T), key);
        }

        public static bool HasContextItem(this TagHelperContext context, Type type)
        {
            return context.HasContextItem(type, true);
        }

        public static bool HasContextItem(this TagHelperContext context, Type type, bool useInherited)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var contextItem = context.GetContextItem(type, useInherited);
            if (contextItem != null)
                return type.IsInstanceOfType(contextItem);

            return false;
        }

        public static bool HasContextItem(this TagHelperContext context, Type type, string key)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (context.Items.ContainsKey(key))
                return type.IsInstanceOfType(context.Items[key]);

            return false;
        }


        public static T GetContextItem<T>(this TagHelperContext context) where T : class
        {
            return context.GetContextItem<T>(true);
        }

        public static T GetContextItem<T>(this TagHelperContext context, bool useInherited) where T : class
        {
            return context.GetContextItem(typeof(T), useInherited) as T;
        }

        public static T GetContextItem<T>(this TagHelperContext context, string key) where T : class
        {
            return context.GetContextItem(typeof(T), key) as T;
        }

        public static object GetContextItem(this TagHelperContext context, Type type)
        {
            return context.GetContextItem(type, true);
        }

        public static object GetContextItem(this TagHelperContext context, Type type, string key)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (!context.Items.ContainsKey(key) || !type.IsInstanceOfType(context.Items[key]))
                return null;

            return context.Items[key];
        }

        public static object GetContextItem(this TagHelperContext context, Type type, bool useInherit)
        {
            if (context.Items.ContainsKey(type))
                return context.Items
                    .First(
                        kVP => kVP.Key.Equals(type)).Value;
            if (useInherit)
                return context.Items.FirstOrDefault(
                    kVP =>
                    {
                        if ((object) (kVP.Key as Type) != null)
                            return type.IsAssignableFrom((Type) kVP.Key);
                        return false;
                    }).Value;
            return null;
        }


        public static void SetContextItem<T>(this TagHelperContext context, T contextItem)
        {
            context.SetContextItem(typeof(T), contextItem);
        }

        public static void SetContextItem(this TagHelperContext context, Type type, object contextItem)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (context.Items.ContainsKey(type))
                context.Items[type] = contextItem;
            else
                context.Items.Add(type, contextItem);
        }

        public static void SetContextItem(this TagHelperContext context, string key, object contextItem)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (context.Items.ContainsKey(key))
                context.Items[key] = contextItem;
            else
                context.Items.Add(key, contextItem);
        }


        public static void RemoveContextItem<T>(this TagHelperContext context)
        {
            context.RemoveContextItem<T>(true);
        }

        public static void RemoveContextItem<T>(this TagHelperContext context, bool useInherited)
        {
            context.RemoveContextItem(typeof(T), useInherited);
        }

        public static void RemoveContextItem(this TagHelperContext context, Type type)
        {
            context.RemoveContextItem(type, true);
        }

        public static void RemoveContextItem(this TagHelperContext context, Type type, bool useInherited)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (context.Items.ContainsKey(type))
            {
                context.Items.Remove(type);
            }
            else
            {
                if (!useInherited)
                    return;
                var keyValuePair = context.Items.FirstOrDefault(
                    kVP =>
                    {
                        if ((object) (kVP.Key as Type) != null)
                            return ((Type) kVP.Key).IsAssignableFrom(type);
                        return false;
                    });
                if (keyValuePair.Equals(new KeyValuePair<object, object>()))
                    return;
                context.Items.Remove(keyValuePair);
            }
        }

        public static void RemoveContextItem(this TagHelperContext context, string key)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (!context.Items.ContainsKey(key))
                return;
            context.Items.Remove(key);
        }
    }
}