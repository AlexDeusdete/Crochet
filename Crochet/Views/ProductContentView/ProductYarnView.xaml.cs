using Crochet.Models;
using Crochet.ViewModels;
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
    public partial class ProductCostView : ContentView
    {
        public ProductCostView()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return "Linhas";
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            (this.BindingContext as ProductCreateEditPageViewModel).VariationId = 
                ((sender as ContentView).BindingContext as ProductYarnCollection).VariationId;

            (this.BindingContext as ProductCreateEditPageViewModel).VariationName = 
                ((sender as ContentView).BindingContext as ProductYarnCollection).VariationName;
            yarnPC.PickerStated = Controls.PickerState.Expanded;
        }
    }
}