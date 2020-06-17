using Crochet.Controls;
using Xamarin.Forms;

namespace Crochet.Views
{
    public partial class FeedStockCreatePage : ContentPage
    {
        public FeedStockCreatePage()
        {
            InitializeComponent();

            EnPrice.ReturnCommand = new Command(() => EnInvAva.Focus());
            EnInvAva.ReturnCommand = new Command(() => EnInvTot.Focus());
            EnInvTot.ReturnCommand = new Command(() => EnTEX.Focus());
            EnTEX.ReturnCommand = new Command(() => EnColorCode.Focus());
            EnColorCode.ReturnCommand = new Command(() => EnThic.Focus());
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
