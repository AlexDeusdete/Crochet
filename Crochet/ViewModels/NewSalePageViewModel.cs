using Crochet.Interfaces;
using Crochet.Models;
using Crochet.Services.API;
using DryIoc;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crochet.ViewModels
{
    public class NewSalePageViewModel : ViewModelBase
    {
        #region Services
        private readonly IProductTypeService _productTypeService;
        private readonly IProductService _productService;
        #endregion

        #region Collections
        public ObservableCollection<ProductType> ProductTypes { get; private set; }
        public ObservableCollection<Product> Products { get; private set; }
        public ObservableCollection<SaleItem> SaleItems { get; private set; }
        #endregion

        #region Commands
        public ICommand LoadProductsCommand { get; private set; }
        public ICommand AddProductCommand { get; private set; }
        public ICommand UpdateSaleValuesCommand { get; set; }
        #endregion

        #region Propertys
        private int _totalItems;

        public int TotalItems 
        {
            get { return _totalItems; }
            set { SetProperty(ref _totalItems, value); } 
        }
        #endregion
        public NewSalePageViewModel(INavigationService navigationService,
                                    IProductTypeService productTypeService,
                                    IProductService productService):base(navigationService)
        {
            _productTypeService = productTypeService;
            _productService = productService;

            LoadProductsCommand = new DelegateCommand<object>(LoadProducts);
            AddProductCommand = new DelegateCommand<object>(AddProduct);
            UpdateSaleValuesCommand = new DelegateCommand(UpdateSaleValues);

            ProductTypes = new ObservableCollection<ProductType>();
            Products = new ObservableCollection<Product>();
            SaleItems = new ObservableCollection<SaleItem>();
        }
        private void UpdateSaleValues()
        {
            TotalItems = SaleItems.Sum(x => x.Qtd);

            if(SaleItems.Any(x=> x.Qtd == 0))
            {
                var items = SaleItems.Where(x => x.Qtd == 0).ToList();
                foreach (var item in items)
                {
                    SaleItems.Remove(item);
                }
            }
        }
        private void AddProduct(object obj)
        {
            if (obj == null)
                return;

            var productFinancial = (ProductFinalcial)obj;

            if (SaleItems.Any(x => x.ProductId == productFinancial.ProductId))
                return;

            var saleItem = new SaleItem
            {
                Price = productFinancial.FinalPrice,
                ProductId = productFinancial.ProductId,
                VariationId = productFinancial.VariationId,
                VariationName = productFinancial.VariationName,
                Qtd = 1,
                Product = Products.Where(x => x.Id == productFinancial.ProductId).FirstOrDefault()
            };

            SaleItems.Add(saleItem);
            UpdateSaleValues();
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
