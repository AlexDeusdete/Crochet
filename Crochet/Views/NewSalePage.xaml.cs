using Crochet.Models;
using Crochet.ViewModels;
using Crochet.Views.NewSaleContentView;
using Xamarin.Forms;

namespace Crochet.Views
{
    public partial class NewSalePage : ContentPage
    {
        public NewSalePage()
        {
            InitializeComponent();
        }

        private void AddProductevent(object sender, System.EventArgs e)
        {
            AnimationCart();
        }

        public void AnimationCart()
        {
            arrowCart.RotateTo(90, 1000);

            var parentAnimation = new Animation();
            var arrowCartAnimation = new Animation(v => arrowCart.Rotation = v, arrowCart.Rotation, arrowCart.Rotation < 0 ? 90 : -90, Easing.SpringIn);
            var cartAnimation = new Animation(v => cart.HeightRequest = v, cart.Height, cart.Height == 50 ? MyGrid.Height * 0.8 : 50, Easing.SpringIn);

            parentAnimation.Add(0, 1, arrowCartAnimation);
            parentAnimation.Add(0, 1, cartAnimation);
            parentAnimation.Commit(this, "ChildAnimations", 16, 1000);
        }

        private void ProductTypeListView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if ((e.PropertyName != "IsVisible" 
                    && !(sender as View).IsVisible)
                || productTypeListView.IsVisible)
                return;

            if (sender is ProductListView)
                productTypeListView.IsVisible = !(sender as ProductListView).IsVisible;
            else
                productListView.IsVisible = !(sender as ProductTypeListView).IsVisible;
        }

        private void AddQtdProductevent(object sender, System.EventArgs e)
        {
            AddRemoveProduct(sender, 1);
        }

        private void AddRemoveProduct(object sender, int qtd)
        {
            var btn = (TemplatedView)sender;
            var item = (SaleItem)btn.BindingContext;
            if ((item.Qtd + qtd) < 0)
                return;
            item.Qtd += qtd;

            var label = (btn.Parent as StackLayout).Children[1] as Label;
            label.BindingContext = item;
            label.SetBinding(Label.TextProperty, "Qtd", BindingMode.TwoWay);

            label = ((btn.Parent as StackLayout).Parent as FlexLayout).Children[1] as Label;
            label.Text = string.Format("Preço: {0:C} | Total: {1:C}", item.Price, item.TotalPrice);

            (this.BindingContext as NewSalePageViewModel).UpdateSaleValuesCommand.Execute(null);
        }

        private void RemoveQtdProductevent(object sender, System.EventArgs e)
        {
            AddRemoveProduct(sender, -1);
        }
    }
}