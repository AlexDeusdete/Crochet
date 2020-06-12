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

        #region Propertys
        private Color _color;
        private int? _thickness;
        private string _tEX;
        private float? _price;
        private int? _inventory;
        private string _brandName;

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
        public int? Inventory
        {
            get { return _inventory; }
            set { SetProperty(ref _inventory, value); }
        }
        public string BrandName
        {
            get { return _brandName; }
            set { SetProperty(ref _brandName, value); }
        }
        #endregion

        public FeedStockCreatePageViewModel(IBrandService brandService, IFeedStockService feedStockService, INavigationService navigationService)
        {
            _brandService = brandService;
            _navigationService = navigationService;
            _feedStockService = feedStockService;

            FeedStockCreateCommand = new DelegateCommand(FeedStockCreate);
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
            var brand = Brands.Where(x => x.Name == _brandName).FirstOrDefault();

            if (brand == null)
            {
                brand = new Brand()
                {
                    Name = string.IsNullOrWhiteSpace(_brandName) ? "Sem Marca" : _brandName
                };

                _brandService.PutItem(brand);
                brand = _brandService.GetItems().Result.Where(x => x.Name == "Sem Marca").FirstOrDefault();
            }

            var item = new FeedStock()
            {
                Color = _color,
                Brand = brand,
                Inventory = _inventory.Value,
                Price = _price.Value,
                TEX = _tEX,
                Thickness = _thickness.Value               
            };

            _feedStockService.UpsertItem(item);

            _navigationService.GoBackAsync();
        }
    }
}
