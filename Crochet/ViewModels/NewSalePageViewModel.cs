using Crochet.Interfaces;
using Crochet.Models;
using Crochet.Services.API;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crochet.ViewModels
{
    public class NewSalePageViewModel : ViewModelBase
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IProductService _productService;

        public ICommand LoadProductsCommand { get; private set; }

        public ObservableCollection<ProductType> ProductTypes { get;private set; }
        public ObservableCollection<Product> Products { get; private set; }
        public NewSalePageViewModel(INavigationService navigationService,
                                    IProductTypeService productTypeService,
                                    IProductService productService):base(navigationService)
        {
            _productTypeService = productTypeService;
            _productService = productService;

            LoadProductsCommand = new DelegateCommand<object>(LoadProducts);

            ProductTypes = new ObservableCollection<ProductType>();
            Products = new ObservableCollection<Product>();
        }
        private async void LoadProducts(object obj)
        {
            if (obj == null)
                return;

            var productType = (ProductType)obj;

            var products = await GetProductByTypeId(productType.Id);
            Products.Clear();

            foreach (var item in products)
            {
                Products.Add(item);
            }
        }
        private async void LoadProductTypes()
        {
            var productTypes = await GetProductTypes();
            ProductTypes.Clear();

            foreach (var item in productTypes)
            {
                ProductTypes.Add(item);
            }
        }
        private async Task<IList<ProductType>> GetProductTypes()
        {
            return await _productTypeService.GetItems();
        }
        private async Task<IList<Product>> GetProductByTypeId(int productTypeId)
        {
            return await _productService.GetItemsByType(productTypeId);
        }
        public override void Initialize(INavigationParameters parameters)
        {
            LoadProductTypes();
        }
    }
}
