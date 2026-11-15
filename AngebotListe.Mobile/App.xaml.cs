using Microsoft.Extensions.DependencyInjection;

namespace AngebotListe.Mobile;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		// Anmeldung abgelaufen (z. B. Server neu gestartet) → zurück zur Anmeldung
		Services.DataService.AnmeldungAbgelaufen += () =>
		{
			if (Windows.Count > 0 && Windows[0].Page is not NavigationPage { CurrentPage: Views.LoginPage })
				Windows[0].Page = new NavigationPage(new Views.LoginPage());
		};
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		// Eğer önceden login yapılmışsa direkt MainPage'e git
		var loggedIn = Preferences.Get("logged_in", false) && Preferences.ContainsKey("api_token");
		Page startPage = loggedIn
			? (Page)new NavigationPage(new MainPage())
			: new NavigationPage(new Views.LoginPage());

		return new Window(startPage);
	}
}