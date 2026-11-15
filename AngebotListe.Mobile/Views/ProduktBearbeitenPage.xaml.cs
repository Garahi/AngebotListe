using AngebotListe.Mobile.Models;
using SkiaSharp;

namespace AngebotListe.Mobile.Views;

public partial class ProduktBearbeitenPage : ContentPage
{
    private readonly Product _original;
    private byte[]? _bildBytes;

    private readonly bool _stammdaten;

    // Sonuç — kullanıcı OK derse bu çağrılır
    public event Action<Product>? Saved;
    // Nur Stammdaten: Produkt soll gelöscht werden (ProduktId)
    public event Action<int>? Geloescht;

    // stammdaten = true: Produktverwaltung (mit Art.-Nr., Kategorie, Löschen);
    // false: Position in einem Angebot anpassen (wie bisher)
    public ProduktBearbeitenPage(Product produkt, bool stammdaten = false)
    {
        InitializeComponent();
        _original = produkt;
        _stammdaten = stammdaten;
        if (stammdaten)
        {
            Title = produkt.ProduktId == 0 ? "Neues Produkt" : "Produkt bearbeiten";
            boxStamm.IsVisible = true;
            btnLoeschen.IsVisible = produkt.ProduktId != 0;
            txtProduktNr.Text = produkt.ProduktNr ?? "";
            txtKategorie.Text = produkt.Kategorie ?? "";
        }

        // Mevcut değerleri yükle
        txtName.Text = produkt.Name ?? "";
        txtBeschreibung.Text = produkt.Beschreibung ?? "";
        txtPreis.Text = produkt.Preis.ToString("0.00");

        _bildBytes = produkt.Bild;
        UpdateBildAnzeige();
    }

    private void UpdateBildAnzeige()
    {
        if (_bildBytes is { Length: > 0 })
        {
            var b = _bildBytes;
            imgVorschau.Source = ImageSource.FromStream(() => new MemoryStream(b));
            lblNoImage.IsVisible = false;
        }
        else if (_original.HatBild)
        {
            // vorhandenes Bild vom Server (größere Vorschau), Bytes werden nur bei Änderung gesendet
            imgVorschau.Source = _original.BildGross;
            lblNoImage.IsVisible = false;
        }
        else
        {
            imgVorschau.Source = null;
            lblNoImage.IsVisible = true;
        }
    }

    // ── Border Tapped wrappers ──────────────────────────
    private void OnKameraTapped(object sender, TappedEventArgs e) => OnKameraClicked(sender, e);
    private void OnGalerieTapped(object sender, TappedEventArgs e) => OnGalerieClicked(sender, e);
    private void OnAbbrechenTapped(object sender, TappedEventArgs e) => OnAbbrechenClicked(sender, e);
    private void OnUebernehmenTapped(object sender, TappedEventArgs e) => OnUebernehmenClicked(sender, e);

    private async void OnKameraClicked(object sender, EventArgs e)
    {
        try
        {
            if (!MediaPicker.Default.IsCaptureSupported)
            {
                await DisplayAlert("Hinweis", "Kamera wird auf diesem Gerät nicht unterstützt.", "OK");
                return;
            }

            var foto = await MediaPicker.Default.CapturePhotoAsync();
            if (foto == null) return;

            using var stream = await foto.OpenReadAsync();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            _bildBytes = Verkleinern(ms.ToArray());
            UpdateBildAnzeige();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fehler", "Kamera konnte nicht geöffnet werden:\n" + ex.Message, "OK");
        }
    }

    private async void OnGalerieClicked(object sender, EventArgs e)
    {
        try
        {
            var foto = await MediaPicker.Default.PickPhotoAsync();
            if (foto == null) return;

            using var stream = await foto.OpenReadAsync();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            _bildBytes = Verkleinern(ms.ToArray());
            UpdateBildAnzeige();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fehler", "Bild konnte nicht geladen werden:\n" + ex.Message, "OK");
        }
    }

    // Handyfotos (oft 3–5 MB) vor dem Hochladen auf max. 1200 px / JPEG verkleinern
    private static byte[] Verkleinern(byte[] daten, int max = 1200)
    {
        try
        {
            using var codec = SKCodec.Create(new MemoryStream(daten));
            using var bmp = SKBitmap.Decode(daten);
            if (bmp == null) return daten;
            // Kamera-Ausrichtung (EXIF) berücksichtigen
            var orient = codec?.EncodedOrigin ?? SKEncodedOrigin.TopLeft;
            float f = Math.Min(1f, (float)max / Math.Max(bmp.Width, bmp.Height));
            int w = Math.Max(1, (int)(bmp.Width * f)), h = Math.Max(1, (int)(bmp.Height * f));
            bool quer = orient is SKEncodedOrigin.RightTop or SKEncodedOrigin.LeftBottom;
            using var surface = SKSurface.Create(new SKImageInfo(quer ? h : w, quer ? w : h));
            var c = surface.Canvas;
            c.Clear(SKColors.White);
            switch (orient)
            {
                case SKEncodedOrigin.BottomRight: c.RotateDegrees(180, w / 2f, h / 2f); break;
                case SKEncodedOrigin.RightTop: c.Translate(h, 0); c.RotateDegrees(90); break;
                case SKEncodedOrigin.LeftBottom: c.Translate(0, w); c.RotateDegrees(270); break;
            }
            using (var img = SKImage.FromBitmap(bmp))
                c.DrawImage(img, new SKRect(0, 0, w, h), new SKSamplingOptions(SKCubicResampler.Mitchell));
            using var snap = surface.Snapshot();
            using var data = snap.Encode(SKEncodedImageFormat.Jpeg, 85);
            return data.ToArray();
        }
        catch
        {
            return daten;
        }
    }

    private void OnLoeschenTapped(object sender, TappedEventArgs e) => OnLoeschenClicked();

    private async void OnLoeschenClicked()
    {
        if (!await DisplayAlert("Löschen?", $"„{_original.Name}“ wirklich löschen?", "Löschen", "Abbrechen")) return;
        await Navigation.PopAsync();
        Geloescht?.Invoke(_original.ProduktId);
    }

    private async void OnAbbrechenClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnUebernehmenClicked(object sender, EventArgs e)
    {
        if (!decimal.TryParse(txtPreis.Text?.Replace(",", "."),
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var preis))
        {
            await DisplayAlert("Fehler", "Ungültiger Preis.", "OK");
            return;
        }

        if (_stammdaten && string.IsNullOrWhiteSpace(txtName.Text))
        {
            await DisplayAlert("Fehler", "Bitte einen Produktnamen eingeben.", "OK");
            return;
        }

        // Yeni bir Product oluştur (orijinali değiştirmemek için)
        var updated = new Product
        {
            ProduktId = _original.ProduktId,
            ProduktNr = _stammdaten ? txtProduktNr.Text?.Trim() : _original.ProduktNr,
            Name = txtName.Text?.Trim() ?? "",
            Beschreibung = txtBeschreibung.Text?.Trim() ?? "",
            Preis = preis,
            Kategorie = _stammdaten ? txtKategorie.Text?.Trim() : _original.Kategorie,
            Aktiv = _original.Aktiv,
            Bild = _bildBytes,
            HatBild = _original.HatBild,
            BildVersion = _original.BildVersion,
        };

        Saved?.Invoke(updated);
        await Navigation.PopAsync();
    }
}
