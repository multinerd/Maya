using Maya.WPF.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Maya.WPF.Converters
{
    /// <summary>
    /// Return a color representation of a value.
    /// </summary>
    [ContentProperty("Converters")]
    public class NumberToColorConverter : MarkupExtension, IValueConverter, INumberToColorConverter
    {
        /// <inheritdoc />
        public AccountingColors AccountingColors { get; set; } = AccountingColors.BlackRed;

        /// <inheritdoc />
        public Color ZeroValueColor { get; set; } = Colors.Gray;

        /// <inheritdoc />
        public bool Inverse { get; set; }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is decimal v)) return null;

            if (v == decimal.Zero)
                return ZeroValueColor;

            if (Inverse)
                v = -v;

            switch (AccountingColors)
            {
                case AccountingColors.GreenRed:
                    return (v > decimal.Zero) ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);

                case AccountingColors.BlackRed:
                    return (v > decimal.Zero) ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.Red);

                default: return null;
            }
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        /// <summary>When implemented in a derived class, returns an object that is provided as the value of the target property for this markup extension. </summary>
        /// <returns>The object value to set on the property where the extension is applied. </returns>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
