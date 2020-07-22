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
        private readonly IProductService _productService;
        public ICommand NavigateToProductCreateCommand { get; private set; }
        public ICommand NavigateToProductEditCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ObservableCollection<ProductGroup> Products { get; private set; }

        #region Property
        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }
        #endregion
        public ProductPageViewModel(INavigationService navigationService, 
                                    IProductService productService)
            : base(navigationService)
        {
            NavigateToProductCreateCommand = new DelegateCommand(NavigateToProductCreate);
            NavigateToProductEditCommand = new DelegateCommand<object>(NavigateToProductEdit);
            RefreshCommand = new DelegateCommand(() =>
            {
                LoadItems();
            });
            _productService = productService;
            Products = new ObservableCollection<ProductGroup>();
        }
        private async void NavigateToProductEdit(object parameter)
        {
            var navParameters = new NavigationParameters
            {
                { "Product", parameter}
            };

            await NavigationService.NavigateAsync("ProductCreateEditPage", navParameters);
        }
        private async void LoadItems()
        {
            IsRefreshing = true;
            var products = await GetProductsAsync();
            Products.Clear();

            foreach(var item in products)
            {
                Products.Add(item);
            }
            IsRefreshing = false;
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadItems();
        }
        public override void Initialize(INavigationParameters parameters)
        {
            LoadItems();
        }
        private async Task<IList<ProductGroup>> GetProductsAsync()
        {
            return await _productService.GetGroupItems();
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
