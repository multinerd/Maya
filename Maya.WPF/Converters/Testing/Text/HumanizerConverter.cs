using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Maya.WPF.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class HumanizerConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string v)
                return new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])").Replace(v.ToString(), " ${x}");

            return value;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
