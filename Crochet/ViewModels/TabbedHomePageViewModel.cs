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
    }
}
