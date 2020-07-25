using System;
using System.Collections;
using Xamarin.Forms;

namespace Crochet.Views
{
    public partial class InventoryPage : ContentPage
    {
        public InventoryPage()
        {
            InitializeComponent();
        }

        private void MyCollectionView_BindingContextChanged(object sender, EventArgs e)
        {
            var collection = (CollectionView)sender;
            if (collection.ItemsSource == null)
                return;

            var qtdItems = (collection.ItemsSource as IList).Count;

            collection.HeightRequest = qtdItems <= 3 ? 140 : Math.Ceiling(((float)qtdItems / 3f)) * 155;
        }
    }
}
