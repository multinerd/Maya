using System;
using System.Globalization;
using System.Windows.Data;

namespace Maya.WPF.Converters
{
    /// <summary>
    /// Converts Int to Doubles and vice versa.
    /// </summary>
    public class IntToDoubleConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToInt32(value);
        }
    }
}
