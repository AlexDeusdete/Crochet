using Crochet.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crochet.ViewModels
{
    public class TrackingDetailPageViewModel : ViewModelBase
    {
        #region Propertys
        private Sale _sale;
        public Sale Sale 
        {
            get { return _sale; }
            set { SetProperty(ref _sale, value); }
        }
        #endregion
        public TrackingDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Sale = parameters.GetValue<Sale>("sale");
        }
    }
}
