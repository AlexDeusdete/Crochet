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

namespace Crochet.ViewModels
{
    public class NewSalePageViewModel : ViewModelBase
    {
        private readonly IProductTypeService _productTypeService;

        public ObservableCollection<ProductType> ProductTypes { get;private set; }
        public NewSalePageViewModel(INavigationService navigationService,
                                    IProductTypeService productTypeService):base(navigationService)
        {
            _productTypeService = productTypeService;

            ProductTypes = new ObservableCollection<ProductType>();
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadProductTypes();
        }
    }
}
