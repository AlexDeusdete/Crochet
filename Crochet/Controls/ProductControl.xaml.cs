using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crochet.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductControl : Grid
    {
        public ProductControl()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PriceList.IsVisible = true;

            var anim = new Animation(v => FinancialCard.Opacity = v,
                                    FinancialCard.Opacity,
                                    FinancialCard.Opacity > 0.5 ? 0 : 1,
                                    null,
                                    () => PriceList.IsVisible = FinancialCard.Opacity == 1);
            anim.Commit(this, "FinancialCard", 16, 700);
        }
    }
}