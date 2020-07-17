using Crochet.Controls;
using Crochet.Interfaces;
using Crochet.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crochet.ViewModels
{
    public class ProductCreateEditPageViewModel : ViewModelBase
    {
        private int _idProduct = 0;
        private List<ProductFinalcial> _productFinalcials;

        private readonly IProductService _productService;
        private readonly IPictureGalleryService _galleryService;
        private readonly IProductPictureService _productPictureService;
        private readonly IProductYarnService _productYarnService;
        private readonly IFeedStockService _feedStockService;
        private readonly IProductFinalcialService _productFinalcialService;
        public ICommand SaveCommand { get; private set; }
        public ICommand SavePictureCommand { get; private set; }
        public ICommand DeletePictureCommand { get; private set; }
        public ICommand CreateVariationCommand { get; private set; }
        public ICommand CreateYarnCommand { get; private set; }
        public ObservableCollection<ProductPicture> Pictures { get; private set; }
        public ObservableCollection<ProductYarnGroup> YarnGroups { get; private set; }
        public ObservableCollection<FeedStockGroup> FeedStockGroups { get; private set; }

        #region Propertys
        private string _productCode;
        private string _name;
        //General
        private int _width;
        private int _height;
        private int _weight;
        private string _difficulty;
        private string _comments;
        //Yarns
        private int _variationId;
        private string _variationName;
        //Finalcial
        private ProductYarnGroup _variation;
        private float _yarnsCost;
        private int _productionHours;
        private float _hourCost;
        private float _additionalCost;
        private float _profitPercentage;
        private float _finalPrice;
        private float _laborCost;
        private float _suggestedPrice;
        private float _profitPracticed;
        private float _profitValue;

        public ProductYarnGroup Variation
        {
            get { return _variation; }
            set { 
                    SetProperty(ref _variation, value);
                    LoadVariantPropertys();
                }
        }
        public float YarnsCost
        {
            get { return _yarnsCost; }
            set { SetProperty(ref _yarnsCost, value); }
        }
        public int ProductionHours
        {
            get { return _productionHours; }
            set 
            { 
                SetProperty(ref _productionHours, value);
                UpdateCalculedFinancialFields(nameof(ProductionHours));
            }
        }
        public float HourCost
        {
            get { return _hourCost; }
            set 
            { 
                SetProperty(ref _hourCost, value);
                UpdateCalculedFinancialFields(nameof(HourCost));
            }
        }
        public float AdditionalCost
        {
            get { return _additionalCost; }
            set 
            { 
                SetProperty(ref _additionalCost, value);
                UpdateCalculedFinancialFields(nameof(AdditionalCost));
            }
        }
        public float ProfitPercentage
        {
            get { return _profitPercentage; }
            set 
            { 
                SetProperty(ref _profitPercentage, value);
                UpdateCalculedFinancialFields(nameof(ProfitPercentage));
            }
        }
        public float FinalPrice
        {
            get { return _finalPrice; }
            set 
            { 
                SetProperty(ref _finalPrice, value);
                UpdateCalculedFinancialFields(nameof(FinalPrice));
            }
        }
        public float LaborCost
        {
            get { return _laborCost; }
            set { SetProperty(ref _laborCost, value); }
        }
        public float SuggestedPrice
        {
            get { return _suggestedPrice; }
            set { SetProperty(ref _suggestedPrice, value); }
        }
        public float ProfitPracticed
        {
            get { return _profitPracticed; }
            set { SetProperty(ref _profitPracticed, value); }
        }
        public float ProfitValue
        {
            get { return _profitValue; }
            set { SetProperty(ref _profitValue, value); }
        }
        public int VariationId
        {
            get { return _variationId; }
            set { SetProperty(ref _variationId, value); }
        }
        public string VariationName
        {
            get { return _variationName; }
            set { SetProperty(ref _variationName, value); }
        }
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
        public ProductCreateEditPageViewModel(INavigationService navigationService, 
                                                IProductService productService, 
                                                IPictureGalleryService galleryService,
                                                IProductPictureService productPicture,
                                                IProductYarnService productYarnService,
                                                IFeedStockService feedStockService,
                                                IProductFinalcialService productFinalcialService)
            :base(navigationService)
        {
            _productService = productService;
            _galleryService = galleryService;
            _productPictureService = productPicture;
            _productYarnService = productYarnService;
            _feedStockService = feedStockService;
            _productFinalcialService = productFinalcialService;

            SaveCommand = new DelegateCommand(Save);
            SavePictureCommand = new DelegateCommand(SavePictureAsync);
            DeletePictureCommand = new DelegateCommand<object>(DeletePicture);
            CreateVariationCommand = new DelegateCommand(CreateVariation);
            CreateYarnCommand = new DelegateCommand<object>(CreateYarn);

            Pictures = new ObservableCollection<ProductPicture>();
            YarnGroups = new ObservableCollection<ProductYarnGroup>();
            FeedStockGroups = new ObservableCollection<FeedStockGroup>();
        }

        private void CreateYarn(object obj)
        {
            var pickerYarn = (YarnPickerControl)obj;
            var yarn = new ProductYarn() 
            {
                ProductId = _idProduct,
                Cost = pickerYarn.Consumption * (pickerYarn.SelectedYarn.Price / pickerYarn.SelectedYarn.InventoryTotal),
                Consumption = pickerYarn.Consumption,
                VariationId = _variationId,
                Yarn = pickerYarn.SelectedYarn,
                VariationName = _variationName
            };

            _productYarnService.UpsertItem(yarn);

            LoadYarnsGroups();
        }
        private async void CreateVariation()
        {
            string result = await Prism.PrismApplicationBase.Current.MainPage.DisplayPromptAsync("Variação", "Nome da Variação :", "Salvar", "Cancelar", "Padrão");
            if (!string.IsNullOrEmpty(result))
            {
                var id = YarnGroups.ToList().Count() > 0 ? YarnGroups.ToList().Max(x => x.VariationId) : 0;
                id++;

                var yarnGroup = new ProductYarnGroup(id, result);
                var yarnCollection = new ProductYarnCollection(id, result);
                yarnGroup.Add(yarnCollection);
                YarnGroups.Add(yarnGroup);
            }
        }
        private async void LoadYarnsGroups()
        {
            var yarnGroups = await GetYarnsGroups();
            YarnGroups.Clear();

            foreach(var item in yarnGroups)
            {
                YarnGroups.Add(item);
            }
        }
        private async Task<IList<ProductYarnGroup>> GetYarnsGroups()
        {
            return await _productYarnService.GetProductYarnsGroup(_idProduct);
        }
        private async void LoadAllYarns()
        {
            var feedStockGroups = await GetAllYarnsGroups();
            FeedStockGroups.Clear();

            foreach (var item in feedStockGroups)
            {
                FeedStockGroups.Add(item);
            }
        }
        private async Task<IList<FeedStockGroup>> GetAllYarnsGroups()
        {
            return await _feedStockService.GetGroupItems();
        }
        private async void DeletePicture(object picture)
        {
            if (picture is ProductPicture productPicture)
            {
                await _productPictureService.DeletePicture(productPicture);
                GetPictures();
            }
        }
        private async void GetPictures()
        {
            IList<ProductPicture> pictures = await _productPictureService.GetPicturesByProductId(_idProduct);
            Pictures.Clear();

            foreach (var item in pictures)
            {
                Pictures.Add(item);
            }
        }
        private async void SavePictureAsync()
        {
            Stream stream = await _galleryService.GetPictureStreamAsync();
            if (stream != null)
            {
                var picture = new ProductPicture()
                {
                    ProductId = _idProduct
                };
                _productPictureService.UpsertPicture(picture, stream);

                GetPictures();
            }
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

            foreach(var item in _productFinalcials)
            {
                _productFinalcialService.UpSertItem(item);
            }
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
                var product = _productService.GetItemByName(productName).Result;
                if (product == null)
                {
                    product = new Product()
                    {
                        Name = productName
                    };

                    _productService.UpsertItem(product);
                    product = _productService.GetItemByName(productName).Result;
                }

                LoadPropertys(product);
            }
        }
        private async void LoadPropertys(Product product)
        {
            _idProduct = product.Id;
            ProductCode = product.ProductCode;
            Name = product.Name;
            Width = product.Width;
            Height = product.Height;
            Weight = product.Weight;
            Difficulty = product.Difficulty;
            Comments = product.Comments;           

            GetPictures();
            LoadYarnsGroups();
            LoadAllYarns();
            _productFinalcials = (await _productFinalcialService.GetFinalcialsByProductId(_idProduct)).ToList() ;
        }
        private async void LoadVariantPropertys()
        {
            if (Variation == null)
                return;

            ProductFinalcial financial = _productFinalcials.Where(x => x.VariationId == Variation.VariationId).FirstOrDefault();
            if(financial != null)
            {
                financial.YarnsCost = Variation.TotalCost;
                YarnsCost = Variation.TotalCost;
                ProductionHours = financial.ProductionHours;
                HourCost = financial.HourCost;
                AdditionalCost = financial.AdditionalCost;
                ProfitPercentage = financial.ProfitPercentage;
                FinalPrice = financial.FinalPrice;
                LaborCost = financial.LaborCost;
                SuggestedPrice = financial.SuggestedPrice;
                ProfitPracticed = financial.ProfitPracticed;
                ProfitValue = financial.ProfitValue;
            }
            else
            {
                YarnsCost = Variation.TotalCost;
                ProductionHours = 0;
                HourCost = 0;
                AdditionalCost = 0;
                ProfitPercentage = 0;
                FinalPrice = Variation.TotalCost;
                LaborCost = 0;
                SuggestedPrice = Variation.TotalCost;
                ProfitPracticed = 0;
                ProfitValue = 0;

                var financialItem = new ProductFinalcial()
                {
                    YarnsCost = YarnsCost,
                    ProductionHours = ProductionHours,
                    HourCost = HourCost,
                    AdditionalCost = AdditionalCost,
                    ProfitPercentage = ProfitPercentage,
                    FinalPrice = FinalPrice,
                    ProductId = _idProduct,
                    VariationId = Variation.VariationId           
                };

                _productFinalcialService.UpSertItem(financialItem);
                _productFinalcials = (await _productFinalcialService.GetFinalcialsByProductId(_idProduct)).ToList();
            }            
        }        
        private void UpdateCalculedFinancialFields(string propertyName)
        {
            if (Variation == null)
                return;

            var financialItem = _productFinalcials.Where(x => x.VariationId == Variation.VariationId).FirstOrDefault();

            if (financialItem == null)
                return;

            switch (propertyName)
            {
                case nameof(ProductionHours):
                    financialItem.ProductionHours = ProductionHours;
                    break;
                case nameof(HourCost):
                    financialItem.HourCost = HourCost;
                    break;
                case nameof(AdditionalCost):
                    financialItem.AdditionalCost = AdditionalCost;
                    break;
                case nameof(ProfitPercentage):
                    financialItem.ProfitPercentage = ProfitPercentage;
                    break;
                case nameof(FinalPrice):
                    financialItem.FinalPrice = FinalPrice;
                    break;
                default:
                    break;
            }                                                         
            LaborCost = financialItem.LaborCost;
            SuggestedPrice = financialItem.SuggestedPrice;
            ProfitPracticed = financialItem.ProfitPracticed;
            ProfitValue = financialItem.ProfitValue;
        }
    }
}
