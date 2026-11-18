using AngebotListe.Mobile.Models;
using AngebotListe.Mobile.Services;
using System.Collections.ObjectModel;

namespace AngebotListe.Mobile.Views;

public partial class AngebotListePage : ContentPage
{
    private readonly ObservableCollection<Angebot> _items = new();
    private List<Angebot> _alle = new();

    public AngebotListePage()
    {
        InitializeComponent();
        cvAngebote.ItemsSource = _items;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Lade();
    }

    private async Task Lade()
    {
        _alle = await DataService.GetAngeboteAsync();
        Filtern();
    }

    // ── Suche: Nr., Datum (z.B. "18.11" oder "2026"), Beschreibung ──
    private void Filtern()
    {
        var such = txtSuche.Text?.Trim() ?? "";
        IEnumerable<Angebot> treffer = _alle;

        if (such.Length > 0)
        {
            var woerter = such.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            treffer = _alle.Where(a =>
            {
                var text = $"{a.AngebotNr} {a.DatumFormatted} {a.Beschreibung}";
                return woerter.All(w => text.Contains(w, StringComparison.OrdinalIgnoreCase));
            });
        }

        _items.Clear();
        foreach (var a in treffer) _items.Add(a);

        lblTreffer.IsVisible = such.Length > 0;
        lblTreffer.Text = $"{_items.Count} von {_alle.Count} Angeboten";
        lblLeer.Text = such.Length > 0 ? "Kein Angebot gefunden" : "Keine Angebote vorhanden";
    }

    private void OnSucheChanged(object sender, TextChangedEventArgs e) => Filtern();

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Lade();
        refreshView.IsRefreshing = false;
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Angebot a) return;
        cvAngebote.SelectedItem = null;
        await Navigation.PushAsync(new AngebotDetailPage(a));
    }
}
