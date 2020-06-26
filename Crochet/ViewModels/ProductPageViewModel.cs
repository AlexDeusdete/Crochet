using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Crochet.ViewModels
{
    public class ProductPageViewModel : ViewModelBase
    {
        public ICommand NavigateToProductCreateCommand { get; private set; }
        public ProductPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            NavigateToProductCreateCommand = new DelegateCommand(NavigateToProductCreate);
        }

        private async void NavigateToProductCreate()
        {
            await NavigationService.NavigateAsync("ProductCreateEditPage");
        }
    }
}
