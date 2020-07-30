using Crochet.Interfaces;
using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Crochet.Converters
{
    public class ProductFinalcialsToRangePrice : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var productFinalcials = (IList<ProductFinalcial>)value;

            float minPrice = 0;
            float maxPrice = 0;

            foreach (var item in productFinalcials)
            {
                if (minPrice == 0 || minPrice > item.FinalPrice)
                    minPrice = item.FinalPrice;

                if (maxPrice == 0 || maxPrice < item.FinalPrice)
                    maxPrice = item.FinalPrice;
            }

            if (minPrice != maxPrice)
                return string.Format("{0:C} - {1:C}", minPrice, maxPrice);
            else
                return string.Format("{0:C}", minPrice);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
