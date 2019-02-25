using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using kcdnug.Mobile.Infrastructure;

namespace kcdnug.Mobile.ViewModels
{
    public class ProfilePageViewModel : AppMapViewModelBase
    {


        public ProfilePageViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
