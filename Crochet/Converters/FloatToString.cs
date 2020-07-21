using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Crochet.Converters
{
    public class FloatToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return (value as float?).ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string str = (string)value;
            if (culture.NumberFormat.CurrencyDecimalSeparator == ",")
                str = str.Replace('.', ',');

            if (float.TryParse(str,out float result))
                return result;
            return null;
        }
    }
}
