using AngebotListe.Mobile.Models;

namespace AngebotListe.Mobile.Views;

public partial class ProduktBearbeitenPage : ContentPage
{
    private readonly Product _original;
    private byte[]? _bildBytes;

    // Sonuç — kullanıcı OK derse bu çağrılır
    public event Action<Product>? Saved;

    public ProduktBearbeitenPage(Product produkt)
    {
        InitializeComponent();
        _original = produkt;

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
            imgVorschau.Source = ImageSource.FromStream(() => new MemoryStream(_bildBytes));
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
            _bildBytes = ms.ToArray();
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
            _bildBytes = ms.ToArray();
            UpdateBildAnzeige();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fehler", "Bild konnte nicht geladen werden:\n" + ex.Message, "OK");
        }
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

        // Yeni bir Product oluştur (orijinali değiştirmemek için)
        var updated = new Product
        {
            ProduktId = _original.ProduktId,
            ProduktNr = _original.ProduktNr,
            Name = txtName.Text?.Trim() ?? "",
            Beschreibung = txtBeschreibung.Text?.Trim() ?? "",
            Preis = preis,
            Kategorie = _original.Kategorie,
            Aktiv = _original.Aktiv,
            Bild = _bildBytes,
        };

        Saved?.Invoke(updated);
        await Navigation.PopAsync();
    }
}
