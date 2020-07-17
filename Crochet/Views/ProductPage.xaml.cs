using Crochet.Interfaces;
using Crochet.Models;
using System.IO;
using Xamarin.Forms;

namespace Crochet.Views
{
    public partial class ProductPage : ContentPage
    {
        public ProductPage()
        {
            InitializeComponent();
        }

        private async void Image_BindingContextChanged(object sender, System.EventArgs e)
        {
            var image = (Image)sender;

            if (image.BindingContext == null)
                return;

            var service = DependencyService.Resolve<IProductPictureService>();
            var imageList = (await service.GetPicturesByProductId((image.BindingContext as Product).Id));
            if (imageList == null || imageList.Count == 0)
                return;

            var imageName = imageList[0].Name;
            Stream stream = service.GetPictureById(imageName);
            if (stream != null)
                image.Source = ImageSource.FromStream(() => stream);
        }
    }
}
