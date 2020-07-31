using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crochet.Views.NewSaleContentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductTypeListView : ContentView
    {
        public ProductTypeListView()
        {
            InitializeComponent();
        }

        private void MyCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.IsVisible = false;            
        }
    }
}