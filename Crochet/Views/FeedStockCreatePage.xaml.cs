using Crochet.Controls;
using Xamarin.Forms;

namespace Crochet.Views
{
    public partial class FeedStockCreatePage : ContentPage
    {
        public FeedStockCreatePage()
        {
            InitializeComponent();
        }

        private void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            if (ColorPicker.CardStated == CardState.Collapsed)
                ColorPicker.CardStated = CardState.Expanded;
            else
                ColorPicker.CardStated = CardState.Collapsed;
        }
    }
}
