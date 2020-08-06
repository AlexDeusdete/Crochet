using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Crochet.Converters
{
    public class IntToStatusNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var status = (int)value;

            switch (status)
            {
                case 0: return "Pedido Criado";
                case 1: return "Compra de Materia Prima";
                case 2: return "Peças em Produção";
                case 3: return "Peças Prontas";
                case 4: return "Pedido Entregue";
                case 5: return "Pedido Finalizado";
                default: return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
