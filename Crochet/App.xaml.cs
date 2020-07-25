using Prism;
using Prism.Ioc;
using Crochet.ViewModels;
using Crochet.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Crochet.Interfaces;
using Prism.Mvvm;
using Crochet.Views.ProductContentView;
using Crochet.Services.API;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

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

        public App(IPlatformInitializer initializer) : base(initializer, true) 
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            AppCenter.Start("android=b1c1da21-0549-4661-80b1-1e4aca722a8c;" +
                              "uwp={Your UWP App secret here};" +
                              "ios={Your iOS App secret here}",
                              typeof(Analytics), typeof(Crashes));

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
            containerRegistry.Register<IProductService, ProductService>();
            containerRegistry.RegisterSingleton<IProductPictureService, ProductPictureService>();
            containerRegistry.Register<IProductYarnService, ProductYarnService>();
            containerRegistry.Register<IProductFinalcialService, ProductFinalcialService>();
            containerRegistry.Register<IApi, API>();
        }
    }
}
