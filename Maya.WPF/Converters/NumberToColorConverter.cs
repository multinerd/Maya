using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Maya.WPF.Converters
{
    // Neg value is red, otherwise default
    /// <summary>
    /// If a number is below 0, red is returned, otherwise black is returned.
    /// </summary>
    public class NumberToColorConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (decimal?)value;
            return state >= 0
                ? new SolidColorBrush(Colors.Black)
                : new SolidColorBrush(Colors.Red);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    // Neg value is red, otherwise default
    /// <summary>
    /// If a number is below 0, black is returned, otherwise red is returned.
    /// </summary>
    public class InverseNumberToColorConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (decimal?)value;
            return state < 0
                ? new SolidColorBrush(Colors.Black)
                : new SolidColorBrush(Colors.Red);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
