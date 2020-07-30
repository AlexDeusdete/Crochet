using Xamarin.Forms;

namespace Crochet.Views
{
    public partial class NewSalePage : ContentPage
    {
        public NewSalePage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
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

            productListView.IsVisible = true;
        }
    }
}
