namespace AngebotListe.Mobile.Models;

public class Angebot
{
    public int AngebotID { get; set; }
    public string? AngebotNr { get; set; }
    public DateTime? Datum { get; set; }
    public string? Beschreibung { get; set; }
    public int ProduktAnzahl { get; set; }

    public string DatumFormatted => Datum?.ToString("dd.MM.yyyy") ?? "";
}

public class AngebotProdukt
{
    public int ID { get; set; }
    public int AngebotID { get; set; }
    public int ProduktID { get; set; }
    public string? ProduktName { get; set; }
    public string? Beschreibung { get; set; }
    public decimal Preis { get; set; }
    public byte[]? Bild { get; set; }

    public string PreisFormatted => Preis.ToString("0.00") + " €";
    public ImageSource? BildSource =>
        Bild is { Length: > 0 } ? ImageSource.FromStream(() => new MemoryStream(Bild)) : null;
}
