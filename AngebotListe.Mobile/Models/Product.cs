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
    public byte[]? Bild { get; set; }

    // Helpers für UI Binding
    public string PreisFormatted => Preis.ToString("0.00") + " €";
    public ImageSource? BildSource =>
        Bild is { Length: > 0 } ? ImageSource.FromStream(() => new MemoryStream(Bild)) : null;
}
