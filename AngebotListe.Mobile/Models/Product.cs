using AngebotListe.Mobile.Services;

namespace AngebotListe.Mobile.Models;

public class Product
{
    public int ProduktId { get; set; }
    public string? ProduktNr { get; set; }
    public string? Name { get; set; }
    public string? Beschreibung { get; set; }
    public decimal Preis { get; set; }
    public string? Kategorie { get; set; }
    public bool Aktiv { get; set; }

    // Bild-Bytes nur, wenn sie in der App neu gewählt wurden.
    // Die Liste vom Server enthält KEINE Bilder mehr (das machte die Seite so langsam);
    // stattdessen wird ein kleines Vorschaubild pro Produkt nachgeladen und zwischengespeichert.
    public byte[]? Bild { get; set; }
    public bool HatBild { get; set; }
    public int BildVersion { get; set; }

    // Helpers für UI Binding
    public string PreisFormatted => Preis.ToString("0.00") + " €";

    public ImageSource? BildSource =>
        Bild is { Length: > 0 } ? ImageSource.FromStream(() => new MemoryStream(Bild))
        : HatBild ? BildVomServer(240) : null;

    public ImageSource? BildGross => Bild is { Length: > 0 } ? BildSource : HatBild ? BildVomServer(900) : null;

    private ImageSource BildVomServer(int breite) => new UriImageSource
    {
        Uri = new Uri(DataService.BildUrl(ProduktId, BildVersion, breite)),
        CachingEnabled = true,
        CacheValidity = TimeSpan.FromDays(30)
    };
}
