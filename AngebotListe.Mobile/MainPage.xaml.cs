using AngebotListe.Mobile.Views;

namespace AngebotListe.Mobile;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        var user = Preferences.Get("user_name", "");
        var role = Preferences.Get("user_role", "");
        if (!string.IsNullOrEmpty(user))
            lblUser.Text = $"👤 {user} ({role})";
    }

    private async void OnProduktListeTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new ProduktListePage());
    }

    private async void OnAngeboteTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new AngebotListePage());
    }

    private async void OnNeuesAngebotTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new NeuesAngebotPage());
    }

    private async void OnLogoutTapped(object sender, TappedEventArgs e)
    {
        var ok = await DisplayAlert("Abmelden?", "Möchten Sie sich wirklich abmelden?", "Ja", "Abbrechen");
        if (!ok) return;

        Preferences.Remove("logged_in");
        Preferences.Remove("user_id");
        Preferences.Remove("user_name");
        Preferences.Remove("user_role");

        Application.Current!.Windows[0].Page = new NavigationPage(new LoginPage());
    }
}
