using Microsoft.Extensions.DependencyInjection;

namespace AngebotListe.Mobile;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		// Eğer önceden login yapılmışsa direkt MainPage'e git
		var loggedIn = Preferences.Get("logged_in", false);
		Page startPage = loggedIn
			? (Page)new NavigationPage(new MainPage())
			: new NavigationPage(new Views.LoginPage());

		return new Window(startPage);
	}
}