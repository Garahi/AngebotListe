using AngebotListe.Mobile.Models;
using AngebotListe.Mobile.Services;
using System.Collections.ObjectModel;

namespace AngebotListe.Mobile.Views;

// Produkt-Stammdaten: anzeigen, suchen, neu anlegen, bearbeiten, löschen
public partial class ProduktListePage : ContentPage
{
    private readonly ObservableCollection<Product> _items = new();
    private bool _erstesMal = true;

    public ProduktListePage()
    {
        InitializeComponent();
        cvProdukte.ItemsSource = _items;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Lade(txtSuche.Text, neuLaden: _erstesMal);
        _erstesMal = false;
    }

    private async Task Lade(string? search = null, bool neuLaden = false)
    {
        var list = await DataService.GetProductsAsync(search, neuLaden);
        _items.Clear();
        foreach (var p in list) _items.Add(p);
        busy.IsRunning = false;
        busy.IsVisible = false;
        lblLeer.Text = string.IsNullOrWhiteSpace(search) ? "Keine Produkte – oben rechts „＋ Neu“" : "Keine Treffer";
        lblAnzahl.Text = $"{_items.Count} Produkte · ✏️ = ändern · 🗑 = löschen";
    }

    private async void OnSucheChanged(object sender, TextChangedEventArgs e)
    {
        await Lade(e.NewTextValue);   // filtert lokal, ohne Serveranfrage
    }

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Lade(txtSuche.Text, neuLaden: true);
        refreshView.IsRefreshing = false;
    }

    // ── Neu ─────────────────────────────────────────────
    private async void OnNeuClicked(object sender, EventArgs e)
    {
        await Oeffnen(new Product { Aktiv = true });
    }

    // ── Bearbeiten ──────────────────────────────────────
    private async void OnProduktSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Product p) return;
        cvProdukte.SelectedItem = null;
        await Oeffnen(p);
    }

    private async Task Oeffnen(Product p)
    {
        var page = new ProduktBearbeitenPage(p, stammdaten: true);
        page.Saved += async geaendert =>
        {
            var (ok, msg) = await DataService.SaveProductAsync(geaendert);
            if (!ok) await DisplayAlert("Fehler", msg, "OK");
            await Lade(txtSuche.Text, neuLaden: true);
        };
        page.Geloescht += async id => await Loeschen(id, frage: false);
        await Navigation.PushAsync(page);
    }

    // Knöpfe in jeder Zeile
    private async void OnAendernTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Product p) await Oeffnen(p);
    }

    private async void OnLoeschenTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is not Product p) return;
        if (!await DisplayAlert("Löschen?", $"„{p.Name}“ wirklich löschen?", "Löschen", "Abbrechen")) return;
        await Loeschen(p.ProduktId, frage: false);
    }

    // ── Löschen ─────────────────────────────────────────
    private async void OnSwipeDelete(object sender, EventArgs e)
    {
        if (sender is SwipeItem si && si.CommandParameter is Product p)
        {
            if (!await DisplayAlert("Löschen?", $"„{p.Name}“ wirklich löschen?", "Löschen", "Abbrechen")) return;
            await Loeschen(p.ProduktId, frage: false);
        }
    }

    private async Task Loeschen(int produktId, bool frage)
    {
        var (ok, msg) = await DataService.DeleteProductAsync(produktId);
        await DisplayAlert(ok ? "OK" : "Fehler", msg, "OK");
        await Lade(txtSuche.Text, neuLaden: true);
    }
}
