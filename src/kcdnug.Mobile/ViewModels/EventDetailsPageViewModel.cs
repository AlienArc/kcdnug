using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using kcdnug.Mobile.Infrastructure;

namespace kcdnug.Mobile.ViewModels
{
    public class EventDetailsPageViewModel : AppMapViewModelBase
    {


        public EventDetailsPageViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
