using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Maya.WPF.Converters
{
    /// <summary>
    /// If a number is below 0, red is returned, otherwise black is returned.
    /// </summary>
    public class NumberToColorConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal v)
                return v >= decimal.Zero
                    ? new SolidColorBrush(Colors.Black)
                    : new SolidColorBrush(Colors.Red);

            return null;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    /// <summary>
    /// If a number is below 0, black is returned, otherwise red is returned.
    /// </summary>
    public class InverseNumberToColorConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal v)
                return v < decimal.Zero
                    ? new SolidColorBrush(Colors.Black)
                    : new SolidColorBrush(Colors.Red);

            return null;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
