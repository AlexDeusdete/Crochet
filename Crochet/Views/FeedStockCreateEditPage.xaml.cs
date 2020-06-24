using Crochet.Controls;
using Crochet.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Markup;

namespace Crochet.Views
{
    public partial class FeedStockCreateEditPage : ContentPage
    {
        public FeedStockCreateEditPage()
        {
            InitializeComponent();

            EnPrice.ReturnCommand = new Command(() => EnInvAva.Focus());
            EnInvAva.ReturnCommand = new Command(() => EnInvTot.Focus());
            EnInvTot.ReturnCommand = new Command(() => EnTEX.Focus());
            EnTEX.ReturnCommand = new Command(() => EnColorCode.Focus());
            EnColorCode.ReturnCommand = new Command(() => EnThic.Focus());
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            if (ColorPicker.CardStated == CardState.Collapsed)
            {
                ColorPicker.CardStated = CardState.Expanded;
                btnAddColor.Text = "Salvar";
            }
            else
            {
                ((FeedStockCreateEditPageViewModel)this.BindingContext).FeedStockAddColorCommand.Execute(null);
                ColorPicker.CardStated = CardState.Collapsed;
                btnAddColor.Text = "+Add Cor";
            }                           
        }

        protected override bool OnBackButtonPressed()
        {
            if (ColorPicker.CardStated == CardState.Expanded)
            {
                ColorPicker.CardStated = CardState.Collapsed;
                btnAddColor.Text = "+Add Cor";
                return true;
            }
            return base.OnBackButtonPressed();
        }
    }
}
