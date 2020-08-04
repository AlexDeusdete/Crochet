using Crochet.Interfaces;
using Crochet.Models;
using Crochet.Services.API;
using DryIoc;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
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
        private readonly ISaleService _saleService;
        private readonly ICustomerService _customerService;
        #endregion

        #region Collections
        public ObservableCollection<ProductType> ProductTypes { get; private set; }
        public ObservableCollection<Product> Products { get; private set; }
        public ObservableCollection<SaleItem> SaleItems { get; private set; }
        public ObservableCollection<Customer> Customers { get; private set; }
        #endregion

        #region Commands
        public ICommand LoadProductsCommand { get; private set; }
        public ICommand AddProductCommand { get; private set; }
        public ICommand UpdateSaleValuesCommand { get;private set; }
        public ICommand SaveSaleCommand { get;private set; }
        public ICommand CreateCustomerCommand { get; private set; }
        #endregion

        #region Propertys
        private int _totalItems;
        private float _totalPrice;
        private float _discount;
        private float _totalAfterDiscount;
        private Customer _customer;
        private string _observation;

        public int TotalItems 
        {
            get { return _totalItems; }
            set { SetProperty(ref _totalItems, value); } 
        }

        public string Observation
        {
            get { return _observation; }
            set { SetProperty(ref _observation, value); }
        }

        public Customer Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }

        public float TotalPrice
        {
            get { return _totalPrice; }
            set 
            { 
                SetProperty(ref _totalPrice, value);
                TotalAfterDiscount = _totalPrice - _discount;
            }
        }

        public float Discount
        {
            get { return _discount; }
            set 
            { 
                SetProperty(ref _discount, value);
                TotalAfterDiscount = _totalPrice - _discount;
            }
        }

        public float TotalAfterDiscount
        {
            get { return _totalAfterDiscount; }
            set { SetProperty(ref _totalAfterDiscount, value); }
        }
        #endregion
        public NewSalePageViewModel(INavigationService navigationService,
                                    IProductTypeService productTypeService,
                                    IProductService productService,
                                    ISaleService saleService,
                                    ICustomerService customerService):base(navigationService)
        {
            _productTypeService = productTypeService;
            _productService = productService;
            _saleService = saleService;
            _customerService = customerService;

            LoadProductsCommand = new DelegateCommand<object>(LoadProducts);
            AddProductCommand = new DelegateCommand<object>(AddProduct);
            UpdateSaleValuesCommand = new DelegateCommand(UpdateSaleValues);
            SaveSaleCommand = new DelegateCommand(SaveSale);
            CreateCustomerCommand = new DelegateCommand(CreateCustomer);

            ProductTypes = new ObservableCollection<ProductType>();
            Products = new ObservableCollection<Product>();
            SaleItems = new ObservableCollection<SaleItem>();
            Customers = new ObservableCollection<Customer>();
        }
        private async void CreateCustomer()
        {
            string result = await Prism.PrismApplicationBase.Current.MainPage.DisplayPromptAsync("Cliente", "Nome do Cliente :", "Salvar", "Cancelar", "");

            if (string.IsNullOrEmpty(result))
                return;

            var customer = Customers.Where(x => x.Name == result).FirstOrDefault();

            if (customer == null)
            {
                customer = new Customer()
                {
                    Name = result
                };

                customer = await _customerService.PutInsertCustomer(customer);

                Customers.Add(customer);
            }

            Customer = Customers.Where(x => x.Id == customer.Id).FirstOrDefault();
        }
        private async Task LoadCustomers()
        {
            var customers = await _customerService.GetCustomers();
            Customers.Clear();

            foreach (var item in customers)
            {
                Customers.Add(item);
            }
        }
        private async void SaveSale()
        {
            var sale = new Sale
            {
                Customer = Customer,
                Discount = Discount,
                Observation = Observation,
                SaleDate = DateTime.Now,
                Status = 0,
                TotalPrice = TotalPrice
            };

            sale = await _saleService.PutInsertSale(sale);

            foreach (var item in SaleItems)
            {
                item.SaleId = sale.Id;
                await _saleService.PutInsertSaleItem(item);
            }

            await NavigationService.GoBackAsync();
        }
        private void UpdateSaleValues()
        {
            TotalItems = SaleItems.Sum(x => x.Qtd);
            TotalPrice = SaleItems.Sum(x => (x.Qtd * x.Price));

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
            LoadCustomers();
        }
    }
}
