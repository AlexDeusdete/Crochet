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
    public class FeedStockCreateEditPageViewModel : BindableBase, IInitialize
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
        private Brand _brand;
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
        public Brand Brand
        {
            get { return _brand; }
            set { SetProperty(ref _brand, value); }
        }
        public string ColorCode
        {
            get { return _colorCode; }
            set { SetProperty(ref _colorCode, value); }
        }
        #endregion

        public FeedStockCreateEditPageViewModel(IBrandService brandService, IFeedStockService feedStockService, INavigationService navigationService)
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
            if (Brand == null)
            {
                Brand = Brands.Where(x => x.Name == "Sem Marca").FirstOrDefault();

                if (Brand == null)
                {
                    var brand = new Brand()
                    {
                        Name = "Sem Marca"
                    };

                    _brandService.PutItem(brand);
                    Brand = _brandService.GetItems().Result.Where(x => x.Name == "Sem Marca").FirstOrDefault();
                }
            }

            var item = new FeedStock()
            {
                Color = _color,
                Brand = Brand,
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

            if (brand == null)
            {
                brand = new Brand()
                {
                    Name = result
                };

                _brandService.PutItem(brand);

                Brands.Clear();

                var brands = await GetBrands();
                foreach (var item in brands)
                {
                    Brands.Add(item);
                }
            }

           Brand = Brands.Where(x => x.BrandId == brand.BrandId).FirstOrDefault();
        }
    }
}
