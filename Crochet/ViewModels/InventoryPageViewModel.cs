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
    public class InventoryPageViewModel : BindableBase, INavigationAware
    {
        private readonly IFeedStockService _feedStockService;
        private readonly INavigationService _navigationService;
        public ObservableCollection<FeedStockGroup> FeedStockGroups { get; private set; }
        public ICommand NavigateToFeedStockCreateCommand { get; private set; }
        public InventoryPageViewModel(IFeedStockService feedStockService, INavigationService navigationService)
        {
            _feedStockService = feedStockService;
            _navigationService = navigationService;

            NavigateToFeedStockCreateCommand = new DelegateCommand(NavigateToFeedStockCreate);
            FeedStockGroups = new ObservableCollection<FeedStockGroup>();
        }

        private void NavigateToFeedStockCreate()
        {
            _navigationService.NavigateAsync("FeedStockCreatePage");
        }

        private async Task<IList<FeedStockGroup>> GetFeedStockGroups()
        {
            return await _feedStockService.GetGroupItems();
        }

        public void OnNavigatedFrom(INavigationParameters parameters){}

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            var feedStockGroups = await GetFeedStockGroups();
            FeedStockGroups.Clear();

            foreach(var item in feedStockGroups)
            {
                FeedStockGroups.Add(item);
            }
        }
    }
}
