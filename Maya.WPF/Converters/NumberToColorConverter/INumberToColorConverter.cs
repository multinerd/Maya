using Maya.WPF.Enums;
using System.Windows.Media;

namespace Maya.WPF.Converters
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface INumberToColorConverter
    {
        /// <summary>
        /// The set of colors to return.
        /// </summary>
        AccountingColors AccountingColors { get; set; }

        /// <summary>
        /// The color to represent 0.
        /// </summary>
        Color ZeroValueColor { get; set; }


        /// <summary>
        /// Inverse the values. (Negatives become positives and vice versa)
        /// </summary>
        bool Inverse { get; set; }
    }
}
