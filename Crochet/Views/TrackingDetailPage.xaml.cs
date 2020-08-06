using Crochet.Models;
using Crochet.ViewModels;
using Xamarin.Forms;

namespace Crochet.Views
{
    public partial class TrackingDetailPage : ContentPage
    {
        public TrackingDetailPage()
        {
            InitializeComponent();
        }
        private void SetStatus(int status)
        {
            switch (status)
            {
                case 0:
                    Frame01.BackgroundColor = Frame01.BorderColor;
                    ButtonStatus.Text = "Materia Prima";
                    break;
                case 1:
                    Frame01.BackgroundColor = Frame01.BorderColor;
                    Frame02.BackgroundColor = Frame02.BorderColor;
                    ButtonStatus.Text = "Iniciar Produção";
                    break;
                case 2:
                    Frame01.BackgroundColor = Frame01.BorderColor;
                    Frame02.BackgroundColor = Frame02.BorderColor;
                    Frame03.BackgroundColor = Frame03.BorderColor;
                    ButtonStatus.Text = "Finalizar Produção";
                    break;
                case 3:
                    Frame01.BackgroundColor = Frame01.BorderColor;
                    Frame02.BackgroundColor = Frame02.BorderColor;
                    Frame03.BackgroundColor = Frame03.BorderColor;
                    Frame04.BackgroundColor = Frame04.BorderColor;
                    ButtonStatus.Text = "Pedido Entregue";
                    break;
                case 4:
                    Frame01.BackgroundColor = Frame01.BorderColor;
                    Frame02.BackgroundColor = Frame02.BorderColor;
                    Frame03.BackgroundColor = Frame03.BorderColor;
                    Frame04.BackgroundColor = Frame04.BorderColor;
                    Frame05.BackgroundColor = Frame05.BorderColor;
                    ButtonStatus.Text = "Finalizar";
                    break;
                case 5:
                    Frame01.BackgroundColor = Frame01.BorderColor;
                    Frame02.BackgroundColor = Frame02.BorderColor;
                    Frame03.BackgroundColor = Frame03.BorderColor;
                    Frame04.BackgroundColor = Frame04.BorderColor;
                    Frame05.BackgroundColor = Frame05.BorderColor;
                    Frame06.BackgroundColor = Frame06.BorderColor;
                    ButtonStatus.Text = "";
                    ButtonStatus.IsVisible = false;
                    break;
                default:
                    break;
            }
        }

        private void FlexLayout_BindingContextChanged(object sender, System.EventArgs e)
        {
            if (FL_Status.BindingContext != null)
                SetStatus((FL_Status.BindingContext as Sale).Status);
        }
    }
}
