using Crochet.Services.LiteDB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Crochet.Converters
{
    public class ImageLiteDBConverter : LiteDBBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (GetDBInstance().FileStorage.Exists((string)value))
                {
                    Stream stream = GetDBInstance().FileStorage.OpenRead((string)value);
                    return ImageSource.FromStream(() => stream);
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
