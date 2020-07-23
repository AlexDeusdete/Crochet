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

        public ObservableCollection<FeedStockGroup> FeedStockGroups { get; private set; }
        public ICommand NavigateToFeedStockCreateCommand { get; private set; }
        public ICommand NavigateToFeedStockEditCommand { get; private set; }
        public ICommand DeleteFeedStockCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        #region Property 
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }
        #endregion
        public InventoryPageViewModel(IFeedStockService feedStockService, INavigationService navigationService)
            : base(navigationService)
        {
            _feedStockService = feedStockService;

            NavigateToFeedStockCreateCommand = new DelegateCommand(NavigateToFeedStockCreate);
            NavigateToFeedStockEditCommand = new DelegateCommand<object>(NavigateToFeedStockEdit);
            DeleteFeedStockCommand = new DelegateCommand<object>(DeleteFeedStock);
            RefreshCommand = new DelegateCommand(() =>
            {
                LoadItens();
            });

            FeedStockGroups = new ObservableCollection<FeedStockGroup>();
        }

        private async void DeleteFeedStock(object obj)
        {
            await _feedStockService.DeleteItem((FeedStock)obj);
            LoadItens();
        }

        private void NavigateToFeedStockCreate()
        {
            NavigationService.NavigateAsync("FeedStockCreateEditPage");
        }

        private async void NavigateToFeedStockEdit(object parameter)
        {
            var navParameters = new NavigationParameters
            {
                { "feedStock", parameter }
            };

            await NavigationService.NavigateAsync("FeedStockCreateEditPage", navParameters);
        }

        private async Task<IList<FeedStockGroup>> GetFeedStockGroups()
        {
            return await _feedStockService.GetGroupItems();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            LoadItens();
        }

        private async void LoadItens()
        {
            IsRefreshing = true;
            var feedStockGroups = await GetFeedStockGroups();
            FeedStockGroups.Clear();

            foreach (var item in feedStockGroups)
            {
                FeedStockGroups.Add(item);
            }
            IsRefreshing = false;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadItens();
        }
    }
}
