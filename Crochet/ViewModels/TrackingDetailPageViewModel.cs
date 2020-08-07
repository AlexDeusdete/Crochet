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

namespace Crochet.ViewModels
{
    public class TrackingDetailPageViewModel : ViewModelBase
    {
        #region Services
        private readonly ISaleService _saleService;
        #endregion

        #region Propertys
        private Sale _sale;
        public Sale Sale
        {
            get { return _sale; }
            set { SetProperty(ref _sale, value); }
        }
        #endregion

        #region Collections
        public ObservableCollection<SaleItem> SaleItems { get; set; }
        #endregion

        #region Commands
        public ICommand NextStatusCommand { get; set; }
        #endregion
        public TrackingDetailPageViewModel(INavigationService navigationService,
                                            ISaleService saleService) : base(navigationService)
        {
            _saleService = saleService;
            SaleItems = new ObservableCollection<SaleItem>();

            NextStatusCommand = new DelegateCommand(NextStatus);
        }

        private void NextStatus()
        {
            _sale.Status += 1;

            var sale = new Sale
            {
                Id = _sale.Id,
                Customer = _sale.Customer,
                DeliveryDate = _sale.DeliveryDate,
                Discount = _sale.Discount,
                Finalized = _sale.Finalized,
                Observation = _sale.Observation,
                SaleDate = _sale.SaleDate,
                Status = _sale.Status,
                TotalPrice = _sale.TotalPrice
            };

            if (sale.Status == 4)
                sale.DeliveryDate = DateTime.Now;

            if (sale.Status == 5)
                sale.Finalized = true;

            Sale = sale;
            _saleService.PutInsertSale(sale);
        }

        private async void LoadItems()
        {
            var items = await GetSaleItemsAsync();
            SaleItems.Clear();

            foreach (var item in items)
            {
                SaleItems.Add(item);
            }
        }

        private async Task<IList<SaleItem>> GetSaleItemsAsync()
        {
            return await _saleService.GetSaleItems(_sale.Id);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Sale = parameters.GetValue<Sale>("sale");
            LoadItems();
        }
    }
}
