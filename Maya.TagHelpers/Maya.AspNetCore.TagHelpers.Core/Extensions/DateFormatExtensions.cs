namespace Maya.AspNetCore.TagHelpers.Core.Extensions
{
    public static class DateFormatExtensions
    {
        public static string ConvertNetFormatTojQuery(this string format)
        {
            format = format.Replace("dddd", "DD");
            format = format.Replace("ddd", "D");
            format = !format.Contains("MMMM")
                ? !format.Contains("MMM")
                    ? !format.Contains("MM") 
                        ? format.Replace("M", "m")
                        : format.Replace("MM", "mm")
                    : format.Replace("MMM", "M")
                : format.Replace("MMMM", "MM");
            return format;
        }

        public static string ConvertNetFormatToMomentJs(this string format)
        {
            format = format.Replace("dd", "DD");
            format = format.Replace("d", "D");
            format = format.Replace("yyyy", "YYYY");
            format = format.Replace("yy", "YY");
            return format;
        }

        public static string ConvertMomentJsFormatToNet(this string format)
        {
            format = format.Replace("DD", "dd");
            format = format.Replace("D", "d");
            format = format.Replace("YYYY", "yyyy");
            format = format.Replace("YY", "yy");
            return format;
        }
    }
}