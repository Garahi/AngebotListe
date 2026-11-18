using AngebotListe.Mobile.Models;
using AngebotListe.Mobile.Services;
using System.Collections.ObjectModel;

namespace AngebotListe.Mobile.Views;

public partial class NeuesAngebotPage : ContentPage
{
    private readonly ObservableCollection<Product> _alleProdukte = new();
    private readonly ObservableCollection<Product> _auswahl = new();
    private string _angebotNr = "";

    public NeuesAngebotPage()
    {
        InitializeComponent();
        cvAlleProdukte.ItemsSource = _alleProdukte;
        cvAuswahl.ItemsSource = _auswahl;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (string.IsNullOrEmpty(_angebotNr))
        {
            _angebotNr = await DataService.NextAngebotNrAsync();
            lblAngebotNr.Text = _angebotNr;
        }
        await LadeAlleProdukte();
        UpdateBadge();
    }

    private async Task LadeAlleProdukte(string? search = null)
    {
        var list = await DataService.GetProductsAsync(search);
        _alleProdukte.Clear();
        foreach (var p in list) _alleProdukte.Add(p);
    }

    private async void OnSucheChanged(object sender, TextChangedEventArgs e)
    {
        await LadeAlleProdukte(e.NewTextValue);
    }

    private async void OnProduktSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Product p) return;
        cvAlleProdukte.SelectedItem = null;

        // Eğer zaten auswahl'da varsa onu düzenle, yoksa orijinali düzenle
        var bestehend = _auswahl.FirstOrDefault(x => x.ProduktId == p.ProduktId);
        var quelle = bestehend ?? p;

        var page = new ProduktBearbeitenPage(quelle);
        page.Saved += updated =>
        {
            // Eski'yi sil, yenisini ekle
            var alt = _auswahl.FirstOrDefault(x => x.ProduktId == updated.ProduktId);
            if (alt != null) _auswahl.Remove(alt);
            _auswahl.Add(updated);
            UpdateBadge();
        };
        await Navigation.PushAsync(page);
    }

    private void OnEntfernenClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is int pid)
        {
            var item = _auswahl.FirstOrDefault(x => x.ProduktId == pid);
            if (item != null)
            {
                _auswahl.Remove(item);
                UpdateBadge();
            }
        }
    }

    private void UpdateBadge()
    {
        lblBadge.Text = $"{_auswahl.Count} Produkte";
    }

    private void OnSpeichernTapped(object sender, TappedEventArgs e) => OnSpeichernClicked(sender, e);

    private async void OnSpeichernClicked(object sender, EventArgs e)
    {
        if (_auswahl.Count == 0)
        {
            await DisplayAlert("Hinweis", "Bitte wählen Sie mindestens ein Produkt aus.", "OK");
            return;
        }

        var id = await DataService.CreateOrUpdateAngebotAsync(_angebotNr, _auswahl.ToList());
        if (id > 0)
        {
            await DisplayAlert("OK",
                $"Angebot {_angebotNr} mit {_auswahl.Count} Produkten gespeichert.", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Fehler", "Speichern fehlgeschlagen.", "OK");
        }
    }
}
