using Xamarin.Forms;

namespace Crochet.Views
{
    public partial class TabbedHomePage : TabbedPage
    {
        public TabbedHomePage()
        {
            InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
