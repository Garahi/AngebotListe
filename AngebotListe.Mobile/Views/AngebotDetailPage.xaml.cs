using AngebotListe.Mobile.Models;
using AngebotListe.Mobile.Services;
using System.Collections.ObjectModel;
using System.Text;

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
    private async void OnTeilenClicked(object sender, EventArgs e)
    {
        if (_produkte.Count == 0)
        {
            await DisplayAlert("Hinweis", "Keine Produkte zum Teilen.", "OK");
            return;
        }

        // 1) Text mesajı oluştur
        var sb = new StringBuilder();
        sb.AppendLine($"📋 *Angebot: {_angebot.AngebotNr}*");
        sb.AppendLine($"📅 {_angebot.DatumFormatted}");
        sb.AppendLine();
        decimal toplam = 0;
        foreach (var ap in _produkte)
        {
            sb.AppendLine($"• *{ap.ProduktName}*  —  {ap.Preis:0.00} €");
            if (!string.IsNullOrWhiteSpace(ap.Beschreibung))
                sb.AppendLine($"  _{ap.Beschreibung}_");
            toplam += ap.Preis;
        }
        sb.AppendLine();
        sb.AppendLine($"💰 *Gesamt: {toplam:0.00} €*");

        var text = sb.ToString();

        // 2) Resimleri temp klasöre yaz (paylaşım için)
        var tempDir = FileSystem.CacheDirectory;
        var bildDateien = new List<ShareFile>();
        int idx = 0;
        foreach (var ap in _produkte)
        {
            if (ap.Bild is { Length: > 0 })
            {
                idx++;
                var path = Path.Combine(tempDir, $"angebot_{_angebot.AngebotNr}_{idx}.jpg");
                await File.WriteAllBytesAsync(path, ap.Bild);
                bildDateien.Add(new ShareFile(path));
            }
        }

        // 3) Share API ile paylaş
        try
        {
            if (bildDateien.Count > 0)
            {
                await Share.Default.RequestAsync(new ShareMultipleFilesRequest
                {
                    Title = _angebot.AngebotNr,
                    Files = bildDateien,
                    PresentationSourceBounds = new Rect(0, 0, 0, 0),
                });

                // Resimler gönderildikten sonra text'i de yolla
                await Task.Delay(500);
                await Share.Default.RequestAsync(new ShareTextRequest
                {
                    Title = _angebot.AngebotNr,
                    Text = text,
                });
            }
            else
            {
                await Share.Default.RequestAsync(new ShareTextRequest
                {
                    Title = _angebot.AngebotNr,
                    Text = text,
                });
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fehler", "Teilen fehlgeschlagen:\n" + ex.Message, "OK");
        }
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
