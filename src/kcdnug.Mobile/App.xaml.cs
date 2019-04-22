using Prism;
using Prism.Ioc;
using kcdnug.Mobile.ViewModels;
using kcdnug.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace kcdnug.Mobile
{
	public partial class App
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

			await NavigationService.NavigateAsync("RootPage");
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<NavigationPage>();
			containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
			containerRegistry.RegisterForNavigation<RootPage, RootPageViewModel>();
			containerRegistry.RegisterForNavigation<EventsPage, EventsPageViewModel>();
			containerRegistry.RegisterForNavigation<SponsorsPage, SponsorsPageViewModel>();
			containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
		}
	}
}
