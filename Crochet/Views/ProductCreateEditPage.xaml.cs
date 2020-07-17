using ImTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Crochet.Views
{
    public partial class ProductCreateEditPage : ContentPage
    {

        private int _indexMenu = 0;
        public ProductCreateEditPage()
        {
            InitializeComponent();
            ((StackLayout)indicatorView.IndicatorLayout).Spacing = 0;
        }

        private void FlexLayout_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is Label label)
            {
                label.Text = ((ContentView[]) indicatorView.ItemsSource)[_indexMenu].ToString();
                _indexMenu++;
            };
        }

        private void IndicatorView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Position")
                return;

            productGeneralView.IsVisible = false;
            productPhotosView.IsVisible = false;
            productCostView.IsVisible = false;
            productProductionView.IsVisible = false;
            productFinalcialView.IsVisible = false;

            switch (indicatorView.Position)
            {
                case 0:
                    productGeneralView.IsVisible = true;
                    break;
                case 1:
                    productPhotosView.IsVisible = true;
                    break;
                case 2:
                    productCostView.IsVisible = true;
                    break;
                case 3:
                    productProductionView.IsVisible = true;
                    break;
                case 4:
                    productFinalcialView.IsVisible = true;
                    break;
                default:
                    break;
            }

        }

        private void SwipeGestureRecognizer_Left(object sender, SwipedEventArgs e)
        {
            if (indicatorView.Position == indicatorView.Count - 1)
                return;

            indicatorView.Position += 1;
        }

        private void SwipeGestureRecognizer_Right(object sender, SwipedEventArgs e)
        {
            if (indicatorView.Position == 0)
                return;
            indicatorView.Position -= 1 ;
        }
    }
}
