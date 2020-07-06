using Crochet.Interfaces;
using Crochet.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Crochet.ViewModels
{
    public class ProductCreateEditPageViewModel : ViewModelBase
    {
        private IProductService _productService;
        private int _idProduct = 0;
        public ICommand SaveCommand { get; private set; }

        #region Propertys
        private string _productCode;
        private string _name;
        private int _width;
        private int _height;
        private int _weight;
        private string _difficulty;
        private string _comments;

        public string ProductCode
        {
            get { return _productCode; }
            set { SetProperty(ref _productCode, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public int Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }
        public int Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }
        public int Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }
        public string Difficulty
        {
            get { return _difficulty; }
            set { SetProperty(ref _difficulty, value); }
        }
        public string Comments
        {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }
        #endregion
        public ProductCreateEditPageViewModel(INavigationService navigationService, IProductService productService)
            :base(navigationService)
        {
            _productService = productService;

            SaveCommand = new DelegateCommand(Save);
        }
        private void Save()
        {
            var product = new Product()
            {
                Id = _idProduct,
                Name = Name,
                Width = Width,
                Height = Height,
                Weight = Weight,
                Comments = Comments,
                Difficulty = Difficulty
            };

            _productService.UpsertItem(product);
            NavigationService.GoBackAsync();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            string productName = parameters.GetValue<string>("ProductName");

            if (String.IsNullOrEmpty(productName))
            {
                var product = parameters.GetValue<Product>("Product");
                LoadPropertys(product);
            }
            else
            {
                Name = productName;
            }
        }

        private void LoadPropertys(Product product)
        {
            _idProduct = product.Id;
            ProductCode = product.ProductCode;
            Name = product.Name;
            Width = product.Width;
            Height = product.Height;
            Weight = product.Weight;
            Difficulty = product.Difficulty;
            Comments = product.Comments;
        }
    }
}
