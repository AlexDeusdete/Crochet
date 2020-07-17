using Crochet.Interfaces;
using Crochet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crochet.Views.ProductContentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPhotosView : ContentView
    {
        public ProductPhotosView()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return "Fotos";
        }
        private void Image_BindingContextChanged(object sender, EventArgs e)
        {
            var image = (Image)sender;
            if (image.BindingContext == null)
                return;

            var service  = DependencyService.Resolve<IProductPictureService>();
            Stream stream = service.GetPictureById("IMG"+(image.BindingContext as ProductPicture).Id.ToString());
            if(stream != null)
                image.Source = ImageSource.FromStream(() => stream);
        }
    }
}