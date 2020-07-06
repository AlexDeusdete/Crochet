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
    public partial class ProductGeneralView : ContentView
    {
        public static readonly BindableProperty BindProperty = 
            BindableProperty.Create(nameof(Bind), 
                                    typeof(object), 
                                    typeof(ProductGeneralView), 
                                    null,
                                    BindingMode.TwoWay,
                                    propertyChanged: (b, o, n) =>
                                    {
                                        var a = b;
                                    });

        public object Bind
        {
            get => (object)GetValue(BindProperty);
            set => SetValue(BindProperty, value);
        }
        public ProductGeneralView()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return "Geral";
        }
    }
}