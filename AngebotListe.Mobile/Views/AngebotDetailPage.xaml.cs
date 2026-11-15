using AngebotListe.Mobile.Models;
using AngebotListe.Mobile.Services;
using System.Collections.ObjectModel;

namespace AngebotListe.Mobile.Views;

public partial class AngebotDetailPage : ContentPage
{
    private readonly Angebot _angebot;
    private readonly ObservableCollection<AngebotProdukt> _produkte = new();

    public AngebotDetailPage(Angebot angebot)
    {
        InitializeComponent();
        _angebot = angebot;
        cvProdukte.ItemsSource = _produkte;

        lblNr.Text = "📄 " + _angebot.AngebotNr;
        lblDatum.Text = "Datum: " + _angebot.DatumFormatted;
        lblFirma.Text = AngebotBild.Firmenname;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LadeProdukte();
    }

    private async Task LadeProdukte()
    {
        var list = await DataService.GetAngebotProdukteAsync(_angebot.AngebotID);
        _produkte.Clear();
        foreach (var p in list) _produkte.Add(p);
        lblAnzahl.Text = $"Produkte: {_produkte.Count}";
    }

    // ── Tıklayınca düzenle ──────────────────────────────
    private async void OnProduktSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not AngebotProdukt ap) return;
        cvProdukte.SelectedItem = null;

        // AngebotProdukt → Product convert (ProduktBearbeitenPage Product alıyor)
        var prodForEdit = new Product
        {
            ProduktId = ap.ProduktID,
            Name = ap.ProduktName,
            Beschreibung = ap.Beschreibung,
            Preis = ap.Preis,
            Bild = ap.Bild,
        };

        var page = new ProduktBearbeitenPage(prodForEdit);
        page.Saved += async updated =>
        {
            // SQL'de güncelle
            ap.ProduktName = updated.Name;
            ap.Beschreibung = updated.Beschreibung;
            ap.Preis = updated.Preis;
            ap.Bild = updated.Bild;

            var ok = await DataService.UpdateAngebotProduktAsync(ap);
            if (ok) await LadeProdukte();
        };
        await Navigation.PushAsync(page);
    }

    // ── Swipe ile sil ───────────────────────────────────
    private async void OnSwipeDelete(object sender, EventArgs e)
    {
        if (sender is SwipeItem si && si.CommandParameter is int id)
        {
            var ok = await DisplayAlert("Löschen?", "Diesen Eintrag wirklich löschen?", "Ja", "Abbrechen");
            if (!ok) return;
            await DataService.DeleteAngebotProduktAsync(id);
            await LadeProdukte();
        }
    }

    // ── Border Tapped wrappers ──────────────────────────
    private void OnHinzufuegenTapped(object sender, TappedEventArgs e) => OnHinzufuegenClicked(sender, e);
    private void OnTeilenTapped(object sender, TappedEventArgs e) => OnTeilenClicked(sender, e);
    private void OnLoeschenTapped(object sender, TappedEventArgs e) => OnLoeschenClicked(sender, e);

    // ── Hinzufügen ──────────────────────────────────────
    private async void OnHinzufuegenClicked(object sender, EventArgs e)
    {
        // Tüm ürünler listesini bir popup'ta göster
        var alle = await DataService.GetProductsAsync();
        if (alle.Count == 0)
        {
            await DisplayAlert("Hinweis", "Keine Produkte in der Datenbank.", "OK");
            return;
        }

        var auswahl = alle.Select(p => $"{p.Name} ({p.Preis:0.00} €)").ToArray();
        var sec = await DisplayActionSheet("Produkt wählen", "Abbrechen", null, auswahl);
        if (string.IsNullOrEmpty(sec) || sec == "Abbrechen") return;

        var idx = Array.IndexOf(auswahl, sec);
        if (idx < 0 || idx >= alle.Count) return;

        var prod = alle[idx];

        // Düzenleme sayfasını aç (fiyat/resim ayarlanabilsin önce)
        var page = new ProduktBearbeitenPage(prod);
        page.Saved += async updated =>
        {
            await DataService.AddProduktToAngebotAsync(_angebot.AngebotID, updated);
            await LadeProdukte();
        };
        await Navigation.PushAsync(page);
    }

    // ── Teilen (WhatsApp / Native Share) ────────────────
    // Ein Bild im Stil des PC-PDFs (Firmenname oben, Produktkarten), bei vielen Produkten mehrere Bilder.
    private async void OnTeilenClicked(object sender, EventArgs e)
    {
        if (_produkte.Count == 0)
        {
            await DisplayAlert("Hinweis", "Keine Produkte zum Teilen.", "OK");
            return;
        }

        try
        {
            var pfade = await AngebotBild.ErstellenAsync(_angebot, _produkte.ToList());
            await Share.Default.RequestAsync(new ShareMultipleFilesRequest
            {
                Title = $"Angebot {_angebot.AngebotNr}",
                Files = pfade.Select(x => new ShareFile(x, "image/jpeg")).ToList(),
                PresentationSourceBounds = new Rect(0, 0, 0, 0),
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fehler", "Teilen fehlgeschlagen:\n" + ex.Message, "OK");
        }
    }

    // ── Firmenname ändern (Tippen auf den Namen oben) ───
    private async void OnFirmaTapped(object sender, TappedEventArgs e)
    {
        var neu = await DisplayPromptAsync("Firmenname", "Erscheint oben auf dem WhatsApp-Bild:",
                                           "Speichern", "Abbrechen", initialValue: AngebotBild.Firmenname, maxLength: 60);
        if (neu == null) return;
        AngebotBild.Firmenname = neu;
        lblFirma.Text = AngebotBild.Firmenname;
    }

    // ── Komple Angebot sil ──────────────────────────────
    private async void OnLoeschenClicked(object sender, EventArgs e)
    {
        var ok = await DisplayAlert("Löschen?",
            $"Angebot {_angebot.AngebotNr} komplett löschen?",
            "Ja", "Abbrechen");
        if (!ok) return;

        var success = await DataService.DeleteAngebotAsync(_angebot.AngebotID);
        if (success)
        {
            await DisplayAlert("OK", "Angebot gelöscht", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Fehler", "Löschen fehlgeschlagen", "OK");
        }
    }
}
