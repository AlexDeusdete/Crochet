using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crochet.ViewModels
{
    public class TabbedHomePageViewModel : ViewModelBase
    {
        public TabbedHomePageViewModel(INavigationService navigationService)
            :base(navigationService)
        {

        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
        }
    }
}
