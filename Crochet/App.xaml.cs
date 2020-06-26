using Prism;
using Prism.Ioc;
using Crochet.ViewModels;
using Crochet.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Crochet.Interfaces;
using Crochet.Services.LiteDB;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Crochet
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/TabbedHomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();            
            containerRegistry.RegisterForNavigation<TabbedHomePage, TabbedHomePageViewModel>();
            containerRegistry.RegisterForNavigation<InventoryPage, InventoryPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductPage, ProductPageViewModel>();
            containerRegistry.RegisterForNavigation<SalePage, SalePageViewModel>();
            containerRegistry.RegisterForNavigation<AcquisitionPage, AcquisitionPageViewModel>();
            containerRegistry.RegisterForNavigation<FeedStockCreateEditPage, FeedStockCreateEditPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductCreateEditPage, ProductCreateEditPageViewModel>();

            containerRegistry.Register<IFeedStockService, FeedStockService>();
            containerRegistry.Register<IBrandService, BrandService>();
           
        }
    }
}
