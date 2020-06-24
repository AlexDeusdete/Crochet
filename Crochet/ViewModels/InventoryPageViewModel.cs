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
    public class InventoryPageViewModel : ViewModelBase
    {
        private readonly IFeedStockService _feedStockService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<FeedStockGroup> FeedStockGroups { get; private set; }
        public ICommand NavigateToFeedStockCreateCommand { get; private set; }
        public ICommand NavigateToFeedStockEditCommand { get; private set; }
        public InventoryPageViewModel(IFeedStockService feedStockService, INavigationService navigationService)
            : base(navigationService)
        {
            _feedStockService = feedStockService;
            _navigationService = navigationService;

            NavigateToFeedStockCreateCommand = new DelegateCommand(NavigateToFeedStockCreate);
            NavigateToFeedStockEditCommand = new DelegateCommand<object>(NavigateToFeedStockEdit);
            FeedStockGroups = new ObservableCollection<FeedStockGroup>();
        }

        private void NavigateToFeedStockCreate()
        {
            _navigationService.NavigateAsync("FeedStockCreateEditPage");
        }

        private void NavigateToFeedStockEdit(object parameter)
        {
            var navParameters = new NavigationParameters
            {
                { "feedStock", parameter }
            };

            _navigationService.NavigateAsync("FeedStockCreateEditPage", navParameters);
        }

        private async Task<IList<FeedStockGroup>> GetFeedStockGroups()
        {
            return await _feedStockService.GetGroupItems();
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
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
