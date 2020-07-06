using Crochet.Interfaces;
using Crochet.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crochet.ViewModels
{
    public class ProductPageViewModel : ViewModelBase
    {
        public ICommand NavigateToProductCreateCommand { get; private set; }
        public ICommand NavigateToProductEditCommand { get; private set; }
        public ObservableCollection<Product> Products { get; private set; }
        private IProductService _productService;
        public ProductPageViewModel(INavigationService navigationService, IProductService productService)
            : base(navigationService)
        {
            NavigateToProductCreateCommand = new DelegateCommand(NavigateToProductCreate);
            NavigateToProductEditCommand = new DelegateCommand<object>(NavigateToProductEdit);
            _productService = productService;
            Products = new ObservableCollection<Product>();
        }

        private async void NavigateToProductEdit(object parameter)
        {
            var navParameters = new NavigationParameters
            {
                { "Product", parameter}
            };

            await NavigationService.NavigateAsync("ProductCreateEditPage", navParameters);
        }

        private async Task LoadItems()
        {
            var products = await GetProductsAsync();
            Products.Clear();

            foreach(var item in products)
            {
                Products.Add(item);
            }

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadItems();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            LoadItems();
        }

        private async Task<IList<Product>> GetProductsAsync()
        {
            return await _productService.GetItems();
        }

        private async void NavigateToProductCreate()
        {
            string result = await Prism.PrismApplicationBase.Current.MainPage.DisplayPromptAsync("Nome", "Nome do Produto :", "Salvar", "Cancelar", "");

            if (String.IsNullOrEmpty(result))
                return;

            var navParameters = new NavigationParameters
            {
                { "ProductName", result}
            };

            await NavigationService.NavigateAsync("ProductCreateEditPage", navParameters);
        }
    }
}
