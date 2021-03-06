﻿using Crochet.Interfaces;
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
    public class FeedStockCreateEditPageViewModel : ViewModelBase
    {
        private readonly IBrandService _brandService;
        private readonly IFeedStockService _feedStockService;
        public ObservableCollection<Brand> Brands { get; private set; }
        public ObservableCollection<Color> Colors { get; private set; }
        public ICommand FeedStockCreateCommand { get; private set; }
        public ICommand FeedStockCreateBrandCommand { get; private set; }
        public ICommand FeedStockAddColorCommand { get; private set; }

        private int _feedStockID;

        #region Propertys
        private Color _color;
        private int? _thickness;
        private string _tEX;
        private float? _price;
        private int? _inventoryAvailable;
        private int? _inventoryTotal;
        private Brand _brand;
        private string _colorCode;
        private string _colorName;

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
        public string ColorName
        {
            get { return _colorName; }
            set { SetProperty(ref _colorName, value); }
        }
        #endregion

        public FeedStockCreateEditPageViewModel(IBrandService brandService, IFeedStockService feedStockService, INavigationService navigationService)
            :base(navigationService)
        {
            _brandService = brandService;
            _feedStockService = feedStockService;

            FeedStockCreateCommand = new DelegateCommand(FeedStockCreate);
            FeedStockCreateBrandCommand = new DelegateCommand(FeedStockCreateBrand);
            FeedStockAddColorCommand = new DelegateCommand(() =>
            {
                Colors.Add(Color);
            });
            Brands = new ObservableCollection<Brand>();
            Colors = new ObservableCollection<Color>();
        }

        private async Task<IList<Brand>> GetBrands()
        {
            return await _brandService.GetItems();
        }
        private async void FeedStockCreate()
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

                    Brand = await _brandService.PutItem(brand);                     
                }
            }

            var item = new FeedStock()
            {
                FeedStockId = _feedStockID,
                Colors = Colors.Select(x => (System.Drawing.Color)x).ToList(),
                Brand = Brand,
                InventoryAvailable = _inventoryAvailable.Value,
                InventoryTotal = _inventoryTotal.Value,
                Price = _price.Value,
                TEX = _tEX,
                Thickness = _thickness.Value,
                ColorCode = _colorCode,
                ColorName = _colorName
            };

            await _feedStockService.UpsertItem(item);

            await NavigationService.GoBackAsync();
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            await LoadBrands();
            var feedStock = parameters.GetValue<FeedStock>("feedStock");

            if (feedStock == null)
                return;

            _feedStockID = feedStock.FeedStockId;
            foreach (var item in feedStock.Colors)
                Colors.Add(item);

            Thickness = feedStock.Thickness;
            TEX = feedStock.TEX;
            Price = feedStock.Price;
            InventoryAvailable = feedStock.InventoryAvailable;
            InventoryTotal = feedStock.InventoryTotal;
            Brand = Brands.Where(x => x.BrandId == feedStock.Brand.BrandId).FirstOrDefault();
            ColorCode = feedStock.ColorCode;
            ColorName = feedStock.ColorName;
        }
        private async Task LoadBrands()
        {
            var brands = await GetBrands();
            foreach (var item in brands)
            {
                Brands.Add(item);
            }
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

                brand = await _brandService.PutItem(brand);

                Brands.Add(brand);
            }

           Brand = Brands.Where(x => x.BrandId == brand.BrandId).FirstOrDefault();
        }
    }
}
