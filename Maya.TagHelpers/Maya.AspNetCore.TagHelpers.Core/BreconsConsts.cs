﻿using System.Collections.Generic;

namespace Maya.AspNetCore.TagHelpers.Core
{
    public static class BreconsConsts
    {
        public static readonly List<string> CheckTypes = new List<string> { "checkbox", "radio" };
        public static readonly List<string> ButtonTypes = new List<string> { "submit", "reset", "button" };
        public static readonly List<string> InputTypes = new List<string> { "text", "password", "email", "number", "date", "datetime", "range" };
        public static readonly List<string> FileTypes = new List<string> { "file" };
    }
}
