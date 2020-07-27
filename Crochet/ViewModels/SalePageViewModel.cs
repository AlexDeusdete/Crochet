﻿using Prism.Commands;
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
        public ICommand NavigateToNewSaleCommand { get; set; }
        public SalePageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            NavigateToNewSaleCommand = new DelegateCommand(() => 
            {
                NavigationService.NavigateAsync("NewSalePage");
            });
        }
    }
}
