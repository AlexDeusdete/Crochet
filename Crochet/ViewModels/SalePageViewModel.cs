using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crochet.ViewModels
{
    public class SalePageViewModel : ViewModelBase
    {
        public SalePageViewModel(INavigationService navigationService)
            :base(navigationService)
        {

        }
    }
}
