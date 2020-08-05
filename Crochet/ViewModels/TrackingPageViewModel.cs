using Crochet.Interfaces;
using Crochet.Models;
using Crochet.Views;
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
    public class TrackingPageViewModel : ViewModelBase
    {
        #region Services
        private readonly ISaleService _saleService;
        #endregion

        #region Collections
        public ObservableCollection<Sale> Sales { get; private set; }
        #endregion

        #region Command
        public ICommand NavigateToTrackingDetailCommand { get; private set; }
        #endregion
        public TrackingPageViewModel(INavigationService navigationService,
                                     ISaleService saleService
                                     ):base(navigationService)
        {
            _saleService = saleService;

            NavigateToTrackingDetailCommand = new DelegateCommand<object>(NavigateToTrackingDetail);
            Sales = new ObservableCollection<Sale>();
        }

        private void NavigateToTrackingDetail(object obj)
        {
            NavigationService.NavigateAsync("TrackingDetailPage", new NavigationParameters { { "sale", (Sale)obj } });
        }

        private async void LoadSales(bool? Finalized)
        {
            var items = await GetSaleAsync(Finalized);
            Sales.Clear();

            foreach (var item in items)
            {
                Sales.Add(item);
            }
        }

        private async Task<IList<Sale>> GetSaleAsync(bool? Finalized)
        {
            return await _saleService.GetSales(null, Finalized);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var finalized = parameters.GetValue<bool?>("finalized");

            LoadSales(finalized);           
        }
    }
}
