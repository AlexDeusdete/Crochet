using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Crochet.Converters
{
    public class SDColorListToFormsColorList : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return ((IList<System.Drawing.Color>)value).Select(x => (Color)x).ToList();
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IList<Color>)value).Select(x => (System.Drawing.Color)x).ToList();
        }
    }
}
