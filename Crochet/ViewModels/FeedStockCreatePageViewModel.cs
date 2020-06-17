using Crochet.Interfaces;
using Crochet.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Crochet.ViewModels
{
    public class FeedStockCreatePageViewModel : BindableBase, IInitialize
    {
        private readonly IBrandService _brandService;
        private readonly INavigationService _navigationService;
        private readonly IFeedStockService _feedStockService;
        public ObservableCollection<Brand> Brands { get; private set; }
        public ICommand FeedStockCreateCommand { get; private set; }
        public ICommand FeedStockCreateBrandCommand { get; private set; }

        #region Propertys
        private Color _color;
        private int? _thickness;
        private string _tEX;
        private float? _price;
        private int? _inventoryAvailable;
        private int? _inventoryTotal;
        private string _brandName;
        private string _colorCode;

        public Color Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }
        public int? Thickness
        {
            get { return _thickness; }
            set { SetProperty(ref _thickness, value);}
        }
        public string TEX
        {
            get { return _tEX; }
            set { SetProperty(ref _tEX, value); }
        }
        public float? Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
        public int? InventoryAvailable
        {
            get { return _inventoryAvailable; }
            set { SetProperty(ref _inventoryAvailable, value); }
        }
        public int? InventoryTotal
        {
            get { return _inventoryTotal; }
            set { SetProperty(ref _inventoryTotal, value); }
        }
        public string BrandName
        {
            get { return _brandName; }
            set { SetProperty(ref _brandName, value); }
        }
        public string ColorCode
        {
            get { return _colorCode; }
            set { SetProperty(ref _colorCode, value); }
        }
        #endregion

        public FeedStockCreatePageViewModel(IBrandService brandService, IFeedStockService feedStockService, INavigationService navigationService)
        {
            _brandService = brandService;
            _navigationService = navigationService;
            _feedStockService = feedStockService;

            FeedStockCreateCommand = new DelegateCommand(FeedStockCreate);
            FeedStockCreateBrandCommand = new DelegateCommand(FeedStockCreateBrand);
            Brands = new ObservableCollection<Brand>();
        }

        public async void Initialize(INavigationParameters parameters)
        {
            var brands = await GetBrands();
            foreach (var item in brands)
            {
                Brands.Add(item);
            }
        }

        private async Task<IList<Brand>> GetBrands()
        {
            return await _brandService.GetItems();
        }

        private void FeedStockCreate()
        {
            var brandSelected = string.IsNullOrWhiteSpace(_brandName) ? "Sem Marca" : _brandName;

            var brand = Brands.Where(x => x.Name == brandSelected).FirstOrDefault();

            if (brand == null)
            {
                brand = new Brand()
                {
                    Name = brandSelected
                };

                _brandService.PutItem(brand);
                brand = _brandService.GetItems().Result.Where(x => x.Name == brandSelected).FirstOrDefault();
            }

            var item = new FeedStock()
            {
                Color = _color,
                Brand = brand,
                InventoryAvailable = _inventoryAvailable.Value,
                InventoryTotal = _inventoryTotal.Value,
                Price = _price.Value,
                TEX = _tEX,
                Thickness = _thickness.Value,
                ColorCode = _colorCode
            };

            _feedStockService.UpsertItem(item);

            _navigationService.GoBackAsync();
        }

        private async void FeedStockCreateBrand()
        {
            string result = await Prism.PrismApplicationBase.Current.MainPage.DisplayPromptAsync("Marca", "Nome da Marca :", "Salvar", "Cancelar", "Sem Marca");
            if (string.IsNullOrEmpty(result))
                return;

            var brand = Brands.Where(x => x.Name == result).FirstOrDefault();

            if (brand != null)
            {
                _brandName = brand.Name;
                return;
            }

            var brandCreate = new Brand()
            {
                Name = result
            };

            _brandService.PutItem(brandCreate);

            Brands.Clear();

            var brands = await GetBrands();
            foreach (var item in brands)
            {
                Brands.Add(item);
            }
        }
    }
}
