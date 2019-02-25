using Prism;
using Prism.Ioc;
using Prism.DryIoc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using kcdnug.Mobile.Views;
using kcdnug.Mobile.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace kcdnug.Mobile
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MainMenuPage, MainMenuPageViewModel>();
            containerRegistry.RegisterForNavigation<EventListPage, EventListPageViewModel>();
            containerRegistry.RegisterForNavigation<SponsorListPage, SponsorListPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<EventDetailsPage, EventDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<SponsorInfoPage, SponsorInfoPageViewModel>();
            containerRegistry.RegisterForNavigation<CheckInPage, CheckInPageViewModel>();
        }
    }
}
