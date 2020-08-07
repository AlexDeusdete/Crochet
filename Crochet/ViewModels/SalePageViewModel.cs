using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Crochet.ViewModels
{
    public class SalePageViewModel : ViewModelBase
    {
        #region Commands
        public ICommand NavigateToNewSaleCommand { get; set; }
        public ICommand NavigateToTrackingCommand { get; set; }
        public ICommand NavigateToSaleClosedCommand { get; set; }
        #endregion
        public SalePageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            NavigateToNewSaleCommand = new DelegateCommand(() => 
            {
                NavigationService.NavigateAsync("NewSalePage");
            });

            NavigateToTrackingCommand = new DelegateCommand(() =>
            {
                NavigationService.NavigateAsync("TrackingPage", new NavigationParameters { { "finalized", false} });
            });

            NavigateToSaleClosedCommand = new DelegateCommand(() =>
            {
                NavigationService.NavigateAsync("TrackingPage", new NavigationParameters { { "finalized", true } });
            });
        }
    }
}
