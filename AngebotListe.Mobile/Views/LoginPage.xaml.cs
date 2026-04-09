using AngebotListe.Mobile.Services;

namespace AngebotListe.Mobile.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginTapped(object sender, TappedEventArgs e)
    {
        lblStatus.IsVisible = false;

        var user = txtUsername.Text?.Trim() ?? "";
        var pass = txtPassword.Text ?? "";

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            ShowError("Bitte Benutzername und Passwort eingeben.");
            return;
        }

        loading.IsVisible = true;
        loading.IsRunning = true;

        var result = await DataService.AuthenticateAsync(user, pass);

        loading.IsVisible = false;
        loading.IsRunning = false;

        if (!result.Success)
        {
            ShowError(result.Message);
            return;
        }

        // Session bilgilerini kaydet
        Preferences.Set("user_id", result.UserId);
        Preferences.Set("user_name", result.Username);
        Preferences.Set("user_role", result.Role);
        Preferences.Set("logged_in", true);

        // MainPage'e geç
        Application.Current!.Windows[0].Page = new NavigationPage(new MainPage());
    }

    private void ShowError(string msg)
    {
        lblStatus.Text = msg;
        lblStatus.IsVisible = true;
    }
}
