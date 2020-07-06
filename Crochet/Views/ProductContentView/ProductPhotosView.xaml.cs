using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crochet.Views.ProductContentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPhotosView : ContentView
    {
        public ProductPhotosView()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return "Fotos";
        }
    }
}