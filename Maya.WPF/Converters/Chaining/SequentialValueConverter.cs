using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Maya.WPF.Converters
{
    /// <summary>
    /// A value converter which contains a list of IValueConverters and invokes their Convert or ConvertBack methods
    /// in the order that they exist in the list.  The output of one converter is piped into the next converter
    /// allowing for modular value converters to be chained together.  If the ConvertBack method is invoked, the
    /// value converters are executed in reverse order (highest to lowest index).  Do not leave an element in the
    /// Converters property collection null, every element must reference a valid IValueConverter instance. If a
    /// value converter's type is not decorated with the ValueConversionAttribute, an InvalidOperationException will be
    /// thrown when the converter is added to the Converters collection.
    /// </summary>
    public class SequentialValueConverter : List<IValueConverter>, IValueConverter
    {
        private string[] _parameters;
        private bool _shouldReverse = false;


        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ExtractParameters(parameter);

            if (_shouldReverse)
            {
                Reverse();
                _shouldReverse = false;
            }

            return this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, GetParameter(converter), culture));
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ExtractParameters(parameter);

            Reverse();
            _shouldReverse = true;

            return this.Aggregate(value, (current, converter) => converter.ConvertBack(current, targetType, GetParameter(converter), culture));
        }

        private void ExtractParameters(object parameter)
        {
            if (parameter != null)
                _parameters = Regex.Split(parameter.ToString(), @"(?<!\\)\|+");
        }

        private string GetParameter(IValueConverter converter)
        {
            if (_parameters == null)
                return null;

            var index = IndexOf(converter);
            string parameter;

            try
            {
                parameter = _parameters[index];
            }

            catch (IndexOutOfRangeException ex)
            {
                Log.WriteToLog(this, "CommandParamater is null. You can avoid this exception by passing in a delimited CommandParamater. \n\tExample: ConverterParameter=param1|NULL)");
                parameter = null;
            }

            if (parameter != null)
                parameter = Regex.Unescape(parameter);

            return parameter;
            //if (_parameters == null || !_parameters.Any())
            //    return null;

            //var index = IndexOf(converter);
            //var parameter = _parameters[index];
            //if (parameter != null)
            //    parameter = Regex.Unescape(parameter);

            //return parameter;
        }
    }
}
